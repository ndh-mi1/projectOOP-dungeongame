---
description: So sánh lớp, phương thức giữa file PlantUML (.puml) và mã C#, cảnh báo và sửa chữa drift.
---
# QUY TRÌNH ĐỒNG BỘ SƠ ĐỒ UML VỚI MÃ NGUỒN (/uml-sync)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Quét toàn bộ mã nguồn C#:**
   - Quét thư mục `src/Game.Core/` để trích xuất danh sách tên lớp, interface, thuộc tính và phương thức công khai.
2. **Đọc tệp sơ đồ PlantUML:**
   - Mở tệp `docs/design/uml/class_diagram_full.puml`.
3. **Đối chiếu và Phát hiện lệch pha (UML Drift):**
   - Tìm các lớp có trong C# nhưng chưa có trong `.puml`.
   - Tìm các phương thức đã đổi tên hoặc thay đổi tham số trong C# nhưng sơ đồ chưa cập nhật.
   - Tìm các lớp trong sơ đồ nhưng đã bị xóa hoặc đổi tên trong C#.
4. **Cập nhật và Xuất báo cáo:**
   - Sửa đổi trực tiếp tệp `.puml` để phản ánh chính xác 100% hiện trạng mã nguồn.
   - In ra chat bảng tóm tắt các điểm vừa được đồng bộ.
