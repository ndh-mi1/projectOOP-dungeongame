# ADR-0001: KHỞI TẠO HẠ TẦNG NGỮ CẢNH VÀ PHÂN QUYỀN KHÔNG XUNG ĐỘT
<!-- Cập nhật: 2026-11-19 | Tác giả: Cả hai (Nguyễn Đình Huy & Nguyễn Công Huy) | Trạng thái: Accepted -->

## 1. BỐI CẢNH (CONTEXT)
Dự án game *Crystal Depths – Hầm Ngục Pha Lê* do nhóm 2 sinh viên (Nguyễn Đình Huy và Nguyễn Công Huy) thực hiện trong học phần MI4090 tại SAMI – ĐHBK Hà Nội. Dự án đòi hỏi vừa phải tuân thủ nghiêm ngặt các tiêu chí chấm điểm khắt khe (Trục II Hiểu sâu $\ge 3.0/6.0$, Clean-room build $\le 3$ phút, tỷ lệ Git blame 40%–60%, 100% C# Core thuần không dính Engine), vừa phải hỗ trợ hai sinh viên làm việc song song độc lập với trợ lý AI mà không xảy ra xung đột merge Git hay mất mát ngữ cảnh giữa các phiên.

## 2. QUYẾT ĐỊNH (DECISION)
Nhóm quyết định thiết lập hạ tầng tương tác Agent phân cấp và cô lập module toàn diện:
1. **Thư mục cấu hình:** Chọn `.agents/` tại thư mục gốc dự án theo chuẩn hiện đại của Antigravity để lưu trữ `rules/`, `workflows/`, và `skills/`.
2. **Nạp ngữ cảnh theo tầng (Tiered Context):**
   - Tier 0: `AGENTS.md` ($\le 6.000$ ký tự), `GEMINI.md` ($\le 2.000$ ký tự) và 3 rules always ($\le 9.000$ ký tự tổng cộng).
   - Tier 1: Nạp khi mở phiên (`STATE.json`, `CONTEXT.md`, `MEMORY.md`, `HANDOFF.md`, `LATEST.md`).
   - Tier 2: Đọc theo mục lục khi cần cho từng task cụ thể.
3. **Phân ranh giới module độc quyền:**
   - Nguyễn Đình Huy (TV1): Sở hữu `src/Game.Core/Entities/`, `Combat/`, `AI/`, `Physics/`, `Loop/`.
   - Nguyễn Công Huy (TV2): Sở hữu `src/Game.Core/World/`, `Items/`, `Storage/`, `src/Game.Infrastructure/`.
   - Kết nối liên module qua `ITileQuery` và `IGameEventBus`. Điểm ghép nối `partial class GameBootstrap`.
4. **Quy tắc tiền tố commit:** Bắt buộc dùng `[Đình Huy]:` và `[Công Huy]:` cho mọi commit Conventional Commits.
5. **Teach Mode & Sổ hiểu bài:** Bật Teach Mode trong các phase cài đặt, quản lý mức độ nắm vững kiến trúc qua `docs/understanding/TV1_ledger.md` và `TV2_ledger.md`.

## 3. CÁC PHƯƠNG ÁN ĐÃ XEM XÉT (ALTERNATIVES CONSIDERED)
- **Phương án 1 (Dùng God Class GameManager chung):** Cả hai bạn cùng viết vào một file quản lý chung.  
  *Lý do loại bỏ:* Gây xung đột merge Git triền miên, vi phạm nguyên lý Single Responsibility (SRP), bị trừ điểm nặng ở tiêu chí 2.4 và 4.5.
- **Phương án 2 (Nhồi nhét toàn bộ tài liệu vào System Prompt mỗi phiên):**  
  *Lý do loại bỏ:* Làm tràn cửa sổ ngữ cảnh (Context Window), tăng chi phí token, làm Agent bị loãng tư duy và dễ sinh ảo giác (hallucination).

## 4. HỆ QUẢ & ĐÁNH ĐỔI (CONSEQUENCES & TRADE-OFFS)
- **Ưu điểm (+):**
  - Thời gian khởi động phiên mới cực nhanh ($\le 2$ phút).
  - Hai thành viên lập trình và viết test song song 100% không bị chặn bởi tiến độ của nhau.
  - Lịch sử Git minh bạch, blame cân bằng 50/50, bảo vệ sinh viên khi vấn đáp trực tiếp.
- **Nhược điểm / Chi phí (-):**
  - Cần duy trì kỷ luật mở/đóng phiên (`/session-start`, `/session-end`).
  - Phải tạo thêm các lớp Adapter và Interface trừu tượng để kết nối giữa 2 phân hệ.
