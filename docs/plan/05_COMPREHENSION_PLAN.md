# KẾ HOẠCH KIỂM ĐỊNH ĐỘ HIỂU SÂU & DIỄN TẬP BẢO VỆ (COMPREHENSION & DRILL PLAN)
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Thành viên:**  
- **Nguyễn Đình Huy – MSSV: 202418918 (TV1 - Lead Combat & AI)**  
- **Nguyễn Công Huy – MSSV: 202418914 (TV2 - Lead World & Storage)**  
**Quy chuẩn áp dụng:** Cổng Hiểu Sâu $G_{Epistemic}$ & Quy trình Vấn đáp 10–15 phút (Phần V & VI Rubric K69)  
**Mục tiêu bảo vệ:** Trục II $\ge 5.0 / 6.0$ | Hệ số cá nhân Nguyễn Đình Huy = 1.00 & Nguyễn Công Huy = 1.00  

---

## 1. QUY CHẾ CỔNG HIỂU SÂU (COMPREHENSION GATE) THEO TỪNG PHASE

Để đảm bảo không có hiện tượng "dán mã AI mà không hiểu" (chống Bẫy 5 K69), nhóm áp dụng quy trình kiểm soát chất lượng **Comprehension Gate** trước khi bất kỳ Phase nào được đóng dấu `[LOCKED]`:

```
[KẾT THÚC CÔNG VIỆC PHASE N]
            │
            ▼
[AGENT SINH 10 CÂU HỎI VẤN ĐÁP B → A]
(5 câu cho Đình Huy / 5 câu cho Công Huy, gồm ≥2 câu hỏi chéo & ≥2 câu Δ/W)
            │
            ▼
[HAI THÀNH VIÊN TỰ TRẢ LỜI KHÔNG NHÌN TÀI LIỆU]
            │
            ▼
[CHẤM ĐIỂM THEO RUBRIC A – F & GHI NHẬN LỖ HỔNG VÀO Pn_gaps.md]
            │
      ┌─────┴────────────────────────┐
      ▼                              ▼
[ĐẠT TỪ MỨC B TRỞ LÊN (≥75%)]   [MỨC C / D / F (<75%)]
      │                              │
      ▼                              ▼
[CHÍNH THỨC CHỐT PHASE]       [SOẠN BÀI ÔN TẬP CẤP TỐC]
(Viết chương Báo cáo)         (Diễn tập lại sau 24h)
```

### Các Ràng buộc Bắt buộc:
1. **Truy vết Chiều ngược ($B \rightarrow A$)**: Giảng viên chỉ thẳng vào một dòng mã nguồn bất kỳ trong thân hàm và yêu cầu sinh viên giải thích: *"Dòng này cài đặt luật nào trong GDD?", "Tại sao điều kiện rẽ nhánh lại viết thế này?", "Bất biến nào đang được bảo vệ ở đây?"*. Tuyệt đối cấm đọc chép lại comment giải thích do AI sinh ra.
2. **Thực hành Sống từ Phase 4 (Live Coding Drill)**: Mỗi thành viên bắt buộc phải tự tay thực hiện 1 bài tập sống trong 2–3 phút khi tắt toàn bộ trợ lý AI:
   - **Nguyễn Đình Huy (TV1)**: Viết sống 1 lớp biến thể AI mới (`NecromancerAI`) hoặc Kỹ năng mới (`IceShieldSkill`).
   - **Nguyễn Công Huy (TV2)**: Viết sống 1 lớp nhà máy đồ mới (`AltarLootFactory`) hoặc Bùa chú Decorator mới (`VampiricEnchant`).
3. **Thao tác Debugger Chuyên nghiệp**: Thành viên phải tự đặt Conditional Breakpoint, kiểm tra biến trong Watch Window và đọc Call Stack để đối chiếu với sơ đồ tuần tự (Sequence Diagram), cấm mò lỗi bằng `Debug.Log`.

---

## 2. NGÂN HÀNG CÂU HỎI MẪU CHO 5 NHÓM CÂU HỎI KIỂM ĐỊNH

