# KINH THÁNH NỘI DUNG & ĐẶC TẢ GAMEPLAY (CONTENT BIBLE)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat, Physics & AI)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead World, Items & Storage)**  
**Thể loại:** Roguelike Dungeon Crawler Top-Down 2D (Thời gian thực)  
**Tài nguyên nền tảng:** Cole Foster Voxel Dungeon Pack Full (CC0) kết hợp 5 gói bổ trợ (Raven Fantasy Icons, Tiny RPG, Tiny Swords, 2D Pixel Dungeon v2, Free Characters)  
**Quy chuẩn tương ứng:** Bước 01 PDF v2026 & Tiêu chí 1.1, 1.3, 3.1 Checklist K69  

---

## 1. BỐI CẢNH CỐT TRUYỆN & CƠ CHẾ KỂ CHUYỆN MÔI TRƯỜNG (ENVIRONMENTAL STORYTELLING)

### 1.1 Cốt truyện chính (Lore)
Vương quốc trên mặt đất duy trì được sự thịnh vượng và nguồn sinh mệnh trường tồn suốt ngàn năm nhờ vào **Trái Tim Pha Lê (Crystal Heart)** ngự trị trên Bàn tế cổ xưa (`altar`) sâu dưới lòng hầm ngục tăm tối. Một ngày nọ, Đại kỵ sĩ thủ hộ tối cao bị tha hóa bởi tà khí hư không, trở thành **Hắc Kỵ Sĩ (Dark Knight)** (`dark_knight`). Hắn đã vung đại kiếm đập vỡ Trái Tim Pha Lê thành 3 mảnh nguyên vẹn và hàng ngàn mảnh vụn nhỏ, làm cho kết giới phong ấn sụp đổ, ma khí ngập tràn, quái vật trỗi dậy chiếm cứ các tầng hầm.

Người chơi xuất phát từ bề mặt, chọn nhập vai **Hiệp Sĩ Quả Cảm (Knight)** (`knight`) hoặc **Pháp Sư Bí Thuật (Mage)** (`mage`). Nhiệm vụ duy nhất: tiến sâu qua các tầng hiểm địa, tiêu diệt 3 đại thủ lĩnh canh giữ các mảnh vỡ (Thủ lĩnh Orc, Golem Dung Nham và chính Hắc Kỵ Sĩ), thu hồi 3 Mảnh Pha Lê để kích hoạt lại Bàn Tế Thần, cứu rỗi thế giới khỏi thảm họa diệt vong. Khi chiến thắng trọn vẹn run chơi, người chơi sẽ giải thoát linh hồn cho Hắc Kỵ Sĩ và mở khóa ngoại trang vĩnh viễn **Hắc Kỵ Sĩ Hoàn Lương** cho các lần chơi sau.

### 1.2 Kể chuyện qua Props (Environmental Storytelling)
Trò chơi không dùng các đoạn cắt cảnh dài dòng làm đứt gãy nhịp chơi, mà áp dụng phương pháp kể chuyện qua cảnh vật trực quan:
- `crystal_deco`: Các mảnh pha lê vỡ găm rải rác trên vách đá phát ra ánh sáng tím/xanh nhấp nháy, dẫn đường cho người chơi tới các phòng bí mật.
- `altar`: Bàn tế đá cổ kính là nơi người chơi có thể dâng tế một phần Máu tối đa (`MaxHP`) để đổi lấy các Cổ vật (`Relic`) hùng mạnh.
- `bookshelf`: Các kệ sách cổ rải rác trong phòng thủ thư, khi tương tác sẽ hiện các dòng ghi chép ngắn về điểm yếu của quái vật hoặc công thức phản ứng nguyên tố.
- `weaponrack` & `bones`: Bộ xương chiến binh và giá vũ khí rỉ sét đánh dấu những căn phòng từng xảy ra giao tranh ác liệt; nhặt được các vũ khí ngẫu nhiên hoặc trang bị hỏng.
- `portcullis` & `door_iron`: Cổng sắt hạ xuống khóa chặt người chơi trong phòng Combat khi có quái vật xuất hiện; chỉ nâng lên khi toàn bộ quái bị tiêu diệt (`OnRoomCleared`).

---

## 2. CẤU TRÚC CHI TIẾT 10 MÀN CHƠI / CHẾ ĐỘ (PCU - PLAYABLE CONTENT UNITS)

Mỗi PCU là một màn chơi hoặc chế độ có cơ chế cơ học (mechanic) riêng biệt, sử dụng bộ tileset và loại quái vật đặc thù từ gói tài nguyên có sẵn:

