# ĐỊNH HƯỚNG MỸ THUẬT, GIAO DIỆN & GAME FEEL (UI/UX & ART DIRECTION)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat & FX View)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead UI, World View & Audio)**  
**Bộ tài nguyên sử dụng:** Voxel Dungeon Pack Full (Cole Foster - CC0) kết hợp 5 gói bổ trợ (Raven Fantasy Icons, Tiny RPG, Tiny Swords, 2D Pixel Dungeon v2, Free Characters)  
**Quy chuẩn tương ứng:** Bước 03, 04 PDF v2026 & Tiêu chí 2.1, 2.3 Checklist K69  

---

## 1. NHẬN DIỆN HÌNH ẢNH & TIÊU CHUẨN ĐỒ HỌA PIXEL-PERFECT

Toàn bộ đồ họa trong game được đồng nhất theo phong cách Pixel Art cổ điển kết hợp chiếu sáng thời gian thực 2D hiện đại (2D URP):

```
┌────────────────────────────────────────────────────────────────────────┐
│                        THIẾT LẬP RENDER PIPELINE                       │
│  - Engine: Unity 2022.3 LTS (URP 2D)                                   │
│  - Độ phân giải gốc (Virtual Resolution): 480 × 270 (Tỷ lệ 16:9)       │
│  - Pixels Per Unit (PPU): 32 (Mỗi tile 32×32 tương đương 1.0 Unit)     │
│  - Filter Mode: Point (no filter) | Compression: None (Tránh mờ pixel) │
│  - Camera: Pixel Perfect Camera Component (Orthographic, Zoom nguyên)  │
└────────────────────────────────────────────────────────────────────────┘
```

### 1.1 Hệ thống Ánh sáng 2D theo Từng Biome
Không dùng ánh sáng toàn phần tĩnh làm mất đi không khí u tối của hầm ngục. Hệ thống sử dụng Universal 2D Lights:
- **Global Light**: Ánh sáng môi trường yếu, thay đổi màu sắc theo tầng (Hầm Đá: Xanh đêm `#1a1c2e`; Hang Nấm: Tím lục ám `#16241a`; Hầm Nước: Xanh ngọc sẫm `#0e242b`; Lò Rèn: Đỏ rực than `#2b0e0e`).
- **Point Light 2D**: Gắn trên các đạo cụ phát sáng thực tế:
  - `torch_deco`: Ánh sáng vàng cam ấm áp, bán kính 4.5 ô, có hiệu ứng nhấp nháy ngẫu nhiên (Flicker noise).
  - `brazier`: Lò than lớn ở phòng Boss, ánh sáng cam đỏ rực rỡ, bán kính 7.0 ô.
  - `magic_circle` & `altar`: Hào quang ma thuật xanh lam tím huyền bí, bán kính 3.0 ô.
- **Bóng đổ tường (`shadow_wall`)**: Tự động sinh ra ở chân tường phía nam qua thuật toán Autotile, tạo chiều sâu thị giác 2.5D.

---

## 2. BỐ CỤC MÀN HÌNH HUD & BỘ UI KIT 9-SLICE NGUYÊN BẢN

Toàn bộ giao diện người dùng được lắp ráp 100% từ thư mục `ui/elements/` của asset pack, tuyệt đối không dùng UI mặc định của Unity:

