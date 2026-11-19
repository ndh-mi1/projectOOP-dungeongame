# PHASE 4: BƯỚC 04 – CÀI ĐẶT OOP, SOLID & TÍCH HỢP VIEW (IMPLEMENTATION PHASE)
**Mã giai đoạn:** P4 | **Thời gian thực hiện:** Tuần 3 đến Tuần 4 (T3–T4)  
**Tiêu chuẩn áp dụng:** Bước 04 PDF v2026 (Trang 24–29) & Tiêu chí 2.1–2.4, 4.3–4.6 Checklist K69  
**Nhân sự thực hiện:**
- **Nguyễn Đình Huy (MSSV: 202418918 - TV1)**: Core Combat, 7 AI, Physics/Swept AABB 60Hz, Game Loop, Combat HUD, FxPlayer, Multi-Pack Animation Adapter.
- **Nguyễn Công Huy (MSSV: 202418914 - TV2)**: World Gen (BSP+MST), Blob47 Autotiler, Items/Inventory Decorator, Save/Load JSON, ScreenManager, Audio.

**Mục tiêu cốt lõi:** Hiện thực hóa toàn bộ bản thiết kế UML thành mã nguồn C# chuẩn mực theo 2 giai đoạn (GĐ1: Core & AI Headless; GĐ2: View, Save, Object Pool, Audio & Multi-Asset Adapter), trải qua 6 mốc kiểm tra (M1 $\rightarrow$ M6), tuân thủ triệt để SOLID và kiểm toán sạch không có `using UnityEngine` trong Core.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. **Giai đoạn 1 (T3 - Core & AI Headless)**: Cài đặt toàn bộ tầng nghiệp vụ thuần C# trong `Game.Core`, chạy độc lập không cần mở Scene Unity; hoàn thiện hệ thống chiến đấu, va chạm swept AABB, 7 kiểu AI và 5 phản ứng nguyên tố.
2. **Giai đoạn 2 (T4 - View, Save & Audio)**: Xây dựng các lớp Adapter mỏng trong Unity, dựng Tilemap tự động qua `Blob47Autotiler`, tích hợp công cụ Editor dựng AnimationClip từ `atlas.json` và adapter khung hình `ISpriteFrameProvider` phối hợp 6 bộ asset, gắn Object Pool và hoàn thiện Save/Load JSON.
3. Vượt qua 6 mốc kiểm tra liên hoàn (Milestones M1 đến M6) đúng tiến độ, đóng băng tính năng (Feature Freeze) tại mốc M6.
4. Bảo đảm 100% tuân thủ quy tắc Coding/Naming Convention, không có mã chết (dead code), không có khối lệnh trùng lặp (DRY).
5. Quản lý việc ứng dụng AI: chỉ sinh mã theo lát cắt nhỏ, thành viên sở hữu phải đọc hiểu từng dòng, chạy test xanh mới commit dưới tên mình.

---

## 2. LỘ TRÌNH 6 MỐC KIỂM TRA LIÊN HOÀN (MILESTONES M1 – M6)

```
[M1: Lõi Tối Giản] ──> [M2: Đủ AI & Combat] ──> [M3: Lát Cắt Nhìn Thấy] ──> [M4: Mở Rộng 10 PCU] ──> [M5: Game Feel & Audio] ──> [M6: FEATURE FREEZE]
   (Giữa T3)               (Cuối T3)                 (Đầu T4)                   (Giữa T4)                 (Cuối T4)                 (Đầu T5)
   - Tầng sinh ra          - Đủ 7 kiểu AI            - Tilemap Autotile         - Đủ 10 PCU JSON           - Rung camera, hit-stop    - Khóa toàn bộ tính năng
   - Player đi lại         - Kỹ năng & Status        - Hoạt ảnh 6 bộ asset      - Save/Load JSON 100%      - BGM theo biome, SFX      - Chỉ sửa bug & viết test
   - 1 Quái đuổi A*        - Swept AABB chống xuyên  - HUD máu, mana            - Object Pool đạn/quái     - Màn Shop, Altar, Menu    - Chuẩn bị đóng gói
   - Giết quái phát Event  - 5 Phản ứng nguyên tố    - View Adapter mỏng        - Bảng điểm HighScore      - Volume lưu bền vững
```

---

## 3. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

