---
description: Đóng phiên làm việc, chạy kiểm tra tĩnh, cập nhật trạng thái, hỏi teach-back và đề xuất commit.
---
# QUY TRÌNH ĐÓNG PHIÊN LÀM VIỆC (/session-end)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra thay đổi tệp:**
   - Chạy `git diff --stat` và liệt kê các tệp đã sửa đổi theo quyền sở hữu trong `TEAM.md`.
   - Cảnh báo đỏ nếu phát hiện bạn đang sửa tệp thuộc vùng độc quyền của thành viên kia.
2. **Chạy kiểm tra tĩnh trước đóng phiên:**
   - Gọi workflow `/pre-commit` để quét using cấm trong Core và các mẫu code smell.
3. **Cập nhật dữ liệu trạng thái:**
   - Chạy `powershell -File tools/update-state.ps1` để làm mới `STATE.json`.
   - Cập nhật mục 1 và mục 6 trong `docs/agent/CONTEXT.md`.
4. **Hoàn thiện nhật ký phiên:**
   - Điền đầy đủ việc đã làm, quyết định kỹ thuật, và liên kết ADR vào nhật ký phiên hôm nay.
   - Cập nhật con trỏ `LATEST.md` tương ứng.
5. **Ghi chú chuyển giao (Handoff):**
   - Nếu có hợp đồng giao tiếp hoặc thay đổi ảnh hưởng tới bạn kia, thêm 1 mục vào `HANDOFF.md`.
6. **Thực hiện 3 câu hỏi Teach-back:**
   - Đặt 3 câu hỏi nhanh về lý do thiết kế và tính đúng đắn của đoạn code vừa viết.
   - Chờ sinh viên trả lời, ghi kết quả vào nhật ký (tuyệt đối không tự trả lời thay sinh viên).
7. **Đề xuất lệnh Commit chuẩn:**
   - In đề xuất lệnh Git và thông điệp commit Conventional Commits kèm tiền tố `[Đình Huy]:` hoặc `[Công Huy]:` để sinh viên tự kiểm tra diff và tự commit.
