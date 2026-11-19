# CHỈ MỤC BỘ NHỚ DỰ ÁN (PROJECT MEMORY INDEX)
<!-- Cập nhật: 2026-11-19 | Bảo trì bởi: /memory-curate | Giới hạn: <= 120 dòng -->

Bộ nhớ dài hạn được lưu trữ theo các nhóm chủ đề riêng biệt. Agent đọc chỉ mục này trước khi cần tra cứu chi tiết.
Định dạng một mục tham chiếu: `[loại] tiêu đề — tệp#mục — ngày cập nhật`

## 1. CÁC SỰ THẬT KỸ THUẬT ĐÃ XÁC MINH (FACTS)
- [fact] Thư mục cấu hình agent là .agents/ — facts.md#antigravity-dir — 2026-11-19
- [fact] Hệ tọa độ chuẩn toàn game là PPU 32 Point Filter — facts.md#ppu-standard — 2026-11-19
- [fact] Ranh giới Core thuần C# không using UnityEngine — facts.md#core-purity — 2026-11-19
- [fact] Định mức sàn Checklist K69 đòi 7 PCU, nhóm làm 10 PCU — facts.md#pcu-scope — 2026-11-19
- [fact] Quy ước tiền tố commit [Đình Huy]: và [Công Huy]: — facts.md#commit-prefix — 2026-11-19
- [fact] Tích hợp MCP Server PixelLab AI Toolkit vẽ asset 32x32 — facts.md#pixellab-mcp — 2026-11-19

## 2. BÀI HỌC KINH NGHIỆM TỪ GỐC RỄ (LESSONS)
- [lesson] Tránh xung đột YAML bằng cách phân chia Scene và Prefab riêng — lessons.md#yaml-conflicts — 2026-11-19
- [lesson] Dùng deterministic random cho unit test để tránh test chập chờn — lessons.md#deterministic-tests — 2026-11-19
- [lesson] Cô lập Core giúp chạy test dưới 2 giây không cần nạp Scene — lessons.md#headless-performance — 2026-11-19
- [lesson] Sử dụng partial class GameBootstrap phân định Composition Root — lessons.md#partial-bootstrap — 2026-11-19

## 3. CÁC BẪY VÀ LỖI CẦN TRÁNH (PITFALLS)
- [pitfall] Cấm tạo God Class GameManager chứa toàn bộ logic — pitfalls.md#god-class — 2026-11-19
- [pitfall] Cấm viết logic game hoặc va chạm trong hàm Update() — pitfalls.md#update-abuse — 2026-11-19
- [pitfall] Cấm dùng is/as + downcast thay thế cho phương thức đa hình — pitfalls.md#downcast-abuse — 2026-11-19
- [pitfall] Cấm dồn quá 60% số commit vào 48 giờ cuối trước ngày chấm — pitfalls.md#commit-dumping — 2026-11-19
- [pitfall] Cấm test tautology tự gán công thức giống mã nguồn — pitfalls.md#tautology-tests — 2026-11-19

## 4. TỪ ĐIỂN THUẬT NGỮ (GLOSSARY)
- [term] Tra cứu thuật ngữ Anh - Việt - Tên Lớp — glossary.md#terms — 2026-11-19

## 5. THÓI QUEN & SỞ THÍCH LÀM VIỆC CỦA THÀNH VIÊN (PREFERENCES)
- [pref] Sở thích và phong cách của Nguyễn Đình Huy (TV1) — prefs/TV1.md — 2026-11-19
- [pref] Sở thích và phong cách của Nguyễn Công Huy (TV2) — prefs/TV2.md — 2026-11-19
