using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.Physics;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Chiến thuật AI xạ thủ giữ cự ly / thả diều (GoF Concrete Strategy - áp dụng cho Mage, Spider, Eye).
    /// Lùi lại khi mục tiêu quá gần (< 4 ô), đứng giữ vị trí bắn khi ở vùng tối ưu (4..7 ô và có LOS),
    /// và tiến lại gần bằng A* nếu quá xa (> 7 ô) hoặc bị che khuất tầm nhìn.
    /// </summary>
    public class KiteAI : IEnemyAI
    {
        private const float DefaultMinPreferredDistance = 4.0f;
        private const float DefaultMaxAttackRange = 7.0f;

        public string BehaviorName => "Kite";
        public float MinPreferredDistance { get; }
        public float MaxAttackRange { get; }

        public KiteAI(
            float minPreferredDistance = DefaultMinPreferredDistance,
            float maxAttackRange = DefaultMaxAttackRange)
        {
            if (minPreferredDistance <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(minPreferredDistance), "Cự ly tối thiểu phải lớn hơn 0.");
            }

            if (maxAttackRange < minPreferredDistance)
            {
                throw new ArgumentException("Tầm đánh tối đa không được nhỏ hơn cự ly giữ khoảng cách tối thiểu.", nameof(maxAttackRange));
            }

            MinPreferredDistance = minPreferredDistance;
            MaxAttackRange = maxAttackRange;
        }

        /// <summary>
        /// Kiểm tra xem quái vật có đủ điều kiện cự ly và đường ngắm thẳng để bắn đạn hay không.
        /// </summary>
        public bool ShouldAttack(Actor enemy, Actor target, ITileQuery tileQuery)
        {
            if (enemy == null || enemy.IsDead || target == null || target.IsDead || tileQuery == null)
            {
                return false;
            }

            double distance = enemy.GridPosition.EuclideanDistance(target.GridPosition);
            if (distance > MaxAttackRange)
            {
                return false;
            }

            return LineOfSight.HasLineOfSight(enemy.GridPosition, target.GridPosition, tileQuery);
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
            double currentDistance = enemyPos.EuclideanDistance(targetPos);

            // Vùng 1: Quá gần -> tìm ô lân cận hợp lệ giúp tăng khoảng cách ra xa mục tiêu
            if (currentDistance < MinPreferredDistance)
            {
                return FindRetreatCell(enemyPos, targetPos, currentDistance, tileQuery);
            }

            bool hasLos = LineOfSight.HasLineOfSight(enemyPos, targetPos, tileQuery);

            // Vùng 2: Trong tầm tối ưu và có đường ngắm thẳng -> đứng yên ngắm bắn
            if (currentDistance <= MaxAttackRange && hasLos)
            {
                return enemyPos;
            }

            // Vùng 3: Quá xa hoặc bị tường che tầm nhìn -> tiến lại gần bằng A*
            return Pathfinder.GetNextStep(enemyPos, targetPos, tileQuery);
        }

        private static GridPos FindRetreatCell(
            GridPos enemyPos,
            GridPos targetPos,
            double currentDistance,
            ITileQuery tileQuery)
        {
            GridPos bestCell = enemyPos;
            double bestDistance = currentDistance;

            foreach (GridPos dir in GridPos.CardinalDirections)
            {
                GridPos candidate = enemyPos + dir;
                if (!tileQuery.IsWalkable(candidate.X, candidate.Y))
                {
                    continue;
                }

                double dist = candidate.EuclideanDistance(targetPos);
                if (dist > bestDistance)
                {
                    bestDistance = dist;
                    bestCell = candidate;
                }
            }

            return bestCell;
        }
    }
}
