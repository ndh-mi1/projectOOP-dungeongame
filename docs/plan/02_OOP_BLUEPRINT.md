# BẢN THIẾT KẾ HƯỚNG ĐỐI TƯỢNG (OOP BLUEPRINT)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat & Engine Core)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead World, Meta & Storage)**  
**Tiêu chuẩn áp dụng:** Kiến trúc 3 tầng sạch (Clean Architecture), SOLID, GoF Patterns & UML 2.5  
**Quy chuẩn tương ứng:** Bước 02, 03, 04 PDF v2026 & Tiêu chí 3.2, 3.3, 4.1–4.6 Checklist K69  

---

## 1. KIẾN TRÚC PHÂN TẦNG & COMPOSITION ROOT

Hệ thống được tổ chức thành 3 tầng độc lập nghiêm ngặt theo nguyên lý Đảo ngược Phụ thuộc (Dependency Inversion Principle - DIP):

```
┌────────────────────────────────────────────────────────────────────────┐
│  TẦNG HIỂN THỊ & ENGINE (Game.Presentation - Unity / MonoBehaviours)   │
│  ActorView, TilemapView, ScreenManager, FxPlayer, PlayerInputAdapter   │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │ (Phụ thuộc qua Interfaces & Events)
┌───────────────────────────────────▼────────────────────────────────────┐
│  TẦNG NGHIỆP VỤ LÕI (Game.Core - Pure C# Class Library)                 │
│  - Assemblies cấm cứng UnityEngine (Game.Core.asmdef)                 │
│  - Entities, Value Objects, RuleEngine, GameLoop, Algorithms, Patterns │
│  - Contracts / Interfaces (IDamageable, IMovable, IInputProvider...)    │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │ (Phụ thuộc qua ISaveStorage / IConfig)
┌───────────────────────────────────▼────────────────────────────────────┐
│  TẦNG HẠ TẦNG & DỮ LIỆU (Game.Infrastructure - Pure C# / IO)           │
│  JsonSaveStorage, ConfigLoader, HighScoreRepository, FileSystemAdapter │
└────────────────────────────────────────────────────────────────────────┘
```

### Điểm Lắp Ráp Duy Nhất (Composition Root):
- Được hiện thực hóa tại lớp `GameBootstrap` (MonoBehaviour duy nhất đóng vai trò khởi động).
- `GameBootstrap.Awake()` là nơi duy nhất thực hiện việc `new` các lớp cụ thể và tiêm (Inject) phụ thuộc vào `GameSession`:
  ```csharp
  // GameBootstrap.cs (Composition Root)
  IInputProvider input = new UnityInputAdapter();
  ISaveStorage storage = new JsonSaveStorage(Application.persistentDataPath);
  IRandom random = new SystemRandom();
  IGameEventBus eventBus = new GameEventBus();
  
  GameSession session = new GameSession(input, storage, random, eventBus);
  ```
- **Ràng buộc sống còn**: Tuyệt đối không dùng mô hình Singleton toàn cục dạng `GameManager.Instance`; cấm đặt logic luật chơi hay kiểm tra thắng/thua trong phương thức `Update()` của MonoBehaviour.

---

## 2. DANH MỤC 28 LỚP THỰC CHẤT TRONG UML CLASS TỔNG DUY NHẤT

Dưới đây là 28 lớp/interface thực chất, mỗi lớp đảm nhận một trách nhiệm duy nhất (Single Responsibility Principle - SRP), phân định rõ ràng quyền sở hữu giữa TV1 và TV2:

