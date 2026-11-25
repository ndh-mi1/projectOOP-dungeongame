using Game.Core.Entities;

namespace Game.Core.State
{
    /// <summary>
    /// Lớp cơ sở trừu tượng giúp chia sẻ cài đặt mặc định cho các trạng thái của Actor (DRY).
    /// </summary>
    public abstract class ActorStateBase : IActorState
    {
        public abstract string StateName { get; }
        public virtual bool CanMove => true;
        public virtual bool CanAttack => true;
        public virtual bool CanTakeDamage => true;
        public virtual bool IsTerminal => false;
        public virtual bool BlocksVoluntaryActions => false;
        public virtual bool IsVoluntaryAction => false;
        public virtual bool RequiresAttackPermission => false;

        public virtual void Enter(Actor actor)
        {
        }

        public virtual void Update(Actor actor, float deltaTime)
        {
        }

        public virtual void Exit(Actor actor)
        {
        }
    }
}
