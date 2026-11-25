using Game.Core.Entities;
using Game.Core.State;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.State
{
    [TestFixture]
    public class ActorStateMachineTests
    {
        private class TestFrozenState : ActorStateBase
        {
            private float _duration;

            public override string StateName => "Frozen";
            public override bool CanMove => false;
            public override bool CanAttack => false;
            public override bool BlocksVoluntaryActions => _duration > 0.0f;

            public TestFrozenState(float duration)
            {
                _duration = duration;
            }

            public override void Update(Actor actor, float deltaTime)
            {
                _duration -= deltaTime;
                if (_duration <= 0.0f)
                {
                    actor.StateMachine.TryTransitionTo(new IdleState());
                }
            }
        }

        [Test]
        public void Constructor_InitializesInIdleStateWithFullCapabilities()
        {
            // Arrange & Act
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);

            // Assert
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
            Assert.That(player.StateMachine.CanMove, Is.True);
            Assert.That(player.StateMachine.CanAttack, Is.True);
            Assert.That(player.StateMachine.CanTakeDamage, Is.True);
        }

        [Test]
        public void TryPerformAttack_FromIdle_TransitionsToAttackAndAutoRecoversAfterDuration()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);

            // Act 1: Kích hoạt tấn công thời lượng 0.25s
            bool attacked = player.TryPerformAttack(new AttackState(0.25f));

            // Assert 1: Đang ở AttackState, khóa di chuyển và khóa đòn tiếp
            Assert.That(attacked, Is.True);
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Attack"));
            Assert.That(player.StateMachine.CanMove, Is.False);
            Assert.That(player.TryPerformAttack(), Is.False);

            // Act 2: Cập nhật hết thời lượng vung kiếm
            player.Update(0.30f);

            // Assert 2: Tự hồi về IdleState
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
            Assert.That(player.StateMachine.CanMove, Is.True);
            Assert.That(player.StateMachine.CanAttack, Is.True);
        }

        [Test]
        public void TryPerformAttack_WhenStunned_GuardBlocksTransition()
        {
            // Arrange (TC-11): Choáng trong 2.0 giây
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            bool stunned = player.ApplyStun(2.0f);
            Assert.That(stunned, Is.True);

            // Act: Cố tình tấn công hoặc di chuyển khi đang bị choáng
            bool attackAllowed = player.TryPerformAttack();
            player.Move(1.0f, 0.0f, 1.0f);

            // Assert: Guard chặn đứng hoàn toàn
            Assert.That(attackAllowed, Is.False);
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Stunned"));
            Assert.That(player.PositionX, Is.EqualTo(0.0f));
        }

        [Test]
        public void StunnedState_UpdatePastDuration_RecoversToIdleAndAllowsAttack()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            player.ApplyStun(1.0f);

            // Act: Cho thời gian trôi qua 1.1s
            player.Update(1.1f);

            // Assert
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
            Assert.That(player.TryPerformAttack(), Is.True);
        }

        [Test]
        public void TakeDamage_DuringHurtInvulnerability_IgnoresDamageUntilExpired()
        {
            // Arrange (TC-08 / R-02): Bất tử 0.5s
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            player.TriggerInvulnerability(0.5f);

            // Act 1: Đánh trong lúc còn i-frames
            player.TakeDamage(new DamageInfo(40));

            // Assert 1: Miễn nhiễm hoàn toàn, máu giữ nguyên 100
            Assert.That(player.CurrentHealth, Is.EqualTo(100));

            // Act 2: Hết thời gian bất tử và đánh lại
            player.Update(0.6f);
            player.TakeDamage(new DamageInfo(40));

            // Assert 2: Nhận đủ 40 sát thương -> còn 60 HP
            Assert.That(player.CurrentHealth, Is.EqualTo(60));
        }

        [Test]
        public void DeadState_TerminalGuard_BlocksRevivalMoveAndAttack()
        {
            // Arrange: Hạ gục nhân vật
            var player = new RoguePlayer("Knight", 2.0f, 3.0f, 50, 4.0f, 50);
            player.TakeDamage(new DamageInfo(50));

            // Act: Thử chuyển về Idle, Stun, Attack hoặc di chuyển
            bool revivedToIdle = player.StateMachine.TryTransitionTo(new IdleState());
            bool attackAllowed = player.TryPerformAttack();
            player.Move(1.0f, 0.0f, 1.0f);

            // Assert: Guard của DeadState khóa vĩnh viễn mọi chuyển đổi
            Assert.That(player.IsDead, Is.True);
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Dead"));
            Assert.That(revivedToIdle, Is.False);
            Assert.That(attackAllowed, Is.False);
            Assert.That(player.PositionX, Is.EqualTo(2.0f));
        }

        [Test]
        public void LiveOcpSimulation_FrozenStateExtension_WorksWithoutModifyingCoreStateMachine()
        {
            // Arrange (Mô phỏng bài tập Live OCP FrozenState ở 02_OOP_BLUEPRINT):
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            var frozen = new TestFrozenState(1.5f);

            // Act
            bool transitioned = player.StateMachine.TryTransitionTo(frozen);
            bool canAttackWhileFrozen = player.TryPerformAttack();
            player.Move(1.0f, 0.0f, 1.0f);

            // Assert 1: Trạng thái mở rộng mới chặn hoàn toàn hành động không cần sửa 1 dòng code cũ
            Assert.That(transitioned, Is.True);
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Frozen"));
            Assert.That(canAttackWhileFrozen, Is.False);
            Assert.That(player.PositionX, Is.EqualTo(0.0f));

            // Act 2: Rã băng sau 1.6s
            player.Update(1.6f);

            // Assert 2: Trở về Idle
            Assert.That(player.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
        }
    }
}
