---
description: Quy trình review chéo PR giữa hai thành viên theo template, kèm 2 câu hỏi hiểu bài bắt buộc.
---
# QUY TRÌNH DUYỆT PULL REQUEST CHÉO (/pr-review)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra thông tin Pull Request:**
   - Đọc mô tả PR theo khung mẫu [.github/pull_request_template.md](file:///d:/project/projectOOPnew/.github/pull_request_template.md).
   - Xác nhận: Tác giả là ai? Nhánh nguồn có đúng định dạng `feat/tv1-*` hoặc `feat/tv2-*` không?
2. **Kiểm tra tiêu chí kỹ thuật bắt buộc:**
   - [x] Tầng Core hoàn toàn sạch, không dính `UnityEngine`.
   - [x] Có đầy đủ Unit Test chuẩn AAA đi kèm và 100% test chạy xanh.
   - [x] Không có code thừa, code chết, không vi phạm SOLID.
   - [x] Không chỉnh sửa mã nguồn thuộc thư mục độc quyền của người review.
3. **Thực hiện đặt 2 câu hỏi hiểu bài (Comprehension Questions):**
   - Người review chọn 2 câu hỏi liên quan đến tính đúng đắn và lý do thiết kế trong PR để hỏi tác giả.
   - Tác giả phải trả lời trực tiếp trong phần bình luận của PR trên GitHub.
4. **Phê duyệt hoặc Yêu cầu sửa đổi:**
   - Nếu tác giả trả lời tốt và code đạt chuẩn: Bấm `Approve` và thực hiện `Squash and merge` vào nhánh `dev`.
   - Nếu chưa đạt: Yêu cầu cập nhật commit sửa lỗi trước khi merge.
