---
description: Thêm lớp mới có kiểm tra Blueprint, giải thích SRP, tạo khung code, test và cập nhật sổ hiểu bài.
---
# QUY TRÌNH THÊM LỚP MỚI VÀO HỆ THỐNG (/new-class)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra sự tồn tại trong Blueprint:**
   - Tra cứu tên lớp dự định tạo trong [docs/plan/02_OOP_BLUEPRINT.md](file:///d:/project/projectOOPnew/docs/plan/02_OOP_BLUEPRINT.md).
   - Nếu lớp chưa có trong thiết kế, yêu cầu chạy workflow `/design-change` trước.
2. **Kích hoạt Teach Mode & Giải thích SRP:**
   - Giải thích lý do tồn tại của lớp trong 1–2 câu súc tích.
   - Nêu rõ: "Nếu bỏ lớp này thì mùi code cụ thể nào sẽ xuất hiện?".
3. **Sinh khung mã nguồn (Class Skeleton):**
   - Tạo file `.cs` đúng namespace và thư mục sở hữu của thành viên trong `src/Game.Core/`.
   - Bảo đảm: Cấm `using UnityEngine;`, cấm field public, có chú thích XML đầy đủ.
4. **Sinh ca Unit Test tương ứng:**
   - Tạo file test chuẩn AAA trong `tests/Game.Core.Tests/` kiểm chứng hành vi chính và ca biên.
5. **Cập nhật Sổ hiểu bài:**
   - Thêm lớp mới vào `docs/understanding/TV1_ledger.md` hoặc `TV2_ledger.md` ở Mức 1.
