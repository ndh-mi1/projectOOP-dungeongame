<!-- activation: model-decision -->
# QUY TẮC PHÂN NHÁNH & CAM KẾT GIT (GIT COMMIT)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. QUY ƯỚC ĐẶT TÊN NHÁNH
- Nhánh chính: `main` (chỉ chứa bản build ổn định, sẵn sàng chấm bảo vệ).
- Nhánh tích hợp: `dev` (nhánh làm việc chung mặc định cho các PR).
- Nhánh tính năng cá nhân:
  - Nguyễn Đình Huy: `feat/tv1-combat`, `feat/tv1-ai`, `feat/tv1-physics`, `test/tv1-core`.
  - Nguyễn Công Huy: `feat/tv2-world`, `feat/tv2-inventory`, `feat/tv2-save`, `feat/tv2-ui`.

## 2. TIỀN TỐ COMMIT MESSAGE BẮT BUỘC
Mọi commit phải tuân thủ chuẩn Conventional Commits tiếng Anh kèm tiền tố tác giả:
- Nguyễn Đình Huy: `[Đình Huy]: <type>(<scope>): <subject>`  
  Ví dụ: `[Đình Huy]: feat(combat): implement swept AABB collision detection to prevent tunneling`
- Nguyễn Công Huy: `[Công Huy]: <type>(<scope>): <subject>`  
  Ví dụ: `[Công Huy]: feat(world): implement BSP dungeon split and MST corridor connector`
- Commit chung: `[Cả hai]: <type>(<scope>): <subject>`  
  Ví dụ: `[Cả hai]: chore(repo): initialize project skeleton and .gitignore`

## 3. KỶ LUẬT COMMIT & BLAME CÂN BẰNG
- **Commit nhỏ, nguyên tử (Atomic Commits):** Mỗi commit chỉ giải quyết một vấn đề kỹ thuật nhỏ kèm test tương ứng.
- **Phân bổ thời gian $\ge 3$ tuần:** Trải đều tối thiểu 40 commits qua ít nhất 3 tuần học tập.
- **Cấm dồn commit 48 giờ cuối:** Tuyệt đối không đẩy $>60\%$ số commit trong 48 giờ trước ngày chấm thi.
- **Tỷ lệ Blame 40% – 60%:** Hai thành viên đóng góp cân đối trên mã logic (`Game.Core`).

## 4. QUY TRÌNH REVIEW CHÉO (CROSS PR)
- Khi hoàn thành một tính năng trên nhánh cá nhân, thành viên tạo Pull Request vào `dev`.
- Bắt buộc thành viên còn lại làm Reviewer. Người review kiểm tra Clean Code, chạy test xanh 100%, không dính `UnityEngine` trong Core rồi mới bấm Approve và Merge.
