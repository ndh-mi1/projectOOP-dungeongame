using System;
using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái khung hình bất tử tạm thời sau khi trúng đòn hoặc lướt né (Luật R-02: i-frames).
    /// Cho phép vẫn di chuyển nhưng miễn nhiễm hoàn toàn sát thương trong thời gian hiệu lực.
    /// </summary>
    public sealed class HurtState : ActorStateBase
    {
        private const float DefaultInvulnerabilityDuration = 0.5f;

        private float _remainingDuration;

        public override string StateName => "Hurt";
        public override bool CanTakeDamage => _remainingDuration <= 0.0f;

        public float RemainingDuration => Math.Max(0.0f, _remainingDuration);

        public HurtState(float invulnerabilityDuration = DefaultInvulnerabilityDuration)
        {
            if (invulnerabilityDuration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(invulnerabilityDuration), "Thời gian bất tử phải lớn hơn 0.");
            }

            _remainingDuration = invulnerabilityDuration;
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
