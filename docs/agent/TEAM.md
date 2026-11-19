# THÔNG TIN NHÓM PHÁT TRIỂN & PHÂN QUYỀN SỞ HỮU (TEAM ROSTER)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) -->

## 1. THÀNH VIÊN DỰ ÁN

### Thành viên 1: Nguyễn Đình Huy (TV1)
- **MSSV:** 202418918
- **Email học tập:** `huy.nd2418918@sis.hust.edu.vn`
- **Email Git / GitHub:** `dinggwee03@gmail.com` (Git username: `ndh-mi1`)
- **Vai trò chuyên môn:** Lead Combat, AI & Engine Core
- **Vùng sở hữu mã nguồn độc quyền:**
  - Logic nghiệp vụ: `src/Game.Core/Entities/`, `src/Game.Core/Combat/`, `src/Game.Core/AI/`, `src/Game.Core/Physics/`, `src/Game.Core/Loop/`
  - Tầng trình diễn: `src/Game.Presentation/CombatViews/`, `src/Game.Presentation/Player/`
  - Kiểm thử: `tests/Game.Core.Tests/Combat/`, `tests/Game.Core.Tests/AI/`, `tests/Game.Core.Tests/Physics/`
  - Nhánh Git: `feat/tv1-*`
  - Tiền tố commit: `[Đình Huy]:`

### Thành viên 2: Nguyễn Công Huy (TV2)
- **MSSV:** 202418914
- **Email học tập / Git:** `huy.nc2418914@sis.hust.edu.vn`
- **Vai trò chuyên môn:** Lead World Generation, Items & Meta Storage
- **Vùng sở hữu mã nguồn độc quyền:**
  - Logic nghiệp vụ: `src/Game.Core/World/`, `src/Game.Core/Items/`, `src/Game.Core/Storage/`
  - Tầng hạ tầng & UI: `src/Game.Infrastructure/`, `src/Game.Presentation/WorldViews/`, `src/Game.Presentation/UI/`, `src/Game.Presentation/Audio/`
  - Kiểm thử: `tests/Game.Core.Tests/World/`, `tests/Game.Core.Tests/Items/`, `tests/Game.Core.Tests/Storage/`
  - Nhánh Git: `feat/tv2-*`
  - Tiền tố commit: `[Công Huy]:`

---

## 2. QUY ƯỚC NHẬN DIỆN NGƯỜI DÙNG PHIÊN (SESSION IDENTIFICATION)
- Khi khởi động phiên (`/session-start`), Agent chạy lệnh `git config user.email` và `git config user.name`:
  - Nếu email khớp với `dinggwee03@gmail.com` hoặc username khớp với `ndh-mi1` $\rightarrow$ Nhận diện là **Nguyễn Đình Huy (TV1)**.
  - Nếu email khớp với `huy.nc2418914@sis.hust.edu.vn` $\rightarrow$ Nhận diện là **Nguyễn Công Huy (TV2)**.
  - Nếu không khớp hoặc không tìm thấy $\rightarrow$ Agent phải hỏi trực tiếp người dùng đang là Đình Huy hay Công Huy để tải đúng sở thích và nhật ký phiên.

---

## 3. NGUYÊN TẮC BẢO VỆ RANH GIỚI "KHÔNG DẪM CHÂN NHAU"
1. Thành viên này TUYỆT ĐỐI KHÔNG trực tiếp chỉnh sửa mã nguồn thuộc thư mục độc quyền của thành viên kia trên cùng một nhánh.
2. Mọi tương tác hoặc yêu cầu tính năng chéo phải thông qua:
   - Thảo luận và ghi chú tại [docs/agent/HANDOFF.md](file:///d:/project/projectOOPnew/docs/agent/HANDOFF.md).
   - Gọi thông qua Interface trừu tượng tại `src/Game.Core/Contracts/` (`ITileQuery`, `IGameEventBus`).
   - Đóng góp thông qua Pull Request trên GitHub và phải có sự phê duyệt (Approve) của người sở hữu.
