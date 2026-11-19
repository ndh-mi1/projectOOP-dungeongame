# Crystal Depths – Hầm Ngục Pha Lê (Roguelike Dungeon 2D)
<!-- Cập nhật: 2026-11-19 | Đơn vị: SAMI – ĐHBK Hà Nội -->

[![Course](https://img.shields.io/badge/Course-MI4090%20(K69)-blue.svg)](https://sami.hust.edu.vn/)
[![University](https://img.shields.io/badge/University-HUST-red.svg)](https://hust.edu.vn/)
[![Engine](https://img.shields.io/badge/Engine-Unity%202022.3%20LTS%20URP-black.svg)](https://unity.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Core%20(Pure%20C%23)-brightgreen.svg)]()
[![License](https://img.shields.io/badge/License-MIT%20%2F%20CC0-green.svg)]()

> **Bài tập lớn học phần Lập trình hướng đối tượng (MI4090 – K69)**  
> **Viện Toán ứng dụng và Tin học (SAMI), Đại học Bách khoa Hà Nội**  
> **Đề tài số 58 (Phân nhánh 1: Real-time Action / Roguelike Dungeon)**

---

## 👥 THÀNH VIÊN NHÓM PHÁT TRIỂN

| STT | Họ và Tên | Mã Số Sinh Viên (MSSV) | Vai Trò & Phân Hệ Sở Hữu | GitHub / Email |
|:---:|---|:---:|---|---|
| 1 | **Nguyễn Đình Huy (TV1)** | **202418918** | **Lead Combat & Engine Core**<br>• Thực thể & Chiến đấu (Entities, Combat)<br>• 7 Kiểu AI Quái vật (Strategy Pattern)<br>• Va chạm Swept AABB 60Hz & Spatial Hash<br>• Vòng lặp Fixed Timestep & Animation Adapter | [@ndh-mi1](https://github.com/ndh-mi1)<br>`dinggwee03@gmail.com` |
| 2 | **Nguyễn Công Huy (TV2)** | **202418914** | **Lead World, Meta & Storage**<br>• Sinh tầng hầm ngẫu nhiên (BSP + MST)<br>• Ghép ngói tự động Blob47 Autotiler<br>• Kho đồ & Trang bị (Decorator Pattern)<br>• Lưu trữ JSON an toàn & UI ScreenManager | `huy.nc2418914@sis.hust.edu.vn` |

---

## 🎮 TỔNG QUAN TRÒ CHƠI

**Crystal Depths** là trò chơi vượt ải hầm ngục thời gian thực (Top-down 2D Real-time Roguelike) được xây dựng trên nền tảng Unity 2022.3 LTS (URP) và ngôn ngữ C#.
- **Cốt truyện:** Vương quốc sống nhờ Trái Tim Pha Lê cổ đại đặt sâu dưới lòng đất. Hắc Kỵ Sĩ đã đập vỡ nó, khiến các mảnh vỡ phân tán qua nhiều tầng hầm ngục nguy hiểm. Người chơi nhập vai Hiệp sĩ hoặc Pháp sư xuống hầm thu thập 3 mảnh pha lê từ 3 trùm để đối đầu trùm cuối.
- **Quy mô cam kết (Full Scope):**
  - **10 PCUs (Màn chơi cơ chế riêng):** 8 tầng chiến dịch theo cốt truyện + 2 chế độ mở rộng (*Endless Descent* & *Boss Rush*).
  - **7 Kiểu hành vi AI:** Chase, Patrol, Kite, Flit, Ambush, Charge, Slam.
  - **Hệ thống Nguyên tố & Phản ứng:** Lửa, Băng, Sấm sét, Nước, Độc với 5 combo khuếch đại sát thương (Bốc hơi, Tan chảy, Điện giật, Đông cứng, Đốt cháy).
  - **Hiệu năng Real-time:** Khóa cứng 60 FPS, deltaTime/FixedUpdate, Generic Object Pool cho đạn và quái vật.

---

## 🏛️ ĐẶC TRƯNG KIẾN TRÚC HƯỚNG ĐỐI TƯỢNG (OOP & SOLID)

1. **Tách Biệt Lõi Nghiệp Vụ Hoàn Toàn (`Game.Core`):**
   - Tầng `src/Game.Core/` là mã nguồn thuần C# ($0$ lệnh `using UnityEngine;`), biên dịch độc lập và chạy bộ test NUnit EditMode trong môi trường Headless mà không cần mở Scene Unity (< 2 giây).
2. **Khớp Nối Phi Kết Nối (Zero Merge Conflict Architecture):**
   - Hai phân hệ của Đình Huy và Công Huy giao tiếp độc quyền thông qua 2 hợp đồng trừu tượng thuần C# tại `src/Game.Core/Contracts/`:
     - `ITileQuery`: Cung cấp hàm truy vấn ô tường cản mà không lộ mảng dữ liệu nội bộ.
     - `IGameEventBus`: Hệ thống Publish-Subscribe phát sự kiện (quái chết, nhặt đồ, đổi máu) không ghép cứng.
   - Điểm gắn kết duy nhất (Composition Root): `GameBootstrap.cs` được thiết kế dưới dạng `partial class`.
3. **Mẫu Thiết Kế GoF Thực Chất:**
   - **Strategy:** `IEnemyAI` (7 thuật toán quái) và `IAttackPattern` (vũ khí).
   - **State:** `ActorState` (Idle, Walk, Attack, Stunned) bảo vệ trạng thái hoạt ảnh.
   - **Decorator:** `WeaponDecorator` bổ sung thuộc tính nguyên tố vào vũ khí tại runtime.
   - **Observer:** `GameEventBus` kết nối liên phân hệ phi tập trung.
   - **Factory Method:** `LootFactory` sinh vật phẩm rơi theo tỷ lệ ngẫu nhiên có kiểm soát.
   - **Flyweight:** `TileKind` chia sẻ 1 thực thể duy nhất cho hàng vạn ô gạch cùng loại.
   - **Command:** `IPlayerCommand` phân rã phím bấm thành các gói lệnh thực thi có thể Undo/Log.

---

## 📁 CẤU TRÚC THƯ MỤC DỰ ÁN

```
projectOOP-dungeongame/
├── .agents/                      # Cấu hình Agent Antigravity (rules, workflows, skills)
│   ├── rules/                    # 12 quy tắc kiến trúc, OOP, SOLID và an toàn
│   ├── workflows/                # 25 quy trình làm việc tự động hóa (/session-*, /pre-commit...)
│   └── skills/                   # 8 kỹ năng kỹ thuật (asmdef, algorithms, report, test...)
├── assets_src/                   # Tài nguyên đồ họa gốc (6 bộ asset PPU 32 chuẩn)
├── docs/                         # Hồ sơ tài liệu kỹ thuật & quy hoạch
│   ├── adr/                      # Bản ghi kiến trúc quyết định (ADR-0001...)
│   ├── agent/                    # Ngữ cảnh Agent, Dashboard, Handoff, Memory
│   ├── checklist/                # Bảng theo dõi 20 tiêu chí BTL (evidence_tracker.md)
│   ├── design/                   # Sơ đồ thiết kế, UML PlantUML (.puml)
│   ├── plan/                     # Master Plan, Content Bible, OOP Blueprint, 6 Phases (P0-P5)
│   ├── ref/                      # Hướng dẫn kỹ thuật và cẩm nang phối hợp asset
│   ├── report/                   # Khung mẫu soạn thảo Báo cáo BTL 6 chương
│   └── understanding/            # Sổ theo dõi năng lực hiểu bài (TV1 & TV2 ledgers)
├── src/                          # Mã nguồn trò chơi (Triển khai trong Phase P0-P4)
│   ├── Game.Core/                # Nghiệp vụ thuần C# (0 using UnityEngine)
│   ├── Game.Presentation/        # Adapter mỏng MonoBehaviour, TilemapView, HUD UI
│   └── Game.Infrastructure/      # Đọc ghi file JSON, Audio, PlayerPrefs
├── tests/                        # Suite kiểm thử tự động NUnit EditMode
│   └── Game.Core.Tests/          # 34 automated tests (24 Unit Tests + 10 Integration Tests)
├── tools/                        # Bộ script PowerShell kiểm toán tự động
│   ├── check-core-purity.ps1     # Quét using cấm trong Core
│   ├── check-code-smells.ps1     # Quét Anti-patterns, catch rỗng, field public
│   ├── git-audit.ps1             # Kiểm toán Git blame và phân bố commit
│   ├── update-state.ps1          # Đếm test/class/commit cập nhật STATE.json
│   └── check-agent-files.ps1     # Kiểm toán trần ký tự và tính toàn vẹn Agent
├── AGENTS.md                     # Hướng dẫn cốt lõi & 10 điều luật cứng
├── GEMINI.md                     # Cấu hình Antigravity IDE
├── .gitmessage                   # Mẫu thông điệp Conventional Commits kèm tiền tố
└── README.md                     # Tài liệu giới thiệu tổng quan dự án
```

---

## ⚡ HƯỚNG DẪN KIỂM TRA & CHẠY THỬ

### 1. Kiểm tra Tính Thuần Khiết của Tầng Core & Code Smells
```powershell
powershell -File tools/check-core-purity.ps1
powershell -File tools/check-code-smells.ps1
```

### 2. Kiểm toán Hạ tầng Ngữ cảnh Agent
```powershell
powershell -File tools/check-agent-files.ps1
```

### 3. Quy ước Commit trên Git
Mọi commit trên kho lưu trữ bắt buộc có tiền tố tác giả chuẩn:
- Nguyễn Đình Huy: `[Đình Huy]: <type>(<scope>): <subject>`
- Nguyễn Công Huy: `[Công Huy]: <type>(<scope>): <subject>`
- Commit chung: `[Cả hai]: <type>(<scope>): <subject>`

---

## 📜 BẢN QUYỀN & TÀI NGUYÊN (CREDITS)
Toàn bộ tài nguyên âm thanh và hình ảnh trong dự án sử dụng các bộ asset miễn phí có giấy phép **CC0 (Public Domain)** hoặc **Free Commercial Use**:
- *Voxel Dungeon Pack Full* bởi Cole Foster (CC0)
- *Raven Fantasy Icons* bởi Raven (CC0)
- *Tiny RPG Character Asset Pack* bởi Jesse M. (CC0)
- *Tiny Swords Free Pack* bởi Pixel Frog (CC0)
- *2D Pixel Dungeon v2* (CC0)
- *Free Characters Animations* (CC0)
