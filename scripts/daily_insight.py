import feedparser
from bs4 import BeautifulSoup
from datetime import datetime, timezone, timedelta
from pathlib import Path
import textwrap


def clean_html(value: str) -> str:
    if not value:
        return ''
    return ' '.join(BeautifulSoup(value, 'html.parser').stripped_strings)


def summarize(entry):
    summary = clean_html(entry.get('summary', entry.get('description', '')))
    if not summary and 'content' in entry:
        summary = clean_html(entry.content[0].value)
    return summary


def insight_from_text(summary, title):
    keywords = []
    for keyword in ['AI', 'edge', 'platform', 'cloud', 'security', 'tool', 'automation', 'agent']:
        if keyword.lower() in summary.lower() or keyword.lower() in title.lower():
            keywords.append(keyword)
    if keywords:
        return f'핵심 키워드: {", ".join(dict.fromkeys(keywords))}를 중심으로 적용 가능한 기술 트렌드를 추출합니다.'
    return '향후 Tizen AI-OS와 연동 가능한 기술적 힌트는 키워드 기반으로 추가 분석이 필요합니다.'


def format_entry(entry):
    title = entry.title.strip()
    link = entry.link
    published = None
    if 'published_parsed' in entry and entry.published_parsed:
        published = datetime(*entry.published_parsed[:6], tzinfo=timezone.utc)
    summary = summarize(entry)
    insight = insight_from_text(summary, title)
    return title, link, published, summary, insight


def fetch_feed(url):
    feed = feedparser.parse(url)
    return feed.entries


if __name__ == '__main__':
    feed_url = 'https://feeds.feedburner.com/geeknews-feed'
    entries = fetch_feed(feed_url)
    now = datetime.now(timezone.utc)
    cutoff = now - timedelta(days=1)
    filtered = []
    for entry in entries:
        title = entry.title.strip()
        if title.startswith('Show GN:') or title.startswith('Ask GN:'):
            continue
        published = None
        if 'published_parsed' in entry and entry.published_parsed:
            published = datetime(*entry.published_parsed[:6], tzinfo=timezone.utc)
        if not published or published < cutoff:
            continue
        filtered.append(format_entry(entry))
    if not filtered:
        print('No new entries within the last 24h after filtering.')
        raise SystemExit(0)
    today = now.astimezone(timezone.utc).date()
    path = Path('dailiy') / f'AgentK_Insight_{today:%Y-%m-%d}.md'
    lines = [f'# AgentK Insight ({today:%Y-%m-%d})', '', f'Generated at {now.isoformat()}', '']
    for title, link, published, summary, insight in filtered:
        lines.append(f'## {title}')
        lines.append(f'- 원문: {link}')
        published_text = (
            published.astimezone(timezone(timedelta(hours=9))).isoformat()
            if published
            else '알 수 없음'
        )
        lines.append(f'- 게시일: {published_text}')
        lines.append('')
        lines.append('[핵심 요약]')
        lines.extend(textwrap.wrap(summary or '요약 정보 없음.', width=80))
        lines.append('')
        lines.append('[기술적 인사이트]')
        lines.extend(textwrap.wrap(insight, width=80))
        lines.append('')
    path.write_text('\n'.join(lines), encoding='utf-8')
    print('Written', path)
