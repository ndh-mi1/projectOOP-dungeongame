namespace Game.Core.State
{
    /// <summary>
    /// Trạng thái tử trận một chiều (Terminal State).
    /// Đóng vai trò Guard chặn vĩnh viễn mọi chuyển đổi, di chuyển và hồi sinh.
    /// </summary>
    public sealed class DeadState : ActorStateBase
    {
        public override string StateName => "Dead";
        public override bool CanMove => false;
        public override bool CanAttack => false;
        public override bool CanTakeDamage => false;
        public override bool IsTerminal => true;
        public override bool BlocksVoluntaryActions => true;
    }
}
