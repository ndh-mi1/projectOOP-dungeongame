using System;

namespace Game.Core.ValueObjects
{
    /// <summary>
    /// Value Object quản lý sinh lực của thực thể sống (Actor).
    /// Đóng gói và bảo vệ bất biến nghiêm ngặt: 0 <= Current <= Max.
    /// </summary>
    public sealed class Health : IEquatable<Health>
    {
        public int Current { get; private set; }
        public int Max { get; }

        public bool IsDead => Current <= 0;
        public float Percentage => Max > 0 ? (float)Current / Max : 0f;

        public Health(int max, int? current = null)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max), "Sinh lực tối đa phải lớn hơn 0.");

            Max = max;
            int initial = current ?? max;

            if (initial < 0 || initial > max)
                throw new ArgumentOutOfRangeException(nameof(current), "Sinh lực khởi tạo phải nằm trong đoạn [0, Max].");

            Current = initial;
        }

        /// <summary>
        /// Trừ máu khi nhận sát thương. Giá trị không bao giờ tụt xuống dưới 0.
        /// </summary>
        public void Damage(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Lượng sát thương trừ máu không được âm.");

            Current = Math.Max(0, Current - amount);
        }

        /// <summary>
        /// Hồi phục sinh lực. Giá trị không vượt quá Max. Không hồi máu nếu thực thể đã chết.
        /// </summary>
        public void Heal(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Lượng máu hồi phục không được âm.");

            if (IsDead)
                return;

            Current = Math.Min(Max, Current + amount);
        }

        public bool Equals(Health other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Current == other.Current && Max == other.Max;
        }

        public override bool Equals(object obj) => obj is Health other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Current, Max);
        public override string ToString() => $"{Current}/{Max} HP";
    }
}
