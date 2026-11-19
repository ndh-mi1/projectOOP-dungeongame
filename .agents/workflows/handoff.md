---
description: Ghi chú bàn giao việc, rủi ro phát hiện hoặc thay đổi hợp đồng giao tiếp vào docs/agent/HANDOFF.md.
---
# QUY TRÌNH BÀN GIAO VIỆC LIÊN THÀNH VIÊN (/handoff)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Thu thập thông tin chuyển giao:**
   - Xác định: Bạn là ai (Đình Huy hay Công Huy)?
   - Bạn vừa hoàn thành hoặc thay đổi hợp đồng interface nào?
   - Thành viên còn lại cần phải làm gì để kết nối hoặc test liên thông?
   - Thời hạn cần hoàn thành (Deadline) là mốc nào?
2. **Ghi nhận vào sổ HANDOFF:**
   - Mở file [docs/agent/HANDOFF.md](file:///d:/project/projectOOPnew/docs/agent/HANDOFF.md).
   - Thêm một mục mới ở cuối file theo định dạng chuẩn:
     ```markdown
     ### [YYYY-MM-DD][Tên Người Gửi] Tiêu đề chuyển giao
     - **Gửi tới:** [Người Nhận]
     - **Nội dung:** [Mô tả chi tiết interface/sự kiện cần kết nối]
     - **Hạn cần khớp:** [Mốc Milestone]
     ```
3. **Thông báo và Xác nhận:**
   - In ra chat thông báo chuyển giao để thành viên kia biết ngay khi mở phiên làm việc tiếp theo bằng `/session-start`.
