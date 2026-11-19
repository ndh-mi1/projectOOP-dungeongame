# BẢN ĐỒ NGỮ CẢNH THEO TẦNG (TIERED CONTEXT MAP)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

Để tối ưu hóa dung lượng cửa sổ ngữ cảnh (Context Window) và tránh làm loãng tư duy của Agent, tài liệu dự án được phân cấp thành 3 tầng nạp (Tiers). Agent và sinh viên chỉ nạp tầng cần thiết cho từng tác vụ.

---

## 1. PHÂN CẤP 3 TẦNG TÀI LIỆU (TIER 0 / TIER 1 / TIER 2)

```
[Tier 0: Luôn luôn nạp] ───> [Tier 1: Nạp khi mở phiên] ───> [Tier 2: Nạp theo nhu cầu cụ thể]
  - AGENTS.md                 - STATE.json                     - docs/plan/ (Chi tiết phase)
  - GEMINI.md                 - CONTEXT.md                     - docs/design/ (UML, Wireframes)
  - 3 Rules Always            - MEMORY.md (Chỉ mục)            - docs/adr/ (Quyết định kiến trúc)
                              - HANDOFF.md (Phần mới)          - Mã nguồn C# / Test tương ứng
                              - LATEST.md (2 TV)
```

---

## 2. CHI TIẾT CÁC TẦNG NẠP

### TIER 0: LUÔN LUÔN NẠP (MỌI PHIÊN, MỌI LƯỢT)
*Ngân sách ký tự: $\le 9.000$ ký tự.*
- `AGENTS.md`: Tôn chỉ dự án, 10 điều luật cứng, quy ước ngôn ngữ, thứ tự nguồn sự thật.
- `GEMINI.md`: Phong cách phản hồi Antigravity và chỉ dẫn slash commands.
- `.agents/rules/session-protocol.md`: Giao thức mở/đóng phiên và checkpoint.
- `.agents/rules/ownership-integrity.md`: Teach Mode, quyền commit, minh bạch AI.
- `.agents/rules/safety-destructive.md`: Lệnh cấm tự động và vùng bất khả xâm phạm.

### TIER 1: NẠP KHI MỞ PHIÊN (/session-start)
*Được nạp tự động bởi workflow `/session-start` để định vị vị trí hiện tại trong vòng $\le 2$ phút:*
- `docs/agent/STATE.json`: Trạng thái máy đọc được (phase, milestone, mục tiêu vs thực tế).
- `docs/agent/CONTEXT.md`: Ảnh chụp hiện trạng dự án ($\le 80$ dòng).
- `docs/agent/memory/MEMORY.md`: Chỉ mục bộ nhớ ngắn hạn ($\le 120$ dòng).
- `docs/agent/HANDOFF.md`: Các ghi chú chuyển giao việc gần nhất giữa Đình Huy và Công Huy.
- `docs/agent/sessions/TV1/LATEST.md` & `TV2/LATEST.md`: Nhật ký phiên gần nhất của cả 2 bạn.

### TIER 2: NẠP THEO NHU CẦU CỤ THỂ (ON-DEMAND)
*Tuyệt đối không nạp trước. Chỉ đọc khi tác vụ chạm vào phân hệ tương ứng:*
- **Khi làm việc với Kế hoạch / Phase:**
  - `docs/plan/00_MASTER_PLAN.md`: Lộ trình tổng thể, RACI matrix.
  - `docs/plan/phases/P<n>_*.md`: Đọc đúng file phase đang thực thi để kiểm tra DoD và WBS.
- **Khi làm việc với Logic & Kiến trúc OOP:**
  - `docs/plan/02_OOP_BLUEPRINT.md`: 28 lớp UML, 4 tính chất OOP, 5 GoF patterns, 6 thuật toán.
  - `docs/design/DESIGN.md`: Triết lý thiết kế và quy ước chi tiết.
  - `docs/adr/INDEX.md` và các ADR cụ thể: Khi cần xem lại lý do đưa ra quyết định kỹ thuật.
- **Khi làm việc với Đồ họa & Giao diện:**
  - `docs/plan/03_UIUX_ART_DIRECTION.md`: Wireframe 10 màn hình, bảng Game Feel.
  - `docs/ref/asset_coordination_guide.md`: Cẩm nang phối hợp 6 bộ asset, PPU 32.
- **Khi làm việc với Kiểm thử & Báo cáo:**
  - `docs/plan/04_TEST_QA_GIT_PLAN.md`: 34 automated tests, Playtest matrix, Bug Log.
  - `docs/checklist/evidence_tracker.md`: Bảng 20 tiêu chí chấm điểm K69.
  - `docs/understanding/`: Sổ hiểu bài cá nhân `TV1_ledger.md` và `TV2_ledger.md`.

---

## 3. NGUYÊN TẮC "ĐỌC MỤC LỤC TRƯỚC, ĐỌC NỘI DUNG SAU"
Khi cần tra cứu một tài liệu lớn trong Tier 2 (ví dụ `01_CONTENT_BIBLE.md` hay `02_OOP_BLUEPRINT.md`), Agent bắt buộc phải:
1. Đọc phần mục lục hoặc dùng công cụ tìm kiếm từ khóa cụ thể.
2. Chỉ mở đúng khoảng dòng (line range) chứa thông tin cần thiết.
3. Không bao giờ đọc toàn bộ file dài hàng ngàn dòng vào bộ nhớ nếu chỉ cần tìm 1 phương thức hoặc 1 chỉ số.
