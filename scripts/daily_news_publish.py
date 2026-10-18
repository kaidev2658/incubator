#!/usr/bin/env python3
"""Publish the AgentK Daily Insight without echoing large RSS payloads."""

from __future__ import annotations

import argparse
import html
import json
import re
import subprocess
import sys
import time
import urllib.request
from datetime import datetime, timezone
from pathlib import Path
from zoneinfo import ZoneInfo

from daily_news_analyze import DEFAULT_MODEL, analysis_by_id, analyze_entries

ROOT = Path(__file__).resolve().parents[1]
FETCH_SCRIPT = ROOT / "scripts" / "daily_news_fetch.py"
FEEDS_FILE = ROOT / "daily" / "feeds.txt"
KST = ZoneInfo("Asia/Seoul")
HEADERS = {
    "User-Agent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 Chrome/120 Safari/537.36",
}


def run(cmd: list[str], *, check: bool = True) -> subprocess.CompletedProcess[str]:
    return subprocess.run(
        cmd,
        cwd=ROOT,
        check=check,
        text=True,
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
    )


def strip_html(value: str) -> str:
    text = html.unescape(value or "")
    text = re.sub(r"<[^>]+>", " ", text)
    text = re.sub(r"\s+", " ", text).strip()
    return text


def short_summary(entry: dict) -> str:
    title = strip_html(entry.get("title", ""))
    summary = strip_html(entry.get("summary") or entry.get("description_original") or "")
    if summary:
        parts = re.split(r"(?<=[.!?。])\s+|(?<=다\.)\s+|(?<=요\.)\s+", summary)
        text = " ".join(parts[:2]).strip()
        if len(text) > 230:
            text = text[:227].rstrip() + "..."
        return text
    return f"{title}에 관한 소식입니다. 원문을 기준으로 세부 맥락을 확인할 필요가 있습니다."


def fetch_engagement(entry: dict) -> dict:
    link = (entry.get("link") or "").strip()
    topic_match = re.search(r"^https://news\.hada\.io/topic\?id=(\d+)", link)
    if not topic_match:
        return {}

    topic_id = topic_match.group(1)
    request = urllib.request.Request(link, headers=HEADERS)
    try:
        with urllib.request.urlopen(request, timeout=8) as response:
            page = response.read().decode("utf-8", errors="replace")
    except Exception:
        return {}

    points_match = re.search(rf"id=['\"]tp{re.escape(topic_id)}['\"]>(\d+)</span>\s*P", page)
    comments_match = re.search(r"data-topic-comment-count=['\"](\d+)['\"]", page)
    engagement = {}
    if points_match:
        engagement["points"] = int(points_match.group(1))
    if comments_match:
        engagement["comments"] = int(comments_match.group(1))
    return engagement


def enrich_engagement(entries: list[dict]) -> list[dict]:
    enriched = []
    for entry in entries:
        copy = dict(entry)
        copy.update(fetch_engagement(entry))
        enriched.append(copy)
    return enriched


def impact_score(entry: dict, index: int) -> int:
    haystack = f"{entry.get('title', '')} {entry.get('summary', '')}".lower()
    points = entry.get("points")
    comments = entry.get("comments")
    if points is not None or comments is not None:
        return int(points or 0) * 3 + int(comments or 0) * 8

    weighted_keywords = [
        (9, ("security", "vulnerability", "hack", "breach", "privacy", "identity", "supply chain")),
        (8, ("ai", "llm", "agent", "claude", "gpt", "model", "compute", "gpu", "nvidia")),
        (7, ("regulation", "law", "lawsuit", "policy", "ban", "antitrust", "climate")),
        (7, ("open source", "linux", "kernel", "compiler", "runtime", "sqlite", "database")),
        (6, ("market", "ipo", "vc", "funding", "revenue", "contract", "price")),
        (5, ("research", "paper", "benchmark", "eval", "clinical", "vaccine")),
    ]
    score = max(0, 3 - index)
    for weight, keywords in weighted_keywords:
        if any(keyword in haystack for keyword in keywords):
            score += weight
    if re.search(r"\b(\d+[%x]?|\$\d+|[0-9,.]+억|[0-9,.]+조)\b", haystack):
        score += 2
    return score


def top_impact_entries(entries: list[dict], limit: int = 3) -> list[dict]:
    ranked = sorted(
        enumerate(entries),
        key=lambda pair: (impact_score(pair[1], pair[0]), -pair[0]),
        reverse=True,
    )
    return [entry for _, entry in ranked[:limit]]


def compact_line(value: str, limit: int = 260) -> str:
    text = re.sub(r"\s+", " ", value).strip()
    if len(text) > limit:
        return text[: limit - 3].rstrip() + "..."
    return text


