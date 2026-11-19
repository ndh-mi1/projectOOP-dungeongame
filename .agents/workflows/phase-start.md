---
description: Khởi động một Phase mới (P0 đến P5), đọc tài liệu phase, phân rã WBS và cập nhật STATE.json.
---
# QUY TRÌNH BẮT ĐẦU GIAI ĐOẠN (/phase-start)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Tiếp nhận tham số giai đoạn:**
   - Người dùng gõ `/phase-start P<n>` (Ví dụ: `/phase-start P0`).
2. **Đọc tài liệu đặc tả phase tương ứng:**
   - Mở tệp `docs/plan/phases/P<n>_*.md` để đọc chi tiết mục tiêu, tiêu chí Checklist liên quan, cơ chế kiểm chứng $A \leftrightarrow B$ và bảng công việc WBS.
3. **Phân rã lát cắt công việc theo thành viên:**
   - Trích xuất danh sách công việc `W<n>.x` gán cho Đình Huy và Công Huy.
   - Cập nhật mục NOW trong `docs/agent/BACKLOG.md`.
4. **Cập nhật dữ liệu trạng thái:**
   - Cập nhật trường `"phase": "P<n>"` và `"phaseStatus": "in_progress"` trong `docs/agent/STATE.json`.
   - Cập nhật mục 1 trong `docs/agent/CONTEXT.md`.
5. **In thông báo khởi động:**
   - In tóm tắt mục tiêu chính của Phase, mốc Milestone cần đạt và danh sách việc ưu tiên cho từng bạn trong 3 ngày tới.
