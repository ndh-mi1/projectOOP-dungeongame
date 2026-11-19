---
name: json-schema-and-config
description: Thiết kế JSON Schema cấu hình game, kiểm tra tính toàn vẹn khi nạp và định nghĩa cây ngoại lệ tùy biến.
---

# THIẾT KẾ DỮ LIỆU CẤU HÌNH JSON VÀ XỬ LÝ NGOẠI LỆ

## 1. QUY ƯỚC ĐẶT TÊN VÀ VỊ TRÍ TỆP DỮ LIỆU
Toàn bộ tệp cấu hình game được lưu trữ tại `Assets/StreamingAssets/Data/`:
- `floors.json`: Danh mục 10 PCUs (Tileset, quái, tỷ lệ phòng).
- `enemies.json`: Cấu hình 7 loại kẻ địch (Máu, tốc độ, AI Strategy, drop table).
- `items.json`: Danh mục 8 vũ khí, giáp và bình thuốc.
- `reactions.json`: 5 phản ứng nguyên tố và hệ số khuếch đại sát thương.

## 2. KIỂM CHỨNG TÍNH TOÀN VẸN (SCHEMA VALIDATION)
Khi nạp file JSON vào bộ nhớ qua `System.Text.Json`:
```csharp
public FloorProfile LoadFloor(string jsonText)
{
    var profile = JsonSerializer.Deserialize<FloorProfile>(jsonText);
    if (string.IsNullOrEmpty(profile.Id) || profile.RoomCount <= 0)
    {
        throw new InvalidLevelDataException(
            $"Dữ liệu tầng hỏng: Id={profile?.Id}, RoomCount={profile?.RoomCount}");
    }
    return profile;
}
```

## 3. CÂY PHÂN CẤP NGOẠI LỆ TÙY BIẾN (CUSTOM EXCEPTIONS)
Tầng Core định nghĩa cây ngoại lệ có cấu trúc rõ ràng trong `Game.Core.Exceptions`:
```
GameException (Base)
├── InvalidLevelDataException (Ném khi file cấu hình sai cú pháp hoặc thiếu field)
├── SaveCorruptedException (Ném khi file save bị sửa tay hỏng checksum)
└── InvalidActionException (Ném khi thực thể ra đòn lúc đang bị Stunned)
```
*Lưu ý:* CẤM viết khối `catch (Exception) {}` rỗng nuốt lỗi. Mọi ngoại lệ bắt buộc phải được ghi log hoặc hiển thị hộp thoại cảnh báo người chơi một cách an toàn.
