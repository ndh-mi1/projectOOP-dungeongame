namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái đứng yên mặc định của thực thể. Cho phép nhận Input di chuyển và tấn công.
    /// </summary>
    public sealed class IdleState : ActorStateBase
    {
        public override string StateName => "Idle";
    }
}
