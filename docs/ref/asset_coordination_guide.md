# HƯỚNG DẪN ĐỒNG BỘ & PHỐI HỢP ĐA GÓI TÀI NGUYÊN (ASSET COORDINATION GUIDE)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Mục tiêu:** Phối hợp hài hòa 6 bộ tài nguyên đồ họa pixel để làm giàu nội dung chơi (PCU, Icons, FX, Animations) mà vẫn đảm bảo tính nhất quán thẩm mỹ tuyệt đối (Visual Consistency), chống bẫy "Asset Clashing / Asset Flip" trong chấm điểm BTL K69.  

---

## 1. TỔNG QUAN 6 BỘ ASSET PACK & PHÂN ĐỊNH VAI TRÒ HỆ THỐNG

| STT | Tên Gói Asset | Nguồn & Bản quyền | Vai trò Kiến trúc trong Game | Kích thước / Chuẩn kỹ thuật |
|:---:|---|---|---|---|
| **1** | **Voxel Dungeon Pack Full** *(Gói gốc chủ đạo)* | Cole Foster (CC0 1.0 Universal) | **Bộ khung nền tảng (Core Foundation)**: Toàn bộ 283 tileset, Autotile blob47, Nine-slice UI kit, Nhân vật chính (Knight, Mage, Dark Knight), Props, Loot và FX cốt lõi. | $32 \times 32$ tiles, PPU 32, 3 hướng nhìn (Down/Up/Side), Point filter. |
| **2** | **Raven Fantasy Icons** *(Gói bổ trợ Icon)* | Raven Fantasy (Free / Itch.io) | **Hệ thống Icon Đa dạng cho Kho đồ & Kỹ năng**: Làm giàu toàn bộ icon vũ khí, trang bị, bình thuốc, ngọc quý, sách phép và biểu tượng nguyên tố trên HUD. | Chọn dùng phiên bản $32 \times 32$ để vừa khít $1:1$ với `slot_normal` trong UI kit của Cole Foster. |
| **3** | **Tiny RPG Character Pack 01** *(Free Soldier & Orc)* | Tiny RPG (Free License) | **Nâng cấp Hoạt ảnh Đòn đánh & Boss Orc**: Cung cấp combo chém 2 pha (`Attack01`, `Attack02`) cho Hiệp sĩ, hoạt ảnh vung búa dữ dội cho Boss `orc_chieftain` và đạn tên `Arrow01(32x32)`. | $100 \times 100$ canvas với nhân vật cao $\sim 28\text{px}$, scale $1.0$, PPU 32. |
| **4** | **Tiny Swords Free Pack** | Pixel Frog (Free License) | **Hệ thống Hạt Particle FX & Quái Tộc Goblin**: Tận dụng hiệu ứng hạt chất lượng cao (nước bắn, bụi chạy, tia lửa va chạm, nổ bốc khói) và biến thể Goblin bắn cung (`Archer`). | Top-down pixel $64 \times 64$, trích xuất dải Particle FX nạp vào `FxPlayer`. |
| **5** | **2D Pixel Dungeon Asset Pack v2.0** | Free Dungeon Pack | **Quái vật Bay & Trang trí Cổ**: Cung cấp quái vật đầu lâu bay rực lửa (`skull`) có animation 4 frame mượt mà cho PCU 6 (Vực Thẳm) và phụ trợ cho Boss cuối. | $16 \times 16$ phóng to $2\times$ (Nearest Neighbor) để đồng bộ chuẩn $32 \times 32$. |
| **6** | **Free Characters Animations Pack** | Free Character Pack | **Hoạt ảnh Phòng thủ Khiên (Block)**: Cung cấp động tác giơ khiên chắn (`Human_Soldier_Block-Sheet`) khi người chơi kích hoạt kỹ năng `ShieldBubble` hoặc thế thủ. | $96 \times 96$ canvas, trích xuất dải frame Block canh giữa tâm nhân vật. |

---

## 2. MA TRẬN PHỐI HỢP TÀI NGUYÊN CHI TIẾT THEO PHÂN HỆ