| # | Tên Lớp / Interface | Thuộc Gói (Package) | Trách nhiệm cốt lõi (1 câu SRP) | Thành viên Sở hữu |
|:---:|---|---|---|:---:|
| **1** | `IDamageable` | `Core.Contracts` | Định nghĩa hợp đồng nhận sát thương và kiểm tra trạng thái sống còn. | **Cả hai duyệt** |
| **2** | `IMovable` | `Core.Contracts` | Định nghĩa hợp đồng di chuyển tọa độ và vận tốc trên lưới. | **Cả hai duyệt** |
| **3** | `IInputProvider` | `Core.Contracts` | Trừu tượng hóa nguồn tín hiệu điều khiển, tách rời bàn phím vật lý khỏi Core. | **Đình Huy (TV1)** |
| **4** | `ISaveStorage` | `Core.Contracts` | Trừu tượng hóa việc đọc/ghi dữ liệu bền vững, cho phép thay thế bằng Mock/Memory. | **Công Huy (TV2)** |
| **5** | `ITileQuery` | `Core.Contracts` | Cung cấp giao diện truy vấn ô cản và địa hình cho thuật toán tìm đường và va chạm. | **Công Huy (TV2)** |
| **6** | `IGameEventBus` | `Core.Contracts` | Cầu nối sự kiện Observer giữa hai phân hệ TV1 và TV2, triệt tiêu phụ thuộc vòng. | **Cả hai duyệt** |
| **7** | `GridPos` | `Core.ValueObjects` | Struct biểu diễn tọa độ nguyên bất biến trên bản đồ lưới kèm phép toán khoảng cách. | **Đình Huy (TV1)** |
| **8** | `Health` | `Core.ValueObjects` | Bảo vệ bất biến giới hạn máu ($0 \le \text{HP} \le \text{MaxHP}$) qua các hàm nghiệp vụ `Damage`/`Heal`. | **Đình Huy (TV1)** |
| **9** | `DamageInfo` | `Core.ValueObjects` | Chứa dữ liệu sát thương bất biến (lượng dame, kiểu sát thương, có chí mạng hay không). | **Đình Huy (TV1)** |
| **10**| `Entity` *(abstract)* | `Core.Entities` | Lớp cơ sở cung cấp định danh duy nhất (`Id`) và tọa độ vị trí trong thế giới game. | **Đình Huy (TV1)** |
| **11**| `Actor` *(abstract)* | `Core.Entities` | Thực thể sống sở hữu Máu (`Health`), khả năng nhận sát thương và máy trạng thái FSM. | **Đình Huy (TV1)** |
| **12**| `RoguePlayer` | `Core.Entities` | Đại diện cho nhân vật người chơi, quản lý thể lực, mana, kinh nghiệm và trang bị. | **Đình Huy (TV1)** |
| **13**| `Enemy` | `Core.Entities` | Kẻ địch cơ bản, ủy quyền việc ra quyết định hành vi cho chiến thuật `IEnemyAI`. | **Đình Huy (TV1)** |
| **14**| `BossEnemy` | `Core.Entities` | Kẻ địch tinh anh có thanh máu lớn, chuyển đổi nhiều pha hành vi dựa trên mốc máu. | **Đình Huy (TV1)** |
| **15**| `MimicEnemy` | `Core.Entities` | Kẻ địch ngụy trang thành rương báu, bất động cho tới khi người chơi kích hoạt tương tác. | **Đình Huy (TV1)** |
| **16**| `IEnemyAI` *(interface)* | `Core.AI` | Định nghĩa giao diện chiến thuật ra quyết định di chuyển và tấn công cho kẻ địch. | **Đình Huy (TV1)** |
| **17**| `ChaseAI` | `Core.AI` | Cài đặt chiến thuật tìm đường ngắn nhất đuổi theo người chơi bằng thuật toán A*. | **Đình Huy (TV1)** |
| **18**| `IActorState` *(interface)* | `Core.State` | Giao diện trạng thái hành vi của Actor (Enter, Update, Exit) có kiểm tra Guard. | **Đình Huy (TV1)** |
| **19**| `StatusEffect` *(abstract)* | `Core.Status` | Hiệu ứng bất lợi hoặc có lợi theo thời gian, kích hoạt tick chu kỳ đa hình. | **Đình Huy (TV1)** |
| **20**| `Skill` *(abstract)* | `Core.Skills` | Kỹ năng chủ động tiêu tốn tài nguyên, có thời gian hồi chiêu và hiệu ứng tác động. | **Đình Huy (TV1)** |
| **21**| `GameLoop` | `Core.Loop` | Điều phối chu trình lặp game cố định 60Hz bằng bộ tích lũy thời gian độc lập FPS. | **Đình Huy (TV1)** |
| **22**| `CombatResolver` | `Core.Combat` | Xử lý công thức tính toán sát thương, giáp, hiệu ứng chí mạng và kích hoạt phản ứng. | **Đình Huy (TV1)** |
| **23**| `Floor` | `Core.World` | Đại diện cho một tầng hầm ngục hoàn chỉnh, sở hữu danh sách phòng và lưới ô địa hình. | **Công Huy (TV2)** |
| **24**| `RoomNode` | `Core.World` | Đại diện cho một phòng hầm ngục (loại phòng, tọa độ, trạng thái khóa hay mở). | **Công Huy (TV2)** |
| **25**| `TileKind` | `Core.World` | Lớp Flyweight chứa dữ liệu dùng chung và hành vi khi dẫm lên ô (đất, tường, nước, dung nham). | **Công Huy (TV2)** |
| **26**| `Item` *(abstract)* | `Core.Items` | Lớp cơ sở cho mọi vật phẩm trong game, định nghĩa hành vi sử dụng `Use(Actor)`. | **Công Huy (TV2)** |
| **27**| `Weapon` | `Core.Items` | Trang bị tấn công, sở hữu một chiến thuật vung vũ khí hoán đổi được `IAttackPattern`. | **Công Huy (TV2)** |
| **28**| `Inventory` | `Core.Items` | Quản lý túi đồ người chơi, bảo vệ bất biến sức chứa và trả về `IReadOnlyList<Item>`. | **Công Huy (TV2)** |

