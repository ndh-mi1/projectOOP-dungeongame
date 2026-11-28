using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Giao diện trạng thái hành vi của thực thể sống (GoF State Pattern).
    /// Khai báo các thuộc tính Guard đa hình nhằm chặn chuyển trạng thái bất hợp lệ
    /// mà không cần dùng lệnh ép kiểu is/as.
    /// </summary>
    public interface IActorState
    {
        string StateName { get; }
        bool CanMove { get; }
        bool CanAttack { get; }
        bool CanTakeDamage { get; }

        /// <summary>
        /// Trạng thái kết thúc một chiều (VD: DeadState). Khi đang ở trạng thái này, mọi chuyển đổi đều bị chặn.
        /// </summary>
        bool IsTerminal { get; }

        /// <summary>
        /// Cho biết trạng thái hiện tại có đang khống chế hành động tự nguyện của thực thể hay không (VD: Choáng, Đóng băng).
        /// </summary>
        bool BlocksVoluntaryActions { get; }

        /// <summary>
        /// Cho biết trạng thái này có phải là hành động chủ động do người chơi/AI khởi phát hay không (VD: Tấn công, Di chuyển).
        /// </summary>
        bool IsVoluntaryAction { get; }

        /// <summary>
        /// Cho biết trạng thái đích này đòi hỏi quyền tấn công (CanAttack) từ trạng thái hiện tại.
        /// </summary>
        bool RequiresAttackPermission { get; }

        void Enter(Actor actor);
        void Update(Actor actor, float deltaTime);
        void Exit(Actor actor);
    }
}
