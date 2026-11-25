using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.ValueObjects;

namespace Game.Core.Physics
{
    /// <summary>
    /// Bộ phát hiện va chạm liên tục Swept AABB kết hợp Spatial Hash lưới 4x4 ô.
    /// Ngăn chặn triệt để lỗi xuyên tường (Tunneling) khi đạn hoặc nhân vật di chuyển tốc độ cao
    /// và giảm độ phức tạp truy vấn cặp va chạm diện rộng từ O(N^2) xuống O(N).
    /// </summary>
    public static class CollisionDetector
    {
        private const int MaxSlideIterations = 2;
        private const float SkinSeparationEpsilon = 0.001f;
        private const float ZeroVelocityTolerance = 0.0f;
        private const float FullStepTime = 1.0f;

        /// <summary>
        /// Tính va chạm quét khối Swept AABB giữa một hộp đang chuyển động theo (velX, velY) và một hộp tĩnh.
        /// Độ phức tạp: O(1).
        /// </summary>
        public static CollisionHit SweptAABB(AABB2D movingBox, float velX, float velY, AABB2D staticBox)
        {
            ComputeAxisEntryExit(
                movingBox.MinX,
                movingBox.MaxX,
                velX,
                staticBox.MinX,
                staticBox.MaxX,
                out float xEntry,
                out float xExit);

            ComputeAxisEntryExit(
                movingBox.MinY,
                movingBox.MaxY,
                velY,
                staticBox.MinY,
                staticBox.MaxY,
                out float yEntry,
                out float yExit);

            float entryTime = Math.Max(xEntry, yEntry);
            float exitTime = Math.Min(xExit, yExit);

            // Kiểm tra điều kiện xảy ra va chạm trong khoảng thời gian bước [0, 1]
            // Đồng thời loại bỏ trường hợp trượt song song tiếp xúc điểm (entryTime == exitTime)
            if (entryTime >= exitTime || entryTime < ZeroVelocityTolerance || entryTime > FullStepTime)
            {
                return CollisionHit.None;
            }

            float normalX = 0.0f;
            float normalY = 0.0f;

            if (xEntry > yEntry)
            {
                normalX = velX > ZeroVelocityTolerance ? -1.0f : 1.0f;
            }
            else if (yEntry > xEntry)
            {
                normalY = velY > ZeroVelocityTolerance ? -1.0f : 1.0f;
            }
            else
            {
                // Va chạm chính diện đúng góc nhọn: chọn pháp tuyến theo cả 2 trục hoặc trục có vận tốc lớn hơn
                if (Math.Abs(velX) >= Math.Abs(velY))
                {
                    normalX = velX > ZeroVelocityTolerance ? -1.0f : 1.0f;
                }
                else
                {
                    normalY = velY > ZeroVelocityTolerance ? -1.0f : 1.0f;
                }
            }

            return new CollisionHit(true, entryTime, normalX, normalY);
        }

        /// <summary>
        /// Phân giải di chuyển an toàn trên bản đồ ITileQuery: chặn cứng tại mặt tường (chống tunneling)
        /// và tự động trượt theo trục song song nếu đi chéo chạm cạnh tường.
        /// </summary>
        public static MovementResult ResolveTileMovement(
            AABB2D box,
            float velX,
            float velY,
            ITileQuery tileQuery)
        {
            if (tileQuery == null)
            {
                throw new ArgumentNullException(nameof(tileQuery), "ITileQuery không được null.");
            }

            AABB2D currentBox = box;
            float remVelX = velX;
            float remVelY = velY;
            CollisionHit firstHit = CollisionHit.None;

            for (int iteration = 0; iteration < MaxSlideIterations; iteration++)
            {
                if (Math.Abs(remVelX) <= float.Epsilon && Math.Abs(remVelY) <= float.Epsilon)
                {
                    break;
                }

                CollisionHit earliestHit = FindEarliestTileCollision(currentBox, remVelX, remVelY, tileQuery);

                if (!earliestHit.HasHit)
                {
                    currentBox = currentBox.Translate(remVelX, remVelY);
                    break;
                }

                if (!firstHit.HasHit)
                {
                    firstHit = earliestHit;
                }

                float safeTime = Math.Max(ZeroVelocityTolerance, earliestHit.HitTime - SkinSeparationEpsilon);
                currentBox = currentBox.Translate(remVelX * safeTime, remVelY * safeTime);

                float remainingFraction = FullStepTime - earliestHit.HitTime;
                if (remainingFraction <= ZeroVelocityTolerance)
                {
                    break;
                }

                // Trượt dọc mặt phẳng tiếp tuyến với pháp tuyến va chạm (Wall Sliding)
                remVelX = Math.Abs(earliestHit.NormalX) > ZeroVelocityTolerance
                    ? ZeroVelocityTolerance
                    : remVelX * remainingFraction;

                remVelY = Math.Abs(earliestHit.NormalY) > ZeroVelocityTolerance
                    ? ZeroVelocityTolerance
                    : remVelY * remainingFraction;
            }

            return new MovementResult(currentBox.X, currentBox.Y, firstHit);
        }

