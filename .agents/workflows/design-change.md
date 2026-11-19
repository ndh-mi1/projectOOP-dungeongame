---
description: Quy trình 4 bước khi thay đổi thiết kế: Soạn ADR, cập nhật UML/Blueprint, cài đặt code và test.
---
# QUY TRÌNH THAY ĐỔI THIẾT KẾ KIẾN TRÚC (/design-change)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Tiếp nhận yêu cầu thay đổi:**
   - Mô tả bài toán hoặc nhu cầu phát sinh cần thêm lớp mới, mẫu thiết kế mới hoặc đổi hợp đồng interface.
2. **Soạn thảo Architecture Decision Record (ADR):**
   - Tạo file mới `docs/adr/XXXX-<ten-quyet-dinh>.md` theo mẫu `0000-template.md`.
   - Nêu rõ bối cảnh, quyết định, phương án thay thế và phân tích vùng ảnh hưởng (Blast Radius).
   - Thêm bản ghi mới vào `docs/adr/INDEX.md` và chờ hai bạn xác nhận `Accepted`.
3. **Cập nhật Thiết kế & Sơ đồ UML:**
   - Cập nhật sơ đồ lớp trong `docs/design/uml/class_diagram_full.puml`.
   - Cập nhật danh mục lớp trong `docs/plan/02_OOP_BLUEPRINT.md` và `docs/design/DESIGN_INDEX.md`.
4. **Cài đặt Mã nguồn & Cập nhật Test:**
   - Tạo code theo đúng interface trong ADR; tuân thủ nguyên lý SOLID.
   - Viết các ca unit test mới phủ biên; chạy lại toàn bộ suite test bảo đảm không có lỗi hồi quy (zero regression).
