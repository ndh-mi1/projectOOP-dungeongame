using System;
using Game.Core.ValueObjects;

namespace Game.Core.Events
{
    /// <summary>
    /// Gói dữ liệu sự kiện phát ra khi một quái vật bị tiêu diệt (Máu về 0).
    /// Được TV1 phát tán và TV2 lắng nghe để kích hoạt thuật toán rơi đồ (LootRoller).
    /// </summary>
    public sealed class EnemyKilledEvent
    {
        public Guid EnemyId { get; }
        public string EnemyType { get; }
        public GridPos Position { get; }
        public int ExperienceReward { get; }

        public EnemyKilledEvent(Guid enemyId, string enemyType, GridPos position, int experienceReward)
        {
            EnemyId = enemyId;
            EnemyType = enemyType ?? string.Empty;
            Position = position;
            ExperienceReward = Math.Max(0, experienceReward);
        }
    }
}