---

## 3. BẢNG MINH CHỨNG 4 TÍNH CHẤT OOP & CÂU HỎI VẤN ĐÁP 1 DÒNG

| Tính chất OOP | Vị trí thể hiện cụ thể trong mã nguồn | Bất biến / Quy tắc kỹ thuật được bảo vệ | Câu giải thích ngắn gọn khi vấn đáp trực tiếp (B $\rightarrow$ A) |
|---|---|---|---|
| **Đóng gói** *(Encapsulation)* | `Health.cs`, `Inventory.cs`, `Wallet.cs` | Ẩn toàn bộ field thành `private`; không mở setter công khai; `Inventory.Items` trả về `IReadOnlyList<Item>`. | *"Em đóng gói toàn bộ chỉ số máu trong lớp Health, chỉ cho phép thay đổi qua TakeDamage và Heal để bảo toàn bất biến không bao giờ âm hoặc vượt Max."* |
| **Trừu tượng** *(Abstraction)* | `IDamageable`, `IInputProvider`, `ISaveStorage`, `ITileQuery` | Tách biệt hoàn toàn bản hợp đồng giao tiếp khỏi chi tiết cài đặt; tuân thủ ISP không tạo interface béo. | *"Em trừu tượng hóa việc lưu trữ qua ISaveStorage để Core không quan tâm dữ liệu ghi vào file JSON hay biến tạm trong Unit Test."* |
| **Kế thừa** *(Inheritance)* | `Entity` $\rightarrow$ `Actor` $\rightarrow$ `RoguePlayer`/`Enemy` $\rightarrow$ `BossEnemy` | Quan hệ thực sự Is-A; độ sâu kế thừa $\le 3$; lớp con tận dụng mã chung của lớp cha không ghi đè rỗng. | *"Cây kế thừa Actor tái sử dụng logic máu, va chạm và FSM chung cho cả người chơi lẫn quái vật mà không bị lặp mã nguồn."* |
| **Đa hình** *(Polymorphism)* | `Actor.Update(dt)`, `Item.Use(target)`, `StatusEffect.Tick(target)` | Lời gọi phương thức ảo thông qua Dynamic Binding; tuyệt đối không dùng `is/as/typeof` để phân nhánh. | *"Hệ thống cập nhật hàng trăm quái vật qua danh sách `List<Actor>` chỉ bằng một lời gọi `actor.Update(dt)` đa hình duy nhất."* |

### Minh chứng Cross-Module LSP (Checklist 4.4):
- **Kịch bản kiểm chứng**: TV2 tạo thêm một Cổ vật mới `VampiricRing` (kế thừa `Relic` $\rightarrow$ `Item`) hoặc Vũ khí mới `FrostWarhammer` (kế thừa `Weapon`). Khi truyền vào phương thức `CombatResolver.ExecuteAttack(Actor attacker, Actor target, Weapon weapon)` do TV1 viết, toàn bộ logic tính toán sát thương và hiệu ứng hút máu/đóng băng chạy chính xác 100% mà không phải sửa một dòng mã nguồn nào trong lớp của TV1.
- **Ngược lại**: TV1 tạo ra loại quái mới `MimicEnemy` (kế thừa `Enemy` $\rightarrow$ `Actor`). Khi được đưa vào danh sách quái trong `RoomNode.SpawnEntities()` do TV2 viết, phòng hầm ngục vẫn quản lý vòng đời quái vật và nhận biết sự kiện phòng dọn sạch (`OnRoomCleared`) hoàn toàn thông suốt.