```
┌────────────────────────────────────────────────────────────────────────┐
│ [GÓC TRÁI TRÊN: CHỈ SỐ NHÂN VẬT]         [GÓC PHẢI TRÊN: TÀI NGUYÊN]   │
│  ♥ ♥ ♥ ♥ ♥ (icon_heart_full/half/empty)   💰 1,250 (icon_coin)         │
│  [HP Bar: bar_frame + bar_fill_hp]       🔑 2     (key)               │
│  [MP Bar: bar_frame + bar_fill_mana]     ⭐ Tầng: B3F (icon_star)     │
│  [SP Bar: bar_frame + bar_fill_stamina]  ⏱️ 08:45                     │
│                                                                        │
│                      [GIỮA TRÊN: BOSS HEALTH BAR]                      │
│            💀 GOLEM MAGMA [bar_frame + bar_fill_hp (đỏ lớn)]           │
│                                                                        │
│                                          [GÓC PHẢI DƯỚI: BẢN ĐỒ CON]   │
│                                          ┌───────────────────────┐     │
│                                          │      [MINIMAP VIEW]   │     │
│                                          │   [Start]─[Combat]    │     │
│                                          └───────────────────────┘     │
│                                                                        │
│                     [GIỮA DƯỚI: THANH PHÍM TẮT HOTBAR]                 │
│      ┌─────┬─────┬─────┬─────┬─────┬─────┬─────┐                      │
│      │ [1] │ [2] │ [3] │ [4] │ [Q] │ [E] │ [R] │                      │
│      │ Fire│Frost│Lightning│Heal│PotHP│PotMP│Dash│                      │
│      └─────┴─────┴─────┴─────┴─────┴─────┴─────┘                      │
│       (Dùng slot_normal / slot_hover / slot_selected)                  │
└────────────────────────────────────────────────────────────────────────┘
```

### 2.1 Quy tắc Dùng 4 Loại Panel 9-Slice:
- `panel_stone`: Dùng cho Menu Hệ thống, Bảng Cài đặt phím và Bảng Tạm dừng (Pause). Tạo cảm giác chắc chắn, kiên cố.
- `panel_wood`: Dùng cho Bảng Cửa hàng buôn bán (`Shop`) và Hòm đồ (`Storage`). Tạo cảm giác mộc mạc, gần gũi của thương nhân.
- `panel_parchment`: Dùng cho Bảng Túi đồ (`Inventory`), Bảng Kỹ năng và các trang sách Lore cổ vật. Tông màu giấy da cổ điển dễ đọc chữ.
- `panel_dark`: Dùng làm khung nền bán trong suốt lót dưới thanh máu Boss và các thông báo nổi (Pop-up).

### 2.2 Con trỏ Chuột (Cursors):
- `cursor_crosshair.png`: Tự động kích hoạt khi người chơi đang trong trạng thái chiến đấu (PlayingState), giúp ngắm bắn chuẩn xác.
- `cursor_arrow.png` & `cursor_hand.png`: Tự động kích hoạt khi mở các màn hình menu, kho đồ hoặc khi rê chuột lên các nút bấm/vật phẩm có thể click.

---

## 3. THIẾT KẾ DÒNG ĐIỀU HƯỚNG VÀ DANH MỤC 10 MÀN HÌNH (SCENE FLOW & WIREFRAMES)

```
[Scene_MainMenu] ──(Bắt đầu)──> [Frm_ClassSelect] ──(Vào ván)──> [Scene_Gameplay (HUD)]
        │                                                                │
        ├──(Cài đặt)──> [Frm_Settings]                                   ├──(Nhấn Esc)──> [Frm_Pause] ──> [Frm_SaveLoad]
        │                                                                ├──(Nhấn Tab)──> [Frm_Inventory]
        ├──(Kỷ lục)──> [Frm_HighScores]                                 ├──(Gặp Merchant)──> [Frm_Shop]
        │                                                                ├──(Gặp Altar)──> [Frm_Altar]
        └──(Thoát)                                                       ├──(HP <= 0)──> [Frm_GameOver] ──> [Frm_HighScores]
                                                                         └──(Diệt Boss cuối)──> [Frm_Victory] ──> [Frm_HighScores]
```

