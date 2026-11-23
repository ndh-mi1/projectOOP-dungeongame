# ẢNH CHỤP NGỮ CẢNH DỰ ÁN (PROJECT CONTEXT SNAPSHOT)
<!-- Cập nhật lần cuối: 2026-11-19 | Cập nhật bởi: Khởi tạo hạ tầng | Sở hữu: Cả hai -->

## 1. VỊ TRÍ HIỆN TẠI (CURRENT STATUS)
- **Phase hiện tại:** P0 (Preflight & Environment Setup) | **Trạng thái:** completed (11 Unit Tests PASS, Asmdef khóa cứng noEngineReferences)
- **Mốc kiểm tra:** M0 (Hoàn tất) -> Hướng tới M1 (Lõi Tối Giản Nhìn Thấy)
- **Kế hoạch tổng thể:** Đã khóa (Locked). Mục tiêu: 10 PCUs, 28 lớp UML, 34 automated tests, 40 commits.

## 2. TÓM TẮT KIẾN TRÚC HỆ THỐNG (10 DÒNG)
1. Kiến trúc phân tầng cô lập: `Game.Core` (thuần C# CSDL logic), `Game.Presentation` (MonoBehaviour Views), `Game.Infrastructure` (Save/Audio).
2. Tầng Core hoàn toàn không chứa tham chiếu đồ họa (0 `using UnityEngine`), chạy độc lập EditMode.
3. Ranh giới module: Đình Huy (Combat, 7 AI, Swept AABB Physics) | Công Huy (BSP World Gen, Items, Save JSON, UI).
4. Khớp nối liên phân hệ độc quyền qua 2 hợp đồng C# trừu tượng: `ITileQuery` và `IGameEventBus`.
5. Mô hình thực thể: `Actor` $\rightarrow$ `RoguePlayer`, `Enemy`; Value Objects: `Health`, `GridPos`, `DamageInfo`.
6. GoF Patterns: Strategy (`IEnemyAI`, `IAttackPattern`), State (`ActorState`), Observer (`GameEventBus`), Decorator (`WeaponDecorator`), Factory Method (`LootFactory`).
7. Bổ trợ: Flyweight (`TileKind` chia sẻ 1 instance/loại ô), Command (`IPlayerCommand`).
8. Cấu hình & Chỉ số: Dữ liệu hóa 100% bằng JSON nạp từ `StreamingAssets/Data/`, ném ngoại lệ tùy biến khi hỏng file.
9. Đồ họa & UI: Đồng nhất hệ tọa độ PPU 32, Point filter, bộ 6 asset tích hợp qua Adapter `ISpriteFrameProvider`.
10. Điểm khởi động duy nhất (Composition Root): `GameBootstrap.cs` viết theo cơ chế `partial class`.

## 3. LỆNH THỰC THI (BUILD / TEST COMMANDS)
- Kiểm tra tính thuần khiết Core: `powershell -File tools/check-core-purity.ps1`
- Quét các mẫu mùi code (Anti-patterns): `powershell -File tools/check-code-smells.ps1`
- Cập nhật tự động trạng thái máy: `powershell -File tools/update-state.ps1`
- Kiểm toán Git Blame & Commit: `powershell -File tools/git-audit.ps1`
- Đường dẫn Unity 6 Editor: `D:\Unity\Editor\6000.0.85f2\Editor\Unity.exe`
- Chạy Unit Test Unity EditMode CLI: `"D:\Unity\Editor\6000.0.85f2\Editor\Unity.exe" -batchmode -runTests -testPlatform EditMode -testResults results.xml -projectPath .`

## 4. 5 QUYẾT ĐỊNH THIẾT KẾ GẦN NHẤT (ADR)
1. [ADR-0001: Khởi tạo Hạ tầng Ngữ cảnh và Phân quyền Không Xung đột](file:///d:/project/projectOOPnew/docs/adr/0001-agent-workspace.md) (Trạng thái: Accepted).
2. [Quyết định chốt Scope 10 PCU](file:///d:/project/projectOOPnew/docs/plan/99_OPEN_QUESTIONS.md): Cam kết đủ 10 PCUs, 8 vật phẩm, 7 AI.
3. [Quyết định chuẩn hóa PPU 32](file:///d:/project/projectOOPnew/docs/ref/asset_coordination_guide.md): Cố định PPU 32 cho toàn bộ 6 bộ asset.
4. [Quyết định giao tiếp phi kết nối](file:///d:/project/projectOOPnew/docs/plan/06_MODULE_DECOUPLING_GUIDE.md): Dùng `ITileQuery` & `IGameEventBus` để cô lập module Đình Huy & Công Huy.
5. [Quyết định tiền tố Git Commit](file:///d:/project/projectOOPnew/docs/plan/04_TEST_QA_GIT_PLAN.md): Bắt buộc dùng `[Đình Huy]:` và `[Công Huy]:`.

## 5. 3 RỦI RO ĐANG MỞ (OPEN RISKS)
- **R1 (Cấu trúc Unity Asmdef):** Cần đảm bảo file `Game.Core.asmdef` chặn sạch `UnityEngine` ngay từ commit đầu tiên.
- **R2 (YAML Merge Conflicts):** Cần tuân thủ không sửa chung file Scene/Prefab; chỉ sửa qua PR phân quyền.
- **R3 (Kỷ luật Git Blame):** Phải phân bố commit ngay từ Tuần 1, không dồn việc vào cuối kỳ.

## 6. VIỆC DANG DỞ (NEXT ACTIONS)
- Hoàn thành Phase P0: Đã cấu hình Packages/manifest.json, ProjectVersion.txt, 4 asmdef, 11 tests.
- Bước tiếp theo của Đình Huy (TV1): Cài đặt Entity cấp cao: Entity.cs -> Actor.cs -> RoguePlayer.cs.
- Hướng tới Mốc M1: Gắn PlayerView (MonoBehaviour Adapter) để hiển thị nhân vật di chuyển WASD trên Scene.
