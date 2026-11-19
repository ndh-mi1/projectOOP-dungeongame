---
description: Quy trình nhập asset mới: Kiểm tra bản quyền CC0, chuẩn hóa PPU 32 và cập nhật CREDITS.md.
---
# QUY TRÌNH NHẬP TÀI NGUYÊN ASSET MỚI (/asset-import)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra bản quyền & Giấy phép (License Check):**
   - Xác nhận tài nguyên có giấy phép CC0 (Public Domain) hoặc SIL Open Font / Free Commercial Use.
   - Tuyệt đối cấm tải các asset không rõ nguồn gốc hoặc có bản quyền thương mại hạn chế.
2. **Chọn lọc lát cắt sử dụng:**
   - Chỉ sao chép đúng file ảnh, sprite sheet hoặc âm thanh thật sự sử dụng vào dự án; cấm import bừa bãi toàn bộ thư mục nặng hàng trăm MB làm rác kho Git.
3. **Chuẩn hóa thông số kỹ thuật:**
   - Cố định Pixels Per Unit (PPU) là **32**.
   - Cấu hình Sprite Filter Mode là **Point (no filter)**.
   - Cấu hình Compression là **None**.
4. **Cập nhật tài liệu bản quyền:**
   - Thêm dòng ghi nhận tác giả và nguồn tải vào tệp `CREDITS.md`.
   - Cập nhật quy tắc nạp frame tương ứng vào [docs/ref/asset_coordination_guide.md](file:///d:/project/projectOOPnew/docs/ref/asset_coordination_guide.md).
