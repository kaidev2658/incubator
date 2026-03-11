# Apps in Toss Product & Tech Brief

## 0) 범위 / 신뢰도 기준
- 범위: 대표님 지정 3개 소스 + 공식 개발문서/예제 저장소 보강
  1. https://toss.im/apps-in-toss
  2. https://developers-apps-in-toss.toss.im
  3. https://github.com/toss/apps-in-toss-examples
- 보강 소스(공식 문서 원문)
  - https://developers-apps-in-toss.toss.im/intro/overview.md
  - https://developers-apps-in-toss.toss.im/tutorials/webview.md
  - https://developers-apps-in-toss.toss.im/tutorials/react-native.md
  - https://developers-apps-in-toss.toss.im/development/integration-process.md
  - https://developers-apps-in-toss.toss.im/development/deploy.md
  - https://developers-apps-in-toss.toss.im/bedrock/reference/framework/권한/permission.md
  - https://developers-apps-in-toss.toss.im/development/llms.html
  - https://developers-apps-in-toss.toss.im/llms.txt
  - https://raw.githubusercontent.com/toss/apps-in-toss-examples/main/README.md
- 신뢰도 라벨
  - **[Confirmed]**: 공식 페이지/공식 문서/공식 repo에서 직접 확인
  - **[Likely]**: 문서 단서 기반 강한 추정
  - **[Open]**: 공개 자료만으로 확정 곤란

## 1) 제품 정의
Apps in Toss는 파트너 서비스가 토스 앱 내부에서 동작하는 **앱인앱(미니앱) 플랫폼**이다. 설치 유도 없이 토스 사용자 접점에서 서비스 노출·실행·성장(마케팅/수익화)까지 연결하는 생태계로 제시된다. **[Confirmed]**

## 2) 앱 모델 / 개발 모델
### 2.1 앱 모델
- 전통 앱스토어 배포 앱보다, 토스 앱 내부 실행을 전제로 한 **미니앱 런타임 모델**이다. **[Confirmed]**
- 앱 식별자는 `appName`이며, `intoss://{appName}/...` 스킴으로 라우팅되는 구조가 문서화되어 있다. **[Confirmed]**

### 2.2 개발 스택
- 공식 개발 경로는 크게 2축:
  - **WebView 기반**: `@apps-in-toss/web-framework`
  - **React Native 기반**: `@apps-in-toss/framework` + Granite 스캐폴딩
  **[Confirmed]**
- SDK 2.x 마이그레이션 정책(업로드 제한 일정)이 명시되어 있어 SDK 버전 정책 통제가 강하다. **[Confirmed]**

### 2.3 권한 모델
- `granite.config.ts`에서 권한을 선언하고 검수/런타임에 반영하는 구조.
- 예시 권한: clipboard, contacts, photos, camera, geolocation 등. **[Confirmed]**

## 3) 배포/출시/운영 모델
### 3.1 패키징
- 빌드 산출물은 `.ait` 번들.
- 콘솔 업로드 후 검토(리뷰) → 승인 → 출시 버튼으로 전체 사용자 반영. **[Confirmed]**

### 3.2 릴리즈 정책
- 번들 용량 제한(압축 해제 기준 100MB), 리소스 분리(CDN/Lazy loading 권고), 검토 반려/재요청 루프, 롤백/핫픽스 프로세스가 문서화되어 있다. **[Confirmed]**
- 테스트 환경과 라이브 환경(CORS/네트워크/권한) 차이에 대한 운영 주의가 명시됨. **[Confirmed]**

### 3.3 운영 가시성
- 출시 후 모니터링(Sentry, 오류 로그, API 실패율, 사용자 신고 등) 절차를 가이드로 제공. **[Confirmed]**

## 4) API/서버 연동 특성
- 핵심 기능(로그인/결제/인앱결제/푸시/프로모션 등)은 **mTLS 기반 서버간 통신**을 요구한다. **[Confirmed]**
- 즉, 단순 클라이언트 미니앱만으로 끝나기보다, **파트너 백엔드 역량**이 제품 완성도에 직접 영향을 준다. **[Likely]**