        private static CollisionHit FindEarliestTileCollision(
            AABB2D box,
            float velX,
            float velY,
            ITileQuery tileQuery)
        {
            AABB2D broadphase = box.GetSweptBroadphase(velX, velY);
            int minTileX = (int)Math.Floor(broadphase.MinX - SkinSeparationEpsilon);
            int maxTileX = (int)Math.Floor(broadphase.MaxX + SkinSeparationEpsilon);
            int minTileY = (int)Math.Floor(broadphase.MinY - SkinSeparationEpsilon);
            int maxTileY = (int)Math.Floor(broadphase.MaxY + SkinSeparationEpsilon);

            CollisionHit earliest = CollisionHit.None;

            for (int tx = minTileX; tx <= maxTileX; tx++)
            {
                for (int ty = minTileY; ty <= maxTileY; ty++)
                {
                    if (tileQuery.IsWalkable(tx, ty))
                    {
                        continue;
                    }

                    AABB2D tileBox = AABB2D.ForTile(tx, ty);
                    CollisionHit hit = SweptAABB(box, velX, velY, tileBox);

                    if (hit.HasHit && (!earliest.HasHit || hit.HitTime < earliest.HitTime))
                    {
                        earliest = hit;
                    }
                }
            }

            return earliest;
        }

        private static void ComputeAxisEntryExit(
            float movingMin,
            float movingMax,
            float velocity,
            float staticMin,
            float staticMax,
            out float entry,
            out float exit)
        {
            if (Math.Abs(velocity) <= float.Epsilon)
            {
                bool isStrictlyOverlapping = movingMax > staticMin && movingMin < staticMax;
                if (isStrictlyOverlapping)
                {
                    entry = float.NegativeInfinity;
                    exit = float.PositiveInfinity;
                }
                else
                {
                    entry = float.PositiveInfinity;
                    exit = float.NegativeInfinity;
                }

                return;
            }

            float invEntry = velocity > ZeroVelocityTolerance
                ? staticMin - movingMax
                : staticMax - movingMin;

            float invExit = velocity > ZeroVelocityTolerance
                ? staticMax - movingMin
                : staticMin - movingMax;

            entry = invEntry / velocity;
            exit = invExit / velocity;
        }
    }

    /// <summary>
    /// Bảng băm không gian lưới cố định (mặc định 4x4 ô mỗi bucket) giúp giảm chi phí kiểm tra
    /// va chạm giữa nhiều thực thể động từ O(N^2) xuống O(N).
    /// </summary>
    public sealed class SpatialHashGrid<T>
    {
        private const int DefaultCellSize = 4;

        private readonly int _cellSize;
        private readonly Dictionary<GridPos, List<T>> _buckets = new Dictionary<GridPos, List<T>>();

        public SpatialHashGrid(int cellSize = DefaultCellSize)
        {
            if (cellSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Kích thước ô SpatialHash phải lớn hơn 0.");
            }

            _cellSize = cellSize;
        }

        public void Clear()
        {
            _buckets.Clear();
        }

        public void Insert(T item, AABB2D bounds)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            GetCellRange(bounds, out int minCx, out int maxCx, out int minCy, out int maxCy);

            for (int cx = minCx; cx <= maxCx; cx++)
            {
                for (int cy = minCy; cy <= maxCy; cy++)
                {
                    var key = new GridPos(cx, cy);
                    if (!_buckets.TryGetValue(key, out List<T> bucket))
                    {
                        bucket = new List<T>();
                        _buckets[key] = bucket;
                    }

                    bucket.Add(item);
                }
            }
        }

        public IReadOnlyCollection<T> Query(AABB2D region)
        {
            var results = new HashSet<T>();
            GetCellRange(region, out int minCx, out int maxCx, out int minCy, out int maxCy);

            for (int cx = minCx; cx <= maxCx; cx++)
            {
                for (int cy = minCy; cy <= maxCy; cy++)
                {
                    if (_buckets.TryGetValue(new GridPos(cx, cy), out List<T> bucket))
                    {
                        foreach (T item in bucket)
                        {
                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }

        private void GetCellRange(AABB2D bounds, out int minCx, out int maxCx, out int minCy, out int maxCy)
        {
            minCx = (int)Math.Floor(bounds.MinX / _cellSize);
            maxCx = (int)Math.Floor(bounds.MaxX / _cellSize);
            minCy = (int)Math.Floor(bounds.MinY / _cellSize);
            maxCy = (int)Math.Floor(bounds.MaxY / _cellSize);
        }
    }
}
