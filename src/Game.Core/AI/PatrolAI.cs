using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.Physics;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Chiến thuật AI lính gác tuần tra (GoF Concrete Strategy - áp dụng cho Skeleton, Goblin).
    /// Tuần tra theo các điểm nhớ (Waypoints); dùng Bresenham Line-of-Sight kiểm tra phát hiện người chơi
    /// trong tầm nhìn để chuyển sang bám đuổi bằng A*.
    /// </summary>
    public class PatrolAI : IEnemyAI
    {
        private const float DefaultDetectionRange = 7.0f;

        private readonly List<GridPos> _waypoints;
        private readonly float _detectionRange;
        private int _currentWaypointIndex;

        public string BehaviorName => "Patrol";
        public bool IsAlerted { get; private set; }
        public int CurrentWaypointIndex => _currentWaypointIndex;

        public PatrolAI(IReadOnlyList<GridPos> waypoints, float detectionRange = DefaultDetectionRange)
        {
            if (waypoints == null || waypoints.Count == 0)
            {
                throw new ArgumentException("Danh sách điểm tuần tra phải có ít nhất 1 tọa độ.", nameof(waypoints));
            }

            if (detectionRange <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(detectionRange), "Tầm phát hiện phải lớn hơn 0.");
            }

            _waypoints = new List<GridPos>(waypoints);
            _detectionRange = detectionRange;
            _currentWaypointIndex = 0;
        }

        public GridPos DecideNextCell(Actor enemy, Actor target, ITileQuery tileQuery)
        {
            if (enemy == null)
            {
                throw new ArgumentNullException(nameof(enemy));
            }

            if (tileQuery == null || enemy.IsDead)
            {
                return enemy.GridPosition;
            }

            if (CanDetectTarget(enemy, target, tileQuery))
            {
                IsAlerted = true;
            }

            if (IsAlerted && target != null && !target.IsDead)
            {
                return Pathfinder.GetNextStep(enemy.GridPosition, target.GridPosition, tileQuery);
            }

            return FollowWaypoints(enemy.GridPosition, tileQuery);
        }

        private bool CanDetectTarget(Actor enemy, Actor target, ITileQuery tileQuery)
        {
            if (target == null || target.IsDead)
            {
                return false;
            }

            double distance = enemy.GridPosition.EuclideanDistance(target.GridPosition);
            if (distance > _detectionRange)
            {
                return false;
            }

            return LineOfSight.HasLineOfSight(enemy.GridPosition, target.GridPosition, tileQuery);
        }

        private GridPos FollowWaypoints(GridPos currentPos, ITileQuery tileQuery)
        {
            GridPos waypoint = _waypoints[_currentWaypointIndex];
            if (currentPos == waypoint)
            {
                if (_waypoints.Count == 1)
                {
                    return currentPos;
                }

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
                waypoint = _waypoints[_currentWaypointIndex];
            }

            return Pathfinder.GetNextStep(currentPos, waypoint, tileQuery);
        }
    }
}
