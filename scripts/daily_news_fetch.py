#!/usr/bin/env python3
"""Fetch a curated RSS/Atom feed slice for the Daily News agent."""

from __future__ import annotations

import argparse
import json
import sys
import urllib.request
import xml.etree.ElementTree as ET
from datetime import datetime, timedelta, timezone
from email.utils import parsedate_to_datetime
from pathlib import Path
from typing import Iterable, List, Optional, Sequence

DEFAULT_FEEDS: List[str] = [
    "https://feeds.feedburner.com/geeknews-feed",
]

HEADERS = {
    "User-Agent": "Mozilla/5.0 (compatible; AgentK/DailyNews; +https://github.com/kaidev2658/incubator)",
}

IGNORED_PREFIXES = ("Show GN:", "Ask GN:")


def log(msg: str) -> None:
    print(msg, file=sys.stderr)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Fetch RSS/Atom entries, filter the last N hours, and dump JSON."
    )
    parser.add_argument("--output", required=True, help="Path to write the JSON result.")
    parser.add_argument(
        "--window-hours",
        type=int,
        default=24,
        help="Only keep entries published within the past N hours (default: 24).",
    )
    parser.add_argument(
        "--feeds-file",
        help="Optional file containing extra feed URLs (one per line, comments start with #).",
    )
    return parser.parse_args()


def load_feeds(feeds_file: Optional[str]) -> List[str]:
    feeds = list(DEFAULT_FEEDS)
    if feeds_file:
        path = Path(feeds_file).expanduser()
        if path.exists():
            for line in path.read_text().splitlines():
                candidate = line.strip()
                if not candidate or candidate.startswith("#"):
                    continue
                feeds.append(candidate)
        else:
            log(f"Feeds file not found: {path}")
    return feeds


def fetch_feed(url: str) -> Optional[bytes]:
    request = urllib.request.Request(url, headers=HEADERS)
    try:
        with urllib.request.urlopen(request, timeout=20) as resp:
            return resp.read()
    except Exception as exc:  # pragma: no cover (retry log)
        log(f"Failed to retrieve {url}: {exc}")
        return None


def localname(tag: str) -> str:
    if not tag:
        return ""
    return tag.split("}", 1)[-1]


def child_text(elem: ET.Element, names: Sequence[str]) -> Optional[str]:
    for child in elem:
        if localname(child.tag) in names and child.text:
            return child.text.strip()
    return None


def parse_published_text(elem: ET.Element) -> Optional[datetime]:
    text = child_text(elem, ("pubDate", "published", "updated"))
    if not text:
        return None
    try:
        dt = parsedate_to_datetime(text)
    except (TypeError, ValueError):
        try:
            dt = datetime.fromisoformat(text)
        except ValueError:
            return None
    if dt.tzinfo is None:
        dt = dt.replace(tzinfo=timezone.utc)
    return dt.astimezone(timezone.utc)


def entry_summary(elem: ET.Element) -> str:
    text = child_text(elem, ("description", "summary", "content", "encoded"))
    return text or ""


def entry_link(elem: ET.Element) -> Optional[str]:
    for child in elem:
        if localname(child.tag) != "link":
            continue
        text = (child.text or "").strip()
        if text:
            return text
        href = child.attrib.get("href")
        if href:
            return href.strip()
    return None


def elements_by_localname(root: ET.Element, names: Sequence[str]) -> List[ET.Element]:
    matches: List[ET.Element] = []
    for elem in root.iter():
        if localname(elem.tag) in names:
            matches.append(elem)
    return matches


def parse_entries(xml_bytes: bytes, source: str) -> Iterable[dict]:
    root = ET.fromstring(xml_bytes)
    item_nodes = elements_by_localname(root, ("item", "entry"))

    for item in item_nodes:
        title = (child_text(item, ("title",)) or "").strip()
        if not title or title.startswith(IGNORED_PREFIXES):
            continue
        link = entry_link(item)
        published = parse_published_text(item)
        if not published or not link:
            continue
        raw_description = entry_summary(item)
        yield {
            "title": title,
            "link": link,
            "summary": raw_description,
            "description_original": raw_description,
            "published": published.isoformat(),
            "source_feed": source,
        }


def main() -> None:
    args = parse_args()
    feeds = load_feeds(args.feeds_file)
    now_utc = datetime.now(timezone.utc)
    cutoff = now_utc - timedelta(hours=args.window_hours)
    records: List[dict] = []

    for lookup in feeds:
        payload = fetch_feed(lookup)
        if not payload:
            continue
        try:
            entries = list(parse_entries(payload, lookup))
        except Exception as exc:  # pragma: no cover
            log(f"Failed to parse {lookup}: {exc}")
            continue
        for entry in entries:
            entry_time = datetime.fromisoformat(entry["published"])
            if entry_time < cutoff:
                continue
            records.append(entry)

    records = sorted(records, key=lambda r: r["published"], reverse=True)

    output_path = Path(args.output).expanduser()
    output_path.parent.mkdir(parents=True, exist_ok=True)
    payload = {
        "generated_at": now_utc.isoformat(),
        "window_hours": args.window_hours,
        "feeds": feeds,
        "entries": records,
    }
    output_path.write_text(json.dumps(payload, ensure_ascii=False, indent=2))

    log(f"Wrote {len(records)} entries to {output_path}")


if __name__ == "__main__":
    main()
