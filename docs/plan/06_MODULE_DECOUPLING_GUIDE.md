# HƯỚNG DẪN PHÂN CHIA MODULE & CÔ LẬP KHÔNG DẪM CHÂN NHAU (ZERO-CONFLICT DECOUPLING ARCHITECTURE)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên:**  
- **Thành viên 1 (TV1):** Nguyễn Đình Huy – MSSV: 202418918 (Lead Combat & Engine Core)  
- **Thành viên 2 (TV2):** Nguyễn Công Huy – MSSV: 202418914 (Lead World, Meta & Storage)  
**Mục tiêu cốt lõi:** Phân chia ranh giới module rõ ràng, triệt tiêu phụ thuộc vòng (Circular Dependency), loại bỏ 100% nguy cơ xung đột mã nguồn (Git Merge Conflicts) và cho phép hai bạn lập trình song song độc lập mà không phải chờ đợi code của nhau.  

---

## 1. NGUYÊN LÝ THIẾT KẾ: TẠI SAO CÁC NHÓM THƯỜNG "DẪM CHÂN NHAU"?

Trong các đề tài Bài tập lớn Game hướng đối tượng, sinh viên thường mắc 2 sai lầm kinh điển dẫn đến việc dẫm chân nhau:
1. **Dồn hết vào "God Class"**: Cả hai bạn cùng viết vào một file duy nhất `GameManager.cs` hoặc `PlayerController.cs`. Bạn A sửa hàm đánh nhau, bạn B sửa hàm nhặt đồ $\rightarrow$ Khi merge Git sẽ bị xung đột hàng trăm dòng mã (Merge Conflict) làm gãy project.
2. **Phụ thuộc vòng (Circular Coupling)**: Lớp của bạn A gọi trực tiếp lớp của bạn B, và lớp của bạn B lại gọi ngược lại lớp của bạn A $\rightarrow$ Bạn A không thể chạy Unit Test nếu bạn B chưa code xong, dẫn tới việc một người ngồi chờ người kia ("Blocking").

### Giải pháp Kiến trúc của Crystal Depths:
Dự án áp dụng **Kiến trúc Hướng Hợp Đồng (Contract-Driven Architecture)** kết hợp **Cầu nối Sự kiện (Event-Driven)**:
- Ranh giới giữa Nguyễn Đình Huy và Nguyễn Công Huy được ngăn cách hoàn toàn bởi **Tầng Hợp đồng (`Game.Core.Contracts`)**.
- Hai thành viên **tuyệt đối không gọi trực tiếp class cụ thể (Concrete Class) của nhau**, mà chỉ giao tiếp qua **Interface** và **EventBus**.

---

## 2. MA TRẬN PHÂN CHIA MODULE & VÙNG SỞ HỮU THƯ MỤC CỐ ĐỊNH

Mỗi thành viên được phân định **Quyền sở hữu độc quyền (Exclusive Ownership)** trên các thư mục mã nguồn riêng biệt:

```
┌────────────────────────────────────────────────────────────────────────┐
│             TẦNG HỢP ĐỒNG DÙNG CHUNG (KHOÁ CỨNG TỪ TUẦN 2)             │
│   src/Game.Core/Contracts/                                             │
│   - IDamageable, IMovable, ITileQuery, ISaveStorage, IGameEventBus     │
│   (Cả hai bạn cùng ký duyệt - Cấm tự ý sửa đổi một mình)               │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
         ┌──────────────────────────┴──────────────────────────┐
         ▼                                                     ▼
┌─────────────────────────────────────┐   ┌─────────────────────────────────────┐
│  PHÂN HỆ CỦA NGUYỄN ĐÌNH HUY (TV1)  │   │  PHÂN HỆ CỦA NGUYỄN CÔNG HUY (TV2)  │
│  [Namespace: Game.Core.Combat/AI]   │   │  [Namespace: Game.Core.World/Items] │
├─────────────────────────────────────┤   ├─────────────────────────────────────┤
│ 📁 src/Game.Core/Entities/          │   │ 📁 src/Game.Core/World/             │
│    Actor.cs, RoguePlayer.cs, Enemy  │   │    Floor.cs, RoomNode.cs, TileKind  │
│ 📁 src/Game.Core/Combat/            │   │ 📁 src/Game.Core/Items/             │
│    CombatResolver.cs, DamageInfo.cs │   │    Inventory.cs, Weapon.cs, Loot    │
│ 📁 src/Game.Core/AI/                │   │ 📁 src/Game.Core/Storage/           │
│    IEnemyAI.cs, ChaseAI, PatrolAI   │   │    SaveManager.cs, HighScoreBoard   │
│ 📁 src/Game.Core/Physics/           │   │ 📁 src/Game.Infrastructure/         │
│    CollisionDetector.cs, AABB, LOS  │   │    JsonSaveStorage.cs, ConfigLoader │
│ 📁 src/Game.Core/Loop/              │   │ 📁 src/Game.Presentation/WorldViews/│
│    GameLoop.cs, RuleEngine.cs       │   │    TilemapView.cs, MinimapView.cs   │
│ 📁 src/Game.Presentation/Combat/    │   │ 📁 src/Game.Presentation/UI/        │
│    ActorView.cs, CombatHudView.cs   │   │    ScreenManager.cs, Forms, Audio   │
│ 📁 tests/Game.Core.Tests/Combat/    │   │ 📁 tests/Game.Core.Tests/World/     │
└─────────────────────────────────────┘   └─────────────────────────────────────┘
```

