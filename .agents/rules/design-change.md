<!-- activation: model-decision -->
# QUY TRÌNH THAY ĐỔI THIẾT KẾ & KIẾN TRÚC (DESIGN CHANGE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. NGUYÊN TẮC "THIẾT KẾ ĐI TRƯỚC CODE"
Mọi lớp mới, mẫu thiết kế mới, hoặc sự điều chỉnh quan hệ giữa các phân hệ không nằm trong [docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md) đều KHÔNG ĐƯỢC PHÉP code ngay lập tức. Phải tuân thủ nghiêm ngặt quy trình 4 bước bên dưới.

## 2. QUY TRÌNH 4 BƯỚC BẮT BUỘC
1. **Bước 1: Soạn thảo Kiến trúc Quyết định (ADR):**
   - Tạo một file ADR mới trong thư mục `docs/adr/` theo mẫu `docs/adr/0000-template.md` (Ví dụ: `0002-add-stamina-system.md`).
   - Nêu rõ bối cảnh, lý do thay đổi, các phương án lựa chọn và đánh giá đánh đổi (Trade-offs: Ưu điểm / Nhược điểm).
   - Hai thành viên cùng thảo luận và thống nhất trạng thái `Accepted`.
2. **Bước 2: Cập nhật Bản vẽ Thiết kế & UML:**
   - Cập nhật sơ đồ lớp tổng thể trong `docs/design/uml/class_diagram_full.puml`.
   - Cập nhật danh mục lớp và trách nhiệm trong `docs/plan/02_OOP_BLUEPRINT.md` và `docs/design/DESIGN_INDEX.md`.
   - Phân tích vùng ảnh hưởng (Blast Radius): Liệt kê các lớp hoặc test bị tác động.
3. **Bước 3: Cài đặt Mã nguồn C#:**
   - Cài đặt interface và lớp mới theo đúng cấu trúc đã thống nhất trong ADR.
   - Tuân thủ nghiêm ngặt nguyên lý SOLID và quy ước đặt tên C#.
4. **Bước 4: Viết và Cập nhật Test Tự động:**
   - Viết các ca kiểm thử NUnit mới phủ các nhánh biên của lớp vừa thêm.
   - Chạy toàn bộ bộ test `tests/Game.Core.Tests/` bảo đảm 100% PASS không làm gãy các tính năng cũ (Regression Testing).
