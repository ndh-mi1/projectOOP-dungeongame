# DANH MỤC CÔNG VIỆC DỰ ÁN (PROJECT BACKLOG)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) -->

## 1. NGUYỄN ĐÌNH HUY (TV1 - COMBAT, AI, PHYSICS, 60HZ ENGINE)

### NOW (Đang làm - Tuần 1: Phase P0 & P1)
- [ ] Thiết lập thư mục `src/Game.Core/Entities/`, `Combat/`, `AI/`, `Physics/`.
- [ ] Soạn thảo khung giao diện `ITileQuery` và cấu hình test rỗng `SanityCheckTests.cs`.
- [ ] Viết kịch bản 5 Use Case phân hệ Combat: Tấn công combo, Kích hoạt kỹ năng, AI truy đuổi, Phản ứng nguyên tố, Chết và hồi sinh.

### NEXT (Tuần 2 đến Tuần 3: Phase P2 & P3)
- [ ] Cài đặt Value Objects: `Health`, `DamageInfo`, `GridPos`, `Stats`.
- [ ] Cài đặt thuật toán A* (`Pathfinder`) và thuật toán va chạm Swept AABB.
- [ ] Cài đặt 7 kiểu hành vi AI (Chase, Patrol, Kite, Flit, Ambush, Charge, Slam).
- [ ] Xây dựng bộ giải quyết phản ứng nguyên tố `ReactionResolver` (5 combo).

### LATER (Tuần 4 đến Tuần 5: Phase P4 & P5)
- [ ] Viết Editor Tool `AtlasAnimationBuilder.cs` và Adapter khung hình đa pack.
- [ ] Hoàn thiện 12 Unit Tests Combat và 5 Integration Tests vòng lặp 60Hz.
- [ ] Diễn tập Live OCP (thêm chiến thuật quái mới trong 3 phút) và Fault-Injection.

---

## 2. NGUYỄN CÔNG HUY (TV2 - WORLD GEN, ITEMS, STORAGE, UI)

### NOW (Đang làm - Tuần 1: Phase P0 & P1)
- [ ] Thiết lập thư mục `src/Game.Core/World/`, `Items/`, `Storage/`, `src/Game.Infrastructure/`.
- [ ] Cấu hình kiểm tra `.gitignore` và thẩm định giấy phép CC0 các bộ asset pack.
- [ ] Viết kịch bản 5 Use Case phân hệ World: Sinh tầng ngẫu nhiên, Nhặt trang bị, Mua sắm tại Shop, Khấn tế Altar, Lưu/Tải trò chơi.

### NEXT (Tuần 2 đến Tuần 3: Phase P2 & P3)
- [ ] Cài đặt thuật toán sinh phòng ngẫu nhiên BSP + MST và BFS kiểm tra liên thông.
- [ ] Cài đặt thuật toán Autotile 47 bit (`Blob47Autotiler`).
- [ ] Cài đặt hệ thống Kho đồ, Trang bị và mẫu Decorator cường hóa vũ khí.
- [ ] Cài đặt `SaveManager`, `JsonSaveStorage` và DTO lưu trữ trạng thái.

### LATER (Tuần 4 đến Tuần 5: Phase P4 & P5)
- [ ] Cài đặt `ScreenManager` điều hướng các Form UI (Title, Pause, Inventory, Shop).
- [ ] Tích hợp 6,500+ RPG icon từ Raven Icons vào ô chứa đồ PPU 32.
- [ ] Hoàn thiện 12 Unit Tests World/Items và 5 Integration Tests Save/Load Round-trip.
- [ ] Đóng gói bản phát hành Clean-room Windows Standalone `.exe x64` $\le 3$ phút.