### Quy tắc "Vùng Cấm" (Boundary Rules):
- **Nguyễn Đình Huy (TV1)**: Toàn quyền tạo và sửa file trong nhánh thư mục của mình. **Tuyệt đối không chạm vào** các file trong `World/`, `Items/`, `Storage/`, `UI/`.
- **Nguyễn Công Huy (TV2)**: Toàn quyền tạo và sửa file trong nhánh thư mục của mình. **Tuyệt đối không chạm vào** các file trong `Entities/`, `Combat/`, `AI/`, `Physics/`, `Loop/`.

---

## 3. CƠ CHẾ GIAO TIẾP KHÔNG DẪM CHÂN NHAU (THE DECOUPLING MECHANISMS)

### 3.1 Giao tiếp Chiều 1: Đình Huy cần biết thông tin Bản đồ của Công Huy (Query qua `ITileQuery`)
- **Bài toán**: Thuật toán tìm đường A* và va chạm Swept AABB của Đình Huy cần biết ô tọa độ $(x, y)$ có phải là tường đá hay không.
- **Cách giải quyết KHÔNG dẫm chân**:
  - Đình Huy **không gọi** lớp `Floor` hay `Tilemap` của Công Huy.
  - Đình Huy chỉ gọi interface:
    ```csharp
    // Trong Pathfinder.cs (Nguyễn Đình Huy viết):
    public class Pathfinder {
        private readonly ITileQuery _map;
        public Pathfinder(ITileQuery map) { _map = map; }
        
        public bool IsBlocked(int x, int y) {
            return _map.IsWalkable(x, y) == false; // GỌI QUA INTERFACE
        }
    }
    ```
  - Công Huy chỉ việc cho lớp `Floor` của mình cài đặt `ITileQuery`:
    ```csharp
    // Trong Floor.cs (Nguyễn Công Huy viết):
    public class Floor : ITileQuery {
        public bool IsWalkable(int x, int y) { /* Tra cứu mảng tile của Công Huy */ }
    }
    ```
  - **Khi viết Unit Test**: Đình Huy tự viết một lớp giả lập `MockTileQuery` (ma trận $10 \times 10$ trong bộ nhớ) để test A* xanh 100% mà **hoàn toàn không cần đợi Công Huy code xong class `Floor`**!

---

### 3.2 Giao tiếp Chiều 2: Đình Huy thông báo Quái chết $\rightarrow$ Công Huy rơi đồ (Qua `IGameEventBus`)
- **Bài toán**: Khi quái vật chết, cần rơi ra bình máu và vàng vào túi đồ của người chơi, đồng thời phát âm thanh và mở cửa phòng.
- **Cách giải quyết KHÔNG dẫm chân**:
  - Đình Huy xử lý xong sát thương, quái hết máu thì chỉ bắn một thông điệp sự kiện:
    ```csharp
    // Trong CombatResolver.cs (Nguyễn Đình Huy viết):
    _eventBus.Publish(new EnemyKilledEvent(enemyId, dropPosition));
    ```
  - Công Huy ở phân hệ của mình đăng ký lắng nghe sự kiện đó:
    ```csharp
    // Trong LootDirector.cs (Nguyễn Công Huy viết):
    _eventBus.Subscribe<EnemyKilledEvent>(evt => {
        var loot = _lootFactory.CreateLoot(evt.Position);
        _audioManager.PlaySFX("sfx_coin");
    });
    ```
  - **Lợi ích**: Code của Đình Huy và Công Huy hoàn toàn không biết sự tồn tại của nhau. Cả hai file biên dịch độc lập, không import namespace của nhau!

---

### 3.3 Điểm Lắp Ráp Chung Duy Nhất: `GameBootstrap.cs`
Để hai bạn không tranh chấp nhau khi khởi tạo game tại Composition Root:
- Lớp `GameBootstrap` được chia thành **2 tệp partial class** hoặc dùng mô hình **Builder**:
  - `GameBootstrap.Combat.cs` (Do Nguyễn Đình Huy quản lý): Khởi tạo `CombatResolver`, `MovementSystem`, `RuleEngine`.
  - `GameBootstrap.World.cs` (Do Nguyễn Công Huy quản lý): Khởi tạo `DungeonGenerator`, `SaveManager`, `ScreenManager`.
  - Tệp chính `GameBootstrap.cs` chỉ chứa 5 dòng ráp các module lại với nhau qua Constructor Injection.