---

## 4. SÁU QUAN HỆ UML VÀ CƠ CHẾ THAM CHIẾU / VÒNG ĐỜI TRONG CODE

```
┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐
│     Entity      │       │   IDamageable   │       │      Floor      │
└────────▲────────┘       └────────▲────────┘       └────────┬────────┘
         │ 1. Inheritance          │ 2. Realization          │ ◆ 3. Composition (Chặt)
┌────────┴────────┐       ┌────────┴────────┐       ┌────────▼────────┐
│     Actor       │       │     Actor       │       │    RoomNode     │
└─────────────────┘       └─────────────────┘       └─────────────────┘

┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐
│    Inventory    │       │     Player      │       │ CombatResolver  │
└────────┬────────┘       └────────┬────────┘       └────────┬────────┘
         │ ◇ 4. Aggregation        │ ── 5. Association       │ ..> 6. Dependency
┌────────▼────────┐       ┌────────▼────────┐       ┌────────▼────────┐
│      Item       │       │     Weapon      │       │   DamageInfo    │
└─────────────────┘       └─────────────────┘       └─────────────────┘
```

1. **Kế thừa (Inheritance `<|--`)**: `Actor <|-- RoguePlayer`. `RoguePlayer` là một `Actor`, kế thừa toàn bộ thuộc tính Máu, tọa độ và phương thức trừu tượng `OnKilled()`.
2. **Hiện thực hóa (Realization `<|..`)**: `IDamageable <|.. Actor`. Lớp `Actor` cài đặt hợp đồng nhận sát thương `TakeDamage(DamageInfo info)` để các phân hệ khác có thể tác động mà không cần biết kiểu cụ thể.
3. **Hợp thành chặt (Composition `*--`)**: `Floor *-- RoomNode` và `Actor *-- Health`.
   - *Minh chứng vòng đời trong code*: `Health` được khởi tạo trực tiếp (`new Health(...)`) bên trong constructor của `Actor`. Khi `Actor` bị hủy hoặc xóa khỏi bộ nhớ, đối tượng `Health` cũng bị hủy theo.
4. **Tụ hợp lỏng (Aggregation `o--`)**: `Inventory o-- Item`.
   - *Minh chứng vòng đời trong code*: `Inventory.AddItem(Item item)` nhận đối tượng `item` từ bên ngoài truyền vào. Khi túi đồ bị xóa (reset ván), các vật phẩm có thể vẫn tồn tại trên mặt đất hoặc chuyển vào kho khác.
5. **Liên kết (Association `-->`)**: `RoguePlayer --> Weapon`.
   - *Minh chứng trong code*: `RoguePlayer` giữ một biến thành viên kiểu `Weapon _currentWeapon`. Nhân vật có thể đổi vũ khí trong runtime mà không làm thay đổi bản chất nhân vật.
6. **Phụ thuộc tạm thời (Dependency `..>`)**: `CombatResolver ..> DamageInfo`.
   - *Minh chứng trong code*: Phương thức `Calculate(DamageInfo damage, Stats stats)` chỉ nhận `DamageInfo` làm tham số đầu vào của hàm; lớp không lưu giữ biến tham chiếu dài hạn tới đối tượng này.

---

## 5. NĂM MẪU THIẾT KẾ GOF BẮT BUỘC & ĐIỂM NEO LIVE OCP

Mỗi mẫu thiết kế được áp dụng để giải quyết một **Mùi mã nguồn (Code Smell)** cụ thể trong trò chơi:

