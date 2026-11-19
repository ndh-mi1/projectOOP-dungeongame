# BẢNG THEO DÕI TIẾN ĐỘ & ĐỊNH MỨC DỰ ÁN (DASHBOARD)
<!-- Cập nhật: 2026-11-19 | Sinh tự động bởi: /status hoặc tools/update-state.ps1 -->

## 1. TIẾN ĐỘ TỔNG QUAN
- **Phase hiện tại:** P0 (Preflight & Environment Setup)
- **Mốc kiểm tra:** M0 (Khởi tạo Hạ tầng)
- **Trạng thái:** Sẵn sàng thực thi
- **Cổng chất lượng:**
  - Cổng $G_{Product}$ (Clean-room Build $\le 3$p, $\ge 80\%$ khối lượng): `NOT_EVALUATED`
  - Cổng $G_{Epistemic}$ (Hiểu sâu, Trục II $\ge 3.0/6.0$): `NOT_EVALUATED`

---

## 2. BẢNG SO SÁNH ĐỊNH MỨC CHECKLIST VS THỰC TẾ

| Hạng Mục Kỹ Thuật | Chỉ Tiêu Sàn Checklist | Mục Tiêu Cam Kết Nhóm | Thực Tế Hiện Tại | Tỷ Lệ Đạt | Đánh Giá |
|---|:---:|:---:|:---:|:---:|:---:|
| **Số màn / PCU cơ chế riêng** | $\ge 7$ | 10 PCUs | 0 | 0% | Chờ P1-P4 |
| **Số Use Case liên thông 2 TV** | $\ge 8$ | 10 (5 TV1 / 5 TV2) | 0 | 0% | Đã đặc tả P1 |
| **Số lớp Entity / VO / Enum thật**| $\ge 13$ | $\ge 16$ | 0 | 0% | Đã thiết kế P2 |
| **Số lớp trong 01 UML Class tổng**| $\ge 20$ | 28 lớp | 0 | 0% | Đã vẽ P3 |
| **Số thuật toán phi tầm thường** | $\ge 4$ | 6 (3 TV1 / 3 TV2) | 0 | 0% | Đã chọn P2 |
| **Số loại AI kẻ địch khác biệt** | $\ge 4$ | 7 loại AI | 0 | 0% | Đã thiết kế P1 |
| **Mẫu thiết kế GoF thực chất** | $\ge 3$ | 5 chính thức + 2 | 0 | 0% | Đã thiết kế P3 |
| **Số sơ đồ động (State/Sequence)**| $\ge 4$ | 5 sơ đồ | 0 | 0% | Đã thiết kế P3 |
| **Số Unit Test thuần logic** | $\ge 18$ | 24 (12 TV1 / 12 TV2)| 0 | 0% | Chờ P4 |
| **Số Integration Tests** | $\ge 8$ | 10 tests | 0 | 0% | Chờ P4-P5 |
| **Tổng số Automated Tests** | $\ge 26$ | 34 tests | 0 | 0% | Chờ P4-P5 |
| **Số ca Playtest chéo** | $\ge 15$ | 20 ca | 0 | 0% | Chờ P5 |
| **Số lỗi Bug Log / Regression** | $\ge 6$ / $\ge 3$ | 8 lỗi / 4 regression | 0 / 0 | 0% | Chờ P5 |
| **Số Commit thực chất trên Git** | $\ge 24$ | $\ge 40$ commits | 0 | 0% | Bắt đầu P0 |
| **Tỷ lệ Git Blame Đình Huy / Công Huy**| 40% – 60% | 50% / 50% | N/A | N/A | Cân bằng |

---

## 3. CẢNH BÁO RỦI RO & PHƯƠNG ÁN CẮT GIẢM (FALLBACK)
- **Chính sách cắt giảm:** Theo cam kết, nhóm chạy trọn vẹn 10 PCUs. Nếu đến giữa Tuần 4 (Mốc M4) bị chậm tiến độ nghiêm trọng, kích hoạt phương án cắt giảm có trật tự: $10 \rightarrow 9 \rightarrow 8 \rightarrow 7$ (vẫn giữ vững sàn 7 PCUs của Checklist).