### Danh mục 10 Màn hình & Wireframe chi tiết:
1. **Scene_MainMenu**: Tiêu đề game pixel lớn, 4 nút bấm (`button_normal/pressed/hover`): *Chơi Mới*, *Tiếp Tục*, *Cài Đặt*, *Bảng Điểm*, *Thoát*. Có hiển thị phiên bản `v2026-OOP`.
2. **Frm_ClassSelect**: Lựa chọn 2 nhân vật (Hiệp Sĩ / Pháp Sư); hiển thị hoạt ảnh idle xoay vòng, thanh chỉ số HP/Mana/Speed và danh sách kỹ năng khởi đầu.
3. **Scene_Gameplay (HUD chính)**: Sân chơi hiển thị góc nhìn camera theo dõi mượt nhân vật, lưới bản đồ, quái vật, đạn đạo và các cụm HUD ở 4 góc màn hình.
4. **Frm_Pause**: Bật đè lên Gameplay khi nhấn `Esc`. Nút bấm: *Tiếp tục*, *Lưu ván*, *Cài đặt*, *Về Menu chính*.
5. **Frm_Settings**: Điều chỉnh âm lượng Âm nhạc (BGM) và Hiệu ứng (SFX) bằng `slider_track` và `slider_handle`; bật/tắt toàn màn hình bằng `checkbox_on/off`; hiển thị danh sách phím tắt điều khiển.
6. **Frm_Inventory**: Mở bằng phím `Tab` hoặc `I`. Lưới 16 ô túi đồ (`slot_normal`), 4 ô trang bị (Nón, Áo, Giày, Nhẫn), khung thông tin chi tiết vật phẩm và 2 nút: *Sử Dụng*, *Vứt Bỏ*.
7. **Frm_Shop**: Giao diện thương nhân chia làm 2 bên: Hàng bán của Shop và Túi đồ người chơi. Hiển thị giá vàng từng món, nút *Mua* và *Bán*.
8. **Frm_Altar**: Giao diện Bàn tế thần huyền bí, hiển thị lời mời gọi dâng hiến $20\%$ MaxHP để đổi lấy 1 trong 3 Cổ vật ngẫu nhiên.
9. **Frm_GameOver**: Nền màn hình chuyển đỏ xám dần. Hiển thị chữ *"BẠN ĐÃ TỬ TRẬN"*, nguyên nhân chết, số tầng đạt được, quái tiêu diệt, tổng điểm và 2 nút: *Chơi Lại*, *Bảng Điểm*.
10. **Frm_Victory**: Nền sáng hoàng kim rực rỡ. Hiển thị chữ *"CHIẾN THẮNG QUẢ CẢM"*, hình ảnh Hiệp sĩ cắm 3 Mảnh Pha Lê vào Bàn Tế Thần, thông báo mở khóa skin Hắc Kỵ Sĩ, nút *Lưu Kỷ Lục*.

---

## 4. TIÊU CHUẨN TRẢI NGHIỆM ĐỒNG BỘ (GAME FEEL)

Mọi hành động va chạm cơ học trong trò chơi đều phải có phản hồi thị giác và thính giác tức thì để mang lại cảm giác chiến đấu sống động, đầm tay:

