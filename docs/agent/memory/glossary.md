# TỪ ĐIỂN THUẬT NGỮ DỰ ÁN (PROJECT GLOSSARY)
<!-- Cập nhật: 2026-11-19 | Bảo trì bởi: /memory-curate -->

Bảng tra cứu đồng nhất thuật ngữ Tiếng Việt, Tiếng Anh và Tên Lớp trong kiến trúc trò chơi.

| Thuật Ngữ Tiếng Việt | Thuật Ngữ Tiếng Anh | Tên Lớp / Interface Tương Ứng | Ý Nghĩa Nghiệp Vụ Cốt Lõi |
|---|---|---|---|
| Màn chơi cơ chế riêng | Playable Content Unit (PCU) | `FloorProfile`, `RoomNode` | Một tầng hầm có tileset, quái và cơ chế bẫy phân biệt. |
| Điểm khởi đầu sinh phòng | Binary Space Partitioning | `BspNode`, `DungeonGenerator` | Thuật toán chia nhị phân không gian để sinh phòng ngẫu nhiên. |
| Cây khung nhỏ nhất | Minimum Spanning Tree | `MstConnector` | Nối các phòng trong tầng bảo đảm liên thông và chu trình phụ. |
| Tự động ghép ngói 47 bit | Blob47 Autotiling | `Blob47Autotiler` | Tính toán 8 ô lân cận để chọn đúng tile góc, cạnh, trong. |
| Va chạm quét hộp bao | Swept AABB Collision | `CollisionDetector` | Tính toán thời gian va chạm $t \in [0, 1]$ chống xuyên tường. |
| Đường ngắm tầm nhìn | Line of Sight (Bresenham) | `LineOfSight` | Kiểm tra đường ngắm thẳng từ quái đến người chơi. |
| Mẫu chiến thuật | Strategy Pattern | `IEnemyAI`, `IAttackPattern` | Đóng gói thuật toán AI và đòn đánh để hoán đổi linh hoạt. |
| Mẫu trạng thái | State Pattern | `ActorState`, `IdleState`, `AttackState` | Quản lý vòng đời hoạt ảnh và cấm hành động khi bị choáng. |
| Mẫu trang trí | Decorator Pattern | `WeaponDecorator`, `BurningWeapon` | Bổ sung hiệu ứng nguyên tố vào vũ khí tại runtime. |
| Mẫu quan sát | Observer Pattern | `IGameEventBus`, `GameEventBus` | Truyền tin phi kết nối giữa phân hệ Combat và World/UI. |
| Trọng lượng nhẹ | Flyweight Pattern | `TileKind` | Chia sẻ thuộc tính bất biến của hàng vạn ô gạch. |
| Chế độ dạy học | Teach Mode | N/A (Agent protocol) | Agent giải thích lý do thiết kế trước khi sinh mã. |
