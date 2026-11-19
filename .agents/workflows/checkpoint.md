---
description: Tóm tắt trạng thái giữa phiên khi hội thoại dài hoặc đổi chủ đề lớn, tránh tràn ngữ cảnh.
---
# QUY TRÌNH TẠO MỐC KIỂM TRA GIỮA PHIÊN (/checkpoint)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra ngưỡng kích hoạt:**
   - Phiên trao đổi vượt quá 20 lượt tương tác, hoặc sắp sửa đổi sang module/phân hệ khác.
2. **Tổng hợp nhanh hiện trạng:**
   - Liệt kê các quyết định vừa đưa ra trong 5–10 lượt chat gần nhất.
   - Ghi nhận các file đã sửa đổi tính đến thời điểm hiện tại.
3. **Cập nhật nhanh vào tài liệu:**
   - Ghi tóm tắt tiến độ vào nhật ký phiên hiện tại (`docs/agent/sessions/TVx/YYYY-MM-DD_NN.md`).
   - Cập nhật mục "Việc dang dở" trong `docs/agent/CONTEXT.md`.
4. **Đưa ra khuyến nghị tiếp theo:**
   - Đề xuất sinh viên mở một phiên chat mới và bắt đầu bằng `/session-start` để giải phóng bộ nhớ context và duy trì độ tập trung tối đa cho Agent.