| Sự kiện Gameplay | Phản hồi Thị giác (Visual FX) | Phản hồi Chuyển động (Screen Shake & Freeze) | Hoạt ảnh Sprite | Hiệu ứng Âm thanh (SFX) |
|---|---|---|---|---|
| **Chém trúng kẻ địch** | Bật tia lửa trúng đòn `hit_spark.png` | Hit-stop đóng băng 0.05s; sprite quái nháy trắng 0.1s | Quái chuyển hoạt ảnh `hurt` | `sfx_slash_hit.wav` |
| **Búa gõ chí mạng** | Bật vệt chém `slash.png` + `dust_puff` | Rung camera chấn động mạnh 0.25s, biên độ 0.3 Unit | Quái bị đẩy lùi 1.5 ô | `sfx_heavy_impact.wav` |
| **Người chơi nhận đòn**| Màn hình nháy đỏ viền vignette; số dame đỏ bay lên | Rung giật nhẹ camera 0.15s, biên độ 0.2 Unit | Người chơi nhấp nháy i-frames 0.5s | `sfx_player_hurt.wav` |
| **Lướt né đòn (Dash)** | Để lại làn khói bụi `dust_puff.png` phía sau | Không rung camera; làm mờ nhẹ chuyển động | Nhân vật lướt vút theo hướng phím | `sfx_dash_whoosh.wav` |
| **Cầu lửa phát nổ** | Vòng nổ lửa `explosion.png` lan rộng 1.5 ô | Rung camera tầm trung 0.2s | Quái dính cháy bốc tia lửa | `sfx_explosion_boom.wav` |
| **Tia sét liên hoàn** | Tia điện `lightning.png` nảy qua các mục tiêu | Nháy sáng nhẹ toàn màn hình 0.08s | Quái giật nảy chuyển `hurt` | `sfx_lightning_zap.wav` |
| **Nhặt được Vàng / Đồ** | Bật đốm sáng lấp lánh `sparkle.png` quanh nhân vật| Không rung | Vật phẩm xoay vòng rồi biến mất | `sfx_coin_pickup.wav` |
| **Mở rương báu** | Rương mở nắp `chest (open)` + vung `sparkle` | Đóng băng nhẹ 0.1s trước khi vật phẩm vọt lên | Rương phát sáng | `sfx_chest_open.wav` |
| **Quái vật tử trận** | Bùng lên cụm khói xám `smoke.png` rồi biến mất | Đóng băng nhẹ 0.04s | Quái chuyển hoạt ảnh `death` | `sfx_enemy_death.wav` |
| **Chuyển tầng / Cổng dịch**| Vòng tròn ma pháp `magic_circle` + cột sáng `teleport`| Màn hình mờ dần về đen (Fade out 0.5s) | Nhân vật tan biến vào luồng sáng | `sfx_teleport.wav` |

---

## 5. HỆ THỐNG ÂM THANH & BẢO LƯU CẤU HÌNH BỀN VỮNG

### 5.1 Danh mục Âm thanh Tối thiểu (CC0):
- **BGM (Nhạc nền vòng lặp)**:
  - `bgm_crypt.ogg`: Giai điệu chậm rãi, u tối, tiếng phong cầm và gõ đá (Biome Hầm Đá).
  - `bgm_fungal.ogg`: Tiếng róc rách ẩm ướt kết hợp âm bass trầm (Biome Hang Nấm & Nước).
  - `bgm_forge.ogg`: Nhịp trống dồn dập, tiếng kim loại va chạm khí thế (Biome Lò Rèn).
  - `bgm_boss.ogg`: Nhạc giao hưởng chiptune dồn dập tiết tấu nhanh cho các màn Boss.
- **SFX (Hiệu ứng âm thanh $\ge 8$ loại)**:
  - Tấn công: `sfx_sword_slash.wav`, `sfx_blunt_hit.wav`, `sfx_bow_shot.wav`.
  - Phép thuật: `sfx_fireball_cast.wav`, `sfx_frost_cast.wav`, `sfx_lightning.wav`.
  - Tương tác: `sfx_footstep.wav`, `sfx_pickup.wav`, `sfx_door_open.wav`, `sfx_button_click.wav`.

### 5.2 Cơ chế Lưu trữ Cấu hình Âm lượng:
- Toàn bộ mức âm lượng BGM ($0.0 - 1.0$) và SFX ($0.0 - 1.0$) cùng cờ Mute được quản lý qua `AudioManager`.
- Khi người chơi điều chỉnh trên màn hình Settings, giá trị được tự động ghi bền vững xuống tệp `profile.json` thông qua `ISaveStorage`. Khởi động lại game, âm lượng được khôi phục chính xác $100\%$, không bị nhảy về mức mặc định.

---

## 6. QUY TẮC NHẤT QUÁN THIẾT KẾ GIỮA NGUYỄN ĐÌNH HUY VÀ NGUYỄN CÔNG HUY (CONSISTENCY GUIDELINES)

