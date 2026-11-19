# KIM CHỈ NAM THIẾT KẾ KIẾN TRÚC & UI/UX (DESIGN GUIDE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) -->

Tài liệu này đóng vai trò là "kim chỉ nam" về mặt kiến trúc phần mềm và định hướng nghệ thuật giao diện, làm cầu nối trực tiếp giữa tài liệu kế hoạch ([docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md) & [03_UIUX_ART_DIRECTION.md](file:///d:/project/projectOOPnew/docs/plan/03_UIUX_ART_DIRECTION.md)) với mã nguồn thực tế.

---

## 1. 5 TRỤ CỘT THIẾT KẾ CỐT LÕI (CORE PILLARS)

1. **Cô Lập Nghiệp Vụ Hoàn Toàn (Engine-Agnostic Core):** Tầng Core hoàn toàn không biết đến sự tồn tại của Unity. Toàn bộ logic kiểm soát luật chơi, di chuyển, chiến đấu, AI và sinh hầm ngục đều là mã C# thuần túy có thể thực thi độc lập ở chế độ EditMode trong thời gian $< 2$ giây.
2. **OOP Trực Quan Nhưng Đủ Phức Tạp (Intuitive yet Substantive OOP):** Không thiết kế hình thức (over-engineering), nhưng cũng không viết mã thiếu máu (Anemic Model). Mỗi lớp, interface hoặc pattern phải giải thích được lý do tồn tại trong 1–2 câu và giải quyết một mùi code cụ thể nếu bị loại bỏ.
3. **Mở Rộng Không Sửa Đổi (Open/Closed Principle & Live OCP):** Các hệ thống có tính mở rộng cao (Kẻ địch AI, Vũ khí, Phản ứng nguyên tố, Vật phẩm tiêu hao) được trừu tượng hóa bằng Interface/Abstract class, cho phép bổ sung biến thể mới trong 3 phút mà không phải sửa một dòng mã nào của các lớp cũ.
4. **Giao Tiếp Phi Kết Nối (Loose Coupling via EventBus):** Hai phân hệ lớn do Nguyễn Đình Huy và Nguyễn Công Huy phụ trách không gọi trực tiếp vào lớp cụ thể của nhau mà kết nối qua 2 hợp đồng trừu tượng: `ITileQuery` (truy vấn va chạm) và `IGameEventBus` (Publish-Subscribe sự kiện).
5. **Thẩm Mỹ Pixel Art Nhất Quán & Game Feel:** Toàn bộ thế giới game tuân thủ tỷ lệ đồ họa **PPU 32**, bộ lọc Point Filter sắc nét, kết hợp hài hòa 6 bộ asset pack và bổ sung đầy đủ các phản hồi giác quan (rung camera, chớp trắng hit-stop, hạt nảy spark).

---

## 2. DESIGN TOKENS GIAO DIỆN & MỸ THUẬT (UI DESIGN TOKENS)

Lấy chuẩn từ `docs/plan/03_UIUX_ART_DIRECTION.md`:

### 2.1 Hệ Thống Tọa Độ & Lưới (Grid & PPU)
- **Pixels Per Unit (PPU):** Cố định **32** cho toàn bộ Sprite, Tileset, Items và UI Icon.
- **Kích thước ô lưới logic:** 1 tile = $1 \times 1$ world unit = $32 \times 32$ pixels.
- **Tỷ lệ hiển thị tham chiếu:** $1920 \times 1080$ (16:9), Pixel Perfect Orthographic Size = 5.625 (tương đương chiều cao hiển thị 11.25 ô gạch).

### 2.2 Bảng Màu Chủ Đạo (Color Palette)
- **Pha Lê Cổ Đại (Cyan/Crystal):** `#38bdf8` (Điểm nhấn phép thuật, Mana, ALTAR).
- **Lửa / Đốt cháy (Orange/Red):** `#f97316` / `#ef4444` (Sát thương, Máu, Lava Biome).
- **Băng tuyết (Ice Blue):** `#7dd3fc` (Làm chậm, Đóng băng).
- **Độc mốc (Poison Green):** `#22c55e` (Độc, Nấm, Moss Biome).
- **Khung Nền UI (Dark Slate):** `#0f172a` (Nền panel 9-slice), `#1e293b` (Nền ô slot chứa đồ).
- **Vàng Kim (Gold Accent):** `#fbbf24` (Tiền vàng, Khung vật phẩm hiếm, Viền nút bấm).

### 2.3 Phông Chữ & UI Kit
- **Phông chữ:** `m5x7.ttf` (hoặc font Bitmap tương thích SIL Open Font), hiển thị chuẩn ở kích thước 16pt / 32pt không bị nhòe pixel.
- **9-Slice UI Border:** Nẹp biên 4 pixel từ tệp `ui_frame_9slice.png` trong pack gốc.

---

## 3. QUY TẮC THAY ĐỔI THIẾT KẾ
Bất kỳ đề xuất thay đổi kiến trúc nào đều phải đi qua workflow `/design-change`:
1. Soạn thảo ADR trong `docs/adr/`.
2. Cập nhật `docs/design/uml/class_diagram_full.puml` và tài liệu này.
3. Liệt kê vùng ảnh hưởng (Blast Radius) đối với các module của thành viên còn lại.
4. Triển khai mã nguồn và bộ test tương ứng.
