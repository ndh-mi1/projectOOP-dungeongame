# GEMINI.md — CẤU HÌNH ANTIGRAVITY DỰ ÁN CRYSTAL DEPTHS
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. NGUYÊN TẮC HÀNH VI ANTIGRAVITY
- **Đọc AGENTS.md trước:** Luôn tuân thủ 10 luật cứng và quy ước tại `AGENTS.md`.
- **Lập kế hoạch trước:** Trình bày kế hoạch ngắn gọn, nêu rõ giả định trước khi sửa code/file.
- **Tóm tắt ngắn gọn:** Báo cáo kết quả chat dưới 1 trang kèm link file; không sao chép nguyên văn file lớn vào chat.
- **Bảo vệ Context:** Nạp theo tầng (Tier 0/1 lúc mở phiên, Tier 2 khi cần); dùng `/session-start` và `/session-end`.

## 2. KÍCH HOẠT QUY TRÌNH & SLASH COMMANDS
- **Mở/Đóng phiên:** Luôn dùng workflow `/session-start` khi bắt đầu và `/session-end` khi kết thúc.
- **Slash Commands tích hợp:**
  - `/plan`: Lập kế hoạch chi tiết cho các phase kỹ thuật.
  - `/grill-me`: Diễn tập phản biện thiết kế và vấn đáp bảo vệ.
  - `/goal`: Chạy tác vụ tự động hóa sâu có DoD đo được.
- **Workflows tùy biến:** Nằm trong `.agents/workflows/` (`/pre-commit`, `/phase-start`, `/teach-back`, `/ocp-drill`...).
