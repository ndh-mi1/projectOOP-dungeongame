---
description: Diễn tập tình huống Live OCP tại bàn chấm: Thêm biến thể quái/vật phẩm mới trong 3 phút không sửa code cũ.
---
# QUY TRÌNH LUYỆN TẬP LIVE OCP TRỰC TIẾP (/ocp-drill)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Giao đề bài thực chiến ngẫu nhiên (Thời gian giới hạn: 3 phút):**
   - **Đề bài cho Đình Huy (TV1):** "Thêm một chiến thuật AI mới `KiteThenAmbushAI` kế thừa `IEnemyAI`, quái giữ khoảng cách bắn tên rồi tàng hình phục kích; không sửa bất kỳ dòng mã nào trong `Enemy` hay `Actor`".
   - **Đề bài cho Công Huy (TV2):** "Thêm một loại bình thuốc mới `StaminaPotion` kế thừa `ConsumableItem`, phục hồi thể lực và đăng ký bán vào Shop; không sửa bất kỳ dòng mã nào trong `Inventory`".
2. **Bắt đầu bấm giờ 3 phút:**
   - Sinh viên tiến hành tạo file `.cs` mới, cài đặt phương thức của interface và đăng ký vào hệ thống.
3. **Biên dịch và Chạy thử nghiệm:**
   - Biên dịch solution; cho quái chạy hoặc đưa bình thuốc vào game test thật.
4. **Đánh giá và Chấm điểm:**
   - Nếu hoàn thành trong $\le 3$ phút và game chạy mượt: Đạt điểm A tuyệt đối mục OCP.
   - Nếu phải sửa vào lớp cũ: Trượt nguyên lý OCP, yêu cầu refactor lại bằng mẫu Strategy/Decorator.
