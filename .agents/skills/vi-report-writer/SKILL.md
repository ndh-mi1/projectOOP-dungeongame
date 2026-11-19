---
name: vi-report-writer
description: Hướng dẫn biên soạn báo cáo kỹ thuật BTL chuẩn học thuật SAMI - HUST, nhúng sơ đồ và thẻ minh chứng.
---

# HƯỚNG DẪN SOẠN BÁO CÁO BTL CHUẨN SAMI - HUST (VI REPORT WRITER)

## 1. CẤU TRÚC BÁO CÁO 6 CHƯƠNG CHUẨN MỰC
- **Chương 1: Giới thiệu & Khảo sát bài toán:** Ý tưởng game, 10 PCUs, phân tích Roguelike và 10 Use Cases.
- **Chương 2: Phân tích Nghiệp vụ & Mô hình Miền:** 20 luật chơi ($R\text{-}01 \dots R\text{-}20$), 6 thuật toán và mô hình thực thể.
- **Chương 3: Thiết kế Kiến trúc & Mẫu Thiết Kế:** 01 UML Class Diagram tổng 28 lớp, 5 GoF patterns và 5 sơ đồ động.
- **Chương 4: Cài đặt Hướng đối tượng & SOLID:** Bằng chứng 4 tính chất OOP, ma trận SOLID, kỹ thuật Object Pool và deltaTime.
- **Chương 5: Kiểm thử Phần mềm & Đảm bảo Chất lượng:** 34 tests tự động, Mutation Testing, Playtest Matrix và Bug Log 8 lỗi.
- **Chương 6: Tổng kết & Đánh giá Đóng góp:** Tự đánh giá theo Checklist K69, tỷ lệ đóng góp Git 50/50 của Đình Huy và Công Huy.

## 2. VĂN PHONG VÀ THẺ MINH CHỨNG
- Văn phong học thuật, trung thực, logic chặt chẽ.
- Mọi kết quả nghiệm thu bắt buộc gắn thẻ minh chứng chuẩn:
  `[Trang BC: Ch4.2 | Tệp nguồn: src/Game.Core/Combat/ReactionResolver.cs | Định mức: 5 Reactions]`
- Công thức toán và độ phức tạp: Dùng KaTeX ($\mathcal{O}(V + E)$, tỉ lệ $48\% / 52\%$).

## 3. NHÚNG BẢNG TRUY VẾT HAI CHIỀU [$A \leftrightarrow B$]
Trong mỗi chương, lập bảng truy vết 3 cột chứng minh tính đồng bộ tuyệt đối:
`Luật chơi / Yêu cầu ($R\text{-}xx$)` $\longleftrightarrow$ `Mã nguồn C# ($Class.Method$)` $\longleftrightarrow$ `Mã ca kiểm thử ($TC\text{-}xx$)`.
