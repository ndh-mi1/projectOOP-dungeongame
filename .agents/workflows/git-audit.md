---
description: Kiểm toán kho Git: đếm số commit, tỷ lệ blame logic, phân bố thời gian và cảnh báo dồn 48h cuối.
---
# QUY TRÌNH KIỂM TOÁN KHO LƯU TRỮ GIT (/git-audit)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Thực thi script kiểm toán:**
   - Chạy lệnh `powershell -File tools/git-audit.ps1`.
2. **Phân tích các chỉ số kỹ thuật Git:**
   - **Số lượng commit:** Tổng số commit logic (yêu cầu $\ge 40$ commits).
   - **Tỷ lệ đóng góp (Git Blame):** Tính toán tỷ lệ phần trăm dòng mã logic thuần trong `src/Game.Core/` (yêu cầu Đình Huy đạt 40%–60%, Công Huy đạt 40%–60%).
   - **Phân bố theo thời gian:** Rải đều qua ít nhất 3 tuần học tập liên tục.
   - **Cảnh báo dồn commit (Dump Warning):** Kiểm tra tỷ lệ commit trong 48 giờ cuối (CẤM vượt quá 60%).
3. **Kiểm tra tiền tố commit:**
   - Xác nhận 100% commit đều có tiền tố chuẩn `[Đình Huy]:`, `[Công Huy]:`, hoặc `[Cả hai]:`.
4. **Báo cáo kết quả:**
   - In bảng kết quả chi tiết ra màn hình; phát tín hiệu ĐẠT hoặc CẢNH BÁO NGUY HIỂM để hai bạn kịp thời điều chỉnh trước kỳ chấm thi.
