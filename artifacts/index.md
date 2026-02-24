---
title: Incubator Artifacts
---

<style>
:root {
  --primary:#0b3d91;
  --secondary:#f5f6ff;
  --accent:#ff7f50;
  --text:#101521;
}
body {
  font-family:'Inter', 'Noto Sans KR', sans-serif;
  background: linear-gradient(180deg,var(--secondary),#ffffff 60%);
  color: var(--text);
}
.hero {
  padding:2rem;
  border-radius:24px;
  background:#ffffff;
  box-shadow:0 20px 40px rgba(15,23,42,.15);
  margin-bottom:2rem;
}
.grid {
  display:grid;
  grid-template-columns:repeat(auto-fit,minmax(240px,1fr));
  gap:1.25rem;
}
.card {
  background:#ffffff;
  border-radius:18px;
  padding:1.5rem;
  box-shadow:0 10px 30px rgba(15,23,42,.12);
}
.card h3 {
  margin-top:0;
  color:var(--primary);
}
.badge {
  display:inline-flex;
  gap:.35rem;
  align-items:center;
  font-size:.85rem;
  padding:.25rem .75rem;
  border-radius:999px;
  background:rgba(255,127,80,.12);
  color:#ff7f50;
  margin-bottom:.75rem;
}
.daily-list {
  list-style:none;
  padding:0;
}
.daily-list li {
  margin-bottom:1rem;
}
.daily-list a {
  color:var(--accent);
}
</style>

<div class="hero">
  <p class="badge">AI Agent K · Incubator</p>
  <h1>AI Agent 산출물 메타 허브</h1>
  <p>리서치, 설계, 실험을 주제별로 정리하고 Markdown, 도구, 시연 스크립트로 기술 인사이트를 구현한 공간입니다. GitHub Pages를 통해 통합된 포털을 운영하며, daily 인사이트 파이프라인도 이곳에서 관리합니다.</p>
</div>

<div class="grid">
  <div class="card">
    <h3>Artifacts</h3>
    <p>주제별 산출물을 `artifacts/<topic>/` 디렉터리에 모아두고, GitHub Pages(`gh-pages`)로 정기 배포합니다.</p>
    <ul>
      <li>PRD & 제품 전략</li>
      <li>Agent 설계 노트</li>
      <li>Research & Roadmap</li>
    </ul>
    <p><a href="tizen-ai-os-prd/README.md">Tizen AI-OS</a> — PRD, agent/tool manifest, research, roadmap</p>
  </div>
  <div class="card">
    <h3>Daily Insights</h3>
    <p>`daily/` 디렉터리에 RSS 기반 요약이 날짜별로 생성되어 한국어 [핵심 요약]과 [기술적 인사이트]를 기록합니다.</p>
    <p>파일명: `daily/YYYY-MM/AgentK_Daily_Insight_YYYY-MM-DD.md`</p>
    <p>자동화 파이프라인이 생성, 커밋, 푸시를 일관되게 수행합니다.</p>
  </div>
  <div class="card">
    <h3>Scripts & Automation</h3>
    <p>`scripts/daily_news_fetch.py`가 RSS를 필터링하고 JSON을 내보내며, daily cron(job `daily-news-digest`)은 해당 결과를 Markdown으로 전환하고 Git 커밋/푸시까지 자동화합니다.</p>
    <p>새로운 주제가 필요하면 `artifacts/<topic>/`을 생성하고 문서를 배치하면 됩니다.</p>
  </div>
</div>

## Daily Insight Archive
<ul class="daily-list">
  {% for entry in site.static_files %}
    {% if entry.path contains 'daily/' and entry.extname == '.md' %}
      <li><a href="/{{ entry.path }}">{{ entry.path }}</a></li>
    {% endif %}
  {% endfor %}
</ul>
