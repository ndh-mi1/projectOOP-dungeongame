---
description: Khởi động phiên làm việc mới, nạp ngữ cảnh Tier 1 và in Briefing ngắn gọn dưới 15 dòng.
---
# QUY TRÌNH MỞ PHIÊN LÀM VIỆC (/session-start)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Xác định thành viên phiên:**
   - Chạy lệnh `git config user.email` để đối chiếu với [docs/agent/TEAM.md](file:///d:/project/projectOOPnew/docs/agent/TEAM.md).
   - Nếu không xác định được, hỏi người dùng: "Bạn là Đình Huy (TV1) hay Công Huy (TV2)?".
2. **Nạp ngữ cảnh Tier 1 (trong vòng <= 2 phút):**
   - Đọc `docs/agent/STATE.json` (Phase, Milestone, Blocker).
   - Đọc `docs/agent/CONTEXT.md` (10 dòng kiến trúc, việc dang dở).
   - Đọc `docs/agent/memory/MEMORY.md` và `docs/agent/HANDOFF.md` (các mục chuyển giao mới).
   - Đọc `docs/agent/sessions/TVx/LATEST.md` của cả hai thành viên.
3. **Kiểm tra trạng thái Git hiện tại:**
   - Chạy `git status` và `git branch --show-current`.
   - Cảnh báo ngay lập tức nếu đang đứng trên nhánh `main` thay vì nhánh tính năng cá nhân.
4. **Kiểm tra Definition of Done (DoD):**
   - Mở file phase hiện tại trong `docs/plan/phases/` (ví dụ: `P0_Preflight.md`) để rà soát mục tiêu.
5. **In bản tóm tắt đầu phiên (Briefing <= 15 dòng):**
   - Xác định: Bạn đang ở Phase nào? Mốc nào?
   - Công việc gần nhất của thành viên còn lại có ảnh hưởng gì tới bạn không?
   - Đề xuất 3 công việc trọng tâm nên làm hôm nay.
6. **Xác nhận mục tiêu & Tạo nhật ký:**
   - Chờ người dùng gõ xác nhận mục tiêu.
   - Tạo file nhật ký phiên mới tại `docs/agent/sessions/TVx/YYYY-MM-DD_NN.md`.
