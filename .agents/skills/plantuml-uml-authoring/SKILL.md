---
name: plantuml-uml-authoring
description: Hướng dẫn vẽ sơ đồ lớp UML 2.5 chuẩn mực bằng PlantUML, biểu diễn đủ 6 quan hệ và tô màu phân hệ TV1/TV2.
---

# HƯỚNG DẪN XÂY DỰNG SƠ ĐỒ UML BẰNG PLANTUML (UML 2.5)

## 1. 6 QUAN HỆ UML BẮT BUỘC (VỚI BỘI SỐ ĐẦY ĐỦ)
Trong bản vẽ UML Class Diagram tổng thể, bắt buộc thể hiện đủ 6 quan hệ:
1. **Thừa kế (Generalization / Inheritance):** `--|>`
   - `RoguePlayer --|> Actor`
2. **Hiện thực hóa (Realization / Interface):** `..|>`
   - `Floor ..|> ITileQuery`
3. **Thành phần phụ thuộc sống chết (Composition):** `*--`
   - `Actor *-- "1" Health` (Health không thể tồn tại độc lập nếu Actor bị hủy).
4. **Tụ hợp chia sẻ vòng đời (Aggregation):** `o--`
   - `Inventory o-- "0..20" Item` (Item có thể tồn tại bên ngoài khi vứt xuống sàn).
5. **Hiệp hội (Association):** `-->`
   - `Actor --> "1" IAttackPattern`
6. **Phụ thuộc lỏng (Dependency):** `..>`
   - `CombatResolver ..> ITileQuery : queries >`

## 2. QUY TẮC ĐỒNG BỘ 100% VỚI MÃ C#
- Tên lớp, interface, enum viết đúng chuẩn PascalCase.
- Ký hiệu tầm vực (Visibility):
  - `-` Private (Bắt buộc cho các field)
  - `#` Protected
  - `+` Public (Các phương thức nghiệp vụ và thuộc tính)
- Chữ ký phương thức: Bắt buộc ghi rõ kiểu tham số và kiểu trả về (Ví dụ: `+ TakeDamage(info: DamageInfo): void`).

## 3. PHÂN ĐỊNH MÀU SẮC THEO THÀNH VIÊN
Sử dụng mã màu CSS trong PlantUML để hội đồng chấm nhận diện ngay tác giả:
```plantuml
package "Combat & AI (Đình Huy)" #FEE2E2 {
    class RoguePlayer {
        - _mana: int
        + CastSkill(skill: ISkill): void
    }
}

package "World & Items (Công Huy)" #DBEAFE {
    class Inventory {
        - _items: List<Item>
        + AddItem(item: Item): bool
    }
}
```
Mỗi khi mã C# thay đổi, chạy workflow `/uml-sync` để cập nhật lại tệp `.puml`.
