---
description: Diễn tập vấn đáp 10 câu B->A, delta, W, chấm theo Rubric A-F và cập nhật sổ hiểu bài.
---
# QUY TRÌNH DIỄN TẬP VẤN ĐÁP HIỂU SÂU (/teach-back)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Khởi tạo buổi vấn đáp (Đóng vai QA Examiner):**
   - Bộ đề gồm 10 câu hỏi chia đều: 5 câu cho Đình Huy, 5 câu cho Công Huy.
   - Cơ cấu bắt buộc: $\ge 2$ câu hỏi chéo liên module, $\ge 2$ câu biến đổi $\Delta$, $\ge 2$ câu phản biện đánh đổi $W$.
2. **Tiến hành hỏi và ghi nhận:**
   - Đặt từng câu một, yêu cầu sinh viên trả lời trực tiếp trong chat mà không chép code.
   - Thử thách phản ứng: "Nếu tôi đổi điều kiện này thì hệ thống sẽ phản ứng thế nào?".
3. **Đánh giá và Chấm điểm theo Rubric:**
   - Xếp hạng câu trả lời theo thang A–F dựa trên tiêu chí Trục II Hiểu sâu của Viện SAMI.
   - Chỉ ra ngay Red Flags (nếu có dấu hiệu thuộc lòng hoặc Anemic Model).
4. **Cập nhật Sổ hiểu bài:**
   - Cập nhật mức (Mức 0 $\rightarrow$ Mức 3) tương ứng trong `docs/understanding/TV1_ledger.md` hoặc `TV2_ledger.md`.
   - Nếu trả lời chưa đạt, ghi nhận lỗ hổng vào `docs/understanding/gaps/` để ôn tập lại.
