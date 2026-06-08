#!/usr/bin/env python3
"""Publish the AgentK Daily Insight without echoing large RSS payloads."""

from __future__ import annotations

import argparse
import html
import json
import re
import subprocess
import sys
from datetime import datetime, timezone
from pathlib import Path
from zoneinfo import ZoneInfo

ROOT = Path(__file__).resolve().parents[1]
FETCH_SCRIPT = ROOT / "scripts" / "daily_news_fetch.py"
KST = ZoneInfo("Asia/Seoul")


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


def insight_for(entry: dict) -> str:
    haystack = f"{entry.get('title', '')} {entry.get('summary', '')}".lower()
    rules = [
        (("ai", "llm", "claude", "chatbot", "agent"), "AI 도구는 결과물보다 워크플로와 검증 경계가 더 중요해지는 방향으로 이동하고 있습니다."),
        (("typescript", "c++", "c ", "python", "llvm", "jit", "compiler"), "언어와 런타임 계층의 변화는 개발 생산성뿐 아니라 배포 비용과 운영 복잡도에 직접 영향을 줍니다."),
        (("security", "hack", "privacy", "identity", "digid", "grapheneos"), "보안 이슈는 기능 문제가 아니라 신뢰 경계와 권한 설계의 문제로 보는 편이 안전합니다."),
        (("gpu", "nvidia", "data center", "spacex", "cuda"), "AI 인프라 경쟁은 모델 성능만큼 컴퓨트 조달, 메모리 구조, 전력·자본 계획에 좌우됩니다."),
        (("kernel", "ebpf", "server", "terminal", "sqlite", "kvm"), "저수준 시스템 도구의 작은 성능·운영성 개선은 반복 업무가 많은 팀에서 큰 누적 효과를 만듭니다."),
        (("figma", "design", "demo", "taste"), "제품 제작에서는 산출물의 양보다 좋은 판단, 빠른 프로토타입, 명확한 데모가 차별점이 되고 있습니다."),
        (("market", "vc", "s&p", "ipo"), "기술 기업의 가치는 제품 서사와 별도로 자본시장 규칙, 유동성, 거버넌스에 의해 조정됩니다."),
    ]
    for needles, sentence in rules:
        if any(needle in haystack for needle in needles):
            return sentence
    return "단일 기사보다 같은 방향의 신호가 반복되는지 추적하면 기술 채택 시점과 리스크를 더 잘 판단할 수 있습니다."


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


def build_markdown(entries: list[dict], date: str, generated_at: str) -> str:
    lines = [
        f"# Daily Insight - {date}",
        "",
        f"- 생성 시각: {generated_at}",
        f"- 수집 항목 수: {len(entries)}",
        f"- 관찰 포인트: {theme_line(entries)} 흐름이 함께 관찰됩니다.",
        "",
        "## 오늘의 한 줄",
        "도구와 인프라는 더 강력해지고 있지만, 실제 경쟁력은 이를 어떤 검증 루프와 운영 경계 안에 넣느냐에서 갈립니다.",
        "",
    ]
    for idx, entry in enumerate(entries, 1):
        title = strip_html(entry.get("title", "")).strip() or f"Item {idx}"
        link = entry.get("link", "").strip()
        lines.extend(
            [
                f"## {idx}. {title}",
                f"- 링크: {link}",
                "",
                "[핵심 요약]",
                short_summary(entry),
                "",
                "[기술적 인사이트]",
                insight_for(entry),
                "",
            ]
        )
    lines.extend(
        [
            "## 종합 메모",
            "- AI와 개발 도구는 생산성 논쟁을 넘어 검증·권한·평가 체계의 문제로 확장되고 있습니다.",
            "- 시스템 성능과 로컬 제어, 저수준 인프라 개선은 반복 작업의 누적 비용을 줄이는 방향으로 중요해지고 있습니다.",
            "- 보안·프라이버시·디지털 주권 이슈는 기술 선택의 주변 조건이 아니라 핵심 설계 변수로 올라오고 있습니다.",
            "- 시장과 인프라 소식은 기술 로드맵이 자본, 전력, 공급망과 분리될 수 없다는 점을 다시 보여줍니다.",
            "",
        ]
    )
    return "\n".join(lines)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--date", help="KST date YYYY-MM-DD. Defaults to today.")
    parser.add_argument("--input-json", help="Use an existing fetched JSON file.")
    parser.add_argument("--skip-fetch", action="store_true", help="Do not fetch before publishing.")
    parser.add_argument("--window-hours", type=int, default=24)
    parser.add_argument("--commit", action="store_true")
    parser.add_argument("--push", action="store_true")
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    now = datetime.now(KST)
    date = args.date or now.strftime("%Y-%m-%d")
    json_path = Path(args.input_json or f"/tmp/daily_news_{date}.json")

    if not args.skip_fetch and not args.input_json:
        run(
            [
                sys.executable,
                str(FETCH_SCRIPT),
                "--output",
                str(json_path),
                "--window-hours",
                str(args.window_hours),
            ]
        )

    payload = json.loads(json_path.read_text())
    entries = payload.get("entries") or payload.get("items") or []
    if not entries:
        print(f"status: failure\nfile: none\nitems: 0\ncommit: none\npush: skipped\nreason: no entries")
        return 1

    generated_at = now.strftime("%Y-%m-%d %H:%M KST")
    target = ROOT / "daily" / date[:7] / f"AgentK_Daily_Insight_{date}.md"
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text(build_markdown(entries, date, generated_at), encoding="utf-8")

    commit_hash = "none"
    push_status = "skipped"
    if args.commit:
        run(["git", "add", str(target.relative_to(ROOT))])
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
    print(f"summary: Daily Insight {date} generated from fetched RSS entries.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
