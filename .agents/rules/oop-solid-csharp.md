<!-- activation: glob:**/*.cs -->
# QUY TẮC THIẾT KẾ OOP & SOLID TRONG C# (OOP SOLID CSHARP)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. 4 TRỤ CỘT OOP THỰC CHẤT (CHỐNG MÔ HÌNH THIẾU MÁU)
- **Đóng gói (Encapsulation):** Cấm field public. Sử dụng `{ get; private set; }` hoặc backing field `_camelCase`. Bắt buộc kiểm tra tính hợp lệ và bảo vệ bất biến trong phương thức/constructor (Ví dụ: `Health` chặn âm, chặn vượt Max).
- **Kế thừa (Inheritance):** Dùng quan hệ IS-A thực chất. Không lạm dụng kế thừa để tái sử dụng mã (ưu tiên Composition over Inheritance).
- **Đa hình (Polymorphism):** CẤM kiểm tra kiểu bằng `is`, `as`, `typeof` hay `switch(type)` để phân nhánh hành vi. Mọi hành vi đa hình phải thực thi qua phương thức ảo hoặc interface (ví dụ: `IAttackPattern.Execute()`, `IEnemyAI.DecideNextAction()`).
- **Trừu tượng (Abstraction):** Tách rõ "hợp đồng làm gì" (interface) khỏi "chi tiết làm thế nào" (lớp cài đặt).

## 2. NGUYÊN LÝ SOLID BẮT BUỘC
- **SRP (Single Responsibility):** Mỗi lớp chỉ có đúng 1 lý do để thay đổi và giải thích được trách nhiệm trong 1 câu duy nhất. Tách riêng tính toán sát thương khỏi hiển thị đồ họa.
- **OCP (Open/Closed):** Mở rộng tính năng bằng cách thêm lớp mới kế thừa interface có sẵn, cấm sửa đổi code cũ đang chạy ổn định.
- **LSP (Liskov Substitution):** Lớp con phải thay thế hoàn toàn được lớp cha mà không làm thay đổi tính đúng đắn của chương trình (đặc biệt khi truyền đối tượng giữa TV1 và TV2 qua interface).
- **ISP (Interface Segregation):** Chia nhỏ interface theo nhu cầu client (`IDamageable`, `IMovable`, `ITileQuery`), không tạo "Fat Interface".
- **DIP (Dependency Inversion):** Lớp cấp cao không phụ thuộc lớp cấp thấp; cả hai phụ thuộc vào trừu tượng. Tiêm phụ thuộc qua Constructor, không tự `new` hạ tầng trong Core.

## 3. DANH SÁCH ANTI-PATTERNS BỊ TRỪ ĐIỂM
- Cấm tạo Singleton `GameManager` chứa toàn bộ logic.
- Cấm đặt logic game hoặc tính toán vật lý trong hàm `Update()` của MonoBehaviour.
- Cấm khối lệnh `catch {}` rỗng; mọi lỗi nghiệp vụ phải ném ngoại lệ tùy biến tường minh.
