---
description: Chạy update-state, sinh bảng DASHBOARD so sánh tiến độ với định mức và cảnh báo tụt lùi.
---
# QUY TRÌNH KIỂM TRA TRẠNG THÁI TIẾN ĐỘ (/status)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Thu thập số liệu tự động:**
   - Chạy script `powershell -File tools/update-state.ps1` để đếm số test, commit, lớp thực tế.
2. **Cập nhật Dashboard dự án:**
   - Đọc dữ liệu từ `docs/agent/STATE.json` và cập nhật bảng so sánh trong `docs/agent/DASHBOARD.md`.
3. **Phân tích độ lệch định mức Checklist K69:**
   - So sánh số lượng Unit Tests thực tế vs Mục tiêu 24 tests (sàn 18).
   - So sánh số lớp UML thực tế vs Mục tiêu 28 lớp (sàn 20).
   - So sánh số lượng PCUs thực tế vs Mục tiêu 10 PCUs (sàn 7).
   - Kiểm tra tỷ lệ Git blame giữa Đình Huy và Công Huy (ngưỡng an toàn: 40% – 60%).
4. **Báo cáo và Cảnh báo:**
   - In ra chat bảng tóm tắt 5 chỉ số cốt lõi.
   - Nếu tiến độ trễ quá 3 ngày so với mốc milestone (M1–M6), phát cảnh báo và đề xuất phương án điều chỉnh WBS hoặc phương án cắt giảm có trật tự ($10 \rightarrow 9 \rightarrow 8 \rightarrow 7$).
