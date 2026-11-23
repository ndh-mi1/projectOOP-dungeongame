using System;

namespace Game.Core.ValueObjects
{
    /// <summary>
    /// Các loại hệ nguyên tố và hình thức sát thương trong giao tranh.
    /// </summary>
    public enum DamageType
    {
        Physical = 0,
        Fire = 1,
        Ice = 2,
        Lightning = 3,
        Poison = 4
    }

    /// <summary>
    /// Value Object đóng gói thông tin một đòn đánh/gói sát thương.
    /// Bất biến, chống phân mảnh tham số khi truyền qua hệ thống Combat.
    /// </summary>
    public readonly struct DamageInfo : IEquatable<DamageInfo>
    {
        public int Amount { get; }
        public DamageType Type { get; }
        public bool IsCritical { get; }

        public DamageInfo(int amount, DamageType type = DamageType.Physical, bool isCritical = false)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Lượng sát thương không được âm.");

            Amount = amount;
            Type = type;
            IsCritical = isCritical;
        }

        public DamageInfo WithBonus(int bonusAmount)
        {
            return new DamageInfo(Amount + bonusAmount, Type, IsCritical);
        }

        public DamageInfo WithType(DamageType newType)
        {
            return new DamageInfo(Amount, newType, IsCritical);
        }

        public bool Equals(DamageInfo other) => Amount == other.Amount && Type == other.Type && IsCritical == other.IsCritical;
        public override bool Equals(object obj) => obj is DamageInfo other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Amount, (int)Type, IsCritical);
        public override string ToString() => $"Damage: {Amount} [{Type}] (Crit: {IsCritical})";
    }
}