| # | Tên PCU | Bộ Tileset chủ đạo | Danh sách Quái vật đặc trưng | Cơ chế Cơ học riêng biệt bắt buộc |
|:---:|---|---|---|---|
| **1** | **Hầm Đá Cổ**<br>*(Ancient Crypt)* | `floor_stone`, `wall_top`, `wall_face` | `rat`, `slime_green`, `skeleton`, `goblin` | **Bẫy gai & Mimic**: Ô gai sàn (`trap_spikes`) chu kỳ 2s thò ra thụt vào gây sát thương; rương giả (`mimic`) trà trộn giữa rương báu. |
| **2** | **Hang Nấm Ẩm**<br>*(Fungal Cavern)* | `floor_moss`, `shadow_wall` | `mushroom_red`, `mushroom_blue`, `mushroom_purple`, `bat`, `spider` | **Tầm nhìn tối & Độc khí**: Tầm nhìn bị bóp hẹp còn bán kính 4 ô; các đám mây độc (`poison_cloud`) rút máu theo thời gian; quái nấm nổ tung khi chết. |
| **3** | **Hầm Ngập Nước**<br>*(Sunken Vault)* | `water_m031`..`m255`, `water_caustic` | `slime_blue`, `eye`, `scorpion`, `golem` (elite) | **Thủy điện dẫn truyền**: Ô nước làm giảm 25% tốc chạy; tia sét (`LightningChain`) khi đánh trúng nước sẽ truyền điện gây choáng toàn bộ quái đứng trong cùng vũng nước. |
| **4** | **Sảnh Boss Orc**<br>*(Orc Warlord Hall)* | Arena gạch hoa cương, `brazier` | **Boss: `orc_chieftain`**, lính triệu hồi: `orc`, `hobgoblin` | **Boss 3 Pha**: Pha 1 chém thường + húc; Pha 2 triệu hồi lính từ 2 cổng phụ; Pha 3 cuồng nộ tăng 50% tốc đánh và phóng rìu xoay. |
| **5** | **Lò Rèn Dung Nham**<br>*(Magma Forge)* | `lava_m031`..`m255`, đá núi lửa | `slime_red`, `scorpion_red`, `hobgoblin` | **Hỏa ngục & Dập cháy**: Bước lên ô dung nham dính trạng thái Cháy (`Burn`); nhặt bình nước hoặc kỹ năng băng có thể làm nguội dung nham thành đá dẫm được trong 5s. |
| **6** | **Vực Thẳm Tăm Tối**<br>*(Abyssal Chasm)* | `pit_m031`..`m255`, đá nứt | `ghost`, `bat`, `mage_frost`, `slime_purple` | **Rơi hố & Xuyên tường**: Ô hố sâu (`pit`) nếu bị đẩy ngã vào sẽ mất 30% HP và rớt xuống phòng phụ; quái bóng ma (`ghost`) bay xuyên mọi bức tường đá. |
| **7** | **Xưởng Boss Golem**<br>*(Golem Foundry)* | Arena đá rèn lửa, lò đúc | **Boss: `golem_magma`**, `slime_red` | **Vùng báo trước AoE (Telegraph Slam)**: Boss nạp đòn 1.2s hiện vòng đỏ cảnh báo trên sàn (`Telegraph`) trước khi dậm động đất làm rung camera và nứt sàn. |
| **8** | **Cung Điện Hắc Kỵ Sĩ**<br>*(Sanctum of the Fallen)* | Arena hoàng gia hoang tàn, `altar` vỡ | **Boss cuối: `dark_knight`**, `skeleton`, `ghost` | **Phản đòn & Ma kiếm**: Boss có khiên phản phép; sử dụng đầy đủ các kỹ năng Dash chém kiếm khí và dịch chuyển tức thời; dâng 3 mảnh pha lê lên Altar để phá giáp bất tử của Boss. |
| **9** | **Chế độ Endless Descent**<br>*(Leo Tháp Vô Tận)* | Trộn ngẫu nhiên 4 biome qua từng tầng | Sinh ngẫu nhiên toàn bộ quái và elite | **Độ khó hàm mũ**: Máu và sát thương quái tăng $15\%$ mỗi tầng; bảng điểm tính bằng: $\text{Tầng} \times 1000 + \text{Vàng} \times 10 + \text{Quái giết} \times 50$. |
| **10**| **Chế độ Boss Rush**<br>*(Thách Đấu Trùm)* | 3 Đấu trường liên tiếp | 3 Đại Boss theo thứ tự: Orc $\rightarrow$ Golem $\rightarrow$ Hắc Kỵ Sĩ | **Seed cố định hàng ngày (Daily Seed)**: Không nhặt đồ rải rác; người chơi nhận ngân sách 500 vàng mua đồ trước trận; so tài thời gian hạ gục Boss trên Leaderboard. |

---

## 3. CÁC LOẠI PHÒNG (ROOM TYPES) TRONG HẦM NGỤC

