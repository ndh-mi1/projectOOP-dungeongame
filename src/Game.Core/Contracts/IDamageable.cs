namespace Game.Core.Contracts
{
    using Game.Core.ValueObjects;

    /// <summary>
    /// Hợp đồng trừu tượng cho mọi đối tượng có khả năng nhận sát thương và hồi phục trong game.
    /// Tuân thủ nguyên lý Phân tách Giao diện (Interface Segregation Principle - ISP).
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Điểm máu hiện tại của thực thể.
        /// </summary>
        int CurrentHealth { get; }

        /// <summary>
        /// Điểm máu tối đa của thực thể.
        /// </summary>
        int MaxHealth { get; }

        /// <summary>
        /// Cho biết thực thể đã chết hay chưa.
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        /// Nhận sát thương dựa trên thông tin gói sát thương truyền vào.
        /// </summary>
        /// <param name="damageInfo">Gói thông tin sát thương bất biến.</param>
        void TakeDamage(DamageInfo damageInfo);

        /// <summary>
        /// Hồi phục điểm máu cho thực thể.
        /// </summary>
        /// <param name="amount">Lượng máu cần hồi (phải lớn hơn 0).</param>
        void Heal(int amount);
    }
}
