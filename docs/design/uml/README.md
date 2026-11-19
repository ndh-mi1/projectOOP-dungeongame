# THƯ MỤC NGUỒN SƠ ĐỒ UML (PLANTUML SOURCE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) -->

Thư mục này chứa toàn bộ mã nguồn PlantUML (`.puml`) phục vụ cho việc sinh tự động sơ đồ kiến trúc phần mềm, bảo đảm tính đồng bộ 100% giữa thiết kế và mã nguồn C#.

## 1. QUY TẮC ĐỒNG BỘ MÃ NGUỒN (UML DRIFT PREVENTION)
1. Tên lớp, thuộc tính, kiểu dữ liệu và danh sách tham số phương thức trong file `.puml` bắt buộc phải khớp chính xác 1-1 với mã C# tương ứng.
2. Khi có sự thay đổi trong code (thêm hàm, đổi tên lớp), bắt buộc chạy workflow `/uml-sync` để cập nhật lại tệp `.puml`.
3. Bản vẽ UML Class Diagram tổng thể bắt buộc vẽ trên **01 sơ đồ duy nhất**, không ghép nối các ảnh rời rạc.

## 2. QUY ƯỚC MÀU SẮC THEO PHÂN HỆ
- **Gói Core Combat (Đình Huy):** Nền `#FEE2E2` (Đỏ nhạt), viền `#EF4444`.
- **Gói Core World & Items (Công Huy):** Nền `#DBEAFE` (Xanh dương nhạt), viền `#3B82F6`.
- **Gói Chung / Contracts:** Nền `#FEF3C7` (Vàng nhạt), viền `#F59E0B`.
- **Gói Hạ Tầng / Presentation:** Nền `#F3F4F6` (Xám nhạt), viền `#6B7280`.