Mỗi tầng hầm ngục (Floor) là một đồ thị liên thông gồm các phòng được kết nối bởi các hành lang (`Corridor`):
1. **Start Room**: Phòng khởi đầu an toàn, chứa đuốc chiếu sáng, không có quái vật.
2. **Combat Room**: Phòng giao tranh tiêu chuẩn; khi người chơi bước vào, cửa sắt hạ xuống (`RoomState.Active`), xuất hiện 1–2 đợt quái; diệt hết quái thì cửa mở (`RoomState.Cleared`).
3. **Treasure Room**: Phòng kho báu khóa bằng cổng sắt; yêu cầu 1 Chìa khóa (`Key`) để mở; chứa Rương vàng (`chest`) nhặt đồ hiếm hoặc vũ khí nâng cấp.
4. **Shop Room**: Phòng thương nhân thân thiện (`merchant`), bán bình máu, mana, trang bị ngẫu nhiên và bùa chú đổi lấy Vàng nhặt được từ quái.
5. **Altar Room**: Bàn tế thần bí (`altar` phát sáng); cho phép hiến tế $20\%$ Máu tối đa vĩnh viễn trong run để nhận 1 Cổ vật Thần thoại (`Relic`).
6. **Trap Room**: Phòng giải đố né bẫy; bố trí dày đặc bẫy gai chu kỳ, máy bắn phi tiêu trên tường; vượt qua được sẽ nhận rương gỗ thưởng.
7. **Boss Room**: Đấu trường rộng lớn không có chướng ngại vật cản tầm nhìn; cửa khóa niêm phong đến khi Boss bị hạ gục.
8. **Exit Room**: Chứa cầu thang đi xuống (`stairs_down`); chỉ mở khóa khi người chơi đã tiêu diệt quái tinh anh hoặc lấy được chìa khóa tầng.

---

## 4. HỆ THỐNG TRÍ TUỆ NHÂN TẠO KẺ ĐỊCH (7 KIỂU AI KHÁC BIỆT)

Không phân biệt quái vật bằng việc đổi màu hay nhân hệ số máu. Toàn bộ 7 kiểu AI được hiện thực hóa qua các lớp chiến thuật độc lập cài đặt interface `IEnemyAI`:

```
               ┌────────────────┐
               │   IEnemyAI     │
               └───────┬────────┘
     ┌──────────┬──────┴───┬──────────┬──────────┬──────────┬──────────┐
     ▼          ▼          ▼          ▼          ▼          ▼          ▼
 [ChaseAI]  [PatrolAI] [KiteAI]   [FlitAI]   [AmbushAI] [ChargeAI] [SlamAI]
  (A* đuổi) (Tuần tra) (Giữ cự ly) (Lao-Rút) (Rương giả) (Húc nhanh) (Dậm AoE)
```

1. **ChaseAI (A* Pursuer)**:
   - *Áp dụng*: `slime_green`, `orc`, `hobgoblin`.
   - *Hành vi*: Tính toán đường đi ngắn nhất tới người chơi bằng thuật toán A* trên lưới ô; cập nhật lộ trình mỗi 0.5s; nếu người chơi bị che khuất thì đi tới vị trí nhìn thấy gần nhất.
2. **PatrolThenChaseAI (Lính gác tuần tra)**:
   - *Áp dụng*: `skeleton`, `goblin`.
   - *Hành vi*: Tuần tra qua lại giữa 2–3 điểm nhớ (`Waypoints`); dùng thuật toán Bresenham Line-of-Sight để quét góc nhìn $90^\circ$; khi phát hiện người chơi chuyển ngay sang trạng thái Alert $\rightarrow$ Chase.
3. **RangedKiteAI (Xạ thủ giữ cự ly)**:
   - *Áp dụng*: `mage_frost`, `spider`, `eye`.
   - *Hành vi*: Kiểm tra khoảng cách $d$ tới người chơi. Nếu $d < 4$ ô: lùi ngược hướng người chơi; nếu $4 \le d \le 7$ ô: đứng bắn đạn băng (`frost_shard`) hoặc đạn độc (`venom_spit`); nếu $d > 7$: từ từ tiến lại gần.
4. **FlitAI (Bóng ma bay lượn)**:
   - *Áp dụng*: `bat`, `ghost`.
   - *Hành vi*: Bay lượn theo quỹ đạo hình sin ziczac; lao bổ vào người chơi tấn công chớp nhoáng rồi lập tức rút lùi ra xa 3 ô chờ hồi chiêu 2s; riêng `ghost` bỏ qua va chạm với tile tường.
5. **AmbushAI (Quái rương mai phục)**:
   - *Áp dụng*: `mimic`, `mimic_gold`.
   - *Hành vi*: Ở trạng thái ngủ hoàn toàn, hiển thị sprite chiếc rương báu tĩnh (`chest`); khi người chơi tiến lại gần $<1.5$ ô hoặc bấm phím `E` để mở, lập tức há miệng cắn bất ngờ gây sát thương chí mạng và chuyển sang đuổi đánh.