def theme_line(entries: list[dict]) -> str:
    text = " ".join(f"{e.get('title', '')} {e.get('summary', '')}" for e in entries).lower()
    themes = []
    if any(k in text for k in ("ai", "llm", "claude", "agent")):
        themes.append("AI 도구와 개발 워크플로")
    if any(k in text for k in ("security", "privacy", "identity", "hack", "digid")):
        themes.append("보안·프라이버시")
    if any(k in text for k in ("compiler", "typescript", "jit", "kernel", "ebpf")):
        themes.append("언어·시스템 인프라")
    if any(k in text for k in ("gpu", "nvidia", "data center", "spacex")):
        themes.append("AI 컴퓨트 인프라")
    if not themes:
        themes.append("개발자 도구와 기술 운영")
    return ", ".join(themes[:4])


def build_markdown(
    entries: list[dict], date: str, generated_at: str, analysis: dict
) -> str:
    analyzed = analysis_by_id(analysis)
    daily = analysis.get("daily") or {}
    observation_points = daily.get("observation_points") or [theme_line(entries)]
    one_line = strip_html(str(daily.get("one_line") or "기사별 분석 결과를 확인할 필요가 있습니다."))
    raw_status = analysis.get("meta", {}).get("status", "unknown")
    report_status = "LLM 검증 완료" if raw_status in {"generated", "cached"} else "분석 제한"
    lines = [
        f"# Daily Insight - {date}",
        "",
        f"- 생성 시각: {generated_at}",
        f"- 수집 항목 수: {len(entries)}",
        f"- 분석 상태: {report_status}",
        "- 관찰 포인트:",
    ]
    lines.extend(f"  - {strip_html(str(item))}" for item in observation_points)
    lines.extend([
        "",
        "## 오늘의 한 줄",
        one_line,
        "",
        "## 임팩트 Top 3",
    ])
    for idx, entry in enumerate(top_impact_entries(entries), 1):
        title = strip_html(entry.get("title", "")).strip() or f"Item {idx}"
        link = entry.get("link", "").strip()
        item = analyzed.get(entry.get("_analysis_id"), {})
        lines.extend(
            [
                f"### {idx}. {title}",
                f"- 링크: {link}",
                f"- 내용: {item.get('summary') or short_summary(entry)}",
                f"- 인사이트: {item.get('insight') or '기사별 분석 결과가 없습니다.'}",
                f"- 영향 영역/신뢰도: {item.get('area', '기타')} / {item.get('confidence', 'low')}",
                "",
            ]
        )
    lines.append("")
    for idx, entry in enumerate(entries, 1):
        title = strip_html(entry.get("title", "")).strip() or f"Item {idx}"
        link = entry.get("link", "").strip()
        item = analyzed.get(entry.get("_analysis_id"), {})
        lines.extend(
            [
                f"## {idx}. {title}",
                f"- 링크: {link}",
                "",
                "[핵심 요약]",
                item.get("summary") or short_summary(entry),
                "",
                "[기술적 인사이트]",
                item.get("insight") or "기사별 분석 결과가 없습니다.",
                "",
                f"[영향 영역/신뢰도] {item.get('area', '기타')} / {item.get('confidence', 'low')}",
                "",
            ]
        )
    lines.append("## 종합 메모")
    for item in daily.get("synthesis") or ["검증된 종합 분석 결과가 없습니다."]:
        lines.append(f"- {strip_html(str(item))}")
    lines.append("")
    return "\n".join(lines)


def print_impact_lines(entries: list[dict], analysis: dict) -> None:
    analyzed = analysis_by_id(analysis)
    for idx, entry in enumerate(top_impact_entries(entries), 1):
        title = strip_html(entry.get("title", "")).strip() or f"Item {idx}"
        link = (entry.get("link") or "").strip()
        points = entry.get("points")
        comments = entry.get("comments")
        engagement = []
        if points is not None:
            engagement.append(f"points={points}")
        if comments is not None:
            engagement.append(f"comments={comments}")
        print(f"impact_{idx}_title: {compact_line(title, 180)}")
        print(f"impact_{idx}_link: {compact_line(link, 220)}")
        if engagement:
            print(f"impact_{idx}_engagement: {', '.join(engagement)}")
        item = analyzed.get(entry.get("_analysis_id"), {})
        print(f"impact_{idx}_summary: {compact_line(item.get('summary') or short_summary(entry), 300)}")
        print(f"impact_{idx}_insight: {compact_line(item.get('insight') or '기사별 분석 결과가 없습니다.', 300)}")
        print(
            f"impact_{idx}_analysis: area={item.get('area', '기타')}, "
            f"confidence={item.get('confidence', 'low')}"
        )


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--date", help="KST date YYYY-MM-DD. Defaults to today.")
    parser.add_argument("--input-json", help="Use an existing fetched JSON file.")
    parser.add_argument("--skip-fetch", action="store_true", help="Do not fetch before publishing.")
    parser.add_argument("--window-hours", type=int, default=24)
    parser.add_argument("--fallback-window-hours", type=int, default=48)
    parser.add_argument("--retries", type=int, default=2)
    parser.add_argument("--retry-delay-seconds", type=int, default=20)
    parser.add_argument("--analysis-model", default=DEFAULT_MODEL)
    parser.add_argument("--analysis-timeout", type=int, default=300)
    parser.add_argument("--analysis-retries", type=int, default=1)
    parser.add_argument("--analysis-cache-dir")
    parser.add_argument("--refresh-analysis", action="store_true")
    parser.add_argument("--output", help="Write to an alternate path for dry-run review.")
    parser.add_argument("--commit", action="store_true")
    parser.add_argument("--push", action="store_true")
    return parser.parse_args()


