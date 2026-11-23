using System;

namespace Game.Core.ValueObjects
{
    /// <summary>
    /// Value Object biểu diễn tọa độ nguyên trên lưới ô bản đồ (Grid Coordinates).
    /// Bất biến, hỗ trợ các phép toán vector số nguyên và khoảng cách.
    /// </summary>
    public readonly struct GridPos : IEquatable<GridPos>
    {
        public int X { get; }
        public int Y { get; }

        public GridPos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static GridPos Zero => new GridPos(0, 0);

        /// <summary>
        /// Tính khoảng cách Manhattan giữa 2 điểm trên lưới: |x1 - x2| + |y1 - y2|.
        /// </summary>
        public int ManhattanDistance(GridPos other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        /// <summary>
        /// Tính khoảng cách Euclidean thực tế: sqrt((x1 - x2)^2 + (y1 - y2)^2).
        /// </summary>
        public double EuclideanDistance(GridPos other)
        {
            int dx = X - other.X;
            int dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static GridPos operator +(GridPos a, GridPos b) => new GridPos(a.X + b.X, a.Y + b.Y);
        public static GridPos operator -(GridPos a, GridPos b) => new GridPos(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(GridPos a, GridPos b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(GridPos a, GridPos b) => !(a == b);

        public bool Equals(GridPos other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is GridPos other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"({X}, {Y})";
    }
}