| Tên Mẫu GoF | Nhóm | Chủ sở hữu | Mùi code xuất hiện nếu BỎ mẫu thiết kế | Điểm mở rộng OCP chuẩn bị cho Live Coding | $\ge 2$ Nhược điểm & Chi phí thực tế (Câu hỏi W) |
|---|:---:|:---:|---|---|---|
| **State Pattern** (`IActorState`) | Behavioral | **Đình Huy (TV1)** | Chuỗi `if/else` lồng nhau phức tạp trong `Update()` để kiểm tra cờ `isAttacking`, `isDead`, `isStunned`. | Tạo thêm một trạng thái mới: `FrozenState` (bất động, không nhận input, đếm ngược thời gian rã băng). | 1. Tăng đột biến số lượng lớp nhỏ.<br>2. Khó chia sẻ biến tạm thời giữa các lần chuyển trạng thái nếu không truyền Context. |
| **Strategy Pattern** (`IEnemyAI`) | Behavioral | **Đình Huy (TV1)** | Lệnh `switch(enemyType)` khổng lồ trong AI Controller; mỗi lần thêm quái phải mở code cũ sửa nhánh. | Tạo thêm một chiến thuật AI mới: `NecromancerAI` (giữ cự ly 6 ô và mỗi 4s triệu hồi 2 quái `skeleton`). | 1. Client phải hiểu rõ sự khác biệt giữa các chiến thuật để khởi tạo đúng.<br>2. Tăng số lượng đối tượng cấp phát trên Heap nếu không kết hợp Flyweight. |
| **Factory Method** (`ILootFactory`) | Creational | **Công Huy (TV2)** | Khối lệnh `new` vật phẩm rải rác ở hàng chục class; logic tỷ lệ rơi đồ bị phân mảnh khắp game loop. | Tạo thêm nhà máy rơi đồ mới: `BossChestLootFactory` (chắc chắn rơi 1 Cổ vật Thần thoại + 50 vàng). | 1. Phải tạo cây phân cấp Creator song song với cây phân cấp Product.<br>2. Đòi hỏi khởi tạo gián tiếp làm tăng độ phức tạp kiến trúc. |
| **Decorator Pattern** (`WeaponEnchant`) | Structural | **Công Huy (TV2)** | Bùng nổ tổ hợp lớp con (Kiếm Lửa, Kiếm Băng, Kiếm Hút Máu, Kiếm Lửa Hút Máu $\rightarrow 2^n$ lớp). | Tạo thêm bùa cường hóa mới: `PoisonEnchantDecorator` (bọc vũ khí bất kỳ để đòn đánh áp thêm hiệu ứng trúng độc). | 1. Thứ tự lồng ghép Decorator có thể gây sai lệch kết quả nếu không quản lý chặt.<br>2. Rất khó gỡ bỏ một lớp bọc nằm sâu ở giữa chuỗi Decorator. |
| **Observer Pattern** (`GameEventBus`) | Behavioral | **Cả hai duyệt** | Phân hệ Core của TV1 phải gọi trực tiếp sang HUD, Audio, Loot của TV2; phụ thuộc vòng gây nghẽn biên dịch. | Tạo thêm người nghe mới: `AchievementTracker` (đăng ký `OnEnemyKilled` để đếm chỉ tiêu mở khóa huy hiệu). | 1. Rủi ro rò rỉ bộ nhớ (Memory Leak) nếu bên nhận quên gọi `Unsubscribe` khi hủy.<br>2. Thứ tự xử lý sự kiện giữa các listener không được bảo đảm tuần tự. |

---

## 6. SÁU THUẬT TOÁN PHI TẦM THƯỜNG (3 ĐÌNH HUY / 3 CÔNG HUY)

### 6.1 Thuật toán của Nguyễn Đình Huy (TV1 - Combat & Engine Core)

#### 1. A* Pathfinding trên Lưới Ô (`Pathfinder.cs`)
- **Mục tiêu**: Tìm đường đi ngắn nhất tránh các ô tường và bẫy cho quái vật tiếp cận người chơi.
- **Hàm Heuristic**: Manhattan distance $h(A, B) = |x_A - x_B| + |y_A - y_B|$.
- **Độ phức tạp**: Thời gian $O((V + E) \log V)$ sử dụng hàng đợi ưu tiên `PriorityQueue`; Không gian $O(V)$.
- **Ca biên cực đoan**: Đích đến nằm bên trong bức tường hoặc bị quái khác vây kín $\rightarrow$ Thuật toán tự động tìm ô khả dụng gần đích nhất (dựa trên $h$) và trả về đường đi tới ô đó thay vì trả về null gây kẹt cứng AI.

