---
name: oral-exam-coach
description: Ngân hàng câu hỏi vấn đáp bảo vệ trực tiếp (B->A, Delta, W), thang đánh giá A-F và nhận diện Red Flags.
---

# CẨM NANG HUẤN LUYỆN VẤN ĐÁP BẢO VỆ ĐIỂM A (ORAL EXAM COACH)

## 1. 3 DẠNG CÂU HỎI VẤN ĐÁP CỦA HỘI ĐỒNG CHẤM K69
1. **Dạng 1: Truy vết mã nguồn ngược về thiết kế ($B \rightarrow A$):**
   - Giảng viên chỉ định 1 dòng code hoặc 1 phương thức cụ thể: *"Tại sao phương thức này lại nằm ở lớp này mà không đưa sang lớp kia? Thuộc tính này có vi phạm tính đóng gói không?"*.
   - *Cách trả lời chuẩn:* Trích dẫn nguyên tắc Single Responsibility (SRP) và mô hình miền (Domain Model), chỉ rõ đối tượng nào sở hữu dữ liệu thì đối tượng đó có trách nhiệm thực thi hành vi tương ứng.
2. **Dạng 2: Biến đổi tham số & Phân tích tác động ($\Delta$):**
   - *"Nếu bây giờ tôi muốn thêm điều kiện: khi người chơi bị dính độc thì tốc độ di chuyển bị giảm 30%, bạn sẽ sửa ở đâu và ảnh hưởng đến bao nhiêu lớp?"*.
   - *Cách trả lời chuẩn:* Không sửa `RoguePlayer`, chỉ cần tạo lớp `PoisonSlowDecorator` bọc lấy `Stats` hiện tại. Nhờ nguyên lý OCP, vùng ảnh hưởng (Blast Radius) chỉ là 1 lớp mới duy nhất.
3. **Dạng 3: Phản biện đánh đổi kiến trúc ($W$):**
   - *"Tại sao dự án lại áp dụng mẫu Strategy cho AI thay vì viết các nhánh if/else cho nhanh? Đánh đổi ở đây là gì?"*.
   - *Cách trả lời chuẩn:* Trình bày rõ: Trade-off là tăng số lượng lớp nhỏ, nhưng đổi lại triệt tiêu mùi code Switch Statements, cô lập thuật toán tìm đường A* và cho phép mở rộng AI mới trong 3 phút (Live OCP) mà không làm gãy các quái vật khác.

## 2. NHẬN DIỆN CÁC DẤU HIỆU NGUY HIỂM (RED FLAGS)
- **Học vẹt:** Trả lời trôi chảy khái niệm lý thuyết nhưng khi được yêu cầu chỉ đúng vị trí trong code thì không tìm thấy $\rightarrow$ Bị đánh trượt $G_{Epistemic}$.
- **Anemic Model:** Các lớp chỉ có getter/setter, mọi phép tính dồn vào một lớp Service/Manager $\rightarrow$ Tối đa điểm C.
- **Tự động chấm:** Nghiêm cấm Agent tự nâng mức hiểu bài cho sinh viên; chỉ nâng mức khi sinh viên tự diễn đạt logic bằng chính ngôn ngữ của mình.
