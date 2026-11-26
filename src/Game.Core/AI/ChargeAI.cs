using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.Physics;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    public enum ChargePhase
    {
        Idle = 0,
        Telegraphing = 1,
        Charging = 2
    }

    /// <summary>
    /// Chiến thuật AI húc xung lực thẳng (GoF Concrete Strategy - áp dụng cho Scorpion).
    /// Khi nhìn thấy mục tiêu trong tầm 6 ô: dừng 0.6s nạp đà cảnh báo, rồi lao vút tốc độ 300%;
    /// nếu đâm vào tường sẽ tự gây choáng 1.0s trên ActorStateMachine.
    /// </summary>
    public class ChargeAI : IEnemyAI
    {
        private const float DefaultTriggerRange = 6.0f;
        private const float DefaultTelegraphDuration = 0.6f;
        private const float DefaultChargeSpeedMultiplier = 3.0f;
        private const float DefaultWallStunDuration = 1.0f;

        private float _telegraphTimer;

        public string BehaviorName => "Charge";
        public ChargePhase Phase { get; private set; }
        public float TriggerRange { get; }
        public float TelegraphDuration { get; }
        public float ChargeSpeedMultiplier { get; }
        public float WallStunDuration { get; }
        public GridPos LockedDirection { get; private set; }
        public float RemainingTelegraphTime => Math.Max(0.0f, _telegraphTimer);
        public float CurrentSpeedMultiplier => Phase == ChargePhase.Charging ? ChargeSpeedMultiplier : 1.0f;

        public ChargeAI(
            float triggerRange = DefaultTriggerRange,
            float telegraphDuration = DefaultTelegraphDuration,
            float chargeSpeedMultiplier = DefaultChargeSpeedMultiplier,
            float wallStunDuration = DefaultWallStunDuration)
        {
            if (triggerRange <= 0.0f || telegraphDuration <= 0.0f || chargeSpeedMultiplier < 1.0f || wallStunDuration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(triggerRange), "Thông số ChargeAI không hợp lệ.");
            }

            TriggerRange = triggerRange;
            TelegraphDuration = telegraphDuration;
            ChargeSpeedMultiplier = chargeSpeedMultiplier;
            WallStunDuration = wallStunDuration;
            Phase = ChargePhase.Idle;
            LockedDirection = GridPos.Zero;
        }

        public void Update(float deltaTime)
        {
            if (deltaTime <= 0.0f || Phase != ChargePhase.Telegraphing)
            {
                return;
            }

            _telegraphTimer -= deltaTime;
            if (_telegraphTimer <= 0.0f)
            {
                _telegraphTimer = 0.0f;
                Phase = ChargePhase.Charging;
            }
        }

        /// <summary>
        /// Kết thúc pha lao húc sau khi đã đâm trúng mục tiêu.
        /// </summary>
        public void NotifyHitTarget()
        {
            ResetToIdle();
        }

        public GridPos DecideNextCell(Actor enemy, Actor target, ITileQuery tileQuery)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            if (target == null || tileQuery == null || enemy.IsDead || target.IsDead || !enemy.StateMachine.CanMove)
            {
                return enemy.GridPosition;
            }

            GridPos enemyPos = enemy.GridPosition;
            GridPos targetPos = target.GridPosition;

            if (Phase == ChargePhase.Telegraphing)
            {
                return enemyPos;
            }

            if (Phase == ChargePhase.Charging)
            {
                GridPos next = enemyPos + LockedDirection;
                if (!tileQuery.IsWalkable(next.X, next.Y))
                {
                    // Đâm trúng tường -> tự gây choáng 1.0s và hủy pha lao
                    enemy.ApplyStun(WallStunDuration);
                    ResetToIdle();
                    return enemyPos;
                }

                return next;
            }

            double distance = enemyPos.EuclideanDistance(targetPos);
            if (distance <= TriggerRange && LineOfSight.HasLineOfSight(enemyPos, targetPos, tileQuery))
            {
                GridPos direction = Pathfinder.GetDominantCardinalDirection(enemyPos, targetPos);
                if (direction != GridPos.Zero)
                {
                    LockedDirection = direction;
                    Phase = ChargePhase.Telegraphing;
                    _telegraphTimer = TelegraphDuration;
                    return enemyPos;
                }
            }

            return Pathfinder.GetNextStep(enemyPos, targetPos, tileQuery);
        }

        private void ResetToIdle()
        {
            Phase = ChargePhase.Idle;
            LockedDirection = GridPos.Zero;
            _telegraphTimer = 0.0f;
        }
    }
}
