---
name: atlas-animation-pipeline
description: Pipeline tự động hóa đọc metadata atlas.json, trích xuất sprite sheet và sinh AnimationClip bằng Editor script.
---

# PIPELINE TỰ ĐỘNG HÓA HOẠT ẢNH TỪ METADATA ATLAS.JSON

## 1. CẤU TRÚC TỆP `atlas.json` CỦA PACK
Trong `assets_src/voxel-dungeon-pack-full/`, tệp `atlas.json` định nghĩa tọa độ khung hình (frame coordinates) của mọi nhân vật và quái vật:
```json
{
  "frames": {
    "knight_idle_down_0": { "frame": {"x": 0, "y": 0, "w": 32, "h": 32} },
    "knight_walk_down_0": { "frame": {"x": 32, "y": 0, "w": 32, "h": 32} }
  }
}
```

## 2. QUY ƯỚC ĐẶT TÊN KHUNG HÌNH (NAMING CONVENTION)
Định dạng tên frame: `<entity>_<action>_<facing>_<frameIndex>`
- `<entity>`: `knight`, `mage`, `dark_knight`, `slime_green`, `skeleton`, `goblin`, `orc_chieftain`.
- `<action>`: `idle`, `walk`, `attack`, `cast`, `hurt`, `die`.
- `<facing>`: `down` (xuống), `up` (lên), `side` (ngang sang phải).
- **Quy tắc hướng trái (Left Facing):** Toàn bộ asset pack chỉ vẽ 3 hướng. Khi nhân vật di chuyển sang trái, View Adapter chỉ cần lật ngang (flip X) SpriteRenderer của hoạt ảnh `side`:
  ```csharp
  spriteRenderer.flipX = (moveDirection.x < 0);
  ```

## 3. CÔNG CỤ EDITOR SCRIPT TỰ ĐỘNG SINH ANIMATIONCLIP
Tại `Assets/Editor/AtlasAnimationBuilder.cs`:
1. Đọc tệp `atlas.json` bằng `JsonUtility` hoặc `System.Text.Json`.
2. Phân nhóm các frame theo `<entity>_<action>_<facing>`.
3. Khởi tạo đối tượng `AnimationClip` mới:
   - Gán tốc độ khung hình (Frame Rate): 8–12 FPS tùy action.
   - Gán mảng `ObjectReferenceKeyframe` chứa các Sprite cắt từ Texture.
   - Cấu hình thuộc tính lặp lại (Loop Time: `true` cho idle/walk, `false` cho attack/die).
4. Lưu tệp `.anim` vào thư mục `Assets/Animations/<Entity>/` qua `AssetDatabase.CreateAsset()`.
5. Tạo `AnimatorOverrideController` gán sẵn vào Prefab trong 3 giây chỉ bằng 1 cú click chuột menu: `Tools > Generate All Dungeon Animations`.
