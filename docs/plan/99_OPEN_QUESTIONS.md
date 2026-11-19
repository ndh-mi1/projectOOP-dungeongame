# DANH SÁCH GIẢ ĐỊNH KỸ THUẬT & CÂU HỎI MỞ (OPEN QUESTIONS & ASSUMPTIONS)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê | **Học phần:** Lập trình Hướng đối tượng MI4090 (K69)  
**Tài liệu tham chiếu:** `HuongdanXDGame_v2026.pdf` (32 trang) & `Checklist_BTL_Game_v2026.xlsx` (3 sheets)  

---

## 1. XỬ LÝ CHÊNH LỆCH ĐỊNH MỨC GIỮA PDF VÀ CHECKLIST XLSX

Khi có sự sai khác giữa Slide hướng dẫn (`HuongdanXDGame_v2026.pdf`) và Phiếu chấm chính thức (`Checklist_BTL_Game_v2026.xlsx`), nguyên tắc bất di bất dịch của Tech Lead là: **Ưu tiên bảng Checklist Nhóm 2 người (vì đây là phiếu chấm điểm thực tế tại bàn bảo vệ) và luôn chọn cận nghiêm ngặt hơn để đạt điểm A tuyệt đối (10.0/10.0).**

| Hạng mục định mức | Con số trong PDF v2026 | Con số trong Checklist XLSX (Nhóm 02 người) | Con số Nhóm cam kết trong Master Plan | Lý do xử lý & Ràng buộc bảo vệ |
|---|:---:|:---:|:---:|---|
| **Số lượng PCU (Màn/Chế độ)** | $\ge 10$ | $\ge 7$ (hoặc $\ge 5$ màn + $\ge 2$ chế độ) | **10 PCU** (Sàn cứng: 7 PCU) | Mục tiêu 10 PCU đạt trần A; nếu sát deadline cho phép cắt tỉa an toàn về 7 PCU vẫn chuẩn theo Checklist. |
| **Số lượng Use Case cốt lõi** | $\ge 8$ | $\ge 8$ chia đều TV1–TV2 | **10 Use Case** (5 TV1 / 5 TV2) | Chia đều mỗi TV 5 UC độc lập có luồng liên phân hệ thực chất. |
| **Thực thể / VO / Enum thực chất** | $\ge 15$ | $\ge 13$ | **18 lớp** (vượt xa sàn $\ge 13$) | Quy tắc đếm lớp thực chất: loại DTO rỗng, Designer, Enum $\le 2$ giá trị. |
| **Lớp trong 1 UML Class tổng** | $\ge 16$ | $\ge 20$ | **28 lớp** | Checklist yêu cầu $\ge 20$ lớp trên 01 bản vẽ tổng duy nhất; không ghép ảnh rời. |
| **Thuật toán phi tầm thường** | $\ge 4$ ($\ge 2$/TV) | $\ge 4$ ($\ge 2$/TV) | **6 thuật toán** (3 TV1 / 3 TV2) | Mỗi TV sở hữu 3 thuật toán tự viết, có giả mã 1-1 với method và phân tích $O(\cdot)$. |
| **Hành vi AI của Kẻ địch** | — | $\ge 4$ kiểu AI | **7 kiểu AI khác biệt** | Chase, Patrol, Kite, Flit, Ambush, Charge, Slam + Boss FSM đa pha. |
| **Item / Skill & Phản ứng combo** | — | $\ge 4$ Item/Skill có combo | **8 Items + 5 Skills + $\ge 5$ Phản ứng** | Dữ liệu hóa qua `reactions.json`, tương tác xuyên 2 phân hệ TV1 $\leftrightarrow$ TV2. |
| **Mẫu thiết kế GoF** | $\ge 3$ (đủ 3 nhóm) | $\ge 3$ ($\ge 2$ nhóm, nối TV1↔TV2) | **5 mẫu GoF bắt buộc** (đủ 3 nhóm) | State, Strategy, Factory Method, Decorator, Observer (Cầu nối liên TV). |
| **Sơ đồ hành vi động** | $\ge 4$ | $\ge 4$ ($\ge 1$ State + $\ge 1$ Seq/TV) | **5 sơ đồ động** (3 State + 2 Sequence) | Có $\ge 1$ Sequence xuyên TV1 $\leftrightarrow$ TV2; khớp 100% Call Stack Debugger. |
| **Unit Test Pure Logic** | — | $\ge 18$ ($\ge 9$/TV, $\ge 2$ Mock/Stub) | **24 Unit Tests** (12 TV1 / 12 TV2) | Chuẩn AAA, F.I.R.S.T., 100% Pass trong $<2$ giây ở EditMode (Headless). |
| **Integration / Regression Test** | — | $\ge 8$ ($\ge 2$ Save, $\ge 3$ TV, $\ge 3$ Reg) | **10 Integration Tests** | Phủ Save/Load round-trip, va chạm góc, hủy thực thể và khóa lỗi hồi quy. |
| **Tổng số Automated Tests** | $\ge 20$ | $\ge 26$ | **34 Automated Tests** | Vượt qua định mức cổng $G_{Epistemic}$ và kịch bản Live Fault-Injection. |
| **Playtest chéo & Bug Log** | — | $\ge 15$ ca / $\ge 6$ bug / $\ge 3$ reg | **20 ca / 8 bug / 4 reg** | TV1 test phân hệ TV2 và ngược lại, Bug Log đủ 6 cột chuẩn. |
| **Số lượng Commit Git** | $\ge 20$ | $\ge 24$ rải $\ge 3$ tuần, 40–60% | **$\ge 40$ commits** rải $\ge 3$ tuần | Tránh dồn $>60\%$ vào 48h cuối; git blame 40%–60% bảo đảm hệ số cá nhân = 1.00. |

