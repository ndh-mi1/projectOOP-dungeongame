using System;
using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái bị choáng (Crowd Control). Khóa hoàn toàn di chuyển và tấn công (Guard chặn hành động)
    /// cho tới khi hết thời gian choáng.
    /// </summary>
    public sealed class StunnedState : ActorStateBase
    {
        private float _remainingDuration;

        public override string StateName => "Stunned";
        public override bool CanMove => false;
        public override bool CanAttack => false;
        public override bool BlocksVoluntaryActions => _remainingDuration > 0.0f;

        public float RemainingDuration => Math.Max(0.0f, _remainingDuration);

        public StunnedState(float duration)
        {
            if (duration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Thời gian bị choáng phải lớn hơn 0.");
            }

            _remainingDuration = duration;
        }

        public override void Update(Actor actor, float deltaTime)
        {
            if (actor == null || deltaTime <= 0.0f)
            {
                return;
            }

            _remainingDuration -= deltaTime;
            if (_remainingDuration <= 0.0f)
            {
                _remainingDuration = 0.0f;
                actor.StateMachine.TryTransitionTo(new IdleState());
            }
        }
    }
}
