# PHASE 5: BƯỚC 05 – KIỂM THỬ TỰ ĐỘNG, ĐÓNG GÓI CLEAN-ROOM & DIỄN TẬP BẢO VỆ
**Mã giai đoạn:** P5 | **Thời gian thực hiện:** Tuần 4 đến Tuần 5 (T4–T5)  
**Tiêu chuẩn áp dụng:** Bước 05 PDF v2026 (Trang 30–32) & Tiêu chí 1.4, 5.1, 5.2, 5.3 Checklist K69  
**Nhân sự thực hiện:**
- **Nguyễn Đình Huy (MSSV: 202418918 - TV1)**: 12 Unit Tests Combat/AI/Physics, 5 Integration Tests va chạm/vòng lặp, Mutation Check, Playtest chéo, Git audit.
- **Nguyễn Công Huy (MSSV: 202418914 - TV2)**: 12 Unit Tests World/Item/Storage, 5 Integration Tests Save/Load round-trip/UI, Bug Log quản lý, Clean-Room Packaging, Tài liệu nghiệm thu.

**Mục tiêu cốt lõi:** Chạy thành công toàn bộ suite $\ge 34$ Automated Tests đạt 100% PASS, thực hiện Playtest chéo 20 ca, khóa 8 lỗi Bug Log bằng Regression Tests, đóng gói bản phát hành Clean-room $\le 3$ phút trên máy chấm sạch, và diễn tập thuần thục quy trình vấn đáp 10–15 phút mở cả 2 cổng $G_{Product}$ và $G_{Epistemic}$.  

---

## 1. MỤC TIÊU KỸ THUẬT CỤ THỂ
1. Hoàn thiện Ma trận truy vết hai chiều: Luật chơi ($R\text{-}xx$) $\leftrightarrow$ Mã Test ($TC\text{-}xx$) $\leftrightarrow$ Mã nguồn C# ($Class.Method$).
2. Đạt $\ge 34$ Automated Tests (24 Unit Tests độc lập + 10 Integration Tests liên phân hệ), thực thi trong $<2$ giây ở EditMode.
3. Vượt qua bài kiểm tra Mutation Testing nội bộ: 100% các đột biến cố ý tại biên Máu, va chạm AABB và lưu file đều bị test bắt chuyển FAIL đỏ (tiêu diệt đột biến).
4. Thực hiện đủ 20 kịch bản Playtest chéo (Đình Huy tìm lỗi Công Huy và ngược lại); ghi nhận ít nhất 8 lỗi vào Bug Log 6 cột chuẩn, có 4 ca Regression Test khóa lỗi vĩnh viễn.
5. Đóng gói sản phẩm Clean-room: bản build Windows Standalone (`.exe x64`), tệp `README.md` hướng dẫn chi tiết, Slide thuyết trình và Video Demo 3–5 phút.
6. Tổ chức 2 buổi diễn tập bảo vệ thử 10–15 phút theo quy trình chuẩn của TS. Trần Ngọc Thăng, vượt qua các bài thử nghiệm $\Delta$, $W$, Live OCP và Fault-Injection.

---

## 2. DANH SÁCH CÔNG VIỆC CHI TIẾT (WBS)

