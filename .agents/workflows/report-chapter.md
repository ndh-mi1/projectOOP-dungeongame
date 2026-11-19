---
description: Soạn thảo chương báo cáo BTL tiếng Việt theo mẫu chuẩn SAMI từ tài liệu và số liệu thực tế sau /phase-lock.
---
# QUY TRÌNH SOẠN THẢO CHƯƠNG BÁO CÁO (/report-chapter)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Xác định chương báo cáo cần viết:**
   - Người dùng gõ `/report-chapter <số_chương>` (từ Chương 1 đến Chương 6).
2. **Nạp khung mẫu chuẩn:**
   - Đọc mẫu cấu trúc tại [docs/report/_TEMPLATE_chapter.md](file:///d:/project/projectOOPnew/docs/report/_TEMPLATE_chapter.md).
3. **Thu thập dữ liệu thực chứng:**
   - Trích xuất số liệu thật từ `docs/agent/STATE.json`, `docs/checklist/evidence_tracker.md`, và git log.
   - Nhúng sơ đồ UML PlantUML hoặc Mermaid tương ứng.
   - Trích dẫn mã nguồn thực tế (tên file, số dòng, chữ ký hàm) đối chiếu hai chiều [$A \leftrightarrow B$].
4. **Biên soạn nội dung chuẩn học thuật:**
   - Viết bằng Tiếng Việt chuẩn mực, mạch lạc, có thẻ minh chứng `[Trang BC: ChX.Y | Tệp nguồn: src/... | Chỉ số định mức]`.
   - Sử dụng công thức LaTeX KaTeX cho độ phức tạp thuật toán và chỉ số đo đạc.
5. **Xuất file chương báo cáo:**
   - Lưu tại `docs/report/chuong_<số_chương>.md` để chuẩn bị biên tập xuất ra PDF chính thức.
