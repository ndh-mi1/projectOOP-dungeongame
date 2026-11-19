# PHASE 3: BƯỚC 03 – THIẾT KẾ HƯỚNG ĐỐI TƯỢNG, UML & DESIGN PATTERN (DETAILED DESIGN)
**Mã giai đoạn:** P3 | **Thời gian thực hiện:** Tuần 2 (T2)  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat UML & Patterns)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead World UML & UI Wireframes)**  
**Tiêu chuẩn áp dụng:** Bước 03 PDF v2026 (Trang 14–23) & Tiêu chí 4.1–4.6, 1.2 Checklist K69  
**Mục tiêu cốt lõi:** Chuẩn hóa toàn bộ bản thiết kế kỹ thuật trước khi gõ code: 01 UML Class Diagram tổng thể $\ge 24$ lớp (đủ 6 quan hệ), 5 Sơ đồ động khớp Call Stack Debugger, 5 Mẫu thiết kế GoF xử lý Code Smell thực tế, Package Diagram 3 tầng và Hợp đồng phân hệ Đình Huy $\leftrightarrow$ Công Huy được khóa cứng.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. Vẽ 01 Sơ đồ lớp tổng thể (UML Class Diagram) duy nhất gồm 28 lớp thực chất bằng PlantUML, thể hiện đầy đủ thuộc tính, chữ ký phương thức chuẩn C# và 6 mối quan hệ UML chuẩn ký hiệu.
2. Thiết kế 5 Sơ đồ động: 3 State Machine (Vòng đời Game, FSM Actor có Guard chặn, FSM Phòng) và 2 Sequence Diagram (1 chu kỳ FixedUpdate, 1 luồng xuyên phân hệ Đình Huy $\leftrightarrow$ Công Huy).
3. Chuẩn hóa 5 mẫu thiết kế GoF (State, Strategy, Factory Method, Decorator, Observer): lập bảng phân vai GoF Roles, xác định mùi code nếu bỏ mẫu, và chuẩn bị sẵn bộ đề Live OCP Drill Kit cho từng TV.
4. Thiết kế Package Diagram 3 tầng sạch và đặc tả Hợp đồng Interface/Event kết nối giữa Đình Huy và Công Huy, bảo đảm triệt tiêu hoàn toàn phụ thuộc vòng (Circular Dependency).
5. Thiết kế Wireframe cho 10 màn hình UI/HUD và cấu trúc DTO thống nhất cho hệ thống Lưu/Tải (`save_slot_1.json`, `highscore.json`).

---

## 2. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

| Mã Việc | Hạng mục công việc (Theo Bước 03 PDF) | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Đầu ra kiểm chứng |
|:---:|---|:---:|:---:|:---:|---|
| **W3.1** | Dựng mã nguồn PlantUML cho 01 Sơ đồ Lớp Tổng 28 lớp đầy đủ 6 quan hệ. | **Cả hai** | 8h | P2 | File `docs/uml/class_diagram_full.puml` |
| **W3.2** | Thiết kế 3 Sơ đồ Trạng thái State Machine có Guard chặn bằng PlantUML. | **Đình Huy (TV1)** | 4h | P2 | File `docs/uml/state_machines.puml` |
| **W3.3** | Thiết kế 2 Sơ đồ Tuần tự Sequence Diagram (trong đó có luồng xuyên TV1 $\leftrightarrow$ TV2). | **Đình Huy (TV1)** | 4h | P2 | File `docs/uml/sequence_diagrams.puml` |
| **W3.4** | Lập Bảng vai trò GoF Roles, phân tích mùi code và chuẩn bị kịch bản Live OCP Drill. | **Cả hai** | 4h | W3.1 | Mục 5 trong `02_OOP_BLUEPRINT.md` |
| **W3.5** | Thiết kế Package Diagram 3 tầng và lập Bảng Hợp đồng Interface/Event TV1 $\leftrightarrow$ TV2. | **Cả hai** | 4h | W3.1 | File `docs/uml/package_diagram.puml` |
| **W3.6** | Vẽ Wireframe chi tiết cho toàn bộ 10 màn hình UI/HUD và sơ đồ luồng Scene Flow. | **Công Huy (TV2)** | 5h | P2 | Mục 2 & 3 trong `03_UIUX_ART_DIRECTION.md`|
| **W3.7** | Chuẩn hóa Schema JSON cho `save_slot_1.json`, `highscore.json`, `profile.json`. | **Công Huy (TV2)** | 3h | P2 | Các file schema trong `docs/ref/schemas/` |

---

