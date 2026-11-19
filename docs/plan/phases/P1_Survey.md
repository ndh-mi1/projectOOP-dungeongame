# PHASE 1: BƯỚC 01 – KHẢO SÁT & ĐẶC TẢ TRÒ CHƠI (SURVEY & GAME DESIGN)
**Mã giai đoạn:** P1 | **Thời gian thực hiện:** Tuần 1 (T1)  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat & Game Loop)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead GDD & PCU Specs)**  
**Tiêu chuẩn áp dụng:** Bước 01 PDF v2026 (Trang 4–7) & Tiêu chí A1.1, 1.1, 3.1 Checklist K69  
**Mục tiêu cốt lõi:** Chuyển hóa ý tưởng game thành Tài liệu Đặc tả Game Design (GDD 2 trang), Mindmap 4W1H, kiến trúc Vòng lặp Game Loop 60Hz tách rời Core/Engine, và bảng 20 luật chơi mã hóa `R-01..R-20` không có luật ẩn.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. Xây dựng Mindmap 4W1H ánh xạ trực tiếp bài toán trò chơi sang 5 thành phần phần mềm chuẩn: WHO $\rightarrow$ Classes, WHAT $\rightarrow$ RuleEngine, WHERE $\rightarrow$ Scenes, WHEN $\rightarrow$ GameLoop, HOW $\rightarrow$ Input & JSON.
2. Hoàn thiện tài liệu GDD 2 trang đặc tả thể loại Roguelike Dungeon 2D, điều khiển 4 hướng, cơ chế chiến đấu và cốt truyện 3 Mảnh Pha Lê.
3. Thiết kế kiến trúc Vòng lặp trò chơi (Game Loop Architecture) 4 bước: `Process Input` $\rightarrow$ `Fixed Update(dt)` $\rightarrow$ `Check Win/Lose & Events` $\rightarrow$ `Render Frame & Audio` (chu kỳ 60 FPS cố định, độc lập phần cứng).
4. Thiết lập 4 Output chuẩn $A, B, C, D$ theo Slide 5 PDF: (A) Luật chơi & Game Loop; (B) Màn chơi & Use Case; (C) Danh mục Thực thể & Chỉ số; (D) Tài nguyên & Dữ liệu JSON.
5. Mã hóa toàn bộ 20 quy tắc trò chơi (`R-01` đến `R-20`) làm cơ sở truy vết hai chiều cho code và test sau này.

---

## 2. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

| Mã Việc | Hạng mục công việc (Theo Bước 01 PDF) | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Đầu ra kiểm chứng |
|:---:|---|:---:|:---:|:---:|---|
| **W1.1** | Xây dựng sơ đồ Mindmap 4W1H và bảng ánh xạ 5 thành phần phần mềm. | **Đình Huy (TV1)** | 3h | P0 | File `docs/plan/diagrams/mindmap_4w1h.md` |
| **W1.2** | Viết GDD 2 trang: Thể loại, cơ chế cốt lõi, điều khiển phím, bối cảnh cốt truyện. | **Công Huy (TV2)** | 4h | P0 | Mục 1 & 2 trong `docs/plan/01_CONTENT_BIBLE.md` |
| **W1.3** | Đặc tả 20 luật chơi mã hóa `R-01..R-20` (sát thương, i-frames, bẫy, rơi đồ, thắng/thua). | **Đình Huy (TV1)** | 5h | W1.2 | Mục 8 trong `docs/plan/01_CONTENT_BIBLE.md` |
| **W1.4** | Thiết kế sơ đồ kiến trúc Game Loop 60Hz (Accumulator loop $\Delta t = 1/60$s) tách bạch Core vs Engine. | **Đình Huy (TV1)** | 4h | W1.1 | Sơ đồ Mermaid trong GDD |
| **W1.5** | Lập danh mục sơ bộ 10 PCU và ma trận điều kiện thắng/thua từng tầng. | **Công Huy (TV2)** | 4h | W1.2 | Bảng PCU trong `01_CONTENT_BIBLE.md` |
| **W1.6** | Định nghĩa 4 bộ Output chuẩn $A, B, C, D$ làm đầu vào cho Bước 02 Phân tích. | **Cả hai** | 3h | W1.3, W1.5 | Bảng tổng hợp Output A-D |

---

