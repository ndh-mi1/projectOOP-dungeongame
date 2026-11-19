---
description: Đánh giá tổng kết tuần, kiểm toán Git blame, test, quản lý rủi ro và lập kế hoạch tuần mới.
---
# QUY TRÌNH ĐÁNH GIÁ ĐỊNH KỲ HÀNG TUẦN (/weekly-review)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm toán Git tuần:**
   - Chạy `powershell -File tools/git-audit.ps1` để thống kê:
     - Tổng số commit trong tuần và phân bố theo ngày.
     - Tỷ lệ Git blame logic giữa Nguyễn Đình Huy và Nguyễn Công Huy.
     - Kiểm tra cảnh báo: Liệu có nguy cơ dồn commit vào cuối tuần không?
2. **Kiểm tra tiến độ ca kiểm thử:**
   - Chạy toàn bộ suite test NUnit EditMode, bảo đảm 100% test màu xanh.
   - Thống kê số test mới được viết trong tuần của từng bạn.
3. **Rà soát Sổ hiểu bài:**
   - Mở `docs/understanding/TV1_ledger.md` và `TV2_ledger.md`.
   - Cảnh báo nếu còn lớp logic quan trọng nào đang ở Mức 0 hoặc Mức 1.
4. **Cập nhật Kế hoạch Tuần mới:**
   - Cập nhật mục NOW, NEXT trong `docs/agent/BACKLOG.md`.
   - Ghi biên bản đánh giá tuần vào nhật ký chung.
