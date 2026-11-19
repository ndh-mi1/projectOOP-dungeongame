# PHASE 2: BƯỚC 02 – PHÂN TÍCH THỰC THỂ, USE CASE & THUẬT TOÁN (DOMAIN ANALYSIS)
**Mã giai đoạn:** P2 | **Thời gian thực hiện:** Cuối Tuần 1 đến Tuần 2 (T1–T2)  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Entities & Combat Algorithms)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead Use Cases & World Algorithms)**  
**Tiêu chuẩn áp dụng:** Bước 02 PDF v2026 (Trang 8–13) & Tiêu chí 1.2, 1.3, 3.1, 3.2, 3.3 Checklist K69  
**Mục tiêu cốt lõi:** Chuyển hóa GDD thành Cây chức năng $F1.x$, Sơ đồ Use Case 10 ca chia đều 2 TV, Từ điển Miền 18 lớp thực chất (chống Anemic Model) và Giả mã chi tiết cho 6 thuật toán phi tầm thường có phân tích $O(\cdot)$ và ca biên.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. Xây dựng Cây phân cấp chức năng $F1.1$ đến $F1.4$ và Sơ đồ Use Case chuẩn UML 2.5 với 2 tác nhân chính (`Player`, `System/AI`).
2. Lập Bảng ánh xạ 1-1 giữa 10 Use Case sang 10 Scene/Form giao diện và Lớp điều phối chuyên trách (`Controller`).
3. Hoàn thiện Từ điển Lớp Thực thể (Data Dictionary) cho 18 lớp thực chất: định nghĩa rõ Tên, Loại, Thuộc tính, Phương thức nghiệp vụ, Ràng buộc bất biến và lý do tách lớp theo SRP.
4. Viết giả mã chi tiết từng bước (Step-by-step Pseudocode), phân tích độ phức tạp thời gian/không gian $O(\cdot)$ và các ca biên cực đoan cho 6 thuật toán (3 Nguyễn Đình Huy / 3 Nguyễn Công Huy).
5. Thiết lập Bảng thông số cân bằng khởi tạo (Balancing Table) và Schema dữ liệu cho tệp cấu hình tầng `floors.json`.

---

## 2. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

| Mã Việc | Hạng mục công việc (Theo Bước 02 PDF) | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Đầu ra kiểm chứng |
|:---:|---|:---:|:---:|:---:|---|
| **W2.1** | Vẽ Cây chức năng $F1.x$ và Sơ đồ Use Case Diagram (10 Use Case). | **Công Huy (TV2)** | 4h | P1 | File PlantUML `docs/uml/usecase_diagram.puml` |
| **W2.2** | Lập Bảng ánh xạ 10 UC $\leftrightarrow$ Scene/Form $\leftrightarrow$ Controller $\leftrightarrow$ Owner. | **Công Huy (TV2)** | 3h | W2.1 | Bảng trong `01_CONTENT_BIBLE.md` |
| **W2.3** | Xây dựng Từ điển 18 Lớp thực chất, rà soát loại bỏ triệt để Anemic Model. | **Đình Huy (TV1)** | 6h | P1 | Bảng từ điển trong `02_OOP_BLUEPRINT.md` |
| **W2.4** | Viết giả mã, phân tích $O(\cdot)$ và ca biên cho 3 thuật toán của TV1 (A*, Swept AABB, LOS). | **Đình Huy (TV1)** | 6h | W2.3 | Giả mã chi tiết trong tài liệu thiết kế |
| **W2.5** | Viết giả mã, phân tích $O(\cdot)$ và ca biên cho 3 thuật toán của TV2 (BSP+MST, Blob47, LootRoll). | **Công Huy (TV2)** | 6h | W2.3 | Giả mã chi tiết trong tài liệu thiết kế |
| **W2.6** | Xây dựng Balancing Table khởi tạo và Schema JSON cho `floors.json`, `enemies.json`. | **Cả hai** | 4h | W2.3 | Tệp mẫu JSON trong `docs/ref/schemas/` |

---