#### 2. Va chạm Quét Khối theo Trục (Swept AABB Collision) & Spatial Hash (`CollisionDetector.cs`)
- **Mục tiêu**: Phát hiện và cản đạn bay nhanh, nhân vật lướt né không bị xuyên tường (tunneling).
- **Cơ chế**: Thay vì kiểm tra điểm ở frame cuối, thuật toán quét hình hộp bao mở rộng (Minkowski Sum) dọc theo vector dịch chuyển $\vec{v} \cdot \Delta t$.
- **Độ phức tạp**: $O(1)$ cho kiểm tra cặp va chạm; Spatial Hash Table giảm kiểm tra diện rộng từ $O(n^2)$ về $O(n)$.
- **Ca biên cực đoan**: Đạn bay tốc độ cực cao xuyên qua bức tường mỏng 1 ô $\rightarrow$ Thời gian va chạm $t_{\text{hit}} \in [0, 1]$ được tính toán chính xác; tọa độ thực thể bị chặn cứng ngay tại bề mặt tiếp xúc của ô tường.

#### 3. Đường Nhìn Thẳng Bresenham (Line of Sight - LOS) (`LineOfSight.cs`)
- **Mục tiêu**: Kiểm tra xem giữa quái vật và người chơi có đường ngắm thẳng hay bị che khuất bởi vách đá.
- **Độ phức tạp**: Thời gian $O(\max(\Delta x, \Delta y))$; Không gian $O(1)$ chỉ dùng số nguyên, không dùng phép chia số thực.
- **Ca biên cực đoan**: Đường quét đi qua đúng góc tiếp giáp giữa 2 ô tường $\rightarrow$ Xử lý chặn tia nhìn nếu cả hai ô kề đều là vật cản cứng.

---

### 6.2 Thuật toán của Nguyễn Công Huy (TV2 - World, Meta & Generation)

#### 4. Phân tách Không gian Nhị phân (BSP) kết hợp Cây Khung Nhỏ nhất (MST) & BFS (`DungeonGenerator.cs`)
- **Mục tiêu**: Tự động sinh hầm ngục dạng lưới phòng ngẫu nhiên luôn đảm bảo tính liên thông $100\%$ và khả thắng.
- **Các bước thực thi**:
  1. Dùng BSP phân chia đệ quy mặt phẳng tầng hầm thành các vùng chứa phòng.
  2. Nối tâm các phòng bằng phép đan tam giác Delaunay và thuật toán Kruskal tìm cây khung nhỏ nhất (MST) để đảm bảo không có chu trình dư thừa làm rối đường.
  3. Bổ sung $15\%$ cạnh ngẫu nhiên tạo đường tắt.
  4. Chạy BFS từ phòng Start để xác thực tính liên thông tới phòng Boss và kiểm tra chìa khóa luôn nằm trước cửa khóa tương ứng.
- **Độ phức tạp**: $O(R \log R)$ với $R$ là số lượng phòng; Kiểm tra BFS $O(V + E)$.
- **Ca biên cực đoan**: Căn phòng sinh ra kích thước nhỏ hơn kích thước tối thiểu $5 \times 5$ $\rightarrow$ Hủy nhánh phân chia và giữ nguyên phòng cha.

#### 5. Thuật toán Ghép Ô Tự động Blob47 (Blob47 Autotiler) (`Blob47Autotiler.cs`)
- **Mục tiêu**: Tự động chọn đúng tile vách, viền nước, bóng tường dựa trên tệp luật ngoài `autotile.json`.
- **Cơ chế Bitmask**: Quét 8 ô lân cận (4 hướng vuông góc và 4 góc chéo). Bit góc chéo chỉ được tính khi cả hai ô vuông góc kề bên đều cùng loại. Ánh xạ giá trị tổng bit ($0..255$) sang đúng 1 trong 47 sprite vách/nước.
- **Độ phức tạp**: Thời gian $O(W \times H)$ một lần duy nhất khi tạo tầng; Không gian $O(1)$.
- **Ca biên cực đoan**: Ô nằm ở mép bản đồ $\rightarrow$ Mặc định gán các ô ngoài biên là loại `Wall` để ngăn lộ khoảng trống vô cực.

