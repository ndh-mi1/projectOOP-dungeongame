using System;
using Game.Core.AI;
using Game.Core.Entities;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.AI
{
    [TestFixture]
    public class AdditionalAITests
    {
        [Test]
        public void FlitAI_PostStrikeRetreat_PushesAwayAndRecoversAfterCooldown()
        {
            // Arrange: Bat ở (2,0), Player ở (1,0) -> d=1 < 3
            var tileQuery = new FakeTileQuery();
            var ai = new FlitAI(retreatDistance: 3.0f, cooldownDuration: 2.0f, ignoreWalls: false);
            var bat = new Enemy("Bat", "Beast", 2, 0, 25, 4.0f, 10, ai);
            var player = new RoguePlayer("Knight", 1, 0, 100, 4.0f, 50);

            // Act 1: Kích hoạt pha rút lui sau khi lao bổ
            Assert.That(ai.CanStrike(bat, player), Is.True);
            ai.TriggerPostStrikeRetreat();
            GridPos retreatCell = ai.DecideNextCell(bat, player, tileQuery);

            // Assert 1: Đang hồi chiêu 2s -> lùi xa ra (3,0), không được lao lại
            Assert.That(ai.IsRetreating, Is.True);
            Assert.That(ai.CanStrike(bat, player), Is.False);
            Assert.That(retreatCell, Is.EqualTo(new GridPos(3, 0)));

            // Act 2: Hết thời gian hồi chiêu 2s -> sẵn sàng lao vào lại
            ai.Update(2.0f);
            GridPos diveCell = ai.DecideNextCell(bat, player, tileQuery);

            // Assert 2
            Assert.That(ai.IsRetreating, Is.False);
            Assert.That(diveCell, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void FlitAI_GhostIgnoreWalls_PassesDirectlyThroughStoneWall()
        {
            // Arrange: Ghost ở (0,0), Player ở (2,0), tường đá chắn ngang cột x=1
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(1, 0);
            tileQuery.Block(1, 1);
            tileQuery.Block(1, -1);

            var ghostAi = new FlitAI(ignoreWalls: true);
            var ghost = new Enemy("Ghost", "Spirit", 0, 0, 40, 3.5f, 20, ghostAi);
            var player = new RoguePlayer("Knight", 2, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ghostAi.DecideNextCell(ghost, player, tileQuery);

            // Assert: Đi xuyên thẳng qua ô tường (1,0)
            Assert.That(next, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void AmbushAI_DormantOutsideTriggerDistance_AwakensAndReadiesCritInsideRange()
        {
            // Arrange: Mimic ngủ ở (0,0), ngưỡng thức < 1.5 ô
            var tileQuery = new FakeTileQuery();
            var ai = new AmbushAI(triggerDistance: 1.5f);
            var mimic = new Enemy("Mimic", "Monster", 0, 0, 90, 3.2f, 50, ai);
            var farPlayer = new RoguePlayer("Knight", 2, 0, 100, 4.0f, 50);

            // Act 1: Player đứng ở (2,0), d=2 >= 1.5 -> Vẫn giả làm rương bất động
            GridPos stepWhenFar = ai.DecideNextCell(mimic, farPlayer, tileQuery);

            // Assert 1
            Assert.That(ai.IsAwakened, Is.False);
            Assert.That(stepWhenFar, Is.EqualTo(new GridPos(0, 0)));

            // Act 2: Player bước lại sát (1,0), d=1 < 1.5 -> Thức giấc và đuổi theo + chí mạng bất ngờ
            farPlayer.SetPosition(1.0f, 0.0f);
            GridPos stepWhenNear = ai.DecideNextCell(mimic, farPlayer, tileQuery);

            // Assert 2
            Assert.That(ai.IsAwakened, Is.True);
            Assert.That(ai.ConsumeSurpriseCrit(), Is.True);
            Assert.That(ai.ConsumeSurpriseCrit(), Is.False);
            Assert.That(stepWhenNear, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void ChargeAI_TelegraphsThenChargesAtTripleSpeed_HitsWallAndSelfStuns()
        {
            // Arrange: Bọ cạp ở (0,0), Player ở (3,0), tường ở (1,0)? Không, tường phía sau Player tại (2,0) để test đâm tường!
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 0);

            var ai = new ChargeAI(triggerRange: 6.0f, telegraphDuration: 0.6f, chargeSpeedMultiplier: 3.0f, wallStunDuration: 1.0f);
            var scorpion = new Enemy("Scorpion", "Beast", 0, 0, 80, 2.2f, 30, ai);
            var player = new RoguePlayer("Knight", 1, 0, 100, 4.0f, 50);

            // Act 1: Thấy Player trong tầm 6 ô -> Vào pha Telegraphing (đứng yên gồng 0.6s)
            GridPos stepTelegraph = ai.DecideNextCell(scorpion, player, tileQuery);

            // Assert 1
            Assert.That(ai.Phase, Is.EqualTo(ChargePhase.Telegraphing));
            Assert.That(ai.CurrentSpeedMultiplier, Is.EqualTo(1.0f));
            Assert.That(stepTelegraph, Is.EqualTo(new GridPos(0, 0)));

            // Act 2: Hết 0.6s -> Lao vút tốc độ 300% qua (1,0)
            ai.Update(0.6f);
            Assert.That(ai.Phase, Is.EqualTo(ChargePhase.Charging));
            Assert.That(ai.CurrentSpeedMultiplier, Is.EqualTo(3.0f));

            GridPos chargeStep1 = ai.DecideNextCell(scorpion, player, tileQuery);
            Assert.That(chargeStep1, Is.EqualTo(new GridPos(1, 0)));

            // Act 3: Tới (1,0), bước tiếp lao thẳng vào tường (2,0) -> Tự gây choáng 1s
            scorpion.SetPosition(1.0f, 0.0f);
            GridPos hitWallStep = ai.DecideNextCell(scorpion, player, tileQuery);

            // Assert 3
            Assert.That(hitWallStep, Is.EqualTo(new GridPos(1, 0)));
            Assert.That(ai.Phase, Is.EqualTo(ChargePhase.Idle));
            Assert.That(scorpion.StateMachine.CurrentState.StateName, Is.EqualTo("Stunned"));
            Assert.That(scorpion.StateMachine.CanMove, Is.False);
        }

        [Test]
        public void SlamAI_TelegraphsThenExecutesAoEDamageAndSafeKnockback()
        {
            // Arrange: Golem ở (0,0), Player ở (1,0), tường ở (4,0) cho phép hất lùi 2 ô từ (1,0) -> (3,0) an toàn
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(4, 0);

            var ai = new SlamAI(slamRadius: 2.0f, telegraphDuration: 1.0f, knockbackTiles: 2);
            var golem = new Enemy("Golem", "Construct", 0, 0, 450, 1.8f, 200, ai);
            var player = new RoguePlayer("Knight", 1, 0, 120, 4.0f, 50);

            // Act 1: Player trong bán kính 2 ô -> Hiện vòng đỏ cảnh báo 1.0s
            ai.DecideNextCell(golem, player, tileQuery);
            bool slamTooEarly = ai.ExecuteSlam(golem, player, tileQuery, 35);

            // Assert 1
            Assert.That(ai.Phase, Is.EqualTo(SlamPhase.Telegraphing));
            Assert.That(ai.IsTelegraphVisible, Is.True);
            Assert.That(slamTooEarly, Is.False);

            // Act 2: Sau 1.0s gồng xong -> Dậm đất gây 35 dmg và hất văng 2 ô từ (1,0) -> (3,0)
            ai.Update(1.0f);
            Assert.That(ai.Phase, Is.EqualTo(SlamPhase.ReadyToSlam));

            bool slammed = ai.ExecuteSlam(golem, player, tileQuery, 35);

            // Assert 2
            Assert.That(slammed, Is.True);
            Assert.That(player.CurrentHealth, Is.EqualTo(85));
            Assert.That(player.GridPosition, Is.EqualTo(new GridPos(3, 0)));
            Assert.That(ai.Phase, Is.EqualTo(SlamPhase.Approaching));
        }

        [Test]
        public void SlamAI_KnockbackAgainstAdjacentWall_DoesNotPushTargetIntoWall()
        {
            // Arrange: Golem (0,0), Player (1,0), Tường chặn ngay tại (2,0) -> không được hất xuyên vào (2,0)
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 0);

            var ai = new SlamAI(slamRadius: 2.0f, telegraphDuration: 1.0f, knockbackTiles: 2);
            var golem = new Enemy("Golem", "Construct", 0, 0, 450, 1.8f, 200, ai);
            var player = new RoguePlayer("Knight", 1, 0, 120, 4.0f, 50);

            ai.DecideNextCell(golem, player, tileQuery);
            ai.Update(1.0f);

            // Act
            ai.ExecuteSlam(golem, player, tileQuery, 30);

            // Assert: Giữ nguyên ở (1,0), an toàn trước mặt tường (2,0)
            Assert.That(player.CurrentHealth, Is.EqualTo(90));
            Assert.That(player.GridPosition, Is.EqualTo(new GridPos(1, 0)));
        }
    }
}
