## 1. THÔNG TIN PULL REQUEST (PR SUMMARY)
- **Tác giả (Owner):** [Nguyễn Đình Huy (TV1) / Nguyễn Công Huy (TV2)]
- **Người Review (Bắt buộc):** [Thành viên còn lại]
- **Nhánh làm việc:** `feat/tvX-...` $\rightarrow$ `dev`
- **Mô tả thay đổi:** [Mô tả ngắn gọn tính năng vừa hoàn thiện]
- **Liên kết tài liệu liên quan:**
  - Quy tắc / Luật chơi: [Mã R-xx hoặc UCxx]
  - Quyết định kiến trúc (ADR): [Link ADR-XXXX nếu có]
  - Mã Bug Log: [Bug-xx nếu là commit sửa lỗi]

---

## 2. CHECKLIST KIỂM ĐỊNH KỸ THUẬT (REVIEWER CHECKLIST)
- [ ] **Core Purity:** Thư mục `src/Game.Core/` hoàn toàn sạch, $0$ dòng `using UnityEngine;` hoặc `using System.IO;`.
- [ ] **Unit Tests:** Đã viết đầy đủ ca test chuẩn AAA và 100% test chạy XANH trong NUnit EditMode.
- [ ] **Đúng Blueprint:** Cấu trúc lớp và interface khớp 100% với [docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md) và file `.puml`.
- [ ] **Clean Code & SOLID:** Không có code chết, không có `catch {}` rỗng, cấm `is/as` + downcast, cấm field public.
- [ ] **Ranh giới sở hữu:** Tác giả không chỉnh sửa mã nguồn thuộc thư mục độc quyền của Reviewer.