---

## 4. CHIẾN LƯỢC PHÁT TRIỂN SONG SONG KHÔNG CHỜ ĐỢI (NON-BLOCKING WORKFLOW)

Nhờ áp dụng Dependency Inversion Principle (DIP), tiến độ của hai bạn được độc lập hóa hoàn toàn trong 5 tuần:

| Giai đoạn | Công việc Song song của Nguyễn Đình Huy (TV1) | Công việc Song song của Nguyễn Công Huy (TV2) | Cơ chế Khóa Độc lập |
|---|---|---|---|
| **Tuần 1** | Viết Game Loop 60Hz, Struct `Health`, `GridPos`. | Viết GDD, thiết kế bảng 10 PCU, cấu trúc phòng. | Dùng `MockInputProvider` test Game Loop. |
| **Tuần 2** | Cài đặt giả mã A*, Swept AABB va chạm, FSM Actor. | Cài đặt giả mã BSP sinh tầng, Autotile blob47, DTO Save. | Dùng `MockTileQuery` test va chạm tường. |
| **Tuần 3** | Cài đặt 7 kiểu AI, Skill, Status, Phản ứng nguyên tố. | Cài đặt Kho đồ, Cường hóa Decorator, `JsonSaveStorage`. | Dùng `FakeActor` test nhặt đồ, trừ máu. |
| **Tuần 4** | Dựng Adapter `ActorView`, viết tool `AtlasAnimationBuilder`. | Dựng `TilemapView`, `ScreenManager`, màn hình Shop/Altar. | Ghép qua `GameEventBus` trong 1 buổi duy nhất. |
| **Tuần 5** | Stress test 60 FPS, chạy Mutation Check phần Combat. | Kiểm tra Save/Load Round-trip, đóng gói Clean-room. | Chơi thử chéo (TV1 test TV2 & ngược lại). |

---

## 5. BẢO VỆ ĐÓNG GÓP GIT 50/50 & VẤN ĐÁP BẢO VỆ

- **Phân nhánh & Quy tắc Commit minh bạch**:
  - Nhánh của Nguyễn Đình Huy: `feat/tv1-combat`, `feat/tv1-ai`, `feat/tv1-physics`.
  - Nhánh của Nguyễn Công Huy: `feat/tv2-world`, `feat/tv2-inventory`, `feat/tv2-save`.
  - **Quy tắc tiền tố Commit bắt buộc**:
    - Nguyễn Đình Huy dùng cú pháp: `[Đình Huy]: <loại>(<phạm vi>): <mô tả>` (ví dụ: `[Đình Huy]: feat(combat): implement swept AABB collision detection to prevent tunneling`).
    - Nguyễn Công Huy dùng cú pháp: `[Công Huy]: <loại>(<phạm vi>): <mô tả>` (ví dụ: `[Công Huy]: feat(world): implement BSP dungeon split and MST corridor connector`).
    - Commit tài liệu và cấu hình chung dùng: `[Cả hai]: <loại>(<phạm vi>): <mô tả>`.
  - Lịch sử Git sẽ hiển thị rõ ràng: mỗi thành viên sở hữu 50% số commit, 50% số dòng mã logic, không có commit nào đè code của nhau, dễ dàng minh chứng trước hội đồng chấm thi.
- **Khi Giảng viên Vấn đáp Chéo (Cross-Module LSP)**:
  - Thầy hỏi Đình Huy: *"Làm sao em nhận biết được quái rơi đồ của bạn Công Huy?"* $\rightarrow$ Đình Huy trả lời: *"Em chỉ bắn sự kiện `EnemyKilledEvent` lên `GameEventBus`. Bạn Công Huy đăng ký nghe sự kiện để sinh vật phẩm. Phân hệ của em hoàn toàn không dính líu trực tiếp tới lớp túi đồ của bạn, bảo đảm nguyên lý Loose Coupling."*
  - Thầy hỏi Công Huy: *"Làm sao em kiểm tra được ô tường cản quái vật của bạn Đình Huy?"* $\rightarrow$ Công Huy trả lời: *"Lớp `Floor` của em cài đặt interface `ITileQuery`. Bạn Đình Huy chỉ gọi qua hàm `IsWalkable(x, y)` của interface mà không truy cập trực tiếp vào cấu trúc mảng nội bộ của em, đảm bảo tính đóng gói tuyệt đối."*

Đây chính là **câu trả lời chuẩn mực điểm 10** khiến hội đồng chấm đánh giá cực kỳ cao năng lực thiết kế phần mềm chuyên nghiệp của hai bạn sinh viên Bách Khoa!
