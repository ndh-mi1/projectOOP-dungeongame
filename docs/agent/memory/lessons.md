# BỘ NHỚ BÀI HỌC KINH NGHIỆM TỪ GỐC RỄ (LESSONS LEARNED)
<!-- Cập nhật: 2026-11-19 | Bảo trì bởi: /memory-curate -->

- [2026-11-19][Cả hai][độ tin cậy: cao][nguồn: docs/plan/06_MODULE_DECOUPLING_GUIDE.md] Tránh xung đột YAML bằng cách phân chia Scene và Prefab riêng; Đình Huy giữ CombatScene, Công Huy giữ WorldScene và MenuScene. (kiểm lại khi: merge PR)
- [2026-11-19][Đình Huy][độ tin cậy: cao][nguồn: docs/plan/04_TEST_QA_GIT_PLAN.md] Dùng `DeterministicRandom` cho unit test để tránh hiện tượng test chập chờn (flaky test) khi tính tỷ lệ chí mạng và rơi đồ. (kiểm lại khi: viết unit tests)
- [2026-11-19][Công Huy][độ tin cậy: cao][nguồn: docs/plan/phases/P4_Implementation.md] Dữ liệu hóa toàn bộ màn chơi qua JSON giúp thay đổi cân bằng game trong 1 giây mà không phải biên dịch lại mã nguồn. (kiểm lại khi: playtest cân bằng)
- [2026-11-19][Cả hai][độ tin cậy: cao][nguồn: docs/plan/06_MODULE_DECOUPLING_GUIDE.md] Tách Composition Root thành `partial class GameBootstrap` giúp hai thành viên đăng ký service riêng mà không xung đột merge Git. (kiểm lại khi: ghép nối M1)
