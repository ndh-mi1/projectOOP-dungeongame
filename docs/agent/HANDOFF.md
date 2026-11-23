# SỔ GIAO TIẾP & BÀN GIAO VIỆC LIÊN THÀNH VIÊN (HANDOFF LOG)
<!-- Quy tắc: CHỈ THÊM DÒNG MỚI Ở DƯỚI, KHÔNG SỬA DÒNG CŨ. MỖI MỤC CÓ NGÀY VÀ TÊN NGƯỜI GỬI -->

Tài liệu này ghi lại các thông báo chuyển giao, thay đổi hợp đồng giao tiếp (Interface), rủi ro phát hiện hoặc yêu cầu hỗ trợ giữa **Nguyễn Đình Huy (TV1)** và **Nguyễn Công Huy (TV2)**.

---

### [2026-11-19][Đình Huy] Thiết lập hợp đồng va chạm `ITileQuery`
- **Gửi tới:** Nguyễn Công Huy (TV2)
- **Nội dung:** Mình đã chuẩn bị đặc tả interface `ITileQuery` tại `src/Game.Core/Contracts/ITileQuery.cs`. Phân hệ Combat và AI của mình sẽ chỉ gọi hàm `bool IsWalkable(int x, int y)` và `bool IsOpaque(int x, int y)`. Bạn hãy cho lớp `Floor` bên module World cài đặt interface này nhé.
- **Hạn cần khớp:** Mốc M1 (Giữa Tuần 3).

### [2026-11-19][Công Huy] Thiết lập sự kiện nhặt đồ & rơi trang bị qua `GameEventBus`
- **Gửi tới:** Nguyễn Đình Huy (TV1)
- **Nội dung:** Bên mình sẽ lắng nghe sự kiện `OnEnemyKilled(EnemyKilledEvent e)` từ EventBus của bạn để tính toán rơi đồ (`LootRoller`). Khi quái chết, bạn nhớ bắn sự kiện kèm tọa độ `GridPos` và mã định danh quái vật nhé.
- **Hạn cần khớp:** Mốc M1 (Giữa Tuần 3).

### [2026-11-19][Cả hai] Thống nhất hạ tầng Agent & Quy tắc Commit
- **Nội dung:** Hai bạn đã chốt tiền tố commit `[Đình Huy]:` và `[Công Huy]:`, toàn bộ tài liệu quy hoạch và các script kiểm tra tự động đã sẵn sàng cho Phase P0.

### [2026-11-24][Đình Huy] Hoàn thiện hợp đồng IDamageable, IMovable và cây thực thể Actor
- **Gửi tới:** Nguyễn Công Huy (TV2)
- **Nội dung:** Mình đã hoàn thành các interface [IDamageable.cs](file:///d:/project/projectOOPnew/src/Game.Core/Contracts/IDamageable.cs), [IMovable.cs](file:///d:/project/projectOOPnew/src/Game.Core/Contracts/IMovable.cs) và lớp cơ sở [Actor.cs](file:///d:/project/projectOOPnew/src/Game.Core/Entities/Actor.cs), [RoguePlayer.cs](file:///d:/project/projectOOPnew/src/Game.Core/Entities/RoguePlayer.cs). Khi bạn cài đặt các lớp vật phẩm (`Item.cs`, `Potion.cs`), bạn có thể gọi thẳng phương thức `target.Heal(amount)` hoặc tác động qua `IDamageable` mà không cần quan tâm chi tiết bên trong.
- **Hạn cần khớp:** Mốc M1.
