using System;
using Game.Core.Contracts;
using Game.Core.State;
using Game.Core.ValueObjects;

namespace Game.Core.Entities
{
    /// <summary>
    /// Lớp cơ sở trừu tượng cho mọi thực thể sống có Máu, khả năng nhận sát thương và di chuyển.
    /// Minh chứng quan hệ Composition: Khởi tạo đối tượng Health và ActorStateMachine nội bộ trong Constructor.
    /// </summary>
    public abstract class Actor : Entity, IDamageable, IMovable
    {
        private const float DefaultMinSpeed = 0.0f;
        private const float DiagonalTolerance = 1.0f;

        private readonly Health _health;

        public string Name { get; }
        public float BaseSpeed { get; protected set; }
        public ActorStateMachine StateMachine { get; }

        public int CurrentHealth => _health.Current;
        public int MaxHealth => _health.Max;
        public bool IsDead => _health.IsDead;

        protected Actor(
            Guid id,
            string name,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed)
            : base(id, initialX, initialY)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên thực thể không được để trống.", nameof(name));
            }

            if (baseSpeed < DefaultMinSpeed)
            {
                throw new ArgumentException("Tốc độ cơ sở không được âm.", nameof(baseSpeed));
            }

            Name = name;
            BaseSpeed = baseSpeed;

            // Composition: Health và ActorStateMachine được tạo trực tiếp nội tại và gắn liền vòng đời với Actor
            _health = new Health(maxHealth);
            StateMachine = new ActorStateMachine(this, new IdleState());
        }

        protected Actor(
            string name,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed)
            : this(Guid.NewGuid(), name, initialX, initialY, maxHealth, baseSpeed)
        {
        }

        /// <summary>
        /// Xử lý nhận sát thương thông qua gói dữ liệu DamageInfo.
        /// Bỏ qua sát thương nếu thực thể đã chết hoặc đang ở trạng thái bất tử i-frames.
        /// </summary>
        public virtual void TakeDamage(DamageInfo damageInfo)
        {
            if (IsDead || !StateMachine.CanTakeDamage)
            {
                return;
            }

            bool wasAlive = !IsDead;
            _health.Damage(damageInfo.Amount);

            if (wasAlive && IsDead)
            {
                OnKilled();
            }
        }

        /// <summary>
        /// Kích hoạt trạng thái bất tử tạm thời (HurtState / i-frames theo luật R-02).
        /// </summary>
        public bool TriggerInvulnerability(float duration)
        {
            return StateMachine.TryTransitionTo(new HurtState(duration));
        }

        /// <summary>
        /// Áp dụng trạng thái bị choáng (StunnedState), khóa di chuyển và tấn công.
        /// </summary>
        public bool ApplyStun(float duration)
        {
            return StateMachine.TryTransitionTo(new StunnedState(duration));
        }

        /// <summary>
        /// Thử kích hoạt trạng thái tấn công. Guard trên FSM sẽ chặn nếu thực thể đang Stunned hoặc Dead.
        /// </summary>
        public bool TryPerformAttack(IActorState attackState = null)
        {
            if (IsDead)
            {
                return false;
            }

            IActorState targetState = attackState ?? new AttackState();
            return StateMachine.TryTransitionTo(targetState);
        }

        /// <summary>
        /// Hồi phục điểm máu cho thực thể (bảo vệ không hồi máu khi đã chết).
        /// </summary>
        public virtual void Heal(int amount)
        {
            if (IsDead)
            {
                return;
            }

            _health.Heal(amount);
        }

        /// <summary>
        /// Di chuyển thực thể theo hướng vector (chuẩn hóa tránh gian lận đi chéo nhanh hơn) và delta time.
        /// Bảo vệ bất biến: Không thể di chuyển khi đã chết hoặc bị khống chế (CanMove == false).
        /// </summary>
        public virtual void Move(float directionX, float directionY, float deltaTime)
        {
            if (IsDead || !StateMachine.CanMove || deltaTime <= 0.0f)
            {
                return;
            }

            float lengthSquared = (directionX * directionX) + (directionY * directionY);
            if (lengthSquared <= 0.0f)
            {
                return;
            }

            float normalizedX = directionX;
            float normalizedY = directionY;

            if (lengthSquared > DiagonalTolerance)
            {
                float length = (float)Math.Sqrt(lengthSquared);
                normalizedX /= length;
                normalizedY /= length;
            }

            float newX = PositionX + (normalizedX * BaseSpeed * deltaTime);
            float newY = PositionY + (normalizedY * BaseSpeed * deltaTime);

            SetPosition(newX, newY);
        }

        public override void Update(float deltaTime)
        {
            StateMachine.Update(deltaTime);
        }

        /// <summary>
        /// Phương thức móc nối (Hook) được kích hoạt ngay khi thực thể chuyển trạng thái chết.
        /// </summary>
        protected virtual void OnKilled()
        {
            StateMachine.TryTransitionTo(new DeadState());
        }
    }
}
