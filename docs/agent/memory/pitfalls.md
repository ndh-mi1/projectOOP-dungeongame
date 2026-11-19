# BỘ NHỚ CÁC BẪY & SAI LẦM CẦN TRÁNH (PITFALLS TO AVOID)
<!-- Cập nhật: 2026-11-19 | Bảo trì bởi: /memory-curate -->

- [2026-11-19][Cả hai][độ tin cậy: cao][nguồn: Checklist K69 Mục 1.4 & R-03] Tuyệt đối không dồn >60% số commit vào 48h cuối trước ngày bảo vệ; sẽ bị trừ điểm hệ số cá nhân hoặc nghi ngờ rửa commit. (kiểm lại khi: kiểm toán Git mỗi tuần)
- [2026-11-19][Đình Huy][độ tin cậy: cao][nguồn: Checklist K69 Tiêu chí 4.4 & Bẫy 1] Không dùng `is/as/typeof` + ép kiểu trong Combat để phân loại vũ khí/kẻ địch; phải dùng đa hình thuần túy qua `IAttackPattern`. (kiểm lại khi: /oop-audit)
- [2026-11-19][Công Huy][độ tin cậy: cao][nguồn: Checklist K69 Tiêu chí 2.2] Cấm viết khối lệnh `catch {}` rỗng hoặc chỉ `Debug.Log`; khi đọc file cấu hình sai phải ném `CustomException` tường minh. (kiểm lại khi: /pre-commit)
- [2026-11-19][Cả hai][độ tin cậy: cao][nguồn: Checklist K69 Tiêu chí 5.1 & Bẫy 4] Cấm viết unit test hình thức (tautology) bằng cách sao chép logic từ mã nguồn sang expected value; sẽ trượt bài Mutation Check. (kiểm lại khi: /mutation-check)