6. **ChargeAI (Quái húc xung lực)**:
   - *Áp dụng*: `scorpion`, `scorpion_red`.
   - *Hành vi*: Khi có đường thẳng nhìn thấy người chơi trong cự ly 6 ô: dừng lại 0.6s giậm chân cảnh báo, sau đó phóng vút thẳng với tốc độ gấp $300\%$ bình thường; chỉ dừng lại khi đâm trúng người chơi hoặc va vào tường gây choáng bản thân 1s.
7. **SlamAI & Boss FSM (Đại lực sĩ dậm đất)**:
   - *Áp dụng*: `golem`, `golem_magma`, `orc_chieftain`.
   - *Hành vi*: Tiếp cận người chơi trong bán kính 2 ô; dừng lại giơ cao tay trong 1.0s đồng thời kích hoạt vòng báo động đỏ trên sàn; sau đó dậm mạnh gây sát thương diện rộng và hất văng người chơi ra xa 2 ô.

---

## 5. NGƯỜI CHƠI, VŨ KHÍ, VẬT PHẨM & KỸ NĂNG

### 5.1 Lớp Nhân vật (Classes)
- **Hiệp Sĩ (Knight)**: Máu cơ bản 120, Thể lực 100, Tốc độ 4.0 ô/s. Vũ khí khởi đầu: Kiếm ngắn (`dagger`) và Khiên gỗ. Sở trường: Chống chịu, chém cận chiến quét góc $90^\circ$.
- **Pháp Sư (Mage)**: Máu cơ bản 80, Mana 120, Tốc độ 4.2 ô/s. Vũ khí khởi đầu: Gậy phép (`staff`). Sở trường: Tấn công tầm xa, phóng đạn nguyên tố đa mục tiêu.
- **Hắc Kỵ Sĩ (Dark Knight - Mở khóa)**: Máu 100, Mana 100. Kỹ năng nội tại: Hút máu $5\%$ sát thương gây ra.

### 5.2 Hệ thống Vũ khí & Chiến thuật Tấn công (`IAttackPattern`)
Mỗi loại vũ khí gắn liền với một thuật toán tấn công riêng biệt, không dùng chung logic:
- `dagger`: Tấn công cực nhanh (0.2s/đòn), tầm đánh hẹp 1 ô, tỷ lệ chí mạng $+20\%$.
- `mace` / `warhammer`: Tốc độ chậm (0.8s/đòn), vung hình quạt $120^\circ$ trước mặt, gây đẩy lùi và làm choáng kẻ địch 0.5s.
- `spear`: Đâm thẳng tắp theo trục hướng mặt cự ly 2.5 ô, gây sát thương xuyên qua tối đa 3 kẻ địch trên một đường thẳng.
- `bow`: Bắn ra đạn mũi tên vật lý (`arrow`), tốc độ bay nhanh, chịu ảnh hưởng của va chạm AABB.
- `staff` / `staff_fire`: Bắn ra các quả cầu năng lượng phép thuật, gây nổ hoặc áp trạng thái nguyên tố khi va chạm.

### 5.3 Trang bị & Tiêu hao (Equipment & Consumables)
*(Tận dụng tối đa bộ thư viện phong phú hơn 6,500 icon $32 \times 32$ từ gói bổ trợ Raven Fantasy Icons khớp $1:1$ với slot UI):*
- **Giáp & Giày**: `helmet` (giảm 10% dmg nhận), `chestplate` (giảm 20% dmg), `boots` (tăng 20% tốc độ di chuyển).
- **Trang sức Cổ vật (`Relic`)**:
  - `ring_ruby`: Đòn đánh có $25\%$ cơ hội áp trạng thái Thiêu đốt (`Burn`).
  - `ring_silver`: Tăng tốc độ hồi Mana thêm $+50\%$.
  - `amulet_emerald`: Miễn nhiễm sát thương độc từ vũng độc và quái nấm.
  - `amulet_ruby`: Hồi phục 2 HP mỗi khi tiêu diệt 1 quái vật.
- **Vật phẩm Tiêu hao (`Consumables`)**:
  - `potion_red`: Hồi ngay lập tức 50 HP.
  - `potion_blue`: Hồi ngay lập tức 50 Mana.
  - `potion_green`: Giải toàn bộ trạng thái bất lợi (Độc, Chậm, Cháy) và ban miễn nhiễm trong 5s.
  - `potion_yellow`: Tăng 50% tốc độ di chuyển trong 8s.
  - `bread` / `meat` / `apple`: Hồi phục lượng nhỏ HP nhưng áp hiệu ứng Hồi phục dần (`Regen`) trong 6s.
  - `scroll`: Dịch chuyển tức thời người chơi về phòng bắt đầu của tầng hiện tại.
  - `book_red` / `book_blue` / `book_purple`: Sách cổ học kỹ năng mới vĩnh viễn trong run.