```
┌────────────────────────────────────────────────────────────────────────┐
│                   HỆ THỐNG GIAO DIỆN & MÔI TRƯỜNG BẢN ĐỒ               │
│  - Mặt sàn, tường đá, nước, dung nham, bóng đổ: Voxel Dungeon (Cole Foster)│
│  - Khung UI Nine-slice (Panel đá/gỗ/giấy, thanh máu, slot): Voxel Dungeon│
│  - Con trỏ chuột, biểu tượng tim, chìa khóa, coin: Voxel Dungeon       │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │ (Lắp ghép đồng bộ kích thước)
┌───────────────────────────────────▼────────────────────────────────────┐
│                    HỆ THỐNG KHO ĐỒ, SHOP & KỸ NĂNG                     │
│  - Toàn bộ Icon đặt trong slot UI 32×32: Raven Fantasy Icons (32×32)  │
│  - Đạn mũi tên vật lý: Arrow01(32×32) từ Tiny RPG Character Pack       │
│  - Biểu tượng Rune nguyên tố (Lửa, Băng, Sét, Độc): Raven Fantasy     │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │ (Tích hợp hoạt ảnh và hiệu ứng)
┌───────────────────────────────────▼────────────────────────────────────┐
│                    HỆ THỐNG THỰC THỂ & HIỆU ỨNG CHIẾN ĐẤU              │
│  - Hiệp sĩ, Pháp sư, Hắc Kỵ Sĩ: Voxel Dungeon Pack                      │
│  - Đòn chém combo 2 pha & Boss Orc: Tiny RPG Character Pack (Orc/Soldier)│
│  - Thế thủ giơ khiên: Free Characters Animations Pack (Block Sheet)    │
│  - Quái đầu lâu bay (PCU 6): 2D Pixel Dungeon Pack (skull)             │
│  - Hạt nổ, bụi lướt né, bọt nước: Tiny Swords Particle FX + Cole Foster│
└────────────────────────────────────────────────────────────────────────┘
```

---

## 3. BA NGUYÊN TẮC KỸ THUẬT BẢO ĐẢM NHẤT QUÁN THỊ GIÁC (ANTI-ASSET CLASH)

Để hội đồng chấm không trừ điểm "chắp vá đồ họa không đồng bộ phong cách", nhóm áp dụng 3 quy chuẩn kỹ thuật nghiêm ngặt:

### 3.1 Quy chuẩn Thước đo Không gian (Pixel-Per-Unit - PPU Cố định)
- Toàn bộ Sprite trong game bắt buộc thiết lập:
  - **Pixels Per Unit (PPU) = 32**.
  - **Filter Mode = Point (no filter)**: Đảm bảo cạnh pixel sắc nét, không bị làm nhòe mờ nội suy (Bilinear/Trilinear).
  - **Compression = None**: Giữ nguyên vẹn bảng màu pixel art nguyên bản.
- Đối với tài nguyên vẽ trên canvas lớn hơn (như Tiny RPG $100 \times 100$ hay Free Characters $96 \times 96$):
  - Chiều cao thực tế của nhân vật trong sprite là khoảng $24 - 30\text{px}$ (khớp với nhân vật cao $28\text{px}$ của Cole Foster).
  - Canh chỉnh Pivot Point tại điểm chân tiếp đất `(0.5, 0.15)` để đảm bảo tọa độ chân đứng trên lưới ô trùng khít với collider $32 \times 32$.

### 3.2 Quy chuẩn Tương thích Góc nhìn (Top-Down Perspective Alignment)
- Cả 6 bộ asset đều tuân thủ góc nhìn từ trên xuống nghiêng nhẹ (Top-Down $\sim 60^\circ$ / Cavalier oblique).
- Hoạt ảnh di chuyển ưu tiên 4 hướng chính (Down, Up, Side; Side lật ngang để thành Left). Khi đổi hướng, nhân vật xoay trục $X$ bằng `flipX` của SpriteRenderer.

### 3.3 Tinh chỉnh Bảng màu Đồng bộ (Color Palette Normalization)
- Tông màu chủ đạo của game được cố định theo bảng màu trầm tối của hầm ngục (Dungeon Crypt Palette).
- Các hiệu ứng hạt lấy từ `Tiny Swords` (vốn có màu hơi tươi sáng) được xử lý qua Shader Tinting hoặc Material 2D URP để hạ bớt độ bão hòa (Saturation $-15\%$) cho hòa nhập hoàn hảo vào môi trường u tối của Cole Foster.

