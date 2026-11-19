# MỤC LỤC TÀI LIỆU THIẾT KẾ & SƠ ĐỒ (DESIGN INDEX)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) -->

Tài liệu này đóng vai trò chỉ mục liên kết toàn bộ các tài liệu phân tích, kiến trúc, sơ đồ lớp và wireframe của dự án.

## 1. TÀI LIỆU KIẾN TRÚC & OOP BLUEPRINT
- **Bản Thiết Kế OOP Tổng Thể:** [docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md)  
  *Nội dung:* 28 lớp C#, 4 tính chất OOP, 6 quan hệ UML, 5 mẫu GoF chính thức + Flyweight/Command, 6 thuật toán và 5 sơ đồ động.
- **Hướng Dẫn Tách Rời Module:** [docs/plan/06_MODULE_DECOUPLING_GUIDE.md](file:///d:/project/projectOOPnew/docs/plan/06_MODULE_DECOUPLING_GUIDE.md)  
  *Nội dung:* Hợp đồng giao tiếp `ITileQuery` và `IGameEventBus`, cấu trúc `partial class GameBootstrap`.
- **Cẩm Nang Kim Chỉ Nam:** [docs/design/DESIGN.md](file:///d:/project/projectOOPnew/docs/design/DESIGN.md)  
  *Nội dung:* 5 trụ cột thiết kế, Design Tokens PPU 32 và bảng màu.

## 2. SƠ ĐỒ THIẾT KẾ UML (PLANTUML SOURCE)
- Thư mục chứa sơ đồ: `docs/design/uml/`
- Sơ đồ lớp tổng thể: `docs/design/uml/class_diagram_full.puml` (Sơ đồ 28 lớp)
- Mẫu sơ đồ khung rỗng: `docs/design/uml/class_diagram_template.puml`
- Sơ đồ động trạng thái:
  - `docs/design/uml/state_actor.puml`: Vòng đời trạng thái Actor (`IdleState`, `WalkState`, `AttackState`, `StunnedState`).
  - `docs/design/uml/state_game.puml`: Vòng đời Game (`Title`, `Playing`, `Paused`, `GameOver`, `Victory`).
  - `docs/design/uml/state_room.puml`: Trạng thái phòng (`Undiscovered`, `ActiveEncounter`, `Cleared`).
- Sơ đồ tuần tự:
  - `docs/design/uml/sequence_combat.puml`: Luồng tấn công từ phím bấm $\rightarrow$ Swept AABB $\rightarrow$ Sát thương $\rightarrow$ Bắn sự kiện quái chết.
  - `docs/design/uml/sequence_save_load.puml`: Luồng Serialize snapshot $\rightarrow$ Đĩa cứng $\rightarrow$ Khôi phục 100% trạng thái.

## 3. MỸ THUẬT & GIAO DIỆN (UI/UX)
- **Định Hướng Nghệ Thuật & Wireframes:** [docs/plan/03_UIUX_ART_DIRECTION.md](file:///d:/project/projectOOPnew/docs/plan/03_UIUX_ART_DIRECTION.md)  
  *Nội dung:* Wireframe 10 màn hình, hệ thống Game Feel, cơ chế rung camera, hiệu ứng hit-stop.
- **Cẩm Nang Phối Hợp 6 Bộ Asset:** [docs/ref/asset_coordination_guide.md](file:///d:/project/projectOOPnew/docs/ref/asset_coordination_guide.md)  
  *Nội dung:* Chuẩn hóa PPU 32, kiến trúc nạp frame đa bộ `ISpriteFrameProvider`.