### Nhóm 1: Câu hỏi Truy vết Chiều ngược ($B \rightarrow A$)
- **Câu 1.1 (Dành cho Nguyễn Đình Huy - Lõi Máu)**:
  *Chỉ vào phương thức `Health.TakeDamage(int amount)`:*
  - *Hỏi*: Tại sao trong hàm này lại dùng cấu trúc `_currentHp = Math.Max(0, _currentHp - amount)` thay vì phép gán trừ thông thường `_currentHp -= amount`? Bất biến miền nào đang được bảo vệ? Nếu bỏ hàm `Math.Max` thì sự cố gì xảy ra trong gameplay?
  - *Đáp án chuẩn (Mức A)*: Đoạn mã bảo vệ bất biến miền $0 \le \text{HP} \le \text{MaxHP}$. Nếu dùng phép trừ thông thường, lượng sát thương vượt mức sẽ khiến máu bị âm; khi đó hệ thống kiểm tra `if (_currentHp == 0)` ở nơi khác có thể bỏ lọt điều kiện chết, dẫn tới việc nhân vật vẫn di chuyển và đánh được dù đã hết máu (trạng thái zombie).
- **Câu 1.2 (Dành cho Nguyễn Công Huy - Kho đồ)**:
  *Chỉ vào thuộc tính `public IReadOnlyList<Item> Items => _items.AsReadOnly();` trong `Inventory.cs`:*
  - *Hỏi*: Tại sao không trả về trực tiếp danh sách nội bộ `List<Item> _items` mà phải bọc qua `IReadOnlyList`?
  - *Đáp án chuẩn (Mức A)*: Trả về `List<Item>` sẽ làm rò rỉ cấu trúc dữ liệu nội bộ ra ngoài (vi phạm tính đóng gói). Bất kỳ lớp ngoài nào cũng có thể gọi `inventory.Items.Add(item)` hoặc `.Clear()` mà không đi qua các phương thức nghiệp vụ của `Inventory`, phá vỡ bất biến giới hạn sức chứa 16 ô của túi đồ.

---

### Nhóm 2: Câu hỏi Biến động / Nhiễu loạn ($\Delta$ - Perturbation Probes)
- **Câu 2.1 (Dành cho Nguyễn Đình Huy - Đổi cơ chế i-frames)**:
  - *Tình huống $\Delta$*: *"Nếu tôi muốn đổi cơ chế khung hình bất tử (i-frames): thay vì người chơi hoàn toàn miễn nhiễm sát thương, họ sẽ chỉ giảm 50% sát thương nhận vào và bị làm chậm 20% tốc độ, bạn cần sửa chính xác ở những dòng code nào? Vùng ảnh hưởng (Blast Radius) là gì?"*
  - *Đáp án chuẩn (Mức A)*: Chỉ sửa duy nhất phương thức `CalculateDamage` trong lớp `CombatResolver.cs` tại nhánh kiểm tra cờ `target.IsInIframes`. Không cần sửa bất kỳ dòng mã nào trong lớp hiển thị đồ họa `ActorView` hay lớp nhận tín hiệu `PlayerInputAdapter`.
- **Câu 2.2 (Dành cho Nguyễn Công Huy - Đổi cấu hình nạp Save)**:
  - *Tình huống $\Delta$*: *"Nếu nhà trường yêu cầu đổi phương thức lưu game: không lưu ra file JSON trên ổ đĩa nữa mà lưu vào cơ sở dữ liệu SQLite hoặc lưu tạm trong bộ nhớ RAM để test, bạn phải thay đổi ở đâu?"*
  - *Đáp án chuẩn (Mức A)*: Nhờ áp dụng nguyên lý DIP, chỉ cần tạo 1 lớp mới `SqliteSaveStorage` cài đặt interface `ISaveStorage` và tráo đổi việc khởi tạo tại điểm lắp ráp duy nhất `GameBootstrap.cs`. Toàn bộ 100% mã nguồn trong tầng `Game.Core` không phải thay đổi một dòng nào.

---