## 3. CÁC ĐẦU RA BẮT BUỘC (DELIVERABLES)
- File PlantUML Sơ đồ Lớp tổng thể: `docs/uml/class_diagram_full.puml` (xuất ra `class_diagram_full.svg`).
- File PlantUML Sơ đồ Động: `state_machines.puml`, `sequence_diagrams.puml`.
- File PlantUML Package Diagram: `package_diagram.puml`.
- Bộ Wireframe 10 màn hình UI kèm mô tả luồng điều hướng phím.
- Bản đặc tả Hợp đồng Interface TV1 $\leftrightarrow$ TV2 (Đã ký duyệt bởi cả 2 TV).
- Bộ Schemas JSON dữ liệu màn chơi và Save/Load dùng chung.

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 4.1**: Sơ đồ lớp UML tổng ($\ge 20$–24 lớp) & 6 quan hệ UML $\leftrightarrow$ Code.
- **Tiêu chí 4.2**: Sơ đồ hành vi động ($\ge 4$ sơ đồ) $\leftrightarrow$ Kiểm chứng Debugger.
- **Tiêu chí 4.3**: Kiến trúc phân tầng ($\ge 3$ tầng) & Kiểm toán using.
- **Tiêu chí 4.5**: SOLID cấu trúc (SRP, ISP, DIP) & Hợp đồng TV1 $\leftrightarrow$ TV2.
- **Tiêu chí 4.6**: GoF Design Pattern ($\ge 3$ mẫu), Live OCP & Đánh đổi (W).

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Tên lớp, visibility (`+`, `-`, `#`), kiểu dữ liệu và chữ ký tham số trên bản vẽ PlantUML phải khớp 1-1 với khai báo mã nguồn C# khi cài đặt ở Phase 4 (0 drift, 0 lệch tên hàm).
- **Chiều ngược [$B \rightarrow A$]**: Kiểm toán ngược 3 lớp ngẫu nhiên bất kỳ trên IDE: các thuộc tính, mối quan hệ kế thừa và hợp thành phải tìm thấy chính xác trên bản vẽ UML tổng.

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] 01 Sơ đồ UML Class tổng thể duy nhất thể hiện đủ 28 lớp, có màu sắc phân biệt theo package và ghi rõ quyền sở hữu TV1 / TV2.
- [x] Đủ 6 mối quan hệ UML chuẩn: Kế thừa (`<|--`), Hiện thực (`<|..`), Hợp thành (`*--`), Tụ hợp (`o--`), Liên kết (`-->`), Phụ thuộc (`..>`), có ghi rõ bội số (Multiplicity: `1`, `0..*`, `1..*`).
- [x] Sơ đồ Sequence xuyên phân hệ thể hiện rõ chuỗi gọi hàm đi qua `GameEventBus` kết nối giữa TV1 và TV2 mà không có phụ thuộc vòng.
- [x] Cả 2 thành viên đều nắm vững vai trò GoF Roles của phân hệ mình và sẵn sàng cho bài thi Live OCP 2–3 phút.
- [x] Bản Hợp đồng Interface TV1 $\leftrightarrow$ TV2 được cả 2 thành viên cam kết không tự ý thay đổi chữ ký hàm khi chưa có sự đồng thuận.

---

## 7. CỔNG HIỂU SÂU (COMPREHENSION GATE P3)
1. **Câu hỏi B $\rightarrow$ A (Kiểm toán 6 quan hệ UML)**:
   - *Hỏi*: *"Chỉ vào sơ đồ lớp, tại sao quan hệ giữa `Floor` và `RoomNode` là Composition (Hợp thành chặt), trong khi giữa `Inventory` và `Item` lại là Aggregation (Tụ hợp lỏng)?"*
   - *Trả lời*: Vì vòng đời của `RoomNode` gắn chặt không thể tách rời khỏi `Floor`. `RoomNode` được khởi tạo bên trong constructor của `Floor`; khi tầng bị hủy, toàn bộ các phòng bị hủy cùng lúc. Ngược lại, `Item` được đưa vào `Inventory` từ bên ngoài qua hàm `AddItem(Item item)`. Khi túi đồ bị hủy hoặc người chơi vứt đồ ra sàn, đối tượng `Item` đó vẫn tiếp tục tồn tại độc lập trong thế giới game.
2. **Câu hỏi Đánh đổi (W - Observer Pattern)**:
   - *Hỏi*: *"Tại sao ta dùng Observer Pattern (`GameEventBus`) làm cầu nối giữa TV1 và TV2 mà không để TV1 gọi thẳng các lớp của TV2 cho nhanh? Nêu 2 chi phí phải trả?"*
   - *Trả lời*: Nếu gọi thẳng, phân hệ Combat của TV1 sẽ phụ thuộc trực tiếp vào phân hệ Loot/UI/Audio của TV2, tạo ra Circular Dependency khiến hai người không thể làm việc và chạy Unit Test độc lập. Hai chi phí phải trả: (1) Khó debug dò vết vì luồng thực thi bị gián đoạn qua sự kiện; (2) Rủi ro Memory Leak nếu bên nhận quên Unsubscribe khi thực thể bị hủy.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương 3: Thiết kế Hướng đối tượng & Mẫu thiết kế**: Trình bày UML Class tổng thể, 6 quan hệ UML, 5 Sơ đồ động, Bảng vai trò 5 GoF patterns, Package Diagram và Wireframe UI.

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Thiết kế UML quá cồng kềnh với nhiều pattern phụ (Command, Memento, Flyweight) gây quá tải thời gian cài đặt ở Phase 4.
- **Biện pháp xử lý**: Đóng băng thiết kế ở đúng **5 mẫu GoF cốt lõi bắt buộc**; chuyển Command Pattern và Flyweight sang dạng "Tùy chọn mở rộng (Optional)" nếu còn thừa thời gian ở Tuần 4.