## 3. CÁC ĐẦU RA BẮT BUỘC (DELIVERABLES)
- `docs/plan/01_CONTENT_BIBLE.md` (GDD hoàn chỉnh, 20 luật R-xx, 10 PCU, bối cảnh).
- Sơ đồ Mindmap 4W1H (File text/markdown hoặc ảnh SVG).
- Sơ đồ luồng Game Loop Fixed Timestep ($\Delta t = 16.67$ms).
- Bảng tổng hợp 4 Output A–D (Slide 5 PDF).

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 1.1**: Quy mô nội dung chơi & Use Case cốt lõi (Khởi tạo danh mục 10 PCU).
- **Tiêu chí 3.1**: Phân rã luật chơi, Use Case & Game Loop ↔ Code (Ánh xạ luật sang `RuleEngine` và vòng lặp `FixedUpdate(dt)`).

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Mỗi luật trong bảng 20 quy tắc $R\text{-}xx$ phải được chỉ định rõ sẽ nằm trong lớp Core nào (VD: $R\text{-}03 \rightarrow \text{Health}$, $R\text{-}01 \rightarrow \text{CombatResolver}$, $R\text{-}07 \rightarrow \text{CollisionDetector}$).
- **Chiều ngược [$B \rightarrow A$]**: Đọc bất kỳ nhánh điều kiện nào trong đặc tả Game Loop có thể truy ngược về đúng luật thắng/thua trong GDD mà không có bất kỳ "luật ẩn" nào nằm trong hiển thị.

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] GDD hoàn chỉnh không quá 3 trang, mô tả rõ cơ chế điều khiển: WASD di chuyển, Chuột ngắm/đánh, Space lướt né, 1–4 dùng skill, Tab mở túi, E tương tác, Esc tạm dừng.
- [x] Đủ 20 quy tắc trò chơi được đánh số `R-01` đến `R-20`, có công thức toán cụ thể cho sát thương, điểm số và xác suất.
- [x] Sơ đồ Game Loop phân định rõ ranh giới: bên nào là `Pure C# (Core)` và bên nào là `Unity Engine (Adapter)`.
- [x] Commit đầy đủ tài liệu lên nhánh `dev` với ít nhất 4 commits rải trong Tuần 1.

---

## 7. CỔNG HIỂU SÂU (COMPREHENSION GATE P1)
1. **Câu hỏi Truy vết ($B \rightarrow A$)**: *"Chỉ vào sơ đồ Game Loop, giải thích vì sao khối `Process Input` lại nằm ở Engine trong khi `FixedUpdate(dt)` lại nằm ở Pure C#?"*
   - *Trả lời*: Vì bàn phím/chuột vật lý phụ thuộc vào hệ điều hành và engine Unity (`Input.GetAxisRaw`). Bằng cách trừu tượng hóa qua `IInputProvider`, engine đóng gói tín hiệu thành `InputCommand` thuần dữ liệu gửi vào Core. Nhờ đó, Core có thể chạy trong môi trường headless test hoặc nhận tín hiệu từ file Replay mà không cần phần cứng thật.
2. **Câu hỏi Biến động ($\Delta$)**: *"Nếu ta muốn tăng tốc độ trò chơi lên $1.5\times$ mà không làm giật đồ họa hay thay đổi thuật toán vật lý, ta sửa ở đâu?"*
   - *Trả lời*: Chỉ cần nhân biến thời gian delta tích lũy `acc += frameDt * 1.5f` trong Game Loop. Tần số tick vật lý `dt = 0.0167s` vẫn giữ nguyên nên các phép tính va chạm và tìm đường hoàn toàn không bị ảnh hưởng.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương 1: Khảo sát Hiện trạng & Đặc tả Trò chơi**: Bao gồm toàn bộ Mindmap 4W1H, GDD, Bảng 20 luật chơi và Kiến trúc Game Loop.

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Viết luật chơi quá phức tạp vượt quá khả năng cài đặt trong 5 tuần (ví dụ: đòi hỏi cơ chế vật lý ragdoll hoặc multiplayer).
- **Biện pháp xử lý**: Giới hạn toàn bộ chuyển động ở mức lưới 2D AABB trên mặt phẳng cố định; mọi tương tác giữa các thực thể quy về hệ thống Máu, Trạng thái (Status) và Sự kiện (Observer).
