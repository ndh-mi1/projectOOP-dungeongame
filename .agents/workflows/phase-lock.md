---
description: Khóa một Phase, kiểm tra Definition of Done, cập nhật evidence_tracker và gợi ý tag Git.
---
# QUY TRÌNH ĐÓNG BĂNG & KHÓA GIAI ĐOẠN (/phase-lock)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Giới hạn: <= 60 dòng -->

## CÁC BƯỚC THỰC HIỆN:
1. **Kiểm tra toàn bộ Definition of Done (DoD):**
   - Đọc mục DoD trong file `docs/plan/phases/P<n>_*.md`.
   - Xác nhận mọi checklist đều đã đạt 100% (Test xanh, 0 code chết, 0 catch rỗng).
2. **Kiểm tra cơ chế hai chiều (A <-> B):**
   - Đối chiếu từ thiết kế sang code và từ code ngược lại tài liệu.
   - Cảnh báo nếu trong `docs/understanding/` vẫn còn lớp quan trọng ở Mức 0 hoặc 1.
3. **Cập nhật Bảng minh chứng Checklist:**
   - Cập nhật cột "Mã Nguồn / Artifact Minh Chứng" trong [docs/checklist/evidence_tracker.md](file:///d:/project/projectOOPnew/docs/checklist/evidence_tracker.md).
4. **Cập nhật STATE.json:**
   - Đặt `"phaseStatus": "locked"` cho phase hiện tại trong `docs/agent/STATE.json`.
5. **Đề xuất Tag Git & Soạn Báo cáo:**
   - Gợi ý lệnh gắn tag Git: `git tag -a v0.<n> -m "Phase P<n> completed and locked"`.
   - Nhắc nhở chạy workflow `/report-chapter` để hoàn thiện chương báo cáo tương ứng.
