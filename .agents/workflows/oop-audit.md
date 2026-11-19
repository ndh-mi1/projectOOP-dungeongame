---
description: Kiểm toán định kỳ 4 tính chất OOP, SOLID, nguyên lý Liskov liên phân hệ và xếp hạng Rubric.
---
# QUY TRÌNH KIỂM TOÁN THIẾT KẾ OOP & SOLID (/oop-audit)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm toán 4 tính chất OOP:**
   - Đóng gói: Kiểm tra tất cả các trường dữ liệu có được bảo vệ và có validation hay không.
   - Kế thừa: Kiểm tra cây phả hệ `Actor`, `Item`, bảo đảm quan hệ IS-A thực chất.
   - Đa hình: Quét mã nguồn, xác nhận không có lệnh `is/as/typeof` hay `switch(type)` phân nhánh hành vi.
   - Trừu tượng: Rà soát việc tách biệt giữa interface và lớp cài đặt.
2. **Kiểm toán nguyên lý SOLID:**
   - SRP: Xác nhận mỗi lớp chỉ đảm nhận đúng 1 trách nhiệm.
   - LSP: Kiểm tra khả năng tráo đổi an toàn giữa các lớp con (đặc biệt khi truyền đối tượng giữa TV1 và TV2 qua interface).
   - DIP: Kiểm tra không có lệnh `new` dịch vụ hạ tầng bên trong `Game.Core`.
3. **Phát hiện mã chết (Dead Code):**
   - Quét tìm các phương thức, lớp hoặc biến không bao giờ được gọi trong mã nguồn hoặc test.
4. **Xếp hạng & Báo cáo:**
   - Xếp hạng kiến trúc hiện tại theo thang điểm A–F của Checklist K69.