## 3. CÁC ĐẦU RA BẮT BUỘC (DELIVERABLES)
- Sơ đồ Cây chức năng & Sơ đồ Use Case Diagram (`usecase_diagram.puml`).
- Bảng ánh xạ 10 UC sang Scene và Controller.
- Bảng Từ điển Lớp Thực thể (18 lớp có phương thức nghiệp vụ, không có lớp DTO rỗng).
- Bộ Giả mã 6 Thuật toán kèm phân tích độ phức tạp thời gian/bộ nhớ và ca biên.
- Bảng thông số cân bằng khởi tạo (Balancing Table) cho quái, vũ khí và kỹ năng.

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 1.2**: Save/Load đồng bộ, dữ liệu hóa màn chơi & Bảng điểm.
- **Tiêu chí 1.3**: Chiều sâu nội dung & Cân bằng độ khó (Balancing Table).
- **Tiêu chí 3.2**: Mô hình miền, thực thể & Dữ liệu hóa $\leftrightarrow$ Entity/VO/Enum ($\ge 13$–16 lớp).
- **Tiêu chí 3.3**: Thuật toán chơi / AI ($\ge 4$, $\ge 2$/TV) & Ranh giới tác nhân.

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Mỗi bước trong giả mã thuật toán phải tương ứng 1-1 với một khối lệnh hoặc phương thức con trong lớp thực thi (VD: bước `ReconstructPath` trong giả mã A* ánh xạ trực tiếp sang hàm `ReconstructPath(Node node)` trong `Pathfinder.cs`).
- **Chiều ngược [$B \rightarrow A$]**: Chỉ vào bất kỳ thuộc tính nào trong từ điển lớp (ví dụ: `_currentHp` trong `Health`) phải giải thích được lý do vì sao nó là `private` và bất biến nào ràng buộc nó (chống đóng gói giả).

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] Sơ đồ Use Case thể hiện đầy đủ 10 Use Case phân chia cân bằng: 5 UC cho TV1 (chiến đấu, di chuyển, loop, boss, replay) và 5 UC cho TV2 (chọn tướng, kho đồ, shop, save/load, điểm cao).
- [x] Toàn bộ 18 lớp trong Từ điển lớp đều có ít nhất 1 phương thức xử lý logic nghiệp vụ hoặc ràng buộc kiểm tra đầu vào (100% không dính bẫy Anemic Model).
- [x] Cả 6 thuật toán đều có phân tích $O(\cdot)$ bằng KaTeX math và có kịch bản xử lý ca biên cực đoan.
- [x] Bảng cân bằng chứng minh được tất cả 10 PCU đều có thể vượt qua (khả thắng 100%).

---

## 7. CỔNG HIỂU SÂU (COMPREHENSION GATE P2)
1. **Câu hỏi B $\rightarrow$ A (TV1)**: *"Chỉ vào thuật toán Swept AABB, giải thích vì sao ta phải tính toán `entryTime` và `exitTime` trên cả 2 trục X và Y thay vì chỉ kiểm tra giao nhau đơn giản?"*
   - *Trả lời*: Vì trong thời gian thực, thực thể di chuyển một đoạn đường $\vec{v} \cdot \Delta t$. Giao nhau đơn giản chỉ kiểm tra vị trí đích, nếu vật cản mỏng hơn vận tốc thì frame trước ở bên này tường, frame sau đã ở bên kia tường (xuyên tường). Swept AABB tính thời điểm va chạm sớm nhất $t_{\text{entry}} = \max(t_{x\text{Entry}}, t_{y\text{Entry}})$ và muộn nhất $t_{\text{exit}} = \min(t_{x\text{Exit}}, t_{y\text{Exit}})$. Nếu $t_{\text{entry}} < t_{\text{exit}}$ và $t_{\text{entry}} \in [0, 1]$, va chạm chắc chắn xảy ra dọc theo đường bay và ta chặn thực thể tại đúng thời điểm $t_{\text{entry}}$.
2. **Câu hỏi B $\rightarrow$ A (TV2)**: *"Trong thuật toán sinh hầm ngục, tại sao sau khi tạo cây khung nhỏ nhất (MST) bạn lại phải thêm lại 15% số cạnh bị loại bỏ?"*
   - *Trả lời*: Cây khung nhỏ nhất (MST) đảm bảo mọi phòng đều liên thông với số cạnh ít nhất, nhưng nó là một cây (Tree - không có chu trình), khiến người chơi chỉ có một con đường độc đạo duy nhất. Thêm lại $15\%$ số cạnh sẽ tạo ra các chu trình vòng lặp (Loops) và đường tắt, mang lại cảm giác khám phá đa hướng đặc trưng của thể loại Roguelike.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương 2: Phân tích Trò chơi & Thuật toán**: Trình bày toàn bộ Cây chức năng, Sơ đồ Use Case, Từ điển miền 18 lớp, Giả mã 6 thuật toán và Bảng thông số cân bằng.

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Thuật toán sinh tầng BSP phức tạp làm kéo dài thời gian phân tích, ảnh hưởng tới tiến độ Tuần 2.
- **Biện pháp xử lý**: Giới hạn cấu trúc BSP ở mức chia 3 cấp đệ quy ($2^3 = 8$ phòng cơ bản); nếu sinh map bị lỗi phân mảnh, dự phòng hàm sinh map cố định từ mẫu file JSON (`level_templates/`).