---

## 2. KẾT QUẢ ĐỒNG THUẬN TỪ BƯỚC 1 PHỎNG VẤN NGƯỢC (GRILL-ME)

Toàn bộ 10 câu hỏi đã được chốt với phương án tối ưu:
1. **Engine & Pipeline**: Unity 6 (`6000.0.85f2`) + Universal Render Pipeline (2D URP) với 2D Lights và Pixel Perfect Camera ($32 \times 32$ PPU). Đã xác nhận trực tiếp qua đường dẫn máy tính: `D:\Unity\Editor\6000.0.85f2\Editor\Unity.exe`.
2. **Tiến độ**: 5 tuần thực tế (T1 đến T5) bám sát lộ trình chuẩn PDF. Feature Freeze vào đầu T5.
3. **Phân chia nhân sự**:
   - **TV1**: Chịu trách nhiệm Combat, Real-time Physics/Collision, A* Pathfinding, AI Behaviors, Skills/Status, Game Loop 60Hz, Combat HUD, FX Player.
   - **TV2**: Chịu trách nhiệm World Generation (BSP + MST), Autotile blob47, Item/Inventory/Equipment, Shop/Altar, Save/Load JSON 2 phân hệ, ScreenManager/UI, Audio.
4. **Báo cáo**: File PDF (25–40 trang) xuất từ mẫu báo cáo Khoa Toán - Tin (SAMI - HUST), kèm đầy đủ sơ đồ UML, ma trận truy vết và balancing table.
5. **Ngôn ngữ**: Song ngữ Việt – Anh (UI Tiếng Việt; Code, Identifier, JSON, Commit Tiếng Anh).
6. **Tên Game & Cốt truyện**: *"Crystal Depths – Hầm Ngục Pha Lê"* (Phương án A, tận dụng tối đa `crystal_deco`, `altar`, `dark_knight`, `knight`, `mage`).
7. **Tài nguyên mở rộng**: Font *Press Start 2P* / *Silkscreen* (SIL OFL); Âm thanh & SFX *Kenney Retro Audio* (CC0); BGM *Kenney RPG Audio / Juhani Junkala Retro Mystic* (CC0).
8. **Môi trường chấm**: Windows Standalone (`.exe x64`), Clean-room $\le 3$ phút; dự phòng build WebGL.
9. **Chiến lược Scope**: Triển khai trực tiếp **Mục tiêu Toàn diện 10 PCU (Full Target Scope)** gồm 8 tầng chiến dịch + 2 chế độ mở rộng, 7 AI, 8 items, 5 skills, 5 phản ứng nguyên tố và đầy đủ các mẫu GoF. Không chịu áp lực co hẹp thời gian; các bước cắt giảm chỉ lưu giữ dưới dạng hồ sơ quản trị rủi ro lý thuyết phục vụ phản biện.
10. **Kỹ thuật Kiểm thử**: Dựng sẵn khung Pure Logic tách biệt `UnityEngine` thông qua Assembly Definition `Game.Core.asmdef` và tiêm phụ thuộc qua Adapter để chạy NUnit Headless.

---

## 3. CÁC GIẢ ĐỊNH KỸ THUẬT TIẾP TỤC THEO DÕI

| STT | Giả định kỹ thuật | Rủi ro tiềm ẩn | Biện pháp kiểm soát & Kiểm chứng |
|:---:|---|---|---|
| **A-01** | `Game.Core` độc lập 100% khỏi `UnityEngine.dll`. | Thành viên sơ ý dùng `Mathf`, `Vector2`, `Debug.Log` của Unity trong Core. | Dùng `System.Numerics.Vector2` hoặc tự định nghĩa struct `GridPos`, struct `Vector2D`; cài đặt pre-commit script kiểm tra `using UnityEngine` trong thư mục `src/Game.Core/`. |
| **A-02** | Khung hình vật lý cố định ở 60 FPS ($\Delta t = 1/60 \approx 0.0167$s). | Thiết bị màn hình 144Hz hoặc máy yếu tụt FPS gây tính toán sai lệch tốc độ. | Áp dụng cấu trúc chuẩn Game Loop với bộ tích lũy thời gian (Accumulator loop: `acc += frameDt; while (acc >= dt) { Update(dt); acc -= dt; }`). |
| **A-03** | Toàn bộ dữ liệu cân bằng nạp từ file JSON ngoài tại thư mục `Assets/StreamingAssets/Data/` hoặc đường dẫn tương đối. | Đường dẫn tuyệt đối cứng (VD: `C:\Users\...`) làm gãy Clean-room trên máy chấm. | Bắt buộc dùng `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ...)` hoặc `Application.streamingAssetsPath` thông qua interface `IConfigPathProvider`. |
| **A-04** | Sinh ngẫu nhiên màn chơi có thể tái lập thông qua `Seed`. | Map sinh ngẫu nhiên bị cụt, phòng Boss không có đường vào, chìa khóa rơi sau cửa khóa. | Dùng BFS kiểm tra tính liên thông phòng sau khi dựng MST; kiểm tra đồ thị phụ thuộc (Khóa phải xuất hiện trên đường đi trước Cửa tương ứng). |
