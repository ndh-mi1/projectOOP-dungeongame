<!-- activation: glob:src/Game.Core/**,**/*.Core/** -->
# QUY TẮC CÔ LẬP LÕI NGHIỆP VỤ (CORE PURITY)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. NGUYÊN TẮC CỐT LÕI
Tầng `Game.Core` là trái tim nghiệp vụ của trò chơi, bắt buộc độc lập 100% khỏi Engine đồ họa Unity và hệ điều hành máy tính. Toàn bộ logic của `Game.Core` phải biên dịch và chạy vượt qua bộ test NUnit EditMode trong môi trường Headless mà không cần mở Scene Unity.

## 2. DANH SÁCH USING BỊ CẤM TUYỆT ĐỐI (BLACKLIST)
Trong mọi tệp `.cs` thuộc thư mục `src/Game.Core/` (hoặc bất kỳ thư mục con nào), CẤM chứa các chỉ thị sau:
- `using UnityEngine;` (bao gồm `UnityEngine.UI`, `UnityEngine.Input`, v.v.)
- `using UnityEditor;`
- `using System.Windows.Forms;`
- `using System.Drawing;`
- `using System.IO;` (ngoại trừ các lớp thuộc `Infrastructure/Storage/` nằm ngoài Core; tầng Core chỉ giao tiếp qua `ISaveStorage`).

## 3. DANH SÁCH USING ĐƯỢC PHÉP (WHITELIST)
- `System`
- `System.Collections.Generic`
- `System.Linq`
- `System.Text`
- `System.Text.Json` (hoặc thông qua adapter)
- Các namespace nội bộ thuần C# thuộc `Game.Core.*`

## 4. QUẢN LÝ PHỤ THUỘC NGOẠI VI
1. **Âm thanh, Đồ họa, Nhập liệu (Input):** Core chỉ định nghĩa các interface trừu tượng (`IAudioPlayer`, `IInputProvider`). Việc phát âm thanh hay đọc phím bấm từ bàn phím do các lớp Adapter mỏng trong `Game.Presentation` cài đặt.
2. **Lưu/Tải dữ liệu:** Core định nghĩa interface `ISaveStorage`. Lớp cài đặt cụ thể đọc ghi file JSON `JsonSaveStorage` nằm ở `Game.Infrastructure` và được tiêm vào `SaveManager` qua Constructor.
3. **Bộ sinh số ngẫu nhiên:** Cấm dùng `UnityEngine.Random` trong Core. Bắt buộc dùng `IRandom` với triển khai `DeterministicRandom` dựa trên hạt giống (seed) để phục vụ kiểm thử tất định.

## 5. CÔNG CỤ TỰ ĐỘNG KIỂM TRA
Script `tools/check-core-purity.ps1` sẽ quét toàn bộ thư mục Core và trả về mã lỗi $\ne 0$ nếu phát hiện bất kỳ dòng using bị cấm nào.
