---
title: Incubator Artifacts
---

<style>
:root {
  --primary: #0b3d91;
  --secondary: #f5f6ff;
  --accent: #ff7f50;
  --text: #101521;
  --muted: #4b5563;
}
body {
  font-family: 'Inter', 'Noto Sans KR', sans-serif;
  background: linear-gradient(180deg, var(--secondary), #ffffff 60%);
  color: var(--text);
  margin: 0;
}
.page {
  min-height: 100vh;
  padding: 2rem 1.5rem 3rem;
}
.layout {
  display: grid;
  grid-template-columns: 260px 1fr;
  gap: 2rem;
}
.hero {
  padding: 2rem;
  border-radius: 24px;
  background: #ffffff;
  box-shadow: 0 20px 40px rgba(15, 23, 42, 0.15);
  margin-bottom: 2rem;
}
.cards {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 1.25rem;
}
.card {
  background: #ffffff;
  border-radius: 18px;
  padding: 1.5rem;
  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12);
}
.card h3 {
  margin-top: 0;
  color: var(--primary);
}
.badge {
  display: inline-flex;
  gap: 0.35rem;
  align-items: center;
  font-size: 0.85rem;
  padding: 0.25rem 0.75rem;
  border-radius: 999px;
  background: rgba(255, 127, 80, 0.12);
  color: #ff7f50;
  margin-bottom: 0.75rem;
}
.sidebar {
  position: sticky;
  top: 2rem;
  align-self: start;
  background: #ffffff;
  border-radius: 18px;
  padding: 1.5rem;
  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.12);
}
.sidebar h2 {
  margin-top: 0;
}
.nav-list {
  list-style: none;
  padding: 0;
  margin: 0;
}
.nav-list li {
  margin-bottom: 0.55rem;
}
.nav-list a {
  color: var(--muted);
  text-decoration: none;
  font-weight: 500;
}
.nav-list a:hover {
  color: var(--primary);
}
.daily-list {
  list-style: none;
  padding: 0;
  margin: 0;
}
.daily-list li {
  margin-bottom: 1rem;
}
.daily-list a {
  color: var(--accent);
  font-weight: 600;
}
</style>

<div class="page">
  <div class="layout">
    <aside class="sidebar">
      <h2>Navigation</h2>
      <ul class="nav-list">
        <li><a href="{{ '/artifacts/' | relative_url }}">Artifacts overview</a></li>
        <li><a href="{{ '/daily/' | relative_url }}">Daily Tech News</a></li>
        <li><a href="{{ '/scripts/' | relative_url }}">Scripts</a></li>
        <li><a href="{{ '/artifacts/tizen-ai-os-prd/' | relative_url }}">Tizen AI-OS</a></li>
        <li><a href="{{ '/artifacts/tizen-ai-os-prd/research/' | relative_url }}">Research notes</a></li>
        <li><a href="{{ '/artifacts/tizen-ai-os-prd/roadmaps/' | relative_url }}">Roadmaps &amp; releases</a></li>
      </ul>
    </aside>
    <main>
      <div class="hero">
        <p class="badge">AI Agent K · Incubator</p>
        <h1>AI Agent 산출물 메타 허브</h1>
        <p>리서치, 설계, 실험을 주제별로 정리하고 Markdown, 도구, 시연 스크립트로 기술 인사이트를 구현한 공간입니다. GitHub Pages를 통해 통합된 포털을 운영하며, Daily Tech News 인사이트 파이프라인도 이곳에서 관리합니다.</p>
      </div>
      <div class="cards">
        <div class="card">
          <h3>Artifacts</h3>
          <p>주제별 산출물을 `artifacts/<topic>/` 디렉터리에 모아두고, GitHub Pages(`gh-pages`)로 정기 배포합니다.</p>
          <ul>
            <li>PRD &amp; 제품 전략</li>
            <li>Agent 설계 노트</li>
            <li>Research &amp; Roadmap</li>
          </ul>
          <p><a href="{{ '/artifacts/tizen-ai-os-prd/' | relative_url }}">Tizen AI-OS</a> — PRD, agent/tool manifest, research, roadmap</p>
        </div>
        <div class="card">
          <h3>Daily Tech News</h3>
          <p>`daily/` 디렉터리에 RSS 기반 기술 뉴스 요약이 날짜별로 생성되며, 한국어 [핵심 요약]과 [기술적 인사이트]를 담습니다.</p>
          <p>파일명: `daily/YYYY-MM/AgentK_Daily_Insight_YYYY-MM-DD.md`</p>
          <p>자동화 파이프라인으로 문서화 → 커밋 → 푸시 흐름을 유지합니다.</p>
        </div>
        <div class="card">
          <h3>Scripts &amp; Automation</h3>
          <p>`scripts/daily_news_fetch.py`가 RSS를 필터링하고 JSON을 생성하며, cron(job `daily-news-digest`)은 해당 데이터를 Markdown으로 전환하고 Git 커밋/푸시까지 수행합니다.</p>
          <p>새로운 주제가 필요하면 `artifacts/<topic>/`을 생성하고 문서를 추가하세요.</p>
        </div>
      </div>
      <section>
        <h2>Daily Tech News Archive</h2>
        <ul class="daily-list">
          {% for entry in site.static_files %}
            {% if entry.path contains 'daily/' and entry.extname == '.md' %}
              <li><a href="{{ entry.path | relative_url }}">{{ entry.path }}</a></li>
            {% endif %}
          {% endfor %}
        </ul>
      </section>
    </main>
  </div>
</div>
