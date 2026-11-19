# CHỈ MỤC QUY TẮC AGENT (RULES INDEX)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

Tài liệu này liệt kê toàn bộ 12 quy tắc của dự án, đường dẫn tệp trong `.agents/rules/`, phạm vi áp dụng và cơ chế kích hoạt (Activation Type) để sinh viên dễ dàng tra cứu hoặc cấu hình trên giao diện Antigravity nếu giao diện hỗ trợ quản lý quy tắc trực quan.

| STT | Tên Quy Tắc | Tệp Nguồn | Kiểu Kích Hoạt (`activation`) | Phạm Vi Áp Dụng & Tóm Tắt |
|:---:|---|---|:---:|---|
| 1 | **Session Protocol** | `.agents/rules/session-protocol.md` | `always` | Mọi tác vụ: Quy trình mở phiên `/session-start`, đóng phiên `/session-end`, ngưỡng `/checkpoint`. |
| 2 | **Ownership Integrity** | `.agents/rules/ownership-integrity.md` | `always` | Mọi tác vụ: Teach Mode, không tự ý commit, minh bạch AI, không tự chấm "đã hiểu" cho sinh viên. |
| 3 | **Safety Destructive** | `.agents/rules/safety-destructive.md` | `always` | Mọi tác vụ: Danh sách lệnh cấm tự động (`git reset --hard`, `push --force`), vùng cấm xóa (`.git`, `docs/ref`). |
| 4 | **OOP SOLID C#** | `.agents/rules/oop-solid-csharp.md` | `glob:**/*.cs` | Mã nguồn C#: 4 trụ cột OOP, SOLID, bảo vệ bất biến, Liskov, cấm downcast `is`/`as`, cấm Singleton God. |
| 5 | **Core Purity** | `.agents/rules/core-purity.md` | `glob:src/Game.Core/**,**/*.Core/**` | Tầng Core: Cấm `using UnityEngine;`, `UnityEditor;`, `System.Windows.Forms;`. Mọi phụ thuộc qua Interface. |
| 6 | **Naming Style** | `.agents/rules/naming-style.md` | `glob:**/*.cs` | Mã nguồn C#: Chuẩn PascalCase/camelCase/_field, tiền tố `I`, sự kiện `On...`, XML doc cho API công khai. |
| 7 | **Testing AAA** | `.agents/rules/testing-aaa.md` | `glob:tests/**` | Kiểm thử: Chuẩn AAA, một hành vi mỗi test, Fake/Stub, `DeterministicRandom`, phủ biên EP/BVA, cấm tautology. |
| 8 | **Unity Specifics** | `.agents/rules/unity-specifics.md` | `glob:Assets/**,**/*.asmdef` | Unity Engine: Giữ file `.meta`, asmdef sạch, MonoBehaviour là Adapter mỏng, PPU 32 + Point filter, Force Text. |
| 9 | **Data JSON Config** | `.agents/rules/data-json-config.md` | `glob:**/*.json` | Dữ liệu cấu hình: Schema, đường dẫn tương đối, ném `InvalidLevelDataException` và `SaveCorruptedException`. |
| 10 | **Docs Vietnamese** | `.agents/rules/docs-vietnamese.md` | `glob:docs/**` | Báo cáo & Tài liệu: Tiếng Việt học thuật, thẻ minh chứng `[Trang BC]`, KaTeX math, bảng truy vết 2 chiều. |
| 11 | **Git Commit** | `.agents/rules/git-commit.md` | `model-decision` | Quản lý Git: Chuẩn Conventional Commits tiếng Anh, tiền tố `[Đình Huy]:` / `[Công Huy]:`, PR chéo, cấm dồn commit. |
| 12 | **Design Change** | `.agents/rules/design-change.md` | `model-decision` | Kiến trúc: Quy trình 4 bước đổi thiết kế (ADR $\rightarrow$ Cập nhật UML/Blueprint $\rightarrow$ Code $\rightarrow$ Test). |

---

### Hướng Dẫn Cấu Hình Bằng Tay Trên Giao Diện IDE (Nếu Cần):
1. **Always-On Rules:** Đảm bảo Rule 1, 2, 3 được đánh dấu `Always Active` hoặc đặt trong thư mục gốc.
2. **Glob Pattern Rules:** Gán mẫu pattern tương ứng vào bộ lọc đường dẫn tệp của từng rule (Rule 4 đến Rule 10).
3. **Model-Decision Rules:** Để chế độ kích hoạt tự động theo ngữ cảnh tác vụ (On-demand / Model decision) cho Rule 11 và Rule 12.
