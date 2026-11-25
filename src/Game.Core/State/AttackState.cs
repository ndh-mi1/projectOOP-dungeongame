using System;
using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái đang thực hiện đòn tấn công với khoảng thời gian ra đòn xác định.
    /// Tự động trở về IdleState khi hoàn tất thời gian vung vũ khí.
    /// </summary>
    public sealed class AttackState : ActorStateBase
    {
        private const float DefaultAttackDuration = 0.25f;

        private float _remainingTime;

        public override string StateName => "Attack";
        public override bool CanMove => false;
        public override bool CanAttack => false;
        public override bool IsVoluntaryAction => true;
        public override bool RequiresAttackPermission => true;

        public AttackState(float duration = DefaultAttackDuration)
        {
            if (duration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Thời gian tấn công phải lớn hơn 0.");
            }

            _remainingTime = duration;
        }

        public override void Update(Actor actor, float deltaTime)
        {
            if (actor == null || deltaTime <= 0.0f)
            {
                return;
            }

            _remainingTime -= deltaTime;
            if (_remainingTime <= 0.0f)
            {
                actor.StateMachine.TryTransitionTo(new IdleState());
            }
        }
    }
}