---

## 4. KIẾN TRÚC OOP CHO VIỆC NẠP ĐA DẠNG NGUỒN TÀI NGUYÊN

Để Core Logic hoàn toàn không bị ảnh hưởng bởi việc sử dụng nhiều bộ asset khác nhau, dự án áp dụng **Adapter Pattern** và **Factory Pattern** cho hệ thống hiển thị:

```
┌────────────────────────────────┐
│      ISpriteFrameProvider      │  <--- (Hợp đồng chung cho mọi bộ asset)
└───────────────▲────────────────┘
                │
    ┌───────────┴───────────┬────────────────────────┐
    │                       │                        │
┌───┴──────────────┐  ┌─────┴───────────────┐  ┌─────┴──────────────┐
│ ColeAtlasAdapter │  │ SheetGridAdapter    │  │ SingleFrameAdapter │
│ (atlas.json)     │  │ (TinyRPG / Aseprite)│  │ (Raven 32x32 Icons)│
└───┬──────────────┘  └─────┬───────────────┘  └─────┬──────────────┘
    │                       │                        │
    └───────────────────────┼────────────────────────┘
                            ▼
               ┌────────────────────────┐
               │    SpriteAnimator      │  <--- (Adapter View mỏng trong Presentation)
               └────────────────────────┘
```

1. **`ISpriteFrameProvider` (Contract)**: Định nghĩa phương thức `GetFrame(string animationName, int frameIndex, Facing facing)`.
2. **`ColeAtlasAdapter`**: Đọc các frame rect từ `atlas.json` của Cole Foster (tối ưu nạp bộ nhớ).
3. **`SheetGridAdapter`**: Chia lưới tự động cho các spritesheet dạng dải băng từ `Tiny RPG` và `FreeCharactersAnimations`.
4. **`SingleFrameAdapter`**: Nạp các file icon đơn lẻ $32 \times 32$ từ `Raven Fantasy Icons` và lưu trữ trong bộ nhớ đệm (Cache Dictionary) để tái sử dụng tức thì.
- **Giá trị bảo vệ luận điểm OOP**: Cấu trúc này là minh chứng hùng hồn cho nguyên lý **Đóng gói (Encapsulation)** và **Mở - Đóng (OCP)**: Thêm một bộ asset mới từ trên mạng chỉ cần tạo thêm một lớp Adapter mới, không sửa đổi bất kỳ dòng mã nào trong `ActorView` hay `CombatHudView`!

---

## 4. TÍCH HỢP PIXELLAB MCP TOOLKIT ĐỂ BỔ SUNG ASSET (DỰ PHÒNG)

Trường hợp trong quá trình phát triển phát sinh nhu cầu vẽ thêm đạo cụ, hiệu ứng, hoặc khung hoạt ảnh phụ mà 6 bộ asset có sẵn chưa phủ kín:
- **Cấu hình MCP Server**: Đã tích hợp sẵn tại `~/.gemini/config/mcp_config.json` và `~/.gemini/settings.json`:
  - **Endpoint:** `https://api.pixellab.ai/mcp`
  - **Token xác thực:** `pixl_b4da9f7d15504d7c9e9583afa8b52f3c`
  - **Tài liệu công cụ:** [https://api.pixellab.ai/mcp/docs](https://api.pixellab.ai/mcp/docs)
- **Quy tắc tạo sinh bắt buộc (Asset Generation Constraints)**:
  1. **Định dạng kích thước:** Bắt buộc sinh ở kích thước $32 \times 32$ pixels (hoặc $64 \times 64$ với Boss lớn) để đồng bộ 100% với hệ quy chiếu PPU 32.
  2. **Bảng màu & Nền:** Luôn yêu cầu `no_background=True` (nền trong suốt PNG) và phong cách tương thích bảng màu Voxel Dungeon.
  3. **Lưu trữ & Bản quyền:** Mọi asset sinh ra được lưu tại `Assets/Art/Generated/`, cập nhật ghi nhận vào `CREDITS.md` và kiểm tra kỹ lưỡng để không gây lệch phong cách (asset clash).