### 5.4 Hệ thống 5 Kỹ năng Chủ động (Active Skills)
1. **Fireball (Cầu Lửa)**: Tiêu hao 25 Mana. Phóng ra quả cầu lửa nổ tung khi va chạm trong bán kính 1.5 ô, gây 40 sát thương hỏa và áp hiệu ứng Thiêu đốt (`Burn`) trong 3s.
2. **FrostNova (Băng Sương)**: Tiêu hao 30 Mana. Phát nổ xung quanh người chơi bán kính 3 ô, gây 25 sát thương băng và đóng băng (`Freeze`) toàn bộ kẻ địch trong 2s.
3. **LightningChain (Tia Sét Liên Hoàn)**: Tiêu hao 35 Mana. Phóng tia sét giật mục tiêu đầu tiên gây 30 sát thương, sau đó nảy sang tối đa 3 kẻ địch kế cận trong phạm vi 3 ô.
4. **Dash (Lướt Né Gió)**: Tiêu hao 20 Thể lực, hồi chiêu 1.5s. Lướt nhanh 3.5 ô theo hướng di chuyển, nhận 0.3s khung bất tử (i-frames), để lại làn khói bụi (`dust_puff`).
5. **ShieldBubble (Khiên Năng Lượng)**: Tiêu hao 40 Mana, hồi chiêu 12s. Tạo lớp bong bóng bảo hộ hấp thụ 50 sát thương trong 6s; khi vỡ phát nổ đẩy lùi quái vật xung quanh.

---

## 6. MA TRẬN PHẢN ỨNG NGUYÊN TỐ LIÊN PHÂN HỆ (ELEMENTAL REACTIONS)

Được cấu hình hoàn toàn trong tệp dữ liệu ngoài `Assets/StreamingAssets/Data/reactions.json`. Đây là minh chứng cốt lõi cho sự kết hợp logic sâu sắc giữa hệ thống Chiến đấu của TV1 và hệ thống Bản đồ/Môi trường của TV2:

```
[Hiệu ứng TV1 / Kỹ năng] ──(Tương tác)──> [Địa hình TV2 / Trạng thái khác] ──> [Phản ứng Combo Mới]
```

| # | Tác nhân A (Trạng thái/Skill) | Tác nhân B (Môi trường/Vũ khí) | Tên Phản ứng Nguyên tố | Hiệu ứng Cơ học phát sinh | File FX & Âm thanh phát sinh |
|:---:|---|---|---|---|---|
| **R1**| Trạng thái Cháy (`Burn`) | Ô gạch Nước (`water`) | **Dập Hỏa (Extinguish)** | Dập tắt ngay trạng thái Cháy; bốc lên một làn khói trắng xóa trong 2s che mắt quái vật. | FX: `smoke.png`<br>SFX: `sfx_hiss.wav` |
| **R2**| Kỹ năng Sét (`LightningChain`) | Ô gạch Nước (`water`) | **Thủy Điện Kích (Electrocute)**| Tia sét truyền qua toàn bộ cụm ô nước kề cận; mọi thực thể đứng trên nước nhận 25 dmg lôi và bị choáng 1s. | FX: `lightning.png`<br>SFX: `sfx_zap.wav` |
| **R3**| Trạng thái Đóng Băng (`Freeze`) | Đòn Búa tạ (`warhammer`) | **Băng Toái (Shatter)** | Kẻ địch đang bị đóng băng nhận $250\%$ sát thương chí mạng từ đòn búa và vỡ vụn ra thành các mảnh băng văng xung quanh. | FX: `frost_shard.png`<br>SFX: `sfx_ice_break.wav` |
| **R4**| Đám mây Khí Độc (`PoisonCloud`)| Đạn Cầu Lửa (`Fireball`) | **Độc Hỏa Nổ (Combustion)** | Đám mây độc bị bén lửa phát nổ diện rộng bán kính 2.5 ô, gây 60 sát thương nổ cho cả người chơi lẫn quái. | FX: `explosion.png`<br>SFX: `sfx_boom.wav` |
| **R5**| Kỹ năng Băng (`FrostNova`) | Ô gạch Dung Nham (`lava`) | **Hóa Đá Dung Nham (Solidify)**| Ô dung nham bị làm lạnh biến thành sàn đá basalt nguội đi được trong 5s; bốc hơi nước che tầm nhìn. | FX: `smoke.png`<br>SFX: `sfx_steam.wav` |

---

## 7. BẢNG 10 USE CASE CỐT LÕI (CHIA ĐỀU NGUYỄN ĐÌNH HUY – NGUYỄN CÔNG HUY)