## 5) 생태계 확장성(기능 범위)
공식 예제/문서에서 다음 축이 확인된다:
- 로그인/인증/결제/인앱결제
- 광고(보상형 포함), 리워드, 프로모션
- 위치/연락처/카메라/클립보드 등 디바이스 권한 기능
- 게임센터/리더보드, 공유, 분석/성장 도구
- Unity WebGL 포팅/최적화 가이드
**[Confirmed]**

=> Apps in Toss는 “단일 임베드 WebView”가 아니라, **SDK+콘솔+API+성장도구가 결합된 플랫폼형 제품**으로 보는 게 정확하다. **[Likely]**

## 6) 예제 저장소 관찰 포인트
공식 examples repo에서 프레임워크/기능 데모가 다수 확인된다:
- webview/rn 샘플
- with-in-app-purchase, with-interstitial-ad, with-rewarded-ad
- with-location-*, with-camera, with-contacts, with-storage
- with-share-* 등
**[Confirmed]**

이는 SDK capability가 실제 샘플 코드 수준으로 공개돼 있고, 파트너 온보딩 마찰을 낮추려는 전략으로 해석 가능하다. **[Likely]**

## 7) Wabi / Essential Apps 대비 관점 요약
### 7.1 공통점
- 플랫폼 내부 실행 모델
- 경량 앱/미니앱 경험 지향
- 배포/운영 체계의 플랫폼화

### 7.2 차이점
- **Apps in Toss**: 생성형 앱빌더보다 파트너 개발자 대상의 **SDK/API/검수/상용운영 체계**가 중심. **[Confirmed]**
- **Essential Apps**: 자연어 생성 + draft/live + partial update/rollback 중심의 end-user builder 성격이 강함. **[Likely/Confirmed]**
- **Wabi**: 플랫폼 내부 생성/실행(비공식 단서 포함) 및 리믹스 네트워크 성격. **[Likely]**

## 8) 기술 장벽 / 리스크
1. **검수-출시 리드타임 관리**: 빠른 실험과 품질통제 균형
2. **mTLS/백엔드 운영 난이도**: 파트너사 역량 편차 흡수 필요
3. **테스트-실서비스 환경 차이**: CORS/권한/네트워크 이슈
4. **권한·정책 준수**: 다크패턴/카테고리 정책/민감 서비스 규제 대응
5. **SDK 버전 정책 대응**: 마이그레이션 시기 관리 실패 리스크

## 9) Tizen PoC 시사점 (실행 가능한 형태)
- Apps in Toss 관점에서 벤치마크할 핵심:
  1) **콘솔 중심 배포 워크플로우**(검토/승인/출시/롤백)
  2) **권한 선언형 모델**(`granite.config.ts` 유사)
  3) **서버연동 보안 기본값**(mTLS 수준은 아니어도 강한 인증경로)
  4) **운영 지표/모니터링 내장**(출시 후 관측성)
- 즉 PoC는 생성 UX 이전에, **운영 가능한 플랫폼 골격**(패키징/검수/배포/롤백/관측성) 검증이 선행되어야 한다. **[Likely]**

## 10) Source Pointers
### Primary (대표님 지정)
- https://toss.im/apps-in-toss
- https://developers-apps-in-toss.toss.im
- https://github.com/toss/apps-in-toss-examples

### Additional (공식 문서/원문)
- https://developers-apps-in-toss.toss.im/intro/overview.md
- https://developers-apps-in-toss.toss.im/tutorials/webview.md
- https://developers-apps-in-toss.toss.im/tutorials/react-native.md
- https://developers-apps-in-toss.toss.im/development/integration-process.md
- https://developers-apps-in-toss.toss.im/development/deploy.md
- https://developers-apps-in-toss.toss.im/bedrock/reference/framework/권한/permission.md
- https://developers-apps-in-toss.toss.im/development/llms.html
- https://developers-apps-in-toss.toss.im/llms.txt
- https://raw.githubusercontent.com/toss/apps-in-toss-examples/main/README.md
- https://raw.githubusercontent.com/toss/apps-in-toss-examples/main/examples/README.md