### Nhóm 3: Câu hỏi Phản biện Đánh đổi Thiết kế (W - Trade-off Defense)
- **Câu 3.1 (Dành cho Nguyễn Đình Huy - State Pattern vs Switch-case)**:
  - *Hỏi*: *"Tại sao bạn lại dùng State Pattern cho Actor và GameSession mà không dùng lệnh `switch(currentState)` trong Update() cho ngắn gọn? Nêu ít nhất 2 nhược điểm cụ thể của State Pattern trong chính dự án này?"*
  - *Đáp án chuẩn (Mức A)*: Switch-case sẽ phình to khủng khiếp khi số trạng thái tăng lên, vi phạm nguyên lý OCP (mỗi lần thêm trạng thái bị choáng hay đóng băng đều phải sửa thân hàm cũ) và vi phạm SRP (logic của 6 trạng thái bị nhồi nhét chung một chỗ). Hai nhược điểm cụ thể: (1) Tăng đột biến số lượng lớp file nhỏ (`IdleState`, `MoveState`, `AttackState`...); (2) Tăng chi phí cấp phát bộ nhớ nếu mỗi lần chuyển trạng thái đều `new` một đối tượng state mới.
- **Câu 3.2 (Dành cho Nguyễn Công Huy - Decorator vs Kế thừa Vũ khí)**:
  - *Hỏi*: *"Tại sao dùng Decorator Pattern cho việc cường hóa vũ khí (`WeaponEnchantDecorator`) thay vì tạo các lớp kế thừa như `BurningSword`, `PoisonSword`? Nêu 2 đánh đổi của Decorator?"*
  - *Đáp án chuẩn (Mức A)*: Tránh hiện tượng bùng nổ tổ hợp lớp con khi có nhiều hiệu ứng (ví dụ: Kiếm vừa Cháy vừa Độc vừa Hút Máu $\rightarrow 2^n$ lớp). Hai đánh đổi: (1) Khó quản lý thứ tự bọc hiệu ứng nếu thứ tự tác động quan trọng; (2) Rất khó gỡ bỏ một bùa chú nằm sâu bên trong chuỗi bọc mà không phải dựng lại toàn bộ đối tượng từ đầu.

---

### Nhóm 4: Câu hỏi Vấn đáp Chéo (Cross-Examination Đình Huy $\leftrightarrow$ Công Huy)
- **Câu 4.1 (Đình Huy chất vấn phân hệ của Công Huy)**:
  - *Hỏi Đình Huy*: *"Giải thích cách mà lớp `Blob47Autotiler` của bạn Công Huy xác định được một ô tường là góc hay mặt ngoài thông qua tệp `autotile.json`?"*
  - *Đáp án chuẩn*: Quét 8 ô lân cận theo thứ tự bitmask (bắc=1, đông=4, nam=16, tây=64...). Các bit góc chéo chỉ được tính khi cả 2 ô vuông góc kề bên đều cùng loại. Tổng bit tạo thành 1 trong 47 giá trị mặt nạ để lấy đúng sprite.
- **Câu 4.2 (Công Huy chất vấn phân hệ của Đình Huy)**:
  - *Hỏi Công Huy*: *"Làm thế nào mà thuật toán Swept AABB của Đình Huy có thể ngăn chặn được hiện tượng đạn bay nhanh xuyên qua tường đá 1 ô ở FPS thấp?"*
  - *Đáp án chuẩn*: Thuật toán không kiểm tra va chạm tại vị trí cuối của frame, mà tạo ra một hình hộp quét (Minkowski sum) kéo dài từ vị trí cũ tới vị trí mới dọc theo vector $\vec{v} \cdot \Delta t$, sau đó tính thời gian va chạm $t_{\text{hit}} \in [0, 1]$ trên từng trục $X$ và $Y$ để chặn thực thể lại ngay bề mặt tiếp xúc.

---

### Nhóm 5: Bộ Bài tập Thực hành Sống (OCP Drill Kit - Tắt Toàn bộ AI)
Bộ bài tập diễn tập viết mã trực tiếp tại bàn chấm trong thời gian 2–3 phút:

#### Bài thực hành OCP cho Nguyễn Đình Huy (Thêm Kẻ địch mới):
- **Đề bài**: Tạo thêm 01 loại quái vật mới `CursedMage` (Pháp sư Nguyền Rủa).
- **Yêu cầu kỹ thuật**:
  1. Tạo file mới `CursedMageAI.cs` kế thừa `IEnemyAI`.
  2. Cài đặt phương thức `ComputeMove(Actor self, Actor target, ITileQuery map)`.
  3. Đăng ký vào `EnemyFactory`.
  4. **Quy tắc vàng**: Tuyệt đối không sửa đổi mã nguồn trong các lớp `RuleEngine`, `GameLoop` hay `Enemy.cs`.

