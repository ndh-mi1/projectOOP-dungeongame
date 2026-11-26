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

        /// <summary>
        /// Trả về ô bước kế tiếp trên đường đi ngắn nhất từ start đến target (tiện ích dùng chung cho AI).
        /// </summary>
        public static GridPos GetNextStep(GridPos start, GridPos target, ITileQuery tileQuery)
        {
            if (start == target)
            {
                return start;
            }

            IReadOnlyList<GridPos> path = FindPath(start, target, tileQuery);
            return path.Count >= 2 ? path[1] : start;
        }

        /// <summary>
        /// Tìm ô lân cận giúp tăng khoảng cách ra xa mối đe dọa (dùng chung cho KiteAI và FlitAI).
        /// </summary>
        public static GridPos FindRetreatStep(
            GridPos selfPos,
            GridPos threatPos,
            ITileQuery tileQuery,
            bool ignoreWalls = false)
        {
            double bestDistance = selfPos.EuclideanDistance(threatPos);
            GridPos bestCell = selfPos;

            foreach (GridPos dir in GridPos.CardinalDirections)
            {
                GridPos candidate = selfPos + dir;
                if (!ignoreWalls && !tileQuery.IsWalkable(candidate.X, candidate.Y))
                {
                    continue;
                }

                double dist = candidate.EuclideanDistance(threatPos);
                if (dist > bestDistance)
                {
                    bestDistance = dist;
                    bestCell = candidate;
                }
            }

            return bestCell;
        }

        /// <summary>
        /// Quy đổi vector từ nguồn tới đích về hướng vuông góc chủ đạo có độ dài 1 ô.
        /// </summary>
        public static GridPos GetDominantCardinalDirection(GridPos from, GridPos to)
        {
            int dx = to.X - from.X;
            int dy = to.Y - from.Y;

            if (dx == 0 && dy == 0)
            {
                return GridPos.Zero;
            }

            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                return new GridPos(dx > 0 ? 1 : -1, 0);
            }

            return new GridPos(0, dy > 0 ? 1 : -1);
        }

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

            var openSet = new MinHeap();
            var closedSet = new HashSet<GridPos>();
            var cameFrom = new Dictionary<GridPos, GridPos>();
            var gScore = new Dictionary<GridPos, int>();

            gScore[start] = 0;
            openSet.Enqueue(start, start.ManhattanDistance(actualTarget));

            int searchedCount = 0;

            while (openSet.Count > 0 && searchedCount < maxSearchNodes)
            {
                GridPos current = openSet.Dequeue();
                if (!closedSet.Add(current))
                {
                    continue;
                }

                searchedCount++;

                if (current == actualTarget)
                {
                    return ReconstructPath(cameFrom, current);
                }

                int currentG = gScore[current];

                foreach (GridPos dir in GridPos.CardinalDirections)
                {
                    GridPos neighbor = current + dir;

                    if (closedSet.Contains(neighbor) || !tileQuery.IsWalkable(neighbor.X, neighbor.Y))
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

        private sealed class MinHeap
        {
            private readonly struct HeapNode
            {
                public readonly GridPos Pos;
                public readonly int Priority;

                public HeapNode(GridPos pos, int priority)
                {
                    Pos = pos;
                    Priority = priority;
                }
            }

            private readonly List<HeapNode> _heap = new List<HeapNode>();

            public int Count => _heap.Count;

            public void Enqueue(GridPos pos, int priority)
            {
                _heap.Add(new HeapNode(pos, priority));
                SiftUp(_heap.Count - 1);
            }

            public GridPos Dequeue()
            {
                GridPos top = _heap[0].Pos;
                int lastIndex = _heap.Count - 1;
                _heap[0] = _heap[lastIndex];
                _heap.RemoveAt(lastIndex);

                if (_heap.Count > 0)
                {
                    SiftDown(0);
                }

                return top;
            }

            private void SiftUp(int index)
            {
                while (index > 0)
                {
                    int parentIndex = (index - 1) / 2;
                    if (_heap[parentIndex].Priority <= _heap[index].Priority)
                    {
                        break;
                    }

                    Swap(index, parentIndex);
                    index = parentIndex;
                }
            }

            private void SiftDown(int index)
            {
                int count = _heap.Count;
                while (true)
                {
                    int left = (2 * index) + 1;
                    int right = (2 * index) + 2;
                    int smallest = index;

                    if (left < count && _heap[left].Priority < _heap[smallest].Priority)
                    {
                        smallest = left;
                    }

                    if (right < count && _heap[right].Priority < _heap[smallest].Priority)
                    {
                        smallest = right;
                    }

                    if (smallest == index)
                    {
                        break;
                    }

                    Swap(index, smallest);
                    index = smallest;
                }
            }

            private void Swap(int a, int b)
            {
                HeapNode temp = _heap[a];
                _heap[a] = _heap[b];
                _heap[b] = temp;
            }
        }

        private static GridPos FindNearestWalkableNeighbor(GridPos target, GridPos start, ITileQuery tileQuery)
        {
            GridPos bestNeighbor = target;
            int bestDistance = int.MaxValue;

            foreach (GridPos dir in GridPos.CardinalDirections)
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