Để đảm bảo sản phẩm hoàn thiện trông như do một người duy nhất thiết kế (chống bẫy trừ điểm mục 2.4 Checklist):
1. **Quy chuẩn Font chữ**: Toàn bộ dự án chỉ sử dụng duy nhất một phông chữ pixel: **Press Start 2P** (cỡ chữ: Tiêu đề 16pt, Nút bấm 10pt, Văn bản thường 8pt, Chỉ số 6pt). Cấm trộn font chữ Arial hay Time New Roman.
2. **Quy chuẩn Khoảng cách Lưới UI (Layout Spacing)**: Mọi lề (margin) và đệm (padding) trên Canvas đều là bội số của 4 pixel (4px, 8px, 16px, 24px).
3. **Quy chuẩn Đặt tên File & Cấu trúc**:
   - Tên Scene: `Scene_MainMenu`, `Scene_Gameplay`, `Scene_GameOver`.
   - Tên Prefab UI Form: `Frm_InventoryOverlay`, `Frm_ShopOverlay`, `Frm_SettingsPanel`.
   - Tên Tệp Dữ liệu Màn: `floor_01_crypt.json`, `floor_02_fungal.json`.
   - Tên Sprite Atlas: Nhất quán theo mẫu `<asset>_<facing>_<animation>`.

---

## 7. CHIẾN LƯỢC PHỐI HỢP ĐA GÓI ASSET (MULTI-ASSET COORDINATION MATRIX)

Nhằm làm phong phú nội dung chơi nhưng vẫn giữ vững tính nhất quán thị giác (chống bẫy Asset Clashing), dự án tích hợp thêm 5 bộ tài nguyên bổ trợ xoay quanh gói nền tảng của Cole Foster (xem chi tiết tại [asset_coordination_guide.md](file:///d:/project/projectOOPnew/docs/ref/asset_coordination_guide.md)):
1. **Gói Gốc (Cole Foster Voxel Dungeon Pack Full)**: Giữ vai trò khung xương sống cho toàn bộ Tileset $32 \times 32$ (blob47 autotile), Nine-slice UI kit, nhân vật chính (Knight, Mage, Dark Knight) và quái cơ bản.
2. **Raven Fantasy Icons ($32 \times 32$)**: Cung cấp hơn 6,500 icon chất lượng cao cho toàn bộ Kho đồ, Cửa hàng và Kỹ năng (Vũ khí, Giáp, Nhẫn, Bùa, Potion, Sách cổ, Biểu tượng Rune nguyên tố), vừa khít $1:1$ với `slot_normal` của Cole Foster.
3. **Tiny RPG Character Pack 01 (Soldier & Orc)**: Bổ sung chuỗi hoạt ảnh chém 2 pha (`Attack01`, `Attack02`) cho Hiệp Sĩ, hoạt ảnh đập vung búa cho Boss `orc_chieftain` và đạn mũi tên vật lý `Arrow01(32x32)`.
4. **Tiny Swords Free Pack (Pixel Frog)**: Trích xuất hệ thống Particle FX (nước bắn, bụi chạy, tia lửa va chạm, nổ khói) và biến thể quái Goblin bắn cung.
5. **2D Pixel Dungeon v2.0**: Bổ sung quái vật sọ bay rực lửa (`skull`) có hoạt ảnh idle 4 frame mượt mà cho tầng Vực Thẳm (PCU 6).
6. **Free Characters Animations Pack**: Cung cấp hoạt ảnh giơ khiên phòng thủ (`Block-Sheet`) khi người chơi thi triển kỹ năng `ShieldBubble`.
* **Kiến trúc Trừu tượng**: Áp dụng **Adapter Pattern** (`ISpriteFrameProvider`) để cô lập hoàn toàn nguồn sprite khỏi logic hiển thị; đảm bảo PPU 32 cố định và Point Filter cho 100% hình ảnh trong game.
