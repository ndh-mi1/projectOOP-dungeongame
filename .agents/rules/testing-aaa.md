<!-- activation: glob:tests/** -->
# QUY TẮC KIỂM THỬ TỰ ĐỘNG & CHUẨN AAA (TESTING AAA)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. CẤU TRÚC TEST CHUẨN AAA (ARRANGE - ACT - ASSERT)
Mọi ca kiểm thử tự động trong NUnit bắt buộc phân tách thành 3 khối rõ ràng:
- **Arrange (Chuẩn bị):** Khởi tạo đối tượng cần test, thiết lập trạng thái ban đầu, cấu hình Stub/Mock/Fake.
- **Act (Hành động):** Gọi đúng 1 phương thức cần kiểm thử hành vi.
- **Assert (Khẳng định):** Kiểm tra trạng thái kỳ vọng của đối tượng hoặc kết quả trả về.

## 2. NGUYÊN TẮC ĐỘC LẬP & TẤT ĐỊNH
- **Một hành vi mỗi test:** Mỗi hàm test chỉ kiểm chứng đúng một quy tắc hoặc chuyển trạng thái cụ thể. Tên hàm test phải thể hiện rõ: `<MethodUnderTest>_<Scenario>_<ExpectedBehavior>`.
- **Sử dụng Fake / Stub / Spy:** Không kết nối cơ sở dữ liệu thật, không ghi file thật trong unit test; dùng `InMemorySaveStorage`, `FakeInputProvider`, `MockTileQuery`.
- **Tất định với DeterministicRandom:** Mọi logic sinh ngẫu nhiên (chí mạng, rơi đồ, sinh phòng) phải nhận `IRandom` được gán hạt giống cố định trong test để kết quả luôn luôn lặp lại như nhau.

## 3. PHỦ ĐỦ BIÊN (EP & BVA) VÀ NGOẠI LỆ
- Phân hoạch tương đương (EP): Phủ đủ các miền giá trị hợp lệ và không hợp lệ.
- Phân tích giá trị biên (BVA): Tập trung các điểm biên nhạy cảm như Máu = 0, Máu = Max, góc phòng AABB, chỉ số kho đồ = 0 / tràn dung lượng.
- Kiểm tra trạng thái cấm và ngoại lệ: Dùng `Assert.Throws<InvalidLevelDataException>(() => ...)` để bảo đảm hệ thống chặn đúng khi dữ liệu lỗi.

## 4. CẤM TEST TỰ CHỨNG THỰC (TAUTOLOGY) & ASSERT YẾU
- Cấm chép nguyên công thức từ mã nguồn vào giá trị kỳ vọng trong test. Giá trị kỳ vọng phải được tính toán độc lập trước.
- Cấm các assertion vô nghĩa như `Assert.IsTrue(true)` hoặc chỉ kiểm tra `Assert.IsNotNull(result)`.
- Bắt buộc kiểm tra Mutation: Nếu cố tình sửa toán tử trong mã nghiệp vụ (`<=` thành `<`), ca test tương ứng phải chuyển sang màu ĐỎ ngay lập tức.
