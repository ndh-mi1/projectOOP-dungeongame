using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Cài đặt thuật toán tìm đường tối ưu A* (A-Star Pathfinding) trên không gian lưới 2D.
    /// Độ phức tạp: O(E * log(V)) với hàm Heuristic khoảng cách Manhattan.
    /// Tách rời độc lập khỏi Engine hiển thị thông qua hợp đồng ITileQuery.
    /// </summary>
    public static class Pathfinder
    {
        private const int DefaultMaxSearchNodes = 2000;
        private const int StepCost = 1;

        private static readonly GridPos[] CardinalDirections = new[]
        {
            new GridPos(0, 1),   // Lên
            new GridPos(0, -1),  // Xuống
            new GridPos(1, 0),   // Phải
            new GridPos(-1, 0)   // Trái
        };

        /// <summary>
        /// Tìm đường đi ngắn nhất từ tọa độ bắt đầu đến tọa độ mục tiêu.
        /// </summary>
        /// <param name="start">Tọa độ xuất phát.</param>
        /// <param name="target">Tọa độ đích đến.</param>
        /// <param name="tileQuery">Hợp đồng truy vấn tính vật cản của ô địa hình.</param>
        /// <param name="maxSearchNodes">Giới hạn số nút mở tối đa chống kẹt lặp vô hạn.</param>
        /// <returns>Danh sách các ô liên tiếp từ start đến đích (bao gồm cả start và đích); trả về rỗng nếu không có đường đi.</returns>
        public static IReadOnlyList<GridPos> FindPath(
            GridPos start,
            GridPos target,
            ITileQuery tileQuery,
            int maxSearchNodes = DefaultMaxSearchNodes)
        {
            if (tileQuery == null)
            {
                throw new ArgumentNullException(nameof(tileQuery), "ITileQuery không được null.");
            }

            if (start == target)
            {
                return new List<GridPos> { start };
            }

            // Xử lý ca biên: Nếu đích đến nằm trong tường/vật cản (TC-10),
            // tìm ô lân cận hợp lệ gần đích nhất để làm mục tiêu mới.
            GridPos actualTarget = target;
            if (!tileQuery.IsWalkable(target.X, target.Y))
            {
                actualTarget = FindNearestWalkableNeighbor(target, start, tileQuery);
                if (actualTarget == target)
                {
                    // Không có ô lân cận nào đi được
                    return Array.Empty<GridPos>();
                }

                if (start == actualTarget)
                {
                    return new List<GridPos> { start };
                }
            }

            var openSet = new PriorityQueue<GridPos, int>();
            var cameFrom = new Dictionary<GridPos, GridPos>();
            var gScore = new Dictionary<GridPos, int>();

            gScore[start] = 0;
            openSet.Enqueue(start, start.ManhattanDistance(actualTarget));

            int searchedCount = 0;

            while (openSet.Count > 0 && searchedCount < maxSearchNodes)
            {
                GridPos current = openSet.Dequeue();
                searchedCount++;

                if (current == actualTarget)
                {
                    return ReconstructPath(cameFrom, current);
                }

                int currentG = gScore[current];

                foreach (GridPos dir in CardinalDirections)
                {
                    GridPos neighbor = current + dir;

                    if (!tileQuery.IsWalkable(neighbor.X, neighbor.Y))
                    {
                        continue;
                    }

                    int tentativeG = currentG + StepCost;

                    if (!gScore.TryGetValue(neighbor, out int existingG) || tentativeG < existingG)
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeG;
                        int fScore = tentativeG + neighbor.ManhattanDistance(actualTarget);
                        openSet.Enqueue(neighbor, fScore);
                    }
                }
            }

            // Không tìm thấy đường đi khả dụng
            return Array.Empty<GridPos>();
        }

        private static GridPos FindNearestWalkableNeighbor(GridPos target, GridPos start, ITileQuery tileQuery)
        {
            GridPos bestNeighbor = target;
            int bestDistance = int.MaxValue;

            foreach (GridPos dir in CardinalDirections)
            {
                GridPos neighbor = target + dir;
                if (tileQuery.IsWalkable(neighbor.X, neighbor.Y))
                {
                    int dist = neighbor.ManhattanDistance(start);
                    if (dist < bestDistance)
                    {
                        bestDistance = dist;
                        bestNeighbor = neighbor;
                    }
                }
            }

            return bestNeighbor;
        }

        private static List<GridPos> ReconstructPath(Dictionary<GridPos, GridPos> cameFrom, GridPos current)
        {
            var path = new List<GridPos> { current };
            while (cameFrom.TryGetValue(current, out GridPos prev))
            {
                current = prev;
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
    }
}
