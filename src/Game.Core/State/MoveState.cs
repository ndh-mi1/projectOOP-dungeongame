namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái đang di chuyển chủ động của thực thể.
    /// </summary>
    public sealed class MoveState : ActorStateBase
    {
        public override string StateName => "Move";
        public override bool IsVoluntaryAction => true;
    }
}
