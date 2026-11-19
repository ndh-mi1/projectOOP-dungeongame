<!-- activation: always -->
# GIAO THỨC PHIÊN LÀM VIỆC (SESSION PROTOCOL)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. KHỞI ĐỘNG PHIÊN (/session-start)
- Mọi phiên mở đầu bằng workflow `/session-start`.
- Agent nhận diện TV qua `git config user.email` đối chiếu `docs/agent/TEAM.md`.
- Chỉ nạp Tier 1: `STATE.json`, `CONTEXT.md`, `MEMORY.md`, `HANDOFF.md`, và `LATEST.md` của 2 TV.
- In Briefing $\le 15$ dòng trước khi thực hiện công việc.

## 2. TRONG PHIÊN & CHECKPOINT
- Bám sát mục tiêu đã xác nhận trong Briefing.
- Khi phiên kéo dài >20 lượt hoặc đổi chủ đề lớn, chạy `/checkpoint` để lưu trạng thái vào nhật ký và `CONTEXT.md`.

## 3. ĐÓNG PHIÊN (/session-end)
- Kết thúc phiên bằng `/session-end`.
- Chạy kiểm tra tĩnh `tools/check-core-purity.ps1` và `tools/check-code-smells.ps1`.
- Cập nhật nhật ký tại `docs/agent/sessions/TVx/` và cập nhật `STATE.json`.
- Ghi việc chuyển giao vào `HANDOFF.md` nếu ảnh hưởng thành viên kia.
- Thực hiện 3 câu hỏi Teach-back và đề xuất commit message chuẩn.
