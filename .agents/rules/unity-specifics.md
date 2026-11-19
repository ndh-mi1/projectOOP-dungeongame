<!-- activation: glob:Assets/**,**/*.asmdef -->
# QUY TẮC PHÁT TRIỂN UNITY & QUẢN LÝ ASSET (UNITY SPECIFICS)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. CẤU HÌNH TÀI NGUYÊN PIXEL ART CHUẨN MỰC
- **Pixels Per Unit (PPU):** Cố định toàn dự án là **32** cho mọi sprite nhân vật, quái vật, địa hình và vật phẩm (kể cả icon 32x32 trích từ Raven Icons).
- **Sprite Import Settings:**
  - Sprite Mode: Single hoặc Multiple (theo atlas/grid sheet).
  - Filter Mode: **Point (no filter)** để giữ nét pixel đặc trưng.
  - Compression: **None** (hoặc High Quality) để tránh biến dạng màu.
- **Serialization Mode:** Bắt buộc cấu hình Unity ở chế độ **Force Text** trong `EditorSettings.asset` để diff được file YAML trên Git.

## 2. QUẢN LÝ FILE META & TRÁNH GÃY GUID
- Mọi tệp trong thư mục `Assets/` đều có tệp `.meta` đi kèm.
- Khi thêm, đổi tên hoặc di chuyển tệp tài nguyên, phải giữ hoặc di chuyển đồng thời tệp `.meta` tương ứng. Tuyệt đối không xóa lẻ `.meta`.

## 3. MONOBEHAVIOUR LÀ ADAPTER MỎNG
- Các lớp kế thừa `MonoBehaviour` (như `ActorView`, `PlayerInputAdapter`, `CombatHudView`) chỉ đóng vai trò chuyển tiếp sự kiện và hiển thị đồ họa.
- Không đưa logic tính toán sát thương, AI hoặc quản lý kho đồ vào MonoBehaviour. MonoBehaviour chỉ lắng nghe sự kiện từ `Game.Core` và kích hoạt Animator, Particle, AudioSource tương ứng.

## 4. CHỐNG XUNG ĐỘT MERGE TRÊN SCENE & PREFAB (YAML)
- Do file Scene (`.unity`) và Prefab (`.prefab`) là định dạng YAML phức tạp rất khó giải quyết xung đột khi merge Git:
  - Phân chia quyền sửa: Nguyễn Đình Huy quản lý `CombatScene` và Prefab thực thể chiến đấu; Nguyễn Công Huy quản lý `WorldScene`, `MenuScene` và UI Prefabs.
  - Tuyệt đối không cùng mở và sửa 1 Scene hoặc 1 Prefab tại cùng một thời điểm. Mọi thay đổi lớn phải trao đổi trước và ghi nhận vào `docs/agent/HANDOFF.md`.
