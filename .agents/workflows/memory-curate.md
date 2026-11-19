---
description: Bảo trì bộ nhớ dài hạn hàng tuần: gộp trùng lặp, xóa mục lỗi thời và chuyển quyết định sang ADR.
---
# QUY TRÌNH BẢO TRÌ BỘ NHỚ DÀI HẠN (/memory-curate)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Rà soát các tệp bộ nhớ:**
   - Mở các tệp trong `docs/agent/memory/`: `facts.md`, `lessons.md`, `pitfalls.md`, `prefs/`.
2. **Đối chiếu thực tế mã nguồn:**
   - Kiểm tra từng mục: Liệu sự thật hoặc bài học này còn đúng với hiện trạng code và Git không?
   - Nếu phát hiện thông tin đã lỗi thời (ví dụ: đã đổi tên lớp hoặc đổi thuật toán): Cập nhật lại hoặc xóa bỏ.
3. **Phát hiện và Chuyển giao ADR:**
   - Nếu có một mục ghi nhớ thực chất là một quyết định kiến trúc quan trọng: Đề xuất chuyển thành một bản ghi ADR chính thức trong `docs/adr/`.
4. **Đồng bộ hóa Chỉ mục MEMORY.md:**
   - Cập nhật lại danh mục tham chiếu trong `docs/agent/memory/MEMORY.md`.
   - Bảo đảm tổng số dòng của `MEMORY.md` luôn $\le 120$ dòng.
5. **Thông báo kết quả:**
   - In ra chat tóm tắt các mục vừa được dọn dẹp và cập nhật.