| Mã UC | Tên Use Case | Phân hệ chính | Scene / Màn hình hiển thị | Lớp Điều phối (Controller) | Thành viên Sở hữu | Luồng tương tác liên phân hệ |
|:---:|---|:---:|---|---|:---:|---|
| **UC01**| Chơi mới & Chọn lớp nhân vật | Meta & Flow | `Scene_MainMenu` | `GameBootstrap`, `CharacterSelectView` | **Nguyễn Công Huy (TV2)** | Khởi tạo cấu hình nhân vật chuyển sang Core TV1. |
| **UC02**| Khám phá, di chuyển & vào/ra phòng | World & Move | `Scene_Gameplay` | `MovementSystem`, `RoomController` | **Cả hai (Đình Huy & Công Huy)** | TV1 di chuyển Actor; TV2 phát hiện đổi phòng `OnRoomEntered`. |
| **UC03**| Chiến đấu: Đánh thường, Skill, Né đòn | Combat Core | `Scene_Gameplay` | `CombatResolver`, `SkillExecutor` | **Nguyễn Đình Huy (TV1)** | Tính sát thương, va chạm, i-frames, gửi DTO cho HUD. |
| **UC04**| Nhặt, dùng & trang bị vật phẩm | Inventory | `Frm_InventoryOverlay` | `InventoryManager`, `EquipmentSystem` | **Nguyễn Công Huy (TV2)** | Thay đổi chỉ số nhân vật trong Core của TV1. |
| **UC05**| Mở rương, Mua bán Shop & Đổi Altar | Meta & Economy | `Frm_ShopOverlay`, `Frm_Altar` | `ShopService`, `LootDirector` | **Nguyễn Công Huy (TV2)** | Trừ tiền, sinh vật phẩm qua `ILootFactory` đưa vào túi. |
| **UC06**| Tạm dừng (Pause) & Cài đặt âm thanh/phím | System & UI | `Frm_PauseMenu`, `Frm_Settings` | `ScreenManager`, `AudioDirector` | **Nguyễn Công Huy (TV2)** | Đóng băng Game Loop TV1; lưu cấu hình âm lượng bền vững. |
| **UC07**| Lưu / Tải ván chơi bền vững | Persistence | `Frm_SaveLoadSlots` | `SaveManager`, `JsonSaveStorage` | **Nguyễn Công Huy (TV2)** | Gom toàn bộ State của TV1 (Actor, HP) & TV2 (Map, Item). |
| **UC08**| Xuống tầng, Đánh Boss, Thắng/Thua | Game Lifecycle | `Scene_Gameplay`, `Frm_GameOver` | `RuleEngine`, `GameSession` | **Nguyễn Đình Huy (TV1)** | Kiểm tra điều kiện thắng/thua, kích hoạt dọn dẹp bộ nhớ. |
| **UC09**| Bảng điểm cao & Thống kê trận | Leaderboard | `Frm_HighScores` | `HighScoreBoard`, `StatisticsTracker`| **Nguyễn Công Huy (TV2)** | Đọc ghi top 10 kỷ lục từ file `highscore.json`. |
| **UC10**| Chế độ Endless / Replay ván chơi | Extended Mode | `Scene_Gameplay` | `ReplayDirector`, `EndlessDirector` | **Nguyễn Đình Huy (TV1)** | Tái lập seed ngẫu nhiên, đọc command history để chạy lại. |

---

## 8. BỘ LUẬT CHƠI MÃ HÓA ĐẦY ĐỦ (20 LUẬT R-01 ĐẾN R-20)

Mọi quy tắc trò chơi đều được đánh mã số duy nhất để truy vết trực tiếp vào mã nguồn và ca kiểm thử tự động, tuyệt đối không có luật ẩn:

- **R-01 (Công thức Sát thương)**: Sát thương nhận thực tế: $\text{DmgActual} = \max(1, \text{DmgRaw} - \text{ArmorValue})$. Sát thương chí mạng nhân hệ số $1.5\times$ hoặc $2.0\times$.
- **R-02 (Khung hình Bất tử - i-frames)**: Sau khi nhận sát thương hoặc khi bấm Dash, người chơi nhận 0.5s bất tử. Trong thời gian này, mọi va chạm gây sát thương đều bị bỏ qua; sprite nhấp nháy tần số 10Hz.
- **R-03 (Bảo toàn Giới hạn Máu)**: Máu của mọi Actor luôn thỏa mãn bất biến $0 \le \text{HP} \le \text{MaxHP}$. Gọi hàm hồi máu `Heal(x)` không bao giờ làm HP vượt quá `MaxHP`. Gọi `TakeDamage(x)` khi $\text{HP} \le x$ lập tức đưa $\text{HP} = 0$ và chuyển trạng thái `DeadState`.
- **R-04 (Tiêu hao & Hồi phục Thể lực)**: Đòn đánh thường tốn 10 Stamina, Dash tốn 20 Stamina. Thể lực tự hồi 15 điểm/giây khi không thực hiện hành động tiêu hao trong vòng 0.8s. Khi Stamina = 0, không thể dùng Dash.
- **R-05 (Tiêu hao Mana & Cooldown)**: Mỗi kỹ năng có chi phí Mana và thời gian hồi chiêu riêng. Kỹ năng chỉ thi triển thành công khi $\text{CurrentMana} \ge \text{Cost}$ VÀ $\text{RemainingCooldown} \le 0$.
- **R-06 (Thời gian Tick Trạng thái)**: Mọi hiệu ứng trạng thái (`StatusEffect`) kích hoạt định kỳ 1.0s một lần. `Burn` gây 5 dmg/s trong 3s; `Poison` gây 3 dmg/s trong 6s; `Freeze` làm tê liệt hoàn toàn trong 2s.
- **R-07 (Chống Xuyên Tường - Swept AABB)**: Vận tốc di chuyển của mọi thực thể và đạn đạo được kiểm tra va chạm theo phương pháp swept volume trên lưới ô. Tuyệt đối không có thực thể nào vượt qua tường đá trong 1 frame bất kể FPS thấp hay đạn bay nhanh.
- **R-08 (Nguyên tắc Rơi Hố - Pit Fall)**: Khi Actor bị đẩy lùi vào ô `pit`, Actor rơi vào trạng thái mất kiểm soát 0.8s, mất $20\%$ HP hiện tại và được dịch chuyển tức thời về tâm ô sàn vững chắc gần nhất vừa đi qua.
- **R-09 (Bẫy Gai Sàn Chu kỳ)**: Ô `trap_spikes` có chu kỳ 3.0s (1.5s ẩn an toàn, 0.5s nhô lên cảnh báo, 1.0s đâm gai gây 15 sát thương). Đứng trên gai khi đang kích hoạt gây sát thương mỗi 0.5s.
- **R-10 (Cửa Phòng & Khóa Niêm Phong)**: Khi người chơi vào phòng Combat, cửa tự động đóng và khóa. Cửa chỉ mở khi số lượng quái vật trong phòng chạm mốc 0 (`EnemiesRemaining == 0`).
- **R-11 (Quy tắc Chìa Khóa & Cửa Kho Báu)**: Cửa rương kho báu hoặc cổng tầng yêu cầu `Key`. Khi người chơi chạm cửa, kiểm tra `Wallet.HasKey(KeyType)`. Nếu có: tiêu thụ 1 chìa, mở cửa vĩnh viễn; nếu không: rung lắc cửa và phát âm thanh cạch cạch.
- **R-12 (Tỷ lệ Rơi Đồ có Pity)**: Tiêu diệt quái thường có $30\%$ rơi Vàng, $10\%$ rơi bình Máu/Mana, $2\%$ rơi trang bị. Cơ chế Bảo hiểm (Pity): nếu sau 5 phòng combat liên tiếp không rơi bình máu, phòng thứ 6 tỷ lệ rơi bình máu chắc chắn là $100\%$.
- **R-13 (Bảo toàn Bất biến Kho đồ)**: Kho đồ người chơi có sức chứa tối đa 16 ô (`Capacity = 16`). Khi nhặt vật phẩm lúc đầy túi, vật phẩm bị nảy ra sàn kèm thông báo `"Túi đồ đã đầy!"`.
- **R-14 (Quy tắc Bàn tế Đổi Máu)**: Tương tác với Bàn tế Thần (`altar`): trừ vĩnh viễn $20\%$ `MaxHP` gốc của người chơi (không thể hồi lại trong run) và sinh ra ngẫu nhiên 1 trong 4 loại Cổ vật Thần thoại.
- **R-15 (Quy tắc Đập Vỡ Băng - Shatter)**: Đòn tấn công vật lý hệ búa (`warhammer` hoặc `mace`) đánh trúng kẻ địch đang mang trạng thái `Freeze` sẽ lập tức phá vỡ băng, gây sát thương bằng $\text{Damage} \times 2.5$ và xóa bỏ trạng thái đóng băng ngay lập tức.
- **R-16 (Dập Lửa Môi trường Nước)**: Bất kỳ thực thể nào mang trạng thái `Burn` khi bước chân vào ô nước (`water`) sẽ được dập tắt `Burn` ngay tại frame đầu tiên, tạo ra đám khói sương mù giảm tầm nhìn của quái vật xung quanh xuống còn 2 ô.
- **R-17 (Hệ thống Tính Điểm Run)**: $\text{Điểm} = (\text{Quái tiêu diệt} \times 50) + (\text{Boss tiêu diệt} \times 1000) + (\text{Số vàng thu thập} \times 5) + (\text{Số tầng vượt qua} \times 500) - (\text{Thời gian chơi tính bằng giây} \times 2)$.
- **R-18 (Điều kiện Thất Bại - Game Over)**: Khi `Player.Health.CurrentHP == 0`, ván chơi kết thúc ngay lập tức. Kích hoạt hiệu ứng tử trận, xóa bỏ file save tạm thời của run hiện tại, lưu kỷ lục vào `highscore.json` và mở màn hình Game Over.
- **R-19 (Điều kiện Thắng Lợi - Victory)**: Sau khi hạ gục Boss Hắc Kỵ Sĩ ở PCU 8 và đặt đủ 3 Mảnh Pha Lê lên Altar, kết giới hầm ngục được thanh tẩy. Dừng đồng hồ tính giờ, lưu tiến trình mở khóa skin mới vào `profile.json`, mở màn hình Victory.
- **R-20 (Khôi phục Trạng thái Save/Load 100%)**: Nạp lại ván chơi từ file JSON phải khôi phục chính xác $100\%$ tọa độ người chơi, HP, Mana, các slot kho đồ, trang bị đang mặc, trạng thái các phòng (đã dọn hay chưa dọn) và seed của màn chơi hiện tại.

