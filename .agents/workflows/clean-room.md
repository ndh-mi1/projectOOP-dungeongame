---
description: Kịch bản thử nghiệm đóng gói và kiểm chứng Clean-room build dưới 3 phút trên máy tính sạch.
---
# QUY TRÌNH KIỂM CHỨNG BẢN BUILD CLEAN-ROOM (/clean-room)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Chuẩn bị gói phát hành (Release Package):**
   - Đóng gói toàn bộ thư mục `build/Release_v2026/` thành file zip `CrystalDepths_v2026_x64.zip`.
   - Kiểm tra: File zip bắt buộc chứa file thực thi `.exe`, thư mục `Data/` (StreamingAssets), và file hướng dẫn `README.md`.
2. **Kịch bản chạy máy sạch (Clean-room Test Scenario):**
   - Giả lập hoặc sao chép file zip sang một máy tính mới không có Unity và không có Visual Studio.
   - Bắt đầu bấm giờ (Yêu cầu: $\le 3$ phút).
   - Giải nén file zip $\rightarrow$ Đọc nhanh `README.md` $\rightarrow$ Nhấp đúp chuột vào file `.exe`.
3. **Kiểm tra tiêu chí chấp nhận (Acceptance Criteria):**
   - [x] Trò chơi khởi động mượt mà, hiển thị màn hình Title trong vòng dưới 5 giây sau khi click.
   - [x] Điều khiển phím bấm, âm thanh BGM/SFX và hiển thị UI 9-slice hoạt động hoàn hảo.
   - [x] Chơi thử 1 lượt: Chết $\rightarrow$ Game Over $\rightarrow$ Replay không bị crash hoặc treo đơ.
4. **Kết luận Cổng G_Product:**
   - Nếu đạt mọi tiêu chuẩn trong $\le 3$ phút: Đánh dấu Cổng $G_{Product} = \text{PASS}$, mở khóa cơ hội đạt điểm A (9.0 – 10.0).
