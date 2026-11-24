using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Chiến thuật AI bám đuổi (GoF Concrete Strategy):
    /// Sử dụng thuật toán A* để tìm đường đi ngắn nhất đuổi theo mục tiêu.
    /// </summary>
    public class ChaseAI : IEnemyAI
    {
        public string BehaviorName => "Chase";

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

            GridPos start = enemy.GridPosition;
            GridPos destination = target.GridPosition;

            if (start == destination)
            {
                return start;
            }

            IReadOnlyList<GridPos> path = Pathfinder.FindPath(start, destination, tileQuery);

            if (path.Count >= 2)
            {
                return path[1];
            }

            return start;
        }
    }
}