| Mã Việc | Hạng mục công việc (Theo Bước 05 PDF) | Thành viên Phụ trách | Ước lượng (Giờ) | Phụ thuộc | Đầu ra kiểm chứng |
|:---:|---|:---:|:---:|:---:|---|
| **W5.1** | Lập Ma trận truy vết kiểm thử hai chiều phủ đủ EP, BVA, State cấm và Ngoại lệ. | **Cả hai (Đình Huy & Công Huy)** | 4h | P4 | File `docs/test/traceability_matrix.md` |
| **W5.2** | Cài đặt và chuẩn hóa 24 Unit Tests Pure Logic chuẩn AAA dùng Mock/Fake chéo. | **Cả hai (Đình Huy 12 / Công Huy 12)** | 8h | P4 | Toàn bộ file trong `tests/Game.Core.Tests/` |
| **W5.3** | Cài đặt 10 Integration Tests phủ Save/Load round-trip, va chạm góc và xóa Observer. | **Cả hai (Đình Huy 5 / Công Huy 5)** | 6h | W5.2 | File `tests/CrossModuleIntegrationTests.cs` |
| **W5.4** | Tiến hành quy trình Playtest chéo 20 kịch bản; lập bảng ghi nhận Bug Log 8 lỗi. | **Cả hai (Đình Huy & Công Huy)** | 8h | P4 | File `docs/test/bug_log.md` |
| **W5.5** | Viết 4 Regression Tests từ các lỗi nghiêm trọng trong Bug Log; xác nhận sửa xanh. | **Cả hai (Đình Huy 2 / Công Huy 2)** | 4h | W5.4 | Thư mục `tests/Regression/` |
| **W5.6** | Chạy quy trình Mutation Check nội bộ trên 3 điểm đột biến cốt lõi. | **Cả hai (Đình Huy & Công Huy)** | 3h | W5.3 | Báo cáo kiểm định trong `04_TEST_QA_GIT_PLAN.md`|
| **W5.7** | Kiểm toán kho Git: rà soát `git shortlog -sn`, tính tỷ lệ blame logic, rà sạch `.gitignore`. | **Nguyễn Đình Huy (TV1)** | 2h | P4 | Báo cáo kiểm toán Git |
| **W5.8** | Đóng gói bản build Windows `.exe x64`, thử nghiệm Clean-room trên máy tính sạch $\le 3$p. | **Nguyễn Công Huy (TV2)** | 4h | P4 | Thư mục `build/Release_v2026/` |
| **W5.9** | Hoàn thiện tài liệu bàn giao: `README.md`, `CREDITS.md` CC0, Slide bảo vệ, Video Demo 3–5p. | **Cả hai (Đình Huy & Công Huy)** | 8h | W5.8 | Video mp4, Slide PDF, README |
| **W5.10**| Soạn thảo toàn bộ Báo cáo tổng kết PDF 6 chương + Phụ lục từ mẫu template. | **Cả hai (Đình Huy & Công Huy)** | 12h | Toàn bộ | File `BaoCao_BTL_Game_NhomXX_v2026.pdf` |
| **W5.11**| Tổ chức 2 buổi diễn tập vấn đáp nhanh 10–15 phút theo quy trình chuẩn K69. | **Cả hai (Đình Huy & Công Huy)** | 4h | W5.10 | Biên bản diễn tập trong `docs/understanding/` |

---

## 3. CÁC ĐẦU RA BẮT BUỘC (DELIVERABLES)
- Bộ mã nguồn kiểm thử tự động: 34 tests 100% PASS trong NUnit EditMode.
- Bảng Playtest Matrix 20 ca và Bug Log 8 lỗi chuẩn 6 cột.
- Gói phát hành Clean-room: `CrystalDepths_v2026_x64.zip` giải nén là chạy ngay trên máy sạch không cần cài Unity hay VS.
- Tài liệu hướng dẫn `README.md` (cấu hình, phím điều khiển, lệnh chạy test qua CLI).
- Tài liệu công nhận bản quyền tài nguyên `CREDITS.md` chứng minh 100% CC0 / SIL Open Font.
- Bộ Slide thuyết trình (15–20 slides) và Video Demo chất lượng cao (3–5 phút có lồng tiếng/phụ đề).
- Báo cáo Bài tập lớn chính thức định dạng PDF (25–40 trang chuẩn mẫu SAMI - HUST).

---

## 4. TIÊU CHÍ CHECKLIST K69 LIÊN QUAN
- **Tiêu chí 1.4**: Đóng gói Clean-room, Hồ sơ bàn giao & Git nhóm ($\ge 24$–40 commits, blame 40–60%).
- **Tiêu chí 5.1**: Thiết kế ca test (EP/BVA/State) & Unit test tách Engine ($\ge 18$–24 tests).
- **Tiêu chí 5.2**: Integration Save/Load, va chạm, Playtest chéo & Regression ($\ge 8$–10 tests, Bug Log $\ge 6$–8 lỗi).
- **Tiêu chí 5.3**: Live Fault-Injection, Debugger & Bảo vệ Code–Test ($\ge 26$–34 tests).

---

## 5. CƠ CHẾ KIỂM CHỨNG HAI CHIỀU ($A \leftrightarrow B$)
- **Chiều xuôi [$A \rightarrow B$]**: Mọi mã ca kiểm thử ($TC\text{-}xx$) trong Ma trận truy vết phải tìm thấy phương thức thực thi tương ứng trong Visual Studio / Rider; 100% đều có Assertion kiểm tra trạng thái cụ thể (0 test rỗng).
- **Chiều ngược [$B \rightarrow A$]**: Mỗi Bug nghiêm trọng trong Bug Log đều phải trích xuất được commit sửa lỗi trên Git và hàm test hồi quy tương ứng đã chuyển từ Đỏ sang Xanh.

