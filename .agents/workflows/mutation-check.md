---
description: Thử nghiệm đột biến tại 5 điểm biên nhạy cảm, chạy test kiểm tra mutant bị tiêu diệt và hoàn nguyên.
---
# QUY TRÌNH KIỂM TRA ĐỘT BIẾN NỘI BỘ (/mutation-check)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Lựa chọn 5 điểm đột biến biên nhạy cảm:**
   - Điểm 1: Đổi `currentHp <= 0` thành `currentHp < 0` trong `Health.TakeDamage()`.
   - Điểm 2: Đổi `overlapX && overlapY` thành `overlapX || overlapY` trong va chạm AABB.
   - Điểm 3: Bỏ qua bước kiểm tra `schemaVersion` trong `JsonSaveStorage.Load()`.
   - Điểm 4: Đổi hệ số phản ứng nguyên tố bốc hơi từ `x1.5` thành `x1.0`.
   - Điểm 5: Đổi điều kiện dừng thuật toán A* khi đích đến nằm ngoài tầm nhìn.
2. **Tiêm đột biến tạm thời (Inject Mutant):**
   - Lần lượt sửa đổi từng điểm đột biến trong mã nguồn C#.
3. **Chạy bộ kiểm thử tự động:**
   - Bấm chạy Test Runner trong NUnit EditMode.
   - **Đạt chuẩn:** Ca test tương ứng phải chuyển sang màu ĐỎ (Mutant bị tiêu diệt).
   - **Thất bại:** Test vẫn xanh (Mutant sống sót) $\rightarrow$ Phát hiện test bị yếu hoặc tautology, bắt buộc viết lại assertion chặt chẽ hơn.
4. **Hoàn nguyên mã nguồn (Rollback):**
   - Khôi phục lại 100% mã nguồn gốc ban đầu; xác nhận toàn bộ test quay lại màu XANH.
