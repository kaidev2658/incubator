import json
import sys
import tempfile
import unittest
from pathlib import Path
from unittest.mock import patch


SCRIPTS = Path(__file__).resolve().parents[1] / "scripts"
sys.path.insert(0, str(SCRIPTS))

import daily_news_analyze as analyze


def source_entries():
    return [
        {
            "title": "OpenAI 에이전트 사고 공개",
            "link": "https://example.com/1",
            "source_feed": "https://example.com/feed",
            "summary": "<p>에이전트 사고 네 건과 탐지 지연이 공개됐다.</p>",
        },
        {
            "title": "새 데이터베이스 런타임 출시",
            "link": "https://example.com/2",
            "source_feed": "https://example.com/feed",
            "summary": "새 런타임은 읽기 지연 시간을 줄이고 운영 지표를 추가했다.",
        },
    ]


def valid_analysis():
    return {
        "items": [
            {
                "id": "item-001",
                "summary": "OpenAI가 에이전트 사고 네 건과 탐지 지연 사실을 공개했다.",
                "insight": "에이전트 운영자는 사전 권한 제한과 함께 사고 탐지 시간 및 사후 감사 절차를 별도 지표로 관리해야 한다.",
                "area": "보안",
                "confidence": "high",
            },
            {
                "id": "item-002",
                "summary": "새 데이터베이스 런타임이 읽기 지연을 줄이고 운영 지표를 확장했다.",
                "insight": "데이터 플랫폼 팀은 평균 성능보다 실제 워크로드의 꼬리 지연과 새 관측 지표의 운영 비용을 함께 비교해야 한다.",
                "area": "인프라",
                "confidence": "medium",
            },
        ],
        "daily": {
            "observation_points": ["에이전트 통제와 시스템 관측성이 함께 중요해졌다."],
            "one_line": "자동화 확대와 함께 사고 탐지와 런타임 관측성의 운영 기준이 구체화되고 있다.",
            "synthesis": [
                "에이전트는 권한 제한만큼 탐지와 감사 속도가 중요하다.",
                "인프라 개선은 실제 워크로드 지표로 검증해야 한다.",
            ],
        },
    }


class DailyNewsAnalyzeTests(unittest.TestCase):
    def test_prepare_entries_strips_html_and_assigns_stable_ids(self):
        prepared = analyze.prepare_entries(source_entries())
        self.assertEqual([item["id"] for item in prepared], ["item-001", "item-002"])
        self.assertNotIn("<p>", prepared[0]["content"])

    def test_parse_gateway_output_ignores_warnings(self):
        model_text = json.dumps(valid_analysis(), ensure_ascii=False)
        envelope = json.dumps({"outputs": [{"text": model_text}]}, ensure_ascii=False)
        parsed = analyze.parse_gateway_output("migration warning\n" + envelope)
        self.assertEqual(parsed["items"][0]["id"], "item-001")

    def test_validate_analysis_accepts_specific_unique_items(self):
        errors = analyze.validate_analysis(valid_analysis(), analyze.prepare_entries(source_entries()))
        self.assertEqual(errors, [])

    def test_validate_analysis_rejects_duplicate_insights(self):
        result = valid_analysis()
        result["items"][1]["insight"] = result["items"][0]["insight"]
        errors = analyze.validate_analysis(result, analyze.prepare_entries(source_entries()))
        self.assertTrue(any("유사 인사이트" in error for error in errors))

    def test_analyze_entries_reuses_valid_cache(self):
        result = valid_analysis()
        with tempfile.TemporaryDirectory() as directory:
            with patch.object(analyze, "invoke_model", return_value=result) as invoke:
                first = analyze.analyze_entries(
                    source_entries(), date="2026-10-18", cache_dir=Path(directory)
                )
                second = analyze.analyze_entries(
                    source_entries(), date="2026-10-18", cache_dir=Path(directory)
                )
            self.assertEqual(invoke.call_count, 1)
            self.assertEqual(first["meta"]["status"], "generated")
            self.assertEqual(second["meta"]["status"], "cached")


if __name__ == "__main__":
    unittest.main()