---

## 6. ĐỊNH NGHĨA HOÀN THÀNH (DEFINITION OF DONE - DOD)
- [x] Chạy lệnh NUnit Test Runner: hiển thị 34/34 tests màu xanh lá cây trong thời gian $<2$ giây.
- [x] Thử nghiệm máy sạch: Clone repo hoặc giải nén file zip trên một máy tính Windows khác không có môi trường lập trình, bấm chạy file `.exe` trò chơi khởi động thông suốt trong vòng dưới 3 phút (mở khóa Cổng $G_{Product} = \text{PASS}$).
- [x] Lịch sử Git hiển thị ít nhất 40 commits logic phân bổ rải đều qua $\ge 3$ tuần; tỷ lệ commit giữa Nguyễn Đình Huy (TV1) và Nguyễn Công Huy (TV2) nằm trong khoảng 40%–60%.
- [x] Thư mục dự án sạch sẽ $100\%$, không có các tệp tin thừa, file rác build hay mã AI sinh không dùng đến.
- [x] Video Demo 3–5 phút trình chiếu trọn vẹn vòng đời game và các cơ chế combo nguyên tố nổi bật.
- [x] Báo cáo PDF hoàn chỉnh cấu trúc 6 chương, có phụ lục UML Class tổng, Balancing Table và Bug Log đầy đủ số liệu thật.

---

## 7. CỔNG HIỂU SÂU & DIỄN TẬP BẢO VỆ (COMPREHENSION GATE P5)
1. **Thực hành Live Fault-Injection tại bàn chấm**:
   - Thầy giáo tiêm lỗi: Sửa toán tử trong hàm kiểm tra va chạm tường thành dấu ngược lại.
   - Sinh viên: Mở Test Runner bấm chạy $\rightarrow$ Ca test `TC-09` hoặc `INT-10` báo FAIL đỏ $\rightarrow$ Đặt Breakpoint, mở Call Stack chỉ đúng vị trí lỗi và sửa lại trong vòng dưới 2 phút.
2. **Vấn đáp Phản biện Đánh đổi (W)**:
   - Hai thành viên Nguyễn Đình Huy và Nguyễn Công Huy bảo vệ thành công tại sao kiến trúc 3 tầng tách Core giúp cứu sống dự án khi cần bảo trì, và giải thích vì sao không áp dụng Singleton bừa bãi.
   - Đạt ngưỡng điểm Trục II $\ge 5.0 / 6.0$, mở khóa tự động Cổng $G_{Epistemic} = \text{PASS}$ chạm trần 10.0 điểm A.

---

## 8. CHƯƠNG BÁO CÁO TƯƠNG ỨNG
- **Chương 5: Kiểm thử Phần mềm & Đảm bảo Chất lượng**: Trình bày ma trận truy vết test, 34 automated tests, phương pháp Mutation Testing, Playtest Matrix và Bug Log 6 cột.
- **Chương 6: Tổng kết, Đánh giá & Hướng phát triển**: Tự đánh giá mức độ hoàn thiện sản phẩm, phân tích tỷ lệ đóng góp thực tế của Đình Huy và Công Huy, bài học kinh nghiệm và các tính năng mở rộng.
- **Phụ lục Báo cáo**: Toàn bộ bản vẽ UML Class tổng thể cỡ lớn, Bảng thông số cân bằng Balancing Table, Chi tiết 8 lỗi Bug Log và Danh mục tài nguyên bản quyền CC0 (`CREDITS.md`).

---

## 9. QUẢN TRỊ RỦI RO & PHƯƠNG ÁN CẮT GIẢM
- **Rủi ro**: Bản build `.exe` bị crash trên máy chấm do thiếu thư viện Visual C++ Redistributable hoặc lỗi đường dẫn tuyệt đối.
- **Biện pháp xử lý**: Bắt buộc build ở chế độ Standalone kèm theo toàn bộ Mono/IL2CPP runtimes độc lập; đóng gói bản Portable Zip không cần cài đặt; chuẩn bị sẵn 1 bản USB cứu sinh chứa sẵn bản build đã kiểm tra chạy mượt trên 2 máy tính khác nhau của bạn bè.