def fetch_entries(json_path: Path, window_hours: int) -> tuple[dict, subprocess.CompletedProcess[str]]:
    fetch_cmd = [
        sys.executable,
        str(FETCH_SCRIPT),
        "--output",
        str(json_path),
        "--window-hours",
        str(window_hours),
    ]
    if FEEDS_FILE.exists():
        fetch_cmd.extend(["--feeds-file", str(FEEDS_FILE)])
    result = run(fetch_cmd)
    payload = json.loads(json_path.read_text())
    return payload, result


def payload_entries(payload: dict) -> list[dict]:
    return payload.get("entries") or payload.get("items") or []


def print_fetch_diagnostics(payload: dict, *, prefix: str = "diagnostic") -> None:
    stats = payload.get("source_stats") or []
    print(f"{prefix}: window_hours={payload.get('window_hours')} feeds={len(payload.get('feeds') or [])}")
    for item in stats:
        print(
            f"{prefix}: feed={item.get('feed')} bytes={item.get('bytes')} "
            f"parsed={item.get('parsed')} kept={item.get('kept')} error={item.get('error')}"
        )


def main() -> int:
    args = parse_args()
    now = datetime.now(KST)
    date = args.date or now.strftime("%Y-%m-%d")
    json_path = Path(args.input_json or f"/tmp/daily_news_{date}.json")

    fetch_attempts = []
    if args.skip_fetch or args.input_json:
        payload = json.loads(json_path.read_text())
    else:
        max_attempts = max(1, args.retries + 1)
        payload = {}
        for attempt in range(1, max_attempts + 1):
            payload, result = fetch_entries(json_path, args.window_hours)
            fetch_attempts.append((args.window_hours, payload, result))
            if payload_entries(payload):
                break
            if attempt < max_attempts:
                time.sleep(max(0, args.retry_delay_seconds))
        if not payload_entries(payload) and args.fallback_window_hours > args.window_hours:
            payload, result = fetch_entries(json_path, args.fallback_window_hours)
            fetch_attempts.append((args.fallback_window_hours, payload, result))

    entries = payload_entries(payload)
    if not entries:
        for _, attempt_payload, result in fetch_attempts:
            if result.stderr.strip():
                print(result.stderr.strip())
            print_fetch_diagnostics(attempt_payload)
        print(f"status: failure\nfile: none\nitems: 0\ncommit: none\npush: skipped\nreason: no entries")
        return 1
    entries = enrich_engagement(entries)
    for index, entry in enumerate(entries, 1):
        entry["_analysis_id"] = f"item-{index:03d}"
    if fetch_attempts and fetch_attempts[-1][0] != args.window_hours:
        print(f"notice: used fallback window_hours={fetch_attempts[-1][0]}")

    analysis = analyze_entries(
        entries,
        date=date,
        model=args.analysis_model,
        timeout=args.analysis_timeout,
        cache_dir=Path(args.analysis_cache_dir).expanduser() if args.analysis_cache_dir else None,
        refresh=args.refresh_analysis,
        retries=args.analysis_retries,
    )

    generated_at = now.strftime("%Y-%m-%d %H:%M KST")
    target = (
        Path(args.output).expanduser()
        if args.output
        else ROOT / "daily" / date[:7] / f"AgentK_Daily_Insight_{date}.md"
    )
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text(build_markdown(entries, date, generated_at, analysis), encoding="utf-8")

    commit_hash = "none"
    push_status = "skipped"
    if args.commit:
        try:
            relative_target = target.resolve().relative_to(ROOT.resolve())
        except ValueError:
            print("status: failure\ncommit: none\npush: skipped\nreason: --commit requires output inside repository")
            return 2
        run(["git", "add", str(relative_target)])
        diff = run(["git", "diff", "--cached", "--quiet"], check=False)
        if diff.returncode == 0:
            commit_hash = "unchanged"
        else:
            run(["git", "commit", "-m", f"feat: add daily insight {date}"])
            commit_hash = run(["git", "rev-parse", "--short", "HEAD"]).stdout.strip()
    if args.push:
        run(["git", "push"])
        push_status = "ok"

    print(f"status: success")
    print(f"file: {target}")
    print(f"items: {len(entries)}")
    print(f"commit: {commit_hash}")
    print(f"push: {push_status}")
    print("reason: none")
    print(f"analysis_status: {analysis.get('meta', {}).get('status', 'unknown')}")
    print(f"analysis_model: {analysis.get('meta', {}).get('model', args.analysis_model)}")
    print(f"analysis_attempts: {analysis.get('meta', {}).get('attempts', 0)}")
    if analysis.get("meta", {}).get("reason"):
        print(f"analysis_reason: {compact_line(analysis['meta']['reason'], 500)}")
    print(f"summary: Daily Insight {date} generated from fetched RSS entries.")
    print_impact_lines(entries, analysis)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
