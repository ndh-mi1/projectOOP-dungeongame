---
name: nunit-aaa-fakes
description: Thiết kế ca kiểm thử NUnit theo mẫu AAA, sử dụng Fakes/Stubs tất định, phủ biên EP/BVA và chống tautology.
---

# HƯỚNG DẪN KIỂM THỬ NUNIT CHUẨN AAA VỚI TEST DOUBLES

## 1. MẪU AAA (ARRANGE - ACT - ASSERT)
```csharp
[Test]
public void TakeDamage_WhenDamageExceedsCurrentHealth_ClampsHealthToZero()
{
    // Arrange: Khởi tạo đối tượng với máu ban đầu là 50
    var health = new Health(maxHealth: 100, currentHealth: 50);
    var damage = new DamageInfo(amount: 80, element: ElementType.Physical);

    // Act: Thực hiện hành vi nhận sát thương
    health.TakeDamage(damage);

    // Assert: Kỳ vọng máu bị chặn dưới tại 0, không được âm
    Assert.AreEqual(0, health.Current);
    Assert.IsTrue(health.IsDead);
}
```

## 2. TEST DOUBLES TẤT ĐỊNH (FAKES / STUBS)
Để kiểm thử logic độc lập khỏi hệ điều hành và phần cứng:
- **`DeterministicRandom`:** Nhận hạt giống (seed) cố định để các ca test về tỉ lệ rơi đồ hoặc chí mạng luôn cho kết quả nhất quán 100%.
- **`InMemorySaveStorage`:** Cài đặt interface `ISaveStorage`, lưu snapshot vào `Dictionary<string, string>` trong RAM thay vì ghi đĩa cứng, giúp test chạy siêu tốc (< 1ms).
- **`MockTileQuery`:** Cài đặt `ITileQuery`, trả về `true` hoặc `false` cho các ô tọa độ cố định để kiểm tra va chạm swept AABB mà không cần khởi tạo cả tầng hầm `Floor`.

## 3. PHỦ BIÊN (EP / BVA) & CHỐNG TAUTOLOGY
- **Phân tích giá trị biên (BVA):**
  - Máu: Biên tại `Current = 0`, `Current = 1`, `Current = Max`, `Current = Max + 1`.
  - Tọa độ lưới: `x = 0`, `x = Width - 1`, ngoài biên `x < 0`, `x >= Width`.
- **Dấu hiệu nhận diện Tautology (CẤM):**
  - Viết: `Assert.AreEqual(damage.Amount - armor.Value, result);` $\rightarrow$ Tautology (chép lại công thức trong code)!
  - Viết chuẩn: `Assert.AreEqual(65, result);` (Tính toán số học độc lập ra giấy trước).