---

## 9. BẢNG THÔNG SỐ CÂN BẰNG KHUNG (BALANCING TABLE)

Bảng thông số này được nạp trực tiếp từ `enemies.json` và `items.json`, cho phép tinh chỉnh mà không cần biên dịch lại mã nguồn:

### 9.1 Bảng Cân bằng Chỉ số Kẻ địch (Enemies Balancing)
| Loại Quái Vật | Kiểu AI áp dụng | Máu tối đa | Sát thương | Tốc độ chạy | Tầm nhìn / Tầm đánh | Tỷ lệ rơi vàng (Vàng tb) |
|---|---|:---:|:---:|:---:|:---:|:---:|
| `rat` | `ChaseAI` | 20 | 6 | 3.5 ô/s | 5 ô / 0.8 ô | 40% (2–5 vàng) |
| `slime_green` | `ChaseAI` | 35 | 8 | 2.0 ô/s | 6 ô / 1.0 ô | 50% (3–8 vàng) |
| `skeleton` | `PatrolThenChaseAI`| 50 | 12 | 2.5 ô/s | 7 ô / 1.2 ô | 60% (5–12 vàng) |
| `bat` | `FlitAI` | 25 | 10 | 4.0 ô/s | 8 ô / 0.8 ô | 30% (2–6 vàng) |
| `spider` | `RangedKiteAI` | 40 | 8 (độc) | 3.0 ô/s | 7 ô / 4.5 ô | 50% (6–10 vàng) |
| `mimic` | `AmbushAI` | 90 | 25 | 3.2 ô/s | 1.5 ô / 1.0 ô | 100% (Rơi đồ hiếm) |
| `scorpion_red` | `ChargeAI` | 80 | 18 (cháy) | 2.2 ô/s (Húc 6.6) | 6 ô / 1.2 ô | 70% (10–20 vàng) |
| `golem_magma` (Boss)| `SlamAI` + FSM | 450 | 35 | 1.8 ô/s | Toàn Arena / 2.5 ô | 100% (Mảnh Pha Lê 2) |
| `dark_knight` (Boss cuối)| Multi-phase Boss FSM | 800 | 45 | 3.5 ô/s | Toàn Arena / 2.0 ô | 100% (Trái Tim Pha Lê) |

### 9.2 Bảng Cân bằng Vũ khí (Weapons Balancing)
| Tên Vũ khí | Kiểu Tấn công | Sát thương gốc | Tốc độ vung | Tầm vươn | Hiệu ứng đặc biệt kèm theo |
|---|---|:---:|:---:|:---:|---|
| Dao găm rỉ (`dagger`) | `MeleeArc` hẹp | 12 | 0.25s | 1.0 ô | $+15\%$ Tỷ lệ đòn đánh chí mạng |
| Kiếm sắt (`sword`) | `MeleeArc` vừa | 20 | 0.40s | 1.4 ô | Đòn đánh ổn định, đẩy lùi nhẹ |
| Búa chiến (`warhammer`)| `MeleeArc` rộng | 38 | 0.85s | 1.6 ô | Gây choáng 0.5s; Phản ứng Băng Toái ($2.5\times$) |
| Giáo thép (`spear`) | `Thrust` thẳng | 22 | 0.50s | 2.5 ô | Đâm xuyên tối đa 3 kẻ địch trên đường bay |
| Cung gỗ (`bow`) | `ProjectileShot` | 18 | 0.45s | 8.0 ô | Đạn bay thẳng, cản lại bởi tường đá |
| Gậy phép lửa (`staff_fire`)| `ProjectileShot` | 28 (hỏa) | 0.60s | 7.0 ô | Nổ bán kính 1.2 ô, áp trạng thái `Burn` |
