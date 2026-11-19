<!-- activation: glob:docs/** -->
# QUY CHUẨN TÀI LIỆU KỸ THUẬT & BÁO CÁO (DOCS VIETNAMESE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. VĂN PHONG VÀ NGÔN NGỮ
- Báo cáo và tài liệu trong `docs/` được viết bằng **Tiếng Việt học thuật chuẩn mực**, mạch lạc, súc tích, mang phong cách kỹ sư phần mềm chuyên nghiệp của Viện SAMI – ĐHBK Hà Nội.
- Thuật ngữ chuyên ngành CNTT và OOP (như Polymorphism, Encapsulation, Dependency Inversion, Mock, Stub, Swept AABB) có thể giữ nguyên tiếng Anh hoặc kèm chú giải tiếng Việt chính xác.

## 2. MA TRẬN TRUY VẾT & THẺ MINH CHỨNG
- Mọi tài liệu thiết kế và kiểm thử phải gắn mã số truy vết chuẩn:
  - Luật chơi: `R-01` đến `R-20`.
  - Use Case: `UC01` đến `UC10`.
  - Ca kiểm thử: `TC-01` đến `TC-24` (Unit Tests) và `INT-01` đến `INT-10` (Integration Tests).
  - Lỗi ghi nhận: `Bug-01` đến `Bug-08`.
- Trong báo cáo chương, sử dụng thẻ minh chứng định dạng: `[Trang BC: ChX.Y | Tệp nguồn: src/... | Chỉ số định mức]` để hội đồng chấm dễ dàng tra cứu đối chiếu.

## 3. CÔNG THỨC TOÁN & SƠ ĐỒ
- Công thức toán và độ phức tạp thuật toán được trình bày bằng KaTeX LaTeX:
  - Đặt inline với `$...$` (Ví dụ: Độ phức tạp $\mathcal{O}(V + E)$, tỉ lệ commit $40\% \le p \le 60\%$).
  - Đặt khối công thức riêng với `$$...$$`.
  - Lưu ý: Escape dấu dollar `\$` trong văn xuôi thông thường để tránh kích hoạt nhầm khối công thức.
- Sơ đồ quan hệ và quy trình luồng động sử dụng Mermaid hoặc liên kết tới mã nguồn PlantUML (`.puml`) đặt trong thư mục `docs/design/uml/`.