#### Bài thực hành OCP cho Nguyễn Công Huy (Thêm Cường hóa mới):
- **Đề bài**: Tạo thêm 01 bùa cường hóa vũ khí mới `VampiricEnchantDecorator`.
- **Yêu cầu kỹ thuật**:
  1. Tạo file mới `VampiricEnchant.cs` kế thừa `WeaponDecorator`.
  2. Ghi đè phương thức `ExecuteAttack(...)` để cộng thêm logic: Hồi máu cho người tấn công bằng $10\%$ sát thương gây ra.
  3. **Quy tắc vàng**: Tuyệt đối không sửa đổi mã nguồn trong lớp `Weapon` gốc hay lớp `CombatResolver`.

---

### Nhóm 6: Thực hành Live Fault-Injection & Debugger
- **Kịch bản diễn tập**:
  1. Giảng viên cố ý mở tệp `Health.cs` sửa dòng:
     ```csharp
     // Sửa sai cố ý:
     public bool IsDead => _currentHp < 0; // Gốc là <= 0
     ```
  2. Bấm chạy bộ Unit Test trong NUnit Test Runner.
  3. Sinh viên phải chỉ ra ngay: Ca test `TC-02` bị **FAIL đỏ**.
  4. Đặt Breakpoint tại dòng kiểm tra `IsDead`, mở cửa sổ Watch quan sát giá trị `_currentHp = 0`.
  5. Giải thích nguyên nhân gốc rễ (Root Cause) và sửa lại code thành `_currentHp <= 0` để toàn bộ 34 test xanh trở lại trong vòng dưới 2 phút.

---

## 3. LỊCH TRÌNH DIỄN TẬP VẤN ĐÁP 10–15 PHÚT CHUẨN K69

Vào tuần cuối (Phase 5 - T5), nhóm tổ chức 2 buổi tổng duyệt đóng vai (Mock Defense) theo đúng kịch bản thời gian của Hội đồng chấm:

| Khung Thời gian | Nội dung Kiểm tra | Thao tác của Sinh viên | Tiêu chí Đánh giá |
|:---:|---|---|---|
| **Phút 0 – 3** | **Clean-room & Git Audit** | 1. Mở máy chấm sạch, giải nén bản build `.exe`, chạy game ngay lập tức không cài thêm SDK.<br>2. Mở GitHub trình chiếu đồ thị commit rải $\ge 3$ tuần và tỷ lệ blame 40%–60%. | Build chạy mượt $\le 3$ phút; không dồn commit sát hạn; .gitignore chuẩn sạch. |
| **Phút 3 – 7** | **Demo Gameplay & Thử Ca biên** | 1. Đi qua 2 màn chơi liên tiếp, thi triển combo phản ứng nguyên tố.<br>2. Thực hiện Save game $\rightarrow$ Tắt hẳn ứng dụng $\rightarrow$ Mở lại Load khôi phục 100%.<br>3. Chơi thua $\rightarrow$ Bấm Replay dọn sạch bộ nhớ. | 0 crash/freeze; không xuyên tường; âm thanh lưu bền vững; game feel tốt. |
| **Phút 7 – 11** | **Test Runner & Debugger Probe** | 1. Mở Unity Test Runner chạy 34 Automated Tests xanh 100% trong $<2$s.<br>2. Thực hiện bài thi Mutation Check hoặc Live Fault-Injection do thầy chỉ định.<br>3. Đặt Breakpoint, dùng Call Stack và Watch chứng minh khớp Sequence Diagram. | Test thực chất (diệt đột biến); dùng Debugger thành thạo; Call Stack khớp thiết kế. |
| **Phút 11 – 15**| **Phản biện $\Delta$/W & Live OCP** | 1. Trả lời 2 câu hỏi nhiễu loạn $\Delta$ và đánh đổi W.<br>2. Trả lời câu hỏi vấn đáp chéo giữa TV1 và TV2.<br>3. Cả hai cùng gõ bài tập Live OCP (tắt AI) trong 2–3 phút trực tiếp trên IDE. | Nắm vững ranh giới Blast Radius; tự thêm lớp biến thể mới chạy thật; bảo vệ điểm A. |
