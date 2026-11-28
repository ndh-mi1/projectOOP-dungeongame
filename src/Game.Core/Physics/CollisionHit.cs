using System;

namespace Game.Core.Physics
{
    /// <summary>
    /// Value Object bất biến mô tả kết quả kiểm tra va chạm Swept AABB.
    /// Lưu thời gian va chạm chuẩn hóa [0, 1] và vector pháp tuyến tại mặt tiếp xúc.
    /// </summary>
    public readonly struct CollisionHit : IEquatable<CollisionHit>
    {
        public static CollisionHit None => new CollisionHit(false, 1.0f, 0.0f, 0.0f);

        public bool HasHit { get; }
        public float HitTime { get; }
        public float NormalX { get; }
        public float NormalY { get; }

        public CollisionHit(bool hasHit, float hitTime, float normalX, float normalY)
        {
            HasHit = hasHit;
            HitTime = hitTime;
            NormalX = normalX;
            NormalY = normalY;
        }

        public bool Equals(CollisionHit other)
        {
            return HasHit == other.HasHit &&
                   HitTime.Equals(other.HitTime) &&
                   NormalX.Equals(other.NormalX) &&
                   NormalY.Equals(other.NormalY);
        }

        public override bool Equals(object obj) => obj is CollisionHit other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(HasHit, HitTime, NormalX, NormalY);
        public override string ToString() => HasHit
            ? $"Hit(t={HitTime:F3}, n=({NormalX}, {NormalY}))"
            : "NoHit";
    }

    /// <summary>
    /// Kết quả phân giải chuyển động cuối cùng sau khi đã chặn tường và trượt mượt (Wall Sliding).
    /// </summary>
    public readonly struct MovementResult
    {
        public float FinalX { get; }
        public float FinalY { get; }
        public CollisionHit FirstHit { get; }

        public MovementResult(float finalX, float finalY, CollisionHit firstHit)
        {
            FinalX = finalX;
            FinalY = finalY;
            FirstHit = firstHit;
        }
    }
}