#### 6. Sinh Đồ Trọng số có Bảo hiểm (Weighted Loot Roller with Pity) (`LootRoller.cs`)
- **Mục tiêu**: Sinh vật phẩm rớt ra từ quái và rương theo bảng xác suất có trọng số, đảm bảo công bằng.
- **Cơ chế**: Sử dụng mảng phân phối tích lũy (Cumulative Weight). Bộ đếm Pity đếm số phòng không có bình máu; khi vượt ngưỡng 5 phòng, trọng số bình máu được cưỡng bức lên mức cực đại.
- **Độ phức tạp**: Thời gian $O(\log K)$ với $K$ là số loại vật phẩm trong bảng loot (tìm kiếm nhị phân).
- **Ca biên cực đoan**: Tổng trọng số bằng 0 do cấu hình JSON lỗi $\rightarrow$ Ném ngoại lệ `InvalidLevelDataException` kèm thông báo lỗi cụ thể thay vì rơi vào vòng lặp vô hạn.

---

## 7. HỆ THỐNG 5 SƠ ĐỒ ĐỘNG (DYNAMIC BEHAVIOR MODELING)

Toàn bộ 5 sơ đồ động dưới đây đều được chuẩn hóa để kiểm chứng khớp $100\%$ trên IDE Debugger (Call Stack và Watch Window):

### 7.1 Sơ đồ Trạng thái Vòng đời Game (Game Lifecycle FSM)
- Các trạng thái: `TitleState` $\rightarrow$ `ClassSelectState` $\rightarrow$ `PlayingState` $\rightleftarrows$ `PausedState` $\rightarrow$ `FloorTransitionState` $\rightarrow$ `VictoryState` / `GameOverState` $\rightarrow$ `HighScoreState`.
- **Guard kiểm soát**: Không thể chuyển từ `PausedState` sang `GameOverState` nếu không thông qua `PlayingState`.

### 7.2 Sơ đồ Trạng thái Hành vi Actor có Guard Chặn (Actor FSM)
- Các trạng thái: `IdleState` $\rightleftarrows$ `MoveState` $\rightarrow$ `AttackState` $\rightarrow$ `HurtState` $\rightarrow$ `StunnedState` $\rightarrow$ `DeadState`.
- **Dòng Guard kiểm chứng**:
  ```csharp
  // Actor.cs
  public void PerformAttack() {
      if (CurrentState is DeadState || CurrentState is StunnedState) {
          return; // GUARD CHẶN ĐỨNG HÀNH ĐỘNG BẤT HỢP LỆ
      }
      TransitionTo(new AttackState(this));
  }
  ```

### 7.3 Sơ đồ Trạng thái Phòng Hầm ngục (Room State FSM)
- Các trạng thái: `Locked` (Cổng hạ) $\rightarrow$ `Active` (Quái xuất hiện) $\rightarrow$ `Cleared` (Cổng mở, phát sinh rương thưởng).

### 7.4 Sơ đồ Tuần tự 1 Khung hình Cố định (Sequence Diagram: 1 FixedUpdate Tick)
- Luồng tuần tự: `GameLoop.Tick(dt)` $\rightarrow$ `InputProvider.GetCommand()` $\rightarrow$ `MovementSystem.Update(dt)` $\rightarrow$ `CollisionDetector.Resolve()` $\rightarrow$ `AISystem.Update(dt)` $\rightarrow$ `CombatResolver.Tick()` $\rightarrow$ `EventBus.Dispatch()`.

### 7.5 Sơ đồ Tuần tự Xuyên Phân hệ TV1 $\leftrightarrow$ TV2 (Cross-Module Sequence)
- **Kịch bản**: Người chơi đánh chết quái $\rightarrow$ Rơi đồ $\rightarrow$ Nhặt đồ vào túi:
  1. `RoguePlayer` (TV1) tấn công $\rightarrow$ `CombatResolver` (TV1) trừ máu `Enemy.Health` về 0.
  2. `Enemy` phát sự kiện `OnEnemyKilled(enemy, position)` lên `GameEventBus` (Cầu nối).
  3. `LootDirector` (TV2) bắt sự kiện $\rightarrow$ gọi `LootFactory.CreateLoot()` (TV2).
  4. Vật phẩm `Item` xuất hiện trên sàn tại `GridPos`.
  5. `RoguePlayer` bước vào ô $\rightarrow$ kích hoạt `Inventory.AddItem(item)` (TV2).
  6. `Inventory` phát sự kiện `OnInventoryChanged` $\rightarrow$ `HudView` cập nhật hiển thị.
  *(Kiểm chứng bằng Breakpoint: Call Stack hiển thị rõ ràng chuỗi lời gọi đi xuyên từ namespace của TV1 sang namespace của TV2).*
