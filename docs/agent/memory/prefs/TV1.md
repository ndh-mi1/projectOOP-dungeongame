# SỞ THÍCH & PHONG CÁCH LÀM VIỆC: NGUYỄN ĐÌNH HUY (TV1)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy (202418918) -->

- **Vai trò:** Lead Combat, AI & Physics Engine.
- **Phong cách làm việc:** Ưu tiên thuật toán chính xác, tối ưu hiệu năng tính toán (CPU, RAM, 60 FPS), thích giải thích logic toán học và hình học (Swept AABB, A*).
- **Thói quen lập trình:**
  - Viết test trước hoặc song song với code nghiệp vụ (TDD style).
  - Ưu tiên sử dụng struct hoặc Value Objects bất biến (`readonly struct`) cho dữ liệu toán học (`GridPos`, `DamageInfo`).
  - Rất nghiêm khắc với việc cấm dính `UnityEngine` trong tầng Core.
- **Cách phản hồi mong muốn từ Agent:**
  - Đi thẳng vào vấn đề kỹ thuật, phân tích ưu nhược điểm thuật toán rõ ràng.
  - Luôn kiểm tra ca biên (Edge cases: kẹt góc, vận tốc quá cao).
  - Chủ động đưa ra câu hỏi Teach-back thử thách về độ phức tạp $\mathcal{O}(\cdot)$.
