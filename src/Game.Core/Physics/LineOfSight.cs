using System;
using Game.Core.Contracts;
using Game.Core.ValueObjects;

namespace Game.Core.Physics
{
    /// <summary>
    /// Cài đặt thuật toán Bresenham kiểm tra đường nhìn thẳng (Line of Sight - LOS) trên lưới ô 2D.
    /// Độ phức tạp: O(max(|dx|, |dy|)), hoàn toàn dùng số nguyên không có phép chia số thực.
    /// </summary>
    public static class LineOfSight
    {
        /// <summary>
        /// Kiểm tra xem tia nhìn từ ô start tới ô end có thông suốt hay bị ô tường (IsOpaque) che khuất.
        /// Xử lý ca biên kẹp góc: nếu bước chéo đi qua khe giữa 2 ô kề đều chắn sáng thì coi như bị chặn.
        /// </summary>
        public static bool HasLineOfSight(GridPos start, GridPos end, ITileQuery tileQuery)
        {
            if (tileQuery == null)
            {
                throw new ArgumentNullException(nameof(tileQuery), "ITileQuery không được null.");
            }

            if (start == end)
            {
                return true;
            }

            int x0 = start.X;
            int y0 = start.Y;
            int x1 = end.X;
            int y1 = end.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (x0 != x1 || y0 != y1)
            {
                int prevX = x0;
                int prevY = y0;

                if (!(x0 == start.X && y0 == start.Y) && tileQuery.IsOpaque(x0, y0))
                {
                    return false;
                }

                int e2 = 2 * err;
                bool stepX = e2 > -dy;
                bool stepY = e2 < dx;

                // Ca biên góc chéo: nếu bước cả X lẫn Y đồng thời qua góc giữa 2 bức tường đều chắn sáng
                if (stepX && stepY && tileQuery.IsOpaque(prevX + sx, prevY) && tileQuery.IsOpaque(prevX, prevY + sy))
                {
                    return false;
                }

                if (stepX)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (stepY)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return true;
        }
    }
}
