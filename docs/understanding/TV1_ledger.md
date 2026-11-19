# SỔ THEO DÕI NĂNG LỰC HIỂU BÀI: NGUYỄN ĐÌNH HUY (TV1)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy (MSSV: 202418918) -->

Thang đo mức độ hiểu bài:
- **Mức 0:** Chưa đọc / Chưa nắm được cấu trúc.
- **Mức 1:** Đã đọc mã nguồn và hiểu luồng chung.
- **Mức 2:** Tự giải thích được lý do thiết kế, SRP và luồng hoạt động mà không cần nhìn mã nguồn.
- **Mức 3:** Đã vượt qua câu hỏi phản biện sâu ($B \rightarrow A, \Delta, W$) trong diễn tập vấn đáp.
*Quy tắc:* Agent KHÔNG tự động nâng mức; chỉ nâng mức khi Đình Huy trả lời đạt yêu cầu trong các buổi `/teach-back` hoặc `/defense-rehearsal`.

| Lớp / Module Nghiệp Vụ | Phân Hệ Sở Hữu | Mức Đạt | Ngày Đánh Giá | Ghi Chú & Bằng Chứng Trả Lời |
|---|:---:|:---:|:---:|---|
| `GridPos`, `Health`, `DamageInfo` | Combat Core | Mức 2 | 2026-11-19 | Đã giải thích được tính bất biến của Value Object. |
| `Actor`, `RoguePlayer`, `Enemy` | Entities Core | Mức 2 | 2026-11-19 | Đã giải thích phân cấp kế thừa IS-A và SRP. |
| `CollisionDetector` (Swept AABB)| Physics Core | Mức 2 | 2026-11-19 | Đã trình bày phương pháp tính thời gian va chạm $t \in [0, 1]$. |
| `Pathfinder` (Thuật toán A*) | AI Core | Mức 2 | 2026-11-19 | Đã giải thích hàm Heuristic Manhattan và hàng đợi ưu tiên. |
| 7 Kiểu Chiến Thuật `IEnemyAI` | AI Core | Mức 2 | 2026-11-19 | Đã giải thích mẫu Strategy hoán đổi hành vi linh hoạt. |
| `ReactionResolver` (5 Combo) | Combat Core | Mức 2 | 2026-11-19 | Đã trình bày ma trận phản ứng nguyên tố không downcast. |
| `GameEventBus` (Observer) | Contracts | Mức 3 | 2026-11-19 | Đã vượt qua câu hỏi vấn đáp liên module với Công Huy. |
| `ITileQuery` (Cross-Module LSP) | Contracts | Mức 3 | 2026-11-19 | Đã bảo vệ thành công nguyên lý LSP khi nhận diện tường. |
| `AtlasAnimationBuilder` | Editor Tools | Mức 1 | 2026-11-19 | Đã nắm luồng sinh AnimationClip tự động từ `atlas.json`. |
