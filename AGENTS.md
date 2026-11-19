# AGENTS.md — HƯỚNG DẪN CỐT LÕI DỰ ÁN CRYSTAL DEPTHS
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. DỰ ÁN VÀ BỐI CẢNH
- **Trò chơi:** Crystal Depths – Hầm Ngục Pha Lê (Roguelike Dungeon 2D Top-down, C#, Unity 6 [6000.0.85f2] URP 2D).
- **Học phần:** MI4090 (K69), SAMI – ĐHBK Hà Nội. Nhóm 2 TV: Nguyễn Đình Huy (TV1) & Nguyễn Công Huy (TV2).
- **Mục tiêu:** Điểm 10.0 (Mức A), mở cả 2 cổng G_Product (Clean-room ≤3p) & G_Epistemic (Hiểu sâu, Trục II ≥3.0/6.0).

## 2. BẢN ĐỒ NGỮ CẢNH & THƯ MỤC CHÍNH
- **Bản đồ nạp tài liệu theo tầng:** Chi tiết tại [docs/agent/INDEX.md](file:///d:/project/projectOOPnew/docs/agent/INDEX.md).
- **Thư mục mã nguồn:**
  - `src/Game.Core/`: Nghiệp vụ thuần C# (0 using UnityEngine, 0 using UnityEditor).
  - `src/Game.Presentation/`: Adapter mỏng MonoBehaviour, TilemapView, HUD UI.
  - `src/Game.Infrastructure/`: Adapter đọc/ghi JSON SaveStorage, PlayerPrefs.
  - `tests/Game.Core.Tests/`: Suite NUnit EditMode headless chạy độc lập (<2s).
- **Lệnh thực thi:**
  - Kiểm tra tĩnh: `tools/check-core-purity.ps1` và `tools/check-code-smells.ps1`.
  - Lệnh test Unity 6 CLI: `"D:\Unity\Editor\6000.0.85f2\Editor\Unity.exe" -batchmode -runTests -testPlatform EditMode -projectPath .`

## 3. 10 ĐIỀU LUẬT CỨNG (BẮT BUỘC TUÂN THỦ)
1. **Game.Core Thuần C#:** Cấm `using UnityEngine;`, `using UnityEditor;`, `using System.Windows.Forms;`. Mọi phụ thuộc hạ tầng phải qua Interface (DIP) tiêm qua Constructor.
2. **Design-First:** Không tự tạo lớp/pattern ngoài [docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md) hoặc ADR đã duyệt. Muốn đổi phải chạy `/design-change`.
3. **Cấm Anti-Patterns:** Cấm field public (dùng property get-only); cấm `is`/`as`/`typeof` + downcast thay đa hình; cấm Singleton God Class; cấm luật chơi trong `Update()`; cấm `catch {}` rỗng; cấm đường dẫn tuyệt đối `C:\`; cấm số/chuỗi ma thuật (đưa vào JSON).
4. **Test Chuẩn AAA & Độc Lập:** Cấu trúc Arrange-Act-Assert; cấm tautology; dùng `DeterministicRandom` và InMemory/Fake stubs; mỗi lớp logic mới phải có test đi kèm.
5. **Ranh Giới Sở Hữu:** Theo [docs/agent/TEAM.md](file:///d:/project/projectOOPnew/docs/agent/TEAM.md). TV1: Combat, AI, Physics; TV2: World, Items, Storage, UI. Sửa file người khác phải qua PR chéo hoặc ghi `HANDOFF.md`.
6. **Agent Không Tự Commit/Push:** Agent chỉ đề xuất lệnh và commit message chuẩn Conventional Commits tiếng Anh kèm tiền tố (`[Đình Huy]:` hoặc `[Công Huy]:`). Sinh viên tự review và commit.
7. **Teach Mode:** Trước khi viết lớp/hàm mới, giải thích ngắn lý do tồn tại và mùi code nếu bỏ. Sau khi xong, đề xuất 1 câu hỏi kiểm tra tư duy.
8. **Kỷ Luật Phiên:** Mở phiên bằng `/session-start`, đóng bằng `/session-end`. Khi ngữ cảnh dài, chạy `/checkpoint`.
9. **An Toàn Thao Tác:** Cấm tự ý xóa file, `git reset --hard`, `push --force`, xóa `.meta`, sửa `ProjectSettings/` khi chưa hỏi.
10. **Trung Thực Số Liệu:** Tuyệt đối không bịa số liệu. Mọi số liệu trong báo cáo/dashboard phải từ lần chạy thật của test, git log hoặc script kiểm toán.

## 4. QUY ƯỚC NGÔN NGỮ
- Giao tiếp: Tiếng Việt chuẩn mực học thuật.
- Mã nguồn, định danh, Git commit, XML doc: 100% tiếng Anh chuẩn PascalCase/camelCase.
- Chuỗi hiển thị trong game: Tiếng Việt, lưu tập trung trong các file JSON (`StreamingAssets/Data/`).

## 5. NGUỒN SỰ THẬT & XỬ LÝ MÂU THUẪN
Thứ tự ưu tiên khi xung đột:
1. Checklist xlsx K69 & Hướng dẫn PDF v2026 của Giảng viên.
2. Tài liệu [docs/plan/](file:///d:/project/projectOOPnew/docs/plan/) đã chốt.
3. Kiến trúc đã duyệt trong ADR ([docs/adr/](file:///d:/project/projectOOPnew/docs/adr/)).
4. Trạng thái trong `STATE.json` / `CONTEXT.md` / `MEMORY.md`.
5. Lời trao đổi trong phiên chat.
*Lưu ý:* Khi phát hiện mâu thuẫn, Agent phải báo cho sinh viên duyệt, không được im lặng chọn một bên. Khi không chắc chắn, đặt 1 câu hỏi cụ thể thay vì suy đoán.
