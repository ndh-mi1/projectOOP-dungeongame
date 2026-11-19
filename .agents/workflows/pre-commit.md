---
description: Kiểm tra trước khi commit: quét Core purity, quét code smells, chạy test nhanh và cảnh báo vi phạm.
---
# QUY TRÌNH KIỂM TRA TRƯỚC COMMIT (/pre-commit)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra tính thuần khiết của Core (Core Purity):**
   - Chạy `powershell -File tools/check-core-purity.ps1`.
   - Nếu phát hiện `using UnityEngine;` hoặc `using System.IO;` trái quy định trong `Game.Core`, dừng quy trình và báo lỗi ngay.
2. **Quét các mẫu mùi code (Anti-patterns):**
   - Chạy `powershell -File tools/check-code-smells.ps1`.
   - Kiểm tra: `catch {}` rỗng, field public, ép kiểu `is/as`, đường dẫn cứng `C:\`, số ma thuật.
3. **Chạy bộ kiểm thử tự động:**
   - Chạy toàn bộ NUnit EditMode unit tests. Bắt buộc 100% test màu xanh (Zero failure).
4. **Kiểm tra ranh giới sở hữu Git:**
   - Rà soát các tệp chuẩn bị commit: Xác nhận thành viên không commit đè lên thư mục độc quyền của bạn kia.
5. **Đưa ra kết luận:**
   - Nếu tất cả đều đạt: In ra "PRE-COMMIT SẠCH 100% - ĐỦ ĐIỀU KIỆN COMMIT".
   - Nếu có vi phạm: Liệt kê chi tiết dòng và tệp lỗi để sinh viên sửa trước khi commit.
