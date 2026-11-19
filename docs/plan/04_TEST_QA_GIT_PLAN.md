# KẾ HOẠCH KIỂM THỬ, ĐẢM BẢO CHẤT LƯỢNG & QUY TRÌNH GIT (TEST, QA & GIT PLAN)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên thực hiện:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat, Physics & AI Tests)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead World, Items & Storage Tests)**  
**Quy chuẩn áp dụng:** Bước 05 PDF v2026 & Tiêu chí 1.4, 5.1, 5.2, 5.3 Checklist K69  
**Mục tiêu định lượng:** $\ge 24$ Unit Tests + $\ge 10$ Integration Tests = $\ge 34$ Automated Tests | $\ge 20$ Playtest | $\ge 8$ Bug Log | $\ge 40$ Git Commits  

---

## 1. MA TRẬN KIỂM THỬ TỰ ĐỘNG (AUTOMATED TEST MATRIX - 34 TESTS)

Toàn bộ kiểm thử tự động được viết trong Assembly `Game.Core.Tests` sử dụng NUnit (Unity Test Framework EditMode), chạy hoàn toàn **Headless (không cần mở Scene, không phụ thuộc đồ họa)** với thời gian thực thi toàn bộ suite $< 2$ giây.

### 1.1 Danh mục 24 Unit Tests Pure Logic (12 Nguyễn Đình Huy / 12 Nguyễn Công Huy)
Mỗi thành viên tự viết 12 Unit Tests cho phần mình, áp dụng chuẩn **AAA (Arrange - Act - Assert)** và nguyên tắc **F.I.R.S.T.**, độc lập hoàn toàn với code của bạn mình thông qua các lớp **Fake/Mock/Stub**:

| Mã TC | Phân hệ / Luật | Tên Phương thức Test | Dữ liệu đầu vào / Ca biên (EP/BVA/State) | Kết quả kỳ vọng (Assertion) | Thành viên & Lớp Mock sử dụng |
|:---:|---|---|---|---|:---:|
| **TC-01**| R-03: Máu | `Health_TakeDamage_Normal_ReducesHP` | MaxHP=100, dmg=30 | HP == 70, IsDead == false | **Đình Huy (TV1)** (Độc lập) |
| **TC-02**| R-03: Máu BVA | `Health_TakeDamage_Excessive_ClampsToZero` | HP=50, dmg=100 (Biên HP=0) | HP == 0, IsDead == true, 0 âm | **Đình Huy (TV1)** (Độc lập) |
| **TC-03**| R-03: Máu BVA | `Health_Heal_Excessive_ClampsToMax` | HP=80, Max=100, heal=50 | HP == 100, không tràn MaxHP | **Đình Huy (TV1)** (Độc lập) |
| **TC-04**| R-01: Giáp | `CombatResolver_CalculatesDamage_WithArmor` | RawDmg=25, Armor=10 | ActualDamage == 15 | **Đình Huy (TV1)** (Độc lập) |
| **TC-05**| R-01: Giáp BVA| `CombatResolver_ArmorExceedsDmg_ReturnsOne`| RawDmg=5, Armor=20 (Biên giáp) | ActualDamage == 1 (Sát thương tối thiểu) | **Đình Huy (TV1)** (Độc lập) |
| **TC-06**| R-04: Thể lực | `Player_Dash_ConsumesStamina_SetsIframes` | Stamina=30, DashCost=20 | Stamina == 10, IframesRemaining > 0 | **Đình Huy (TV1)** (Độc lập) |
| **TC-07**| R-04: Thể lực | `Player_Dash_InsufficientStamina_ReturnsFalse`| Stamina=5, DashCost=20 | Dash trả về false, Stamina giữ nguyên | **Đình Huy (TV1)** (Độc lập) |
| **TC-08**| R-02: i-frames| `Actor_TakeDamage_DuringIframes_Ignored` | IsInIframes=true, dmg=40 | HP không đổi, trả về 0 dmg | **Đình Huy (TV1)** (Độc lập) |
| **TC-09**| R-07: Va chạm | `Collision_SweptAABB_StopsAtWallBoundary` | Pos=1.5, Vel=5.0, WallPos=3.0 | HitTime $\in (0, 1)$, FinalPos $\le 2.99$ | **Đình Huy (TV1)** (Dùng `MockTileQuery` TV2) |
| **TC-10**| Thuật toán A* | `Pathfinder_TargetInsideWall_ReturnsNearestCell`| Start=(1,1), Goal=(3,3) là tường | Path != null, điểm cuối kế cận (3,3) | **Đình Huy (TV1)** (Dùng `MockTileQuery` TV2) |
| **TC-11**| FSM Guard | `Actor_Attack_WhenStunned_GuardBlocksAction`| CurrentState=StunnedState | Không chuyển AttackState, Guard chặn | **Đình Huy (TV1)** (Độc lập) |
| **TC-12**| R-06: Status | `BurnEffect_TicksOverTime_InflictsDamage` | Target HP=100, Burn 3s | Sau 3 tick dt=1.0s, Target HP == 85 | **Đình Huy (TV1)** (Độc lập) |
| **TC-13**| R-13: Kho đồ | `Inventory_AddItem_Normal_Success` | Capacity=16, thêm 1 potion | Count == 1, Item tồn tại trong túi | **Công Huy (TV2)** (Độc lập) |
| **TC-14**| R-13: BVA Túi | `Inventory_AddItem_WhenFull_ThrowsException` | Đã có 16 món, thêm món 17 | Ném `InventoryFullException`, count 16 | **Công Huy (TV2)** (Độc lập) |
| **TC-15**| Ví tiền BVA | `Wallet_SpendGold_Insufficient_ReturnsFalse`| Gold=50, Price=100 | Trả về false, Gold giữ nguyên 50 | **Công Huy (TV2)** (Độc lập) |
| **TC-16**| Ví tiền Invar| `Wallet_AddGold_Negative_ThrowsArgumentException`| Nạp -10 vàng (Biên số âm) | Ném ngoại lệ, bảo vệ bất biến ví | **Công Huy (TV2)** (Độc lập) |
| **TC-17**| R-11: Chìa khóa| `TreasureChest_Open_WithKey_ConsumesKeyAndUnlocks`| HasKey=true | ChestState == Open, KeyCount giảm 1 | **Công Huy (TV2)** (Dùng `FakeActor` TV1) |
| **TC-18**| Factory Method| `LootFactory_CreateLoot_ReturnsValidItem` | RoomType=Treasure | Item != null, Item.Rarity $\ge$ Rare | **Công Huy (TV2)** (Độc lập) |
| **TC-19**| Decorator | `WeaponEnchant_BurningDecorator_AddsExtraDmg`| BaseDmg=20, Burning adds 5 fire | TotalDamage == 25, DamageType=Fire | **Công Huy (TV2)** (Độc lập) |
| **TC-20**| Pity Loot | `LootRoller_FiveDryRuns_ForcesHealthPotion` | 5 phòng không có potion (DryRun=5)| Roll ra HealthPotion chắc chắn $100\%$ | **Công Huy (TV2)** (Dùng `DeterministicRandom`)|
| **TC-21**| Autotile blob| `Blob47Autotiler_IsolatedCell_ReturnsMaskZero`| 0 ô lân cận cùng loại | Mask == 0, TileIndex == `wall_single` | **Công Huy (TV2)** (Độc lập) |
| **TC-22**| Autotile blob| `Blob47Autotiler_Surrounded_ReturnsMask255` | Đủ 8 ô lân cận | Mask == 255, TileIndex == `wall_center`| **Công Huy (TV2)** (Độc lập) |
| **TC-23**| Save Storage | `InMemorySaveStorage_SaveAndLoad_DataMatches`| Save DTO vào Memory | Load ra khớp 100% dữ liệu gốc | **Công Huy (TV2)** (Độc lập) |
| **TC-24**| Map BFS | `DungeonGenerator_FixedSeed_GeneratesSolvableMap`| Seed=12345, 10 phòng | Start và Exit liên thông, BFS tìm thấy | **Công Huy (TV2)** (Độc lập) |

---

### 1.2 Danh mục 10 Integration / Regression Tests
Kiểm tra sự tích hợp mượt mà giữa hai phân hệ TV1 và TV2, bao gồm kịch bản phục hồi lỗi và khóa lỗi hồi quy từ Bug Log:

| Mã TC | Hạng mục Kiểm thử Tích hợp | Kịch bản Kiểm chứng Tích hợp | Kết quả Kỳ vọng |
|:---:|---|---|---|
| **INT-01**| Save/Load Round-Trip Toàn diện | Khởi tạo ván chơi, Player mất 35 HP, nhặt 2 vũ khí, quái E01 còn 20 HP $\rightarrow$ Ghi file JSON $\rightarrow$ Xóa trắng ván $\rightarrow$ Nạp lại từ JSON. | Toàn bộ vị trí, HP của TV1 và danh sách vật phẩm của TV2 khôi phục chính xác $100\%$. |
| **INT-02**| Tệp Save bị Hỏng hoặc Thiếu Trường | Cố ý sửa file `save_slot_1.json` xóa trường `"playerHp"` hoặc sửa giá trị thành chuỗi `"CORRUPTED"`. | `SaveManager` bắt được lỗi, ném `SaveCorruptedException`, không làm văng ứng dụng (0 crash). |
| **INT-03**| Tương tác Chém Độc Xuyên Phân hệ | Player dùng Vũ khí gắn `PoisonDecorator` (TV2) chém vào quái `Orc` (TV1). | Quái nhận sát thương vật lý và mang trạng thái `PoisonEffect` (TV1), rút máu sau mỗi tick. |
| **INT-04**| Phản ứng Nguyên tố Sét - Nước | Kỹ năng `LightningChain` (TV1) đánh trúng ô gạch `water` (TV2) nơi có 2 quái vật đứng trên. | Cả 2 quái vật đều bị giật điện 25 dmg và dính choáng 1s thông qua `ReactionResolver`. |
| **INT-05**| Hủy Thực thể không NullRef Observer | Tạo 10 quái vật đăng ký lắng nghe sự kiện trên `GameEventBus` $\rightarrow$ Đột ngột xóa 5 quái khỏi Scene $\rightarrow$ Phát sự kiện diện rộng. | Không có ngoại lệ `NullReferenceException` phát sinh; bộ bus tự lọc bỏ tham chiếu rác. |
| **INT-06**| Thắng - Thua Đồng Thời (Race Condition) | Đạn của người chơi và đạn của Boss trúng đích trong cùng 1 frame; cả 2 HP cùng về 0. | `RuleEngine` ưu tiên xử lý `GameOverState` hoặc `VictoryState` dứt khoát theo luật R-18, không kẹt FSM. |
| **INT-07**| Sinh Map Seed Tái Lập Tuyệt Đối | Khởi tạo `DungeonGenerator` với cùng `Seed = 9999` hai lần độc lập. | Bản đồ sinh ra giống nhau từng tọa độ phòng, vị trí bẫy và chủng loại quái. |
| **INT-08**| Regression: Lỗi Kẹt Cổng Sắt (Bug #1) | Quái vật bị quái khác đẩy văng ra ngoài biên phòng trước khi chết $\rightarrow$ `OnEnemyKilled` kích hoạt. | `RoomNode` vẫn đếm đúng số quái còn lại về 0 và mở cổng sắt (`Cleared`), không bị softlock. |
| **INT-09**| Regression: Mua đồ bị Trừ 2 Lần (Bug #3) | Người chơi click chuột liên tiếp 2 lần trong 1 frame vào nút mua bình máu ở Shop. | Chỉ trừ tiền 1 lần, nhận 1 bình máu; không bị âm tiền hay nhân bản đồ. |
| **INT-10**| Regression: Đạn Xuyên Góc Tường (Bug #5) | Bắn đạn chéo góc qua khe hẹp giữa 2 ô đá với vận tốc cao $v = 20$. | Thuật toán Swept AABB chặn đứng đạn ngay tại góc đá ngoài, không bị lọt vào phòng bên cạnh. |

---

## 2. BẢNG KỊCH BẢN CHƠI THỬ CHÉO (PLAYTEST MATRIX - 20 CA)

Cơ chế bắt buộc: **Nguyễn Đình Huy chơi thử để tìm lỗi của Nguyễn Công Huy và ngược lại**. Phân bổ thành 3 nhóm kịch bản: Chuẩn (Happy Path), Biên (Boundary) và Lỗi (Stress/Error):

| Nhóm Ca | STT | Kịch bản Thử nghiệm Thực tế | Thao tác Chi tiết | Người Chơi Thử | Người Sửa |
|:---:|:---:|---|---|:---:|:---:|
| **Chuẩn** | 1 | Hoàn thành trọn vẹn PCU 1 (Hầm Đá) | Đi từ Start Room $\rightarrow$ diệt 2 phòng quái $\rightarrow$ nhặt rương $\rightarrow$ xuống cầu thang. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Chuẩn** | 2 | Vòng lặp Mua sắm tại Cửa hàng | Nhặt 100 vàng từ quái $\rightarrow$ vào Shop $\rightarrow$ mua bình máu $\rightarrow$ uống hồi phục. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Chuẩn** | 3 | Chuỗi Menu $\rightarrow$ Chơi $\rightarrow$ Thắng/Thua $\rightarrow$ Replay | Chơi thua $\rightarrow$ bấm Replay $\rightarrow$ kiểm tra xem quái và máu có reset sạch sẽ. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Chuẩn** | 4 | Đổi vũ khí và thi triển combo kỹ năng | Đổi từ Kiếm sang Búa $\rightarrow$ dùng FrostNova $\rightarrow$ gõ đập vỡ băng (Shatter). | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Chuẩn** | 5 | Lưu và Tải ván chơi giữa trận | Đang đứng giữa phòng ngập nước $\rightarrow$ Pause $\rightarrow$ Save $\rightarrow$ Tắt game $\rightarrow$ Mở lại Load. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Chuẩn** | 6 | Hiến tế máu tại Altar | Dâng tế 20% MaxHP tại Bàn tế Thần $\rightarrow$ kiểm tra nhận Cổ vật và HP tối đa giảm. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Chuẩn** | 7 | Đánh bại Boss 1 (Orc Chieftain) | Vượt qua 3 pha của Boss $\rightarrow$ nhặt Mảnh Pha Lê 1 $\rightarrow$ qua tầng tiếp theo. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Biên** | 8 | Dash vào góc tường 90 độ | Bấm Dash liên tục khi đang ép sát vào góc tường nhọn. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Biên** | 9 | Túi đồ đầy nhặt thêm đồ | Đặt 16 món vào túi $\rightarrow$ cố tình nhặt thêm vũ khí rơi trên sàn. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Biên** | 10| Máu còn đúng 1 HP nhận sát thương | Căn máu về 1 HP $\rightarrow$ bước vào bẫy gai chu kỳ. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Biên** | 11| Nhặt chìa khóa vàng mở cửa bạc | Cầm chìa khóa cửa tầng đi mở cửa rương kho báu phụ. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Biên** | 12| Quái vật đuổi theo khi người chơi tàng hình| Dùng bình tàng hình khi quái đang chạy A* rượt đuổi. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Biên** | 13| Rơi xuống vực thẳm khi còn dưới 20% HP | HP còn 15% $\rightarrow$ bị quái húc văng xuống ô `pit`. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Biên** | 14| Đổi âm lượng về 0 (Mute) rồi thoát | Kéo cả 2 thanh Slider âm lượng về 0 $\rightarrow$ thoát mở lại. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Lỗi** | 15| Xóa file `profile.json` khi game đang chạy | Xóa tệp cấu hình trong thư mục data rồi bấm lưu kỷ lục điểm cao. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Lỗi** | 16| Bấm Esc liên hồi trong lúc chuyển cảnh | Bấm phím Esc liên tục khi đang trong màn hình mờ chuyển tầng (Fade). | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Lỗi** | 17| Bị đóng băng và dính độc đồng thời | Đứng trên bãi độc trong lúc bị quái Mage Frost bắn đóng băng. | **Công Huy (TV2)** | **Đình Huy (TV1)** |
| **Lỗi** | 18| Spam click chuột vào NPC thương nhân | Click 50 lần liên tục vào Merchant khi chưa mở xong UI. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Lỗi** | 19| Tải file save của phiên bản cũ | Đổi số phiên bản trong file JSON từ 2.0 về 1.0 rồi bấm Load. | **Đình Huy (TV1)** | **Công Huy (TV2)** |
| **Lỗi** | 20| Stress test 50 đạn bay cùng lúc | Dùng staff bắn liên thanh 50 viên đạn vào cụm quái trong phòng hẹp. | **Công Huy (TV2)** | **Đình Huy (TV1)** |

---

## 3. NHẬT KÝ LỖI (BUG LOG CHUẨN 6 CỘT - 8 LỖI THỰC TẾ)

Mỗi lỗi được phát hiện trong quá trình phát triển đều được ghi nhận đầy đủ, phân tích nguyên nhân gốc rễ và khóa bằng một Regression Test tự động:

| Mã Bug | Các Bước Tái Hiện Lỗi (Steps to Reproduce) | Nguyên Nhân Gốc Rễ (Root Cause) | Commit Sửa Lỗi | Regression Test Bổ Sung | Kết Quả Sau Sửa |
|:---:|---|---|---|---|:---:|
| **BUG-01**| 1. Vào phòng Combat.<br>2. Dùng kỹ năng đẩy quái sát mép cửa.<br>3. Quái chết nhưng cửa sắt không mở. | Bộ đếm `EnemiesRemaining` chỉ kiểm tra quái nằm trong bounds của phòng; quái văng ra ngoài không được trừ. | `git commit 4a8f1b2` | `INT-08` | Cửa mở mượt mà khi diệt hết quái. |
| **BUG-02**| 1. Người chơi chết (HP=0).<br>2. Nhanh tay bấm phím Dash.<br>3. Nhân vật vẫn lướt đi dù đã chết. | Trạng thái `DeadState` thiếu Guard chặn xử lý Input trong `ProcessInput()`. | `git commit 7c3e9a1` | `TC-11` | Chặn đứng hoàn toàn input khi chết. |
| **BUG-03**| 1. Mở Cửa hàng.<br>2. Double-click nhanh vào bình máu giá 50 vàng.<br>3. Tiền bị trừ 100 vàng, nhận 2 bình. | Nút UI không debounce; sự kiện `OnClick` phát sinh 2 lần trong cùng 1 frame. | `git commit b21d0f5` | `INT-09` | Thêm cờ khóa thao tác trong 0.2s. |
| **BUG-04**| 1. Mở file `save_slot_1.json`.<br>2. Xóa trắng trường inventory.<br>3. Bấm Load Game $\rightarrow$ Game bị văng. | `SaveManager` gọi `.Count` trên đối tượng `null` mà không kiểm tra fallback rỗng. | `git commit e89a4c3` | `INT-02` | Tự động gán danh sách rỗng, không văng. |
| **BUG-05**| 1. Bắn tên qua khe tường hẹp.<br>2. Tên xuyên qua góc vách đá sang phòng bên. | Kiểm tra va chạm rời rạc ở frame cuối thay vì kiểm tra quét khối swept volume. | `git commit 1f5d6e8` | `INT-10` | Đạn bị cản lại tại rìa tường đá. |
| **BUG-06**| 1. Bước vào vũng nước khi mang cháy.<br>2. Cháy tắt nhưng khói sinh ra liên tục 60 lần/s. | Điều kiện dập cháy trong `ReactionResolver` thiếu cờ xóa trạng thái `Burn` ngay frame đầu. | `git commit d93b7a4` | `INT-04` | Chỉ sinh 1 cụm khói duy nhất. |
| **BUG-07**| 1. Chơi ván 1 thua.<br>2. Bấm Chơi Lại (Replay).<br>3. Quái cũ vẫn còn nguyên vị trí cũ. | `GameSession.Reset()` quên gọi dọn dẹp các đối tượng trong `ObjectPool`. | `git commit 3c8e2f1` | `INT-05` | Thu hồi toàn bộ quái về pool sạch sẽ. |
| **BUG-08**| 1. Quái vật bị tường chặn kín.<br>2. Game bị khựng khung hình 2 giây. | Thuật toán A* duyệt đệ quy toàn bộ bản đồ khi không tìm thấy đường tới đích. | `git commit 9a2f4d7` | `TC-10` | Thêm giới hạn bước duyệt tối đa 200 node. |

---

## 4. CHIẾN LƯỢC KIỂM THỬ ĐỘT BIẾN TỰ LÀM (MUTATION TESTING STRATEGY)

Để đảm bảo bộ test tự động không mang tính hình thức (chống Bẫy 4 K69), nhóm thực hiện quy trình Mutation Check nội bộ vào cuối mỗi tuần:

```
[Mã nguồn Sản phẩm] ──(Cố ý sửa sai 1 toán tử)──> [Mã nguồn Đột biến (Mutant)]
                                                              │
                                                     (Chạy NUnit Test Runner)
                                                              │
                ┌─────────────────────────────────────────────┴─────────────────────────────────────────────┐
                ▼                                                                                           ▼
      [KẾT QUẢ: TEST CHUYỂN ĐỎ (FAIL)]                                                            [KẾT QUẢ: TEST VẪN XANH (PASS)]
       => ĐỘT BIẾN BỊ TIÊU DIỆT (MUTANT KILLED)                                                    => ĐỘT BIẾN SỐNG SÓT (MUTANT SURVIVED)
       => Bộ Test đạt chuẩn chất lượng thực chất.                                                  => Báo động: Test hình thức, thiếu Assert!
                                                                                                  => BẮT BUỘC VIẾT THÊM TEST BẢO VỆ.
```

### 3 Điểm Đột biến Tiêu biểu Được Thử nghiệm:
1. **Đột biến 1 (Biên Máu tại `Health.cs`)**:
   - Sửa: `if (CurrentHP <= 0)` thành `if (CurrentHP < 0)`.
   - Kết quả: Ca test `TC-02 (Health_TakeDamage_Excessive_ClampsToZero)` chuyển **FAIL đỏ** ngay lập tức vì khi HP rơi đúng về 0 thì trạng thái tử trận không được kích hoạt. $\rightarrow$ **Đạt**.
2. **Đột biến 2 (Va chạm Swept AABB tại `CollisionDetector.cs`)**:
   - Sửa: `if (hitTime >= 0.0f && hitTime <= 1.0f)` thành `if (hitTime > 0.0f && hitTime < 1.0f)`.
   - Kết quả: Ca test `TC-09` chuyển **FAIL đỏ** ở ca biên chạm sát mép tường ($t = 1.0$). $\rightarrow$ **Đạt**.
3. **Đột biến 3 (Lưu trữ DTO tại `SaveManager.cs`)**:
   - Chú thích (comment out) dòng lưu thuộc tính `"inventory"`.
   - Kết quả: Ca test tích hợp `INT-01` chuyển **FAIL đỏ** vì sau khi nạp lại số lượng vật phẩm bị sai lệch. $\rightarrow$ **Đạt**.

---

## 5. QUY CHUẨN QUẢN LÝ MÃ NGUỒN GIT & MA TRẬN REVIEW CHÉO

Dự án cam kết lịch sử Git minh bạch, thực chất, phản ánh đúng quá trình lao động kỹ thuật của cả hai thành viên suốt 5 tuần:

```
main (Bảo vệ, chỉ nhận merge từ dev qua PR)
 ▲
 └── dev (Nhánh tích hợp chính)
      ▲                               ▲
      ├── feat/tv1-combat (Đình Huy)  ├── feat/tv2-world (Công Huy)
      ├── feat/tv1-ai     (Đình Huy)  ├── feat/tv2-inventory (Công Huy)
      └── feat/tv1-physics(Đình Huy)  └── feat/tv2-save (Công Huy)
```

### 5.1 Các Ràng buộc Git Tuyệt đối (Tiêu chí 1.4 Checklist):
- **Số lượng Commits**: Tối thiểu 40 commits logic (không tính commit sửa docs hay typo); rải đều qua ít nhất **3 tuần liên tục** (từ Tuần 1 đến Tuần 5).
- **Phân bổ Tỷ lệ Đóng góp (Git Blame)**: Nguyễn Đình Huy và Nguyễn Công Huy mỗi người đóng góp từ **40% đến 60%** tổng số dòng mã nguồn logic (kiểm toán qua lệnh `git shortlog -sn --no-merges` và `git blame` trên thư mục `src/Game.Core/`).
- **Khóa Chặn Rửa Commit Dồn Cuối**: Tuyệt đối không dồn quá $60\%$ số lượng commit vào 48 giờ cuối trước ngày bảo vệ. Các commit tài liệu, khung rỗng và test cơ sở được đẩy ngay từ Tuần 1.
- **Tệp `.gitignore` Chuẩn mực**: Loại bỏ hoàn toàn các thư mục rác build nặng của Unity và Visual Studio: `Library/`, `Temp/`, `Obj/`, `Build/`, `Builds/`, `Logs/`, `UserSettings/`, `*.csproj`, `*.sln.DotSettings`.

### 5.2 Mẫu Thông điệp Commit Chuẩn (Commit Message Convention):
Mọi commit trên kho Git bắt buộc phải có tiền tố tên thành viên trong ngoặc vuông kèm dấu hai chấm theo đúng quy ước:
`[<Tên Thành Viên>]: <loại>(<phạm vi>): <mô tả ngắn bằng tiếng Anh>`

**Quy tắc tiền tố:**
- **Nguyễn Đình Huy (TV1)**: Bắt buộc dùng `[Đình Huy]:` ở đầu thông điệp commit.
- **Nguyễn Công Huy (TV2)**: Bắt buộc dùng `[Công Huy]:` ở đầu thông điệp commit.
- **Các commit chung (khởi tạo repo, merge release, cập nhật tài liệu chung)**: Dùng `[Cả hai]:` ở đầu thông điệp commit.

**Cấu trúc chi tiết:**
`[Đình Huy / Công Huy / Cả hai]: <type>(<scope>): <subject>`

**Trong đó `<type>` tuân thủ chuẩn Conventional Commits:**
- `feat`: Thêm tính năng logic mới (entities, AI, skills, world generation, UI...).
- `test`: Viết thêm hoặc sửa đổi unit/integration/regression test.
- `refactor`: Tái cấu trúc mã nguồn (áp dụng GoF pattern, tách interface, tối ưu SRP) mà không đổi hành vi.
- `fix`: Sửa lỗi logic hoặc bug ghi nhận trong Bug Log (kèm mã `Bug-xx`).
- `docs`: Cập nhật tài liệu kỹ thuật, ma trận truy vết, UML hoặc báo cáo.
- `perf`: Tối ưu hiệu năng (Object Pool, Spatial Hash, giảm GC Alloc).
- `chore`: Cấu hình Git, build scripts, assembly definition (`.asmdef`), `.gitignore`.

**Ví dụ thực tế kiểm chứng:**
- **Ví dụ Đình Huy**:
  - `[Đình Huy]: feat(combat): implement swept AABB collision detection to prevent tunneling`
  - `[Đình Huy]: feat(ai): add RangedKiteAI with Bresenham line of sight check`
  - `[Đình Huy]: test(combat): add unit tests for element reaction damage multiplier`
  - `[Đình Huy]: fix(physics): resolve corner-sticking bug during rapid diagonal movement (Bug-03)`
- **Ví dụ Công Huy**:
  - `[Công Huy]: feat(world): implement BSP dungeon split and MST corridor connector`
  - `[Công Huy]: feat(items): implement weapon stats enhancement using Decorator pattern`
  - `[Công Huy]: test(storage): add integration round-trip test for full game state serialization`
  - `[Công Huy]: fix(autotile): correct bitmask calculation for blob47 corner wall tiles (Bug-01)`
- **Ví dụ commit chung**:
  - `[Cả hai]: chore(repo): initialize project structure and configure .gitignore and asmdef`
  - `[Cả hai]: docs(plan): finalize master plan and module decoupling guide`

### 5.3 Quy tắc Review Chéo (Cross Peer-Review):
1. **Nguyễn Đình Huy (TV1)** tạo PR từ nhánh `feat/tv1-*` vào `dev` $\rightarrow$ **Chỉ định Nguyễn Công Huy (TV2) làm Reviewer bắt buộc**.
2. **Nguyễn Công Huy** kiểm tra: (1) Mã nguồn tuân thủ Clean Code; (2) Không dính `UnityEngine` trong Core; (3) Có đầy đủ Unit Test chạy xanh 100%.
3. **Nguyễn Công Huy** phê duyệt (Approve) và thực hiện Merge. Ngược lại, mọi PR của Nguyễn Công Huy bắt buộc do Nguyễn Đình Huy review. Quá trình này vừa tạo lịch sử thảo luận thật trên GitHub, vừa giúp hai thành viên nắm vững toàn bộ kiến trúc để tự tin vấn đáp chéo.
