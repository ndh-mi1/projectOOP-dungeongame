<!-- activation: glob:**/*.cs -->
# QUY ƯỚC ĐẶT TÊN & ĐỒNG NHẤT MÃ NGUỒN (NAMING STYLE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. QUY ƯỚC ĐẶT TÊN C# CHUẨN MỰC
- **Lớp, Struct, Enum, Interface, Method, Property:** Sử dụng **PascalCase**  
  Ví dụ: `RoguePlayer`, `DamageInfo`, `AttackState`, `CalculateDamage()`, `CurrentHealth`.
- **Interface:** Bắt buộc có tiền tố **`I`** viết hoa  
  Ví dụ: `IDamageable`, `IMovable`, `ITileQuery`, `IEnemyAI`.
- **Tham số hàm, Biến cục bộ:** Sử dụng **camelCase**  
  Ví dụ: `targetActor`, `damageAmount`, `remainingCooldown`.
- **Biến thành viên riêng (Private Fields):** Sử dụng **`_camelCase`**  
  Ví dụ: `_maxHealth`, `_attackPattern`, `_eventBus`.
- **Sự kiện (Events & Handlers):**
  - Tên sự kiện: Bắt đầu bằng **`On`** hoặc danh từ hành động quá khứ (Ví dụ: `OnEnemyKilled`, `OnHealthChanged`).
  - Phương thức xử lý: Đặt theo định dạng `Handle<EventName>` (Ví dụ: `HandleEnemyKilled`).

## 2. CHẤT LƯỢNG MÃ & CLEAN CODE
- **Tên có ý nghĩa nghiệp vụ:** Cấm đặt tên biến 1 ký tự (trừ biến chỉ số vòng lặp `i`, `j`, tọa độ `x`, `y`) hoặc tên vô nghĩa như `data`, `temp`, `manager2`, `doWork()`.
- **XML Documentation:** Mọi Interface, Lớp công khai và Phương thức public bắt buộc có chú thích XML (`/// <summary>`) nêu rõ chức năng, tham số (`<param>`) và giá trị trả về (`<returns>`).
- **Nguyên tắc DRY (Don't Repeat Yourself):** Không sao chép các khối lệnh tính toán va chạm hay logic giống nhau giữa các lớp của Đình Huy và Công Huy. Các logic dùng chung phải đưa vào lớp tiện ích hoặc lớp cơ sở.
