---
description: Diễn tập tình huống Live Fault-Injection: Bắt lỗi tiêm bằng NUnit Test và Debugger trong 2 phút.
---
# QUY TRÌNH LUYỆN TẬP TIÊM LỖI & DEBUGGER (/fault-drill)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Tiêm lỗi ẩn (Fault-Injection):**
   - Agent đóng vai Giảng viên chấm thi, cố ý sửa 1 dòng code logic (ví dụ: đảo dấu toán tử va chạm góc phòng hoặc sửa chỉ số hồi máu).
2. **Kích hoạt quy trình xử lý của sinh viên (Thời gian: 2 phút):**
   - Bước 1: Mở Test Runner bấm chạy bộ kiểm thử $\rightarrow$ Xác định chính xác ca test nào báo màu ĐỎ (FAIL).
   - Bước 2: Đặt Breakpoint tại ca test bị lỗi, bật Debugger (F5) trong Visual Studio / Rider.
   - Bước 3: Mở cửa sổ Call Stack và Watch kiểm tra giá trị biến nội bộ để định vị chính xác dòng lệnh bị lỗi.
   - Bước 4: Sửa lại code cho đúng, chạy lại test $\rightarrow$ Test chuyển sang màu XANH (PASS).
3. **Đánh giá kết quả:**
   - Hoàn thành dưới 2 phút, giải thích rõ nguyên nhân qua Call Stack: Mở khóa điểm tối đa Tiêu chí 5.3 Checklist K69.
