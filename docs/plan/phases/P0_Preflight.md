# PHASE 0: CHUẨN BỊ NỀN TẢNG (PREFLIGHT PHASE)
**Mã giai đoạn:** P0 | **Thời gian thực hiện:** Đầu Tuần 1 (T1)  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2)**  
**Tiêu chuẩn áp dụng:** "Hot Tip K69" (Slide 3 PDF v2026) & Tiêu chí 1.4, 4.3 Checklist K69  
**Mục tiêu cốt lõi:** Khóa cứng nền tảng Engine (Unity 2D URP), cấu trúc Solution 3 tầng, quy tắc Git Branch/PR chéo, và thiết lập đường ống kiểm thử tự động Headless chạy độc lập với Scene ngay từ ngày đầu tiên.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. Thiết lập cây thư mục repo chuẩn theo mẫu `TenGame_NhomXX_v2026` quy định tại Slide 28 PDF.
2. Cấu hình Assembly Definition `Game.Core.asmdef` với thiết lập cấm cứng tham chiếu `UnityEngine` để bảo vệ tính thuần C# của Core Domain.
3. Thiết lập tệp `.gitignore` chuẩn cho Unity và Visual Studio, ngăn chặn hoàn toàn việc đẩy thư mục build rác lên kho lưu trữ Git.
4. Chạy thành công 01 Unit Test mẫu rỗng trong Unity Test Runner (EditMode) để xác nhận đường ống kiểm thử độc lập hoạt động trơn tru.
5. Khởi tạo kho theo dõi minh chứng `docs/checklist/evidence_tracker.md` và kiểm kê tài nguyên asset có sẵn.

---

## 2. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

| Mã Việc | Nhiệm vụ kỹ thuật chi tiết | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Đầu ra kiểm chứng |
|:---:|---|:---:|:---:|:---:|---|
| **W0.1** | Khởi tạo Git repo, tạo file `.gitignore` chuẩn Unity/VS và branch `main`, `dev`. | **Đình Huy (TV1)** | 2h | Không | Repo Git sạch, không có file rác |
| **W0.2** | Cấu hình cây thư mục `src/Game.Core`, `src/Game.Presentation`, `src/Game.Infrastructure`, `tests/`. | **Công Huy (TV2)** | 2h | W0.1 | Cây thư mục khớp Slide 28 PDF |
| **W0.3** | Tạo file `Game.Core.asmdef` (bỏ chọn `Auto Referenced`, cấm engine) và `Game.Core.Tests.asmdef`. | **Đình Huy (TV1)** | 3h | W0.2 | Assembly Core thuần C# |
| **W0.4** | Viết 01 Unit Test rỗng `SanityCheckTests.cs` kiểm tra `Assert.IsTrue(true)` trong Test Runner EditMode. | **Đình Huy (TV1)** | 2h | W0.3 | Test xanh 100% trong $<0.5$s |
| **W0.5** | Kiểm kê tài nguyên đồ họa từ 6 gói asset pack vào `docs/ref/asset_coordination_guide.md`. | **Công Huy (TV2)** | 4h | Không | Bảng kiểm kê sprite & atlas JSON |
| **W0.6** | Thiết lập mẫu Pull Request (PR Template) và quy tắc review chéo bắt buộc giữa Đình Huy và Công Huy. | **Công Huy (TV2)** | 2h | W0.1 | File `.github/pull_request_template.md` |
| **W0.7** | Khởi tạo bảng theo dõi minh chứng hai chiều `docs/checklist/evidence_tracker.md`. | **Cả hai** | 2h | W0.2 | Bảng 20 tiêu chí sẵn sàng ghi chép |

---

## 3. CÁC ĐẦU RA BẮT BUỘC (DELIVERABLES)
- `.gitignore` (Cấm `Library/`, `Temp/`, `bin/`, `obj/`, `Build/`).
- `src/Game.Core/Game.Core.asmdef` (Không chứa `UnityEngine`).
- `tests/Game.Core.Tests/SanityCheckTests.cs` (Unit Test chạy độc lập).
- `docs/ref/asset_inventory.md` (Thống kê 108 sprite, 482 animation, 283 tile).
- `docs/checklist/evidence_tracker.md` (Bảng theo dõi 20 tiêu chí).

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 1.4**: Đóng gói Clean-room & Git nhóm (Khởi tạo commit đầu tiên đúng tuần 1, .gitignore chuẩn).
- **Tiêu chí 4.3**: Kiến trúc phân tầng & Kiểm toán using (Assembly Core độc lập hoàn toàn khỏi Engine).

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Từ sơ đồ tổ chức thư mục trong Slide 28 PDF $\rightarrow$ Cài đặt chính xác các thư mục `src/Game.Core`, `src/Game.Presentation`, `src/Game.Data`.
- **Chiều ngược [$B \rightarrow A$]**: Mở file `.asmdef` trên IDE xác nhận `references: []` không chứa thư viện đồ họa $\rightarrow$ Chạy lệnh Test Runner thấy test pass mà không cần nạp Scene.

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] Kho Git được khởi tạo với commit đầu tiên của Tuần 1 mang thông điệp chuẩn: `[Cả hai]: feat(preflight): initial repo skeleton` (thiết lập chuẩn tiền tố `[Đình Huy]:` và `[Công Huy]:` cho toàn bộ dự án).
- [x] Tệp `.gitignore` chặn được các thư mục `Library/` và `Temp/` khi gõ `git status`.
- [x] `Game.Core` biên dịch sạch, nếu thử gõ `using UnityEngine;` sẽ báo lỗi biên dịch ngay lập tức.
- [x] Test Runner trong Unity hiển thị 1 test xanh lá cây (PASS) tại tab EditMode.
- [x] Nhánh `dev` được cấu hình làm nhánh mặc định cho các PR.

---

## 7. CỔNG HIỂU SÂU (COMPREHENSION GATE P0)
1. **Câu hỏi B $\rightarrow$ A**: *"Tại sao ta phải tách thư mục `Game.Core` thành một Assembly Definition riêng mà không để chung trong thư mục Scripts mặc định của Unity?"*
   - *Trả lời*: Để ép trình biên dịch C# cấm tham chiếu tới `UnityEngine.dll`. Điều này buộc toàn bộ logic miền phải thuần OOP, giúp chạy Unit Test trong tích tắc mà không cần khởi động engine Unity, đồng thời chống lại thói quen viết logic gameplay vào `Update()` của MonoBehaviour.
2. **Câu hỏi Đánh đổi (W)**: *"Nếu không dùng Assembly Definition thì chi phí kiểm thử tự động tăng lên như thế nào?"*
   - *Trả lời*: Nếu không tách, mọi bài test đều phải chạy ở PlayMode, đòi hỏi nạp toàn bộ Scene, đồ họa và assets, làm thời gian chạy test tăng từ vài giây lên vài phút, cản trở quy trình TDD.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương chuẩn bị**: Nội dung mục *Giới thiệu môi trường & Cấu trúc mã nguồn* trong Báo cáo tổng kết BTL.

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Thành viên chưa cài đặt đúng phiên bản Unity 6 (`6000.0.85f2`) hoặc thiếu module build Windows Standalone.
- **Biện pháp xử lý**: Sử dụng Unity Hub đồng bộ phiên bản Unity 6 chính xác (đường dẫn đã xác nhận trên máy: `D:\Unity\Editor\6000.0.85f2\Editor\Unity.exe`); TV1 hỗ trợ TV2 cấu hình Unity Test Framework qua TeamViewer/AnyDesk trong ngày đầu tuần.
