using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    public enum SlamPhase
    {
        Approaching = 0,
        Telegraphing = 1,
        ReadyToSlam = 2
    }

    /// <summary>
    /// Chiến thuật AI đại lực sĩ dậm đất AoE (GoF Concrete Strategy - áp dụng cho Golem, Orc Chieftain).
    /// Tiếp cận mục tiêu trong bán kính 2 ô, dừng lại gồng cảnh báo đỏ trên sàn 1.0s,
    /// sau đó dậm mạnh gây sát thương diện rộng và hất văng người chơi ra xa 2 ô (có kiểm tra va chạm tường).
    /// </summary>
    public class SlamAI : IEnemyAI
    {
        private const float DefaultSlamRadius = 2.0f;
        private const float DefaultTelegraphDuration = 1.0f;
        private const int DefaultKnockbackTiles = 2;

        private float _telegraphTimer;

        public string BehaviorName => "Slam";
        public SlamPhase Phase { get; private set; }
        public float SlamRadius { get; }
        public float TelegraphDuration { get; }
        public int KnockbackTiles { get; }
        public bool IsTelegraphVisible => Phase == SlamPhase.Telegraphing;
        public float RemainingTelegraphTime => Math.Max(0.0f, _telegraphTimer);

        public SlamAI(
            float slamRadius = DefaultSlamRadius,
            float telegraphDuration = DefaultTelegraphDuration,
            int knockbackTiles = DefaultKnockbackTiles)
        {
            if (slamRadius <= 0.0f || telegraphDuration <= 0.0f || knockbackTiles < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slamRadius), "Thông số SlamAI không hợp lệ.");
            }

            SlamRadius = slamRadius;
            TelegraphDuration = telegraphDuration;
            KnockbackTiles = knockbackTiles;
            Phase = SlamPhase.Approaching;
        }

        public void Update(float deltaTime)
        {
            if (deltaTime <= 0.0f || Phase != SlamPhase.Telegraphing)
            {
                return;
            }

            _telegraphTimer -= deltaTime;
            if (_telegraphTimer <= 0.0f)
            {
                _telegraphTimer = 0.0f;
                Phase = SlamPhase.ReadyToSlam;
            }
        }

        public GridPos DecideNextCell(Actor enemy, Actor target, ITileQuery tileQuery)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            if (target == null || tileQuery == null || enemy.IsDead || target.IsDead)
            {
                return enemy.GridPosition;
            }

            if (Phase == SlamPhase.Telegraphing || Phase == SlamPhase.ReadyToSlam)
            {
                return enemy.GridPosition;
            }

            double distance = enemy.GridPosition.EuclideanDistance(target.GridPosition);
            if (distance <= SlamRadius)
            {
                Phase = SlamPhase.Telegraphing;
                _telegraphTimer = TelegraphDuration;
                return enemy.GridPosition;
            }

            return Pathfinder.GetNextStep(enemy.GridPosition, target.GridPosition, tileQuery);
        }

        /// <summary>
        /// Kích hoạt cú dậm AoE khi hoàn tất gồng: gây sát thương và hất văng mục tiêu ra xa nếu còn trong bán kính.
        /// </summary>
        public bool ExecuteSlam(Actor enemy, Actor target, ITileQuery tileQuery, int slamDamage)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            if (tileQuery == null)
            {
                throw new ArgumentNullException(nameof(tileQuery));
            }

            if (Phase != SlamPhase.ReadyToSlam || enemy.IsDead)
            {
                return false;
            }

            Phase = SlamPhase.Approaching;
            _telegraphTimer = 0.0f;

            if (target == null || target.IsDead)
            {
                return false;
            }

            double distance = enemy.GridPosition.EuclideanDistance(target.GridPosition);
            if (distance > SlamRadius)
            {
                return false;
            }

            if (slamDamage > 0)
            {
                target.TakeDamage(new DamageInfo(slamDamage, DamageType.Physical));
            }

            ApplyKnockback(enemy.GridPosition, target, tileQuery);
            return true;
        }

        private void ApplyKnockback(GridPos sourcePos, Actor target, ITileQuery tileQuery)
        {
            if (target.IsDead || KnockbackTiles <= 0)
            {
                return;
            }

            GridPos pushDir = Pathfinder.GetDominantCardinalDirection(sourcePos, target.GridPosition);
            if (pushDir == GridPos.Zero)
            {
                pushDir = new GridPos(1, 0);
            }

            float finalX = target.PositionX;
            float finalY = target.PositionY;

            for (int step = 1; step <= KnockbackTiles; step++)
            {
                GridPos nextCell = new GridPos(
                    (int)Math.Floor(finalX + pushDir.X),
                    (int)Math.Floor(finalY + pushDir.Y));

                if (!tileQuery.IsWalkable(nextCell.X, nextCell.Y))
                {
                    break;
                }

                finalX += pushDir.X;
                finalY += pushDir.Y;
            }

            target.SetPosition(finalX, finalY);
        }
    }
}
