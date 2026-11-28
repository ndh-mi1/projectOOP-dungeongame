using System;
using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Máy trạng thái hữu hạn (Finite State Machine) điều phối vòng đời hành vi của Actor.
    /// Sử dụng kiểm tra Guard đa hình qua IActorState (không ép kiểu is/as) để chặn chuyển trạng thái cấm.
    /// </summary>
    public sealed class ActorStateMachine
    {
        private readonly Actor _owner;

        public IActorState CurrentState { get; private set; }

        public bool CanMove => CurrentState.CanMove;
        public bool CanAttack => CurrentState.CanAttack;
        public bool CanTakeDamage => CurrentState.CanTakeDamage;

        public ActorStateMachine(Actor owner, IActorState initialState)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            CurrentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            CurrentState.Enter(_owner);
        }

        /// <summary>
        /// Thực hiện chuyển đổi trạng thái có kiểm tra điều kiện Guard.
        /// Trả về false nếu trạng thái hiện tại là kết thúc (Dead) hoặc đang khống chế hành động tự nguyện.
        /// </summary>
        public bool TryTransitionTo(IActorState nextState)
        {
            if (nextState == null)
            {
                throw new ArgumentNullException(nameof(nextState), "Trạng thái chuyển đến không được null.");
            }

            // Guard 1: Không thể thoát khỏi trạng thái kết thúc (VD: DeadState)
            if (CurrentState.IsTerminal)
            {
                return false;
            }

            // Guard 2: Khi đang bị khống chế (VD: Stunned/Frozen), chặn mọi trạng thái hành động chủ động (Attack/Move)
            if (CurrentState.BlocksVoluntaryActions && nextState.IsVoluntaryAction)
            {
                return false;
            }

            // Guard 3: Không cho phép chuyển sang trạng thái đòi hỏi tấn công nếu trạng thái hiện tại không cho phép
            if (nextState.RequiresAttackPermission && !CurrentState.CanAttack)
            {
                return false;
            }

            CurrentState.Exit(_owner);
            CurrentState = nextState;
            CurrentState.Enter(_owner);
            return true;
        }

        /// <summary>
        /// Cập nhật trạng thái hiện hành theo từng chu kỳ thời gian.
        /// </summary>
        public void Update(float deltaTime)
        {
            if (deltaTime <= 0.0f)
            {
                return;
            }

            CurrentState.Update(_owner, deltaTime);
        }
    }
}
