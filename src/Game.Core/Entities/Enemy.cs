using System;
using Game.Core.AI;
using Game.Core.Contracts;
using Game.Core.Events;
using Game.Core.ValueObjects;

namespace Game.Core.Entities
{
    /// <summary>
    /// Thực thể kẻ địch trong hầm ngục kế thừa từ Actor.
    /// Minh chứng GoF Strategy Pattern: Ủy quyền ra quyết định di chuyển cho IEnemyAI.
    /// Hỗ trợ hoán đổi chiến thuật trong thời gian chạy (Runtime Strategy Swap).
    /// </summary>
    public class Enemy : Actor
    {
        private const int DefaultMinReward = 0;
        private IEnemyAI _aiStrategy;
        private readonly IGameEventBus _eventBus;

        public string EnemyType { get; }
        public int ExperienceReward { get; }
        public IEnemyAI CurrentAI => _aiStrategy;

        public Enemy(
            Guid id,
            string name,
            string enemyType,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed,
            int experienceReward,
            IEnemyAI initialAI,
            IGameEventBus eventBus = null)
            : base(id, name, initialX, initialY, maxHealth, baseSpeed)
        {
            if (string.IsNullOrWhiteSpace(enemyType))
            {
                throw new ArgumentException("Loại kẻ địch không được để trống.", nameof(enemyType));
            }

            EnemyType = enemyType;
            ExperienceReward = Math.Max(DefaultMinReward, experienceReward);
            _aiStrategy = initialAI ?? throw new ArgumentNullException(nameof(initialAI), "Chiến thuật AI khởi tạo không được null.");
            _eventBus = eventBus;
        }

        public Enemy(
            string name,
            string enemyType,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed,
            int experienceReward,
            IEnemyAI initialAI,
            IGameEventBus eventBus = null)
            : this(Guid.NewGuid(), name, enemyType, initialX, initialY, maxHealth, baseSpeed, experienceReward, initialAI, eventBus)
        {
        }

        /// <summary>
        /// Hoán đổi chiến thuật hành vi tại thời gian thực (GoF Strategy Pattern - Tiêu chí 4.6).
        /// </summary>
        /// <param name="newStrategy">Chiến thuật mới thay thế.</param>
        public void SetAIStrategy(IEnemyAI newStrategy)
        {
            _aiStrategy = newStrategy ?? throw new ArgumentNullException(nameof(newStrategy), "Chiến thuật mới không được null.");
        }

        /// <summary>
        /// Thực thi 1 chu kỳ ra quyết định và di chuyển của AI theo chiến thuật hiện tại.
        /// </summary>
        public void PerformAITurn(Actor target, ITileQuery tileQuery, float deltaTime)
        {
            if (IsDead || target == null || target.IsDead || tileQuery == null || deltaTime <= 0.0f)
            {
                return;
            }

            GridPos nextCell = _aiStrategy.DecideNextCell(this, target, tileQuery);

            float dirX = nextCell.X - PositionX;
            float dirY = nextCell.Y - PositionY;

            Move(dirX, dirY, deltaTime);
        }

        public override void Update(float deltaTime)
        {
            if (IsDead)
            {
                return;
            }

            // Dành chỗ cho cập nhật chu kỳ nội bộ của quái vật
        }

        /// <summary>
        /// Kích hoạt khi quái vật bị tiêu diệt, phát sự kiện lên GameEventBus để phân hệ TV2 nhận biết rơi đồ.
        /// </summary>
        protected override void OnKilled()
        {
            base.OnKilled();

            _eventBus?.Publish(new EnemyKilledEvent(Id, EnemyType, GridPosition, ExperienceReward));
        }
    }
}
