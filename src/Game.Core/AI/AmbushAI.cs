using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Chiến thuật AI mai phục ngụy trang rương báu (GoF Concrete Strategy - áp dụng cho Mimic).
    /// Ngủ yên bất động cho tới khi người chơi áp sát dưới 1.5 ô hoặc tương tác mở rương (phím E),
    /// sau đó đánh thức, tung cú cắn bất ngờ chí mạng và chuyển sang đuổi theo bằng A*.
    /// </summary>
    public class AmbushAI : IEnemyAI
    {
        private const float DefaultTriggerDistance = 1.5f;

        public string BehaviorName => "Ambush";
        public float TriggerDistance { get; }
        public bool IsAwakened { get; private set; }
        public bool IsSurpriseCritReady { get; private set; }

        public AmbushAI(float triggerDistance = DefaultTriggerDistance)
        {
            if (triggerDistance <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(triggerDistance), "Khoảng cách đánh thức phải lớn hơn 0.");
            }

            TriggerDistance = triggerDistance;
        }

        /// <summary>
        /// Đánh thức Mimic ngay lập tức khi người chơi bấm phím tương tác (E) mở rương giả.
        /// </summary>
        public void TriggerInteract()
        {
            Awaken();
        }

        /// <summary>
        /// Tiêu thụ cờ đòn cắn bất ngờ chí mạng đầu tiên ngay sau khi tỉnh giấc.
        /// </summary>
        public bool ConsumeSurpriseCrit()
        {
            if (!IsSurpriseCritReady)
            {
                return false;
            }

            IsSurpriseCritReady = false;
            return true;
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

            if (!IsAwakened)
            {
                double distance = enemy.GridPosition.EuclideanDistance(target.GridPosition);
                if (distance >= TriggerDistance)
                {
                    return enemy.GridPosition;
                }

                Awaken();
            }

            return Pathfinder.GetNextStep(enemy.GridPosition, target.GridPosition, tileQuery);
        }

        private void Awaken()
        {
            if (IsAwakened)
            {
                return;
            }

            IsAwakened = true;
            IsSurpriseCritReady = true;
        }
    }
}
