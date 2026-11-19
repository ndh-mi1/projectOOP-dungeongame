# SỔ THEO DÕI NĂNG LỰC HIỂU BÀI: NGUYỄN CÔNG HUY (TV2)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Công Huy (MSSV: 202418914) -->

Thang đo mức độ hiểu bài:
- **Mức 0:** Chưa đọc / Chưa nắm được cấu trúc.
- **Mức 1:** Đã đọc mã nguồn và hiểu luồng chung.
- **Mức 2:** Tự giải thích được lý do thiết kế, SRP và luồng hoạt động mà không cần nhìn mã nguồn.
- **Mức 3:** Đã vượt qua câu hỏi phản biện sâu ($B \rightarrow A, \Delta, W$) trong diễn tập vấn đáp.
*Quy tắc:* Agent KHÔNG tự động nâng mức; chỉ nâng mức khi Công Huy trả lời đạt yêu cầu trong các buổi `/teach-back` hoặc `/defense-rehearsal`.

| Lớp / Module Nghiệp Vụ | Phân Hệ Sở Hữu | Mức Đạt | Ngày Đánh Giá | Ghi Chú & Bằng Chứng Trả Lời |
|---|:---:|:---:|:---:|---|
| `Floor`, `RoomNode` | World Core | Mức 2 | 2026-11-19 | Đã giải thích cấu trúc lưu trữ và đóng gói mảng ô gạch. |
| `TileKind` (Flyweight Pattern) | World Core | Mức 2 | 2026-11-19 | Đã trình bày lý do tiết kiệm RAM bằng 1 instance duy nhất/loại ô. |
| `DungeonGenerator` (BSP + MST) | World Core | Mức 2 | 2026-11-19 | Đã giải thích thuật toán chia nhị phân và nối chu trình phụ. |
| `Blob47Autotiler` (47 Bit) | World Core | Mức 2 | 2026-11-19 | Đã giải thích quy tắc bit chéo chỉ tính khi 2 ô vuông góc kề có gạch. |
| `Item`, `Weapon`, `Armor` | Items Core | Mức 2 | 2026-11-19 | Đã giải thích tính đóng gói và đa hình qua interface `IItem`. |
| `WeaponDecorator` (Decorator) | Items Core | Mức 2 | 2026-11-19 | Đã giải thích cơ chế bọc vũ khí runtime chống bùng nổ lớp con. |
| `SaveManager` & `JsonSaveStorage` | Storage Core | Mức 3 | 2026-11-19 | Đã vượt qua câu hỏi về cơ chế ghi file an toàn chống crash. |
| `ITileQuery` (Cài đặt trên Floor)| Contracts | Mức 3 | 2026-11-19 | Đã bảo vệ thành công nguyên lý LSP khi cấp API cho Đình Huy. |
| `ScreenManager` | UI Presentation| Mức 1 | 2026-11-19 | Đã nắm luồng điều hướng Form Title/Pause/Inventory. |
