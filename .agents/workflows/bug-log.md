---
description: Ghi nhận lỗi phát hiện vào Bug Log chuẩn 6 cột, nhắc nhở commit sửa lỗi và viết Regression Test.
---
# QUY TRÌNH GHI NHẬN LỖI BUG LOG & HỒI QUY (/bug-log)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Thu thập thông tin sự cố (Lỗi Playtest hoặc Test hỏng):**
   - Mã lỗi (tăng dần: `Bug-01` đến `Bug-08`).
   - Phân hệ xảy ra sự cố (Combat / World / Save / Physics / UI).
   - Mô tả lỗi và các bước tái hiện (Steps to Reproduce).
   - Mức độ nghiêm trọng (Blocker / Critical / Major / Minor).
   - Thành viên phát hiện và thành viên phụ trách sửa lỗi.
2. **Ghi nhận vào Bảng Bug Log:**
   - Cập nhật dòng mới vào bảng Bug Log chuẩn 6 cột trong `docs/plan/04_TEST_QA_GIT_PLAN.md` và `docs/test/bug_log.md`.
3. **Yêu cầu sửa lỗi & Khóa lỗi bằng Regression Test:**
   - Thành viên phụ trách tiến hành sửa lỗi trong mã nguồn.
   - Bắt buộc viết 1 ca Regression Test tương ứng trong `tests/Regression/` tái hiện lại lỗi đó; xác nhận test từ ĐỎ chuyển sang XANH sau khi sửa.
   - Ghi mã commit sửa lỗi vào cột "Commit Fix" trong Bug Log.