### Giai đoạn 1: Core & AI Headless (Tuần 3)
| Mã Việc | Hạng mục công việc kỹ thuật | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Mốc / Đầu ra |
|:---:|---|:---:|:---:|:---:|---|
| **W4.1** | Cài đặt các Value Objects: `Health`, `GridPos`, `DamageInfo`, `Stats`. | **Nguyễn Đình Huy (TV1)** | 4h | P3 | M1 / `Core/ValueObjects/` |
| **W4.2** | Cài đặt các Entity cốt lõi: `Entity`, `Actor`, `RoguePlayer`, `Enemy`. | **Nguyễn Đình Huy (TV1)** | 6h | W4.1 | M1 / `Core/Entities/` |
| **W4.3** | Cài đặt cấu trúc thế giới: `Floor`, `RoomNode`, `TileKind` (Flyweight). | **Nguyễn Công Huy (TV2)** | 6h | P3 | M1 / `Core/World/` |
| **W4.4** | Cài đặt thuật toán A* (`Pathfinder`) và tích hợp `ChaseAI`. | **Nguyễn Đình Huy (TV1)** | 6h | W4.2, W4.3 | M1 / `Core/AI/` |
| **W4.5** | Cài đặt `GameEventBus` và kiểm chứng sự kiện `OnEnemyKilled`. | **Cả hai (Đình Huy & Công Huy)** | 3h | W4.2 | **M1: Hoàn thành Lõi tối giản** |
| **W4.6** | Cài đặt 6 kiểu AI còn lại (Patrol, Kite, Flit, Ambush, Charge, Slam). | **Nguyễn Đình Huy (TV1)** | 8h | W4.4 | M2 / `Core/AI/` |
| **W4.7** | Cài đặt thuật toán va chạm Swept AABB chống tunneling & Spatial Hash. | **Nguyễn Đình Huy (TV1)** | 8h | W4.2, W4.3 | M2 / `Core/Physics/` |
| **W4.8** | Cài đặt hệ thống Kỹ năng, Trạng thái (StatusEffect) và `ReactionResolver`. | **Nguyễn Đình Huy (TV1)** | 6h | W4.2 | M2 / `Core/Combat/` |
| **W4.9** | Cài đặt thuật toán sinh tầng BSP + MST + BFS kiểm tra liên thông. | **Nguyễn Công Huy (TV2)** | 8h | W4.3 | **M2: Đủ AI & Combat Headless** |

### Giai đoạn 2: View, Save, Object Pool & Audio (Tuần 4)
| Mã Việc | Hạng mục công việc kỹ thuật | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Mốc / Đầu ra |
|:---:|---|:---:|:---:|:---:|---|
| **W4.10**| Viết công cụ Editor tự động dựng `AnimationClip` (`AtlasAnimationBuilder.cs`) và các adapter `ISpriteFrameProvider` phối hợp 6 bộ asset. | **Nguyễn Đình Huy (TV1)** | 5h | P0 | Tool trong `Assets/Editor/`, `App/Adapters/` |
| **W4.11**| Cài đặt `Blob47Autotiler` và kết nối với Unity `TilemapView`. | **Nguyễn Công Huy (TV2)** | 6h | W4.9 | M3 / `App/Views/` |
| **W4.12**| Dựng các Adapter mỏng: `ActorView`, `PlayerInputAdapter`, `CombatHudView`.| **Nguyễn Đình Huy (TV1)** | 8h | W4.10 | **M3: Lát cắt nhìn thấy được** |
| **W4.13**| Dữ liệu hóa 10 PCU qua file JSON (`floors.json`, `enemies.json`). | **Nguyễn Công Huy (TV2)** | 6h | W4.9 | M4 / `StreamingAssets/Data/` |
| **W4.14**| Cài đặt hệ thống Kho đồ, Trang bị (dùng Raven Icons), Decorator cường hóa vũ khí. | **Nguyễn Công Huy (TV2)** | 6h | W4.3 | M4 / `Core/Items/` |
| **W4.15**| Cài đặt `SaveManager`, `JsonSaveStorage`, DTO lưu/tải và `HighScoreBoard`. | **Nguyễn Công Huy (TV2)** | 6h | W4.13 | M4 / `Core/Storage/` |
| **W4.16**| Cài đặt Generic `ObjectPool<T>` cho đạn đạo, quái vật và hạt hiệu ứng (Tiny Swords FX). | **Nguyễn Đình Huy (TV1)** | 5h | W4.12 | **M4: Mở rộng 10 PCU & Lưu trữ**|
| **W4.17**| Cài đặt `ScreenManager` điều hướng các Form: Title, Pause, Inventory, Shop. | **Nguyễn Công Huy (TV2)** | 7h | W4.14 | M5 / `App/UI/` |
| **W4.18**| Cài đặt `AudioManager`, cấu hình âm lượng bền vững và bộ FX Game Feel. | **Cả hai (Đình Huy & Công Huy)** | 6h | W4.12 | M5 / `App/Audio/`, `FxPlayer` |
| **W4.19**| Kiểm toán toàn bộ mã nguồn, dọn sạch code thừa, cố định API. | **Cả hai (Đình Huy & Công Huy)** | 4h | Toàn bộ | **M6: Feature Freeze (Đóng băng)**|

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 2.1**: Gameplay loop đồng bộ 2 phân hệ, UI/HUD & Game feel.
- **Tiêu chí 2.2**: Độ ổn định liên phân hệ, ngoại lệ & An toàn gameplay.
- **Tiêu chí 2.3**: Hiệu năng real-time (60 FPS, deltaTime, Object Pool).
- **Tiêu chí 2.4**: Chất lượng mã nguồn, Clean Code & Đồng nhất 2 thành viên.
- **Tiêu chí 4.4**: Đóng gói bất biến, Đa hình & Cross-Module LSP.

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Toàn bộ 28 lớp trong bản vẽ UML tổng được hiện thực hóa đầy đủ bằng các file `.cs` tương ứng; các phương thức giữ đúng kiểu dữ liệu và thứ tự tham số.
- **Chiều ngược [$B \rightarrow A$]**: Quét tự động bằng PowerShell script: kiểm tra toàn bộ thư mục `src/Game.Core/` cam kết $0$ dòng `using UnityEngine;` và $0$ dòng `using UnityEditor;`.

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] Đạt đầy đủ 6 mốc kiểm tra M1 đến M6 đúng hạn vào cuối Tuần 4.
- [x] Bộ test tự động (24 Unit Tests) chạy xanh 100% trong NUnit EditMode.
- [x] Game chạy mượt mà chuỗi Menu $\rightarrow$ Chơi $\rightarrow$ Pause $\rightarrow$ Win/Lose $\rightarrow$ Replay đạt 60 FPS ổn định, không có hiện tượng giật lag do rác bộ nhớ (Zero GC spike).
- [x] Không còn bất kỳ khối lệnh `catch {}` rỗng nào; toàn bộ lỗi đọc ghi file hay dữ liệu hỏng đều ném `CustomException` có thông điệp tường minh.
- [x] Lịch sử Git ghi nhận đóng góp cân đối: Nguyễn Đình Huy (TV1) đạt 45%–55%, Nguyễn Công Huy (TV2) đạt 45%–55% mã logic.

