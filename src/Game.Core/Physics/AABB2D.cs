using System;
using Game.Core.ValueObjects;

namespace Game.Core.Physics
{
    /// <summary>
    /// Value Object biểu diễn hộp giới hạn thẳng hàng theo trục (Axis-Aligned Bounding Box 2D).
    /// Bất biến, dùng cho phát hiện va chạm liên tục Swept AABB và phân hoạch không gian.
    /// </summary>
    public readonly struct AABB2D : IEquatable<AABB2D>
    {
        private const float TileSize = 1.0f;
        private const float HalfScale = 0.5f;

        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public float MinX => X;
        public float MinY => Y;
        public float MaxX => X + Width;
        public float MaxY => Y + Height;
        public float CenterX => X + (Width * HalfScale);
        public float CenterY => Y + (Height * HalfScale);

        public AABB2D(float x, float y, float width, float height)
        {
            ValidateFinite(x, nameof(x));
            ValidateFinite(y, nameof(y));
            ValidatePositive(width, nameof(width));
            ValidatePositive(height, nameof(height));

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Tạo AABB từ tọa độ tâm và kích thước chiều rộng/cao.
        /// </summary>
        public static AABB2D FromCenter(float centerX, float centerY, float width, float height)
        {
            ValidateFinite(centerX, nameof(centerX));
            ValidateFinite(centerY, nameof(centerY));
            ValidatePositive(width, nameof(width));
            ValidatePositive(height, nameof(height));

            return new AABB2D(
                centerX - (width * HalfScale),
                centerY - (height * HalfScale),
                width,
                height);
        }

        /// <summary>
        /// Tạo AABB chuẩn kích thước 1x1 đơn vị cho một ô lưới bản đồ.
        /// </summary>
        public static AABB2D ForTile(int gridX, int gridY)
        {
            return new AABB2D(gridX, gridY, TileSize, TileSize);
        }

        public static AABB2D ForTile(GridPos pos)
        {
            return ForTile(pos.X, pos.Y);
        }

        /// <summary>
        /// Trả về một AABB mới đã tịnh tiến theo vector (dx, dy).
        /// </summary>
        public AABB2D Translate(float dx, float dy)
        {
            return new AABB2D(X + dx, Y + dy, Width, Height);
        }

        /// <summary>
        /// Kiểm tra hai hộp bao có giao nhau (diện tích giao dương) hay không.
        /// </summary>
        public bool Overlaps(AABB2D other)
        {
            return X < other.MaxX &&
                   MaxX > other.MinX &&
                   Y < other.MaxY &&
                   MaxY > other.MinY;
        }

        /// <summary>
        /// Tính hộp bao rộng (Broadphase) bao trọn cả vị trí xuất phát lẫn vị trí đích của bước di chuyển.
        /// </summary>
        public AABB2D GetSweptBroadphase(float velX, float velY)
        {
            float bx = velX > 0.0f ? X : X + velX;
            float by = velY > 0.0f ? Y : Y + velY;
            float bw = Width + Math.Abs(velX);
            float bh = Height + Math.Abs(velY);
            return new AABB2D(bx, by, bw, bh);
        }

        public bool Equals(AABB2D other)
        {
            return X.Equals(other.X) &&
                   Y.Equals(other.Y) &&
                   Width.Equals(other.Width) &&
                   Height.Equals(other.Height);
        }

        public override bool Equals(object obj) => obj is AABB2D other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);
        public override string ToString() => $"AABB({X:F2}, {Y:F2}, {Width:F2}x{Height:F2})";

        private static void ValidateFinite(float value, string paramName)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                throw new ArgumentException("Giá trị tọa độ AABB phải hữu hạn.", paramName);
            }
        }

        private static void ValidatePositive(float value, string paramName)
        {
            if (float.IsNaN(value) || float.IsInfinity(value) || value <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(paramName, "Kích thước AABB phải lớn hơn 0.");
            }
        }
    }
}
