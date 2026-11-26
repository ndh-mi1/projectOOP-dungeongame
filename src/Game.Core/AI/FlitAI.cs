using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Chiến thuật AI bay lượn lao–rút (GoF Concrete Strategy - áp dụng cho Bat, Ghost).
    /// Lao bổ tiếp cận tấn công, sau đó rút lui giữ khoảng cách 3 ô và chờ hồi chiêu 2s bằng quỹ đạo ziczac.
    /// Khi bật IgnoreWalls (Ghost), quái xuyên qua tường đá bỏ qua kiểm tra IsWalkable.
    /// </summary>
    public class FlitAI : IEnemyAI
    {
        private const float DefaultRetreatDistance = 3.0f;
        private const float DefaultCooldownDuration = 2.0f;
        private const float StrikeDistance = 1.5f;

        private bool _zigzagToggle;

        public string BehaviorName => "Flit";
        public bool IgnoreWalls { get; }
        public float RetreatDistance { get; }
        public float CooldownDuration { get; }
        public float RemainingCooldown { get; private set; }
        public bool IsRetreating => RemainingCooldown > 0.0f;

        public FlitAI(
            float retreatDistance = DefaultRetreatDistance,
            float cooldownDuration = DefaultCooldownDuration,
            bool ignoreWalls = false)
        {
            if (retreatDistance <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(retreatDistance), "Khoảng cách rút lui phải lớn hơn 0.");
            }

            if (cooldownDuration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(cooldownDuration), "Thời gian hồi chiêu phải lớn hơn 0.");
            }

            RetreatDistance = retreatDistance;
            CooldownDuration = cooldownDuration;
            IgnoreWalls = ignoreWalls;
            RemainingCooldown = 0.0f;
        }

        public void Update(float deltaTime)
        {
            if (deltaTime <= 0.0f)
            {
                return;
            }

            _zigzagToggle = !_zigzagToggle;
            if (RemainingCooldown > 0.0f)
            {
                RemainingCooldown = Math.Max(0.0f, RemainingCooldown - deltaTime);
            }
        }

        /// <summary>
        /// Kích hoạt pha rút lui sau khi hoàn tất cú lao bổ tấn công.
        /// </summary>
        public void TriggerPostStrikeRetreat()
        {
            RemainingCooldown = CooldownDuration;
        }

        public bool CanStrike(Actor enemy, Actor target)
        {
            if (enemy == null || enemy.IsDead || target == null || target.IsDead || IsRetreating)
            {
                return false;
            }

            return enemy.GridPosition.EuclideanDistance(target.GridPosition) <= StrikeDistance;
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

            GridPos enemyPos = enemy.GridPosition;
            GridPos targetPos = target.GridPosition;
            double distance = enemyPos.EuclideanDistance(targetPos);

            if (IsRetreating)
            {
                if (distance < RetreatDistance)
                {
                    return Pathfinder.FindRetreatStep(enemyPos, targetPos, tileQuery, IgnoreWalls);
                }

                // Đã ở khoảng cách an toàn -> bay lượn ziczac vuông góc chờ hết hồi chiêu
                return FindZigzagStep(enemyPos, targetPos, tileQuery);
            }

            if (IgnoreWalls)
            {
                return StepDirectlyThroughWalls(enemyPos, targetPos);
            }

            return Pathfinder.GetNextStep(enemyPos, targetPos, tileQuery);
        }

        private GridPos StepDirectlyThroughWalls(GridPos from, GridPos to)
        {
            GridPos best = from;
            double bestDistance = from.EuclideanDistance(to);

            foreach (GridPos dir in GridPos.CardinalDirections)
            {
                GridPos candidate = from + dir;
                double dist = candidate.EuclideanDistance(to);
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    best = candidate;
                }
            }

            return best;
        }

        private GridPos FindZigzagStep(GridPos enemyPos, GridPos targetPos, ITileQuery tileQuery)
        {
            GridPos toTarget = Pathfinder.GetDominantCardinalDirection(enemyPos, targetPos);
            int sign = _zigzagToggle ? 1 : -1;
            GridPos primaryPerp = new GridPos(-toTarget.Y * sign, toTarget.X * sign);
            GridPos candidate = enemyPos + primaryPerp;

            if (IgnoreWalls || tileQuery.IsWalkable(candidate.X, candidate.Y))
            {
                return candidate;
            }

            GridPos fallback = enemyPos - primaryPerp;
            if (IgnoreWalls || tileQuery.IsWalkable(fallback.X, fallback.Y))
            {
                return fallback;
            }

            return enemyPos;
        }
    }
}