---

## 7. CỔNG HIỂU SÂU (COMPREHENSION GATE P4)
1. **Câu hỏi B $\rightarrow$ A (Cross-Module LSP)**:
   - *Hỏi*: *"Làm thế nào bạn chứng minh được nguyên lý Liskov Substitution Principle (LSP) được bảo toàn khi lớp `Weapon` của Công Huy tương tác với lớp `CombatResolver` của Đình Huy?"*
   - *Trả lời*: `CombatResolver` chỉ nhận tham số kiểu trừu tượng `Weapon` và gọi phương thức `weapon.GetAttackPattern().Execute(...)`. Cho dù Công Huy truyền vào một lớp con cụ thể như `BurningSwordDecorator` hay `SpearWeapon`, hàm của Đình Huy vẫn xử lý mượt mà mà không cần dùng lệnh ép kiểu `if (weapon is SpearWeapon)` hay kiểm tra loại vũ khí, đảm bảo lớp con thay thế hoàn hảo cho lớp cha mà không làm thay đổi tính đúng đắn của chương trình.
2. **Thực hành Sống Live OCP (3 phút tại bàn chấm)**:
   - **Nguyễn Đình Huy (TV1) thực hiện**: Thêm 1 chiến thuật AI mới `KiteThenAmbushAI` kế thừa `IEnemyAI`, biên dịch và cho quái chạy thật.
   - **Nguyễn Công Huy (TV2) thực hiện**: Thêm 1 loại bình thuốc mới `StaminaPotion` kế thừa `ConsumableItem`, đăng ký vào Shop bán thật.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương 4: Cài đặt Hướng đối tượng & Nguyên lý SOLID**: Trình bày kiến trúc phân tầng, bằng chứng 4 tính chất OOP, ma trận SOLID, kỹ thuật Object Pool và tối ưu hiệu năng deltaTime.

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Việc dựng tay từng AnimationClip từ hàng ngàn frame ảnh tốn quá nhiều thời gian gây trễ hạn Giai đoạn 2.
- **Biện pháp xử lý**: Bắt buộc hoàn thành công cụ Editor script `AtlasAnimationBuilder.cs` và adapter `ISpriteFrameProvider` tại công việc W4.10 ngay đầu Tuần 4. Công cụ này đọc trực tiếp `atlas.json` và frame coordinates từ 6 bộ asset để tự động sinh toàn bộ AnimationClip và Animator Override Controller chỉ bằng 1 cú click chuột trong 3 giây.
