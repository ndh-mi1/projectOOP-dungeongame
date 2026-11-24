using System;
using Game.Core.AI;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.Events;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.AI
{
    [TestFixture]
    public class ChaseAITests
    {
        [Test]
        public void DecideNextCell_TargetAdjacent_ReturnsTargetCell()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var ai = new ChaseAI();
            var enemy = new Enemy("Skeleton", "Undead", 0, 0, 50, 2.0f, 20, ai);
            var player = new RoguePlayer("Knight", 1, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(enemy, player, tileQuery);

            // Assert: Bước tới ô người chơi (1, 0)
            Assert.That(next, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void DecideNextCell_TargetFarAway_ReturnsFirstStepAlongAStarPath()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var ai = new ChaseAI();
            var enemy = new Enemy("Skeleton", "Undead", 0, 0, 50, 2.0f, 20, ai);
            var player = new RoguePlayer("Knight", 3, 0, 100, 4.0f, 50);

            // Act: Đường đi (0,0) -> (1,0) -> (2,0) -> (3,0)
            GridPos next = ai.DecideNextCell(enemy, player, tileQuery);

            // Assert: Bước tiếp theo là ô (1, 0)
            Assert.That(next, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void DecideNextCell_WhenEnemyOrTargetDead_ReturnsCurrentEnemyPosition()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var ai = new ChaseAI();
            var enemy = new Enemy("Skeleton", "Undead", 2, 2, 50, 2.0f, 20, ai);
            var player = new RoguePlayer("Knight", 4, 4, 20, 4.0f, 50);

            // Giết người chơi
            player.TakeDamage(new DamageInfo(30));
            Assert.That(player.IsDead, Is.True);

            // Act
            GridPos next = ai.DecideNextCell(enemy, player, tileQuery);

            // Assert: Không di chuyển nữa
            Assert.That(next, Is.EqualTo(new GridPos(2, 2)));
        }

        [Test]
        public void Enemy_PerformAITurn_MovesTowardsTarget()
        {
            // Arrange: Quái tại (0, 0), tốc độ 4.0 ô/giây, Player tại (2, 0)
            var tileQuery = new FakeTileQuery();
            var ai = new ChaseAI();
            var enemy = new Enemy("Goblin", "Beast", 0.0f, 0.0f, 40, 4.0f, 15, ai);
            var player = new RoguePlayer("Knight", 2.0f, 0.0f, 100, 4.0f, 50);

            // Act: Đi trong 0.25 giây (4.0 * 0.25 = 1.0)
            enemy.PerformAITurn(player, tileQuery, 0.25f);

            // Assert: Quái đã di chuyển sang phải
            Assert.That(enemy.PositionX, Is.GreaterThan(0.0f));
            Assert.That(enemy.PositionY, Is.EqualTo(0.0f));
        }

        [Test]
        public void Enemy_SetAIStrategy_SwapsBehaviorAtRuntime()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var initialAi = new ChaseAI();
            var enemy = new Enemy("Mimic", "Monster", 2, 2, 60, 3.0f, 50, initialAi);

            // Mock Strategy mới tại chỗ
            var stationaryAi = new DummyStationaryAI();

            // Act: Hoán đổi Strategy trong runtime (Tiêu chí 4.6)
            enemy.SetAIStrategy(stationaryAi);

            // Assert
            Assert.That(enemy.CurrentAI.BehaviorName, Is.EqualTo("Stationary"));
        }

        [Test]
        public void Enemy_OnKilled_PublishesEnemyKilledEventToEventBus()
        {
            // Arrange
            var eventBus = new GameEventBus();
            EnemyKilledEvent receivedEvent = null;

            eventBus.Subscribe<EnemyKilledEvent>(e => receivedEvent = e);

            var enemy = new Enemy("Orc", "OrcWarlord", 3.0f, 4.0f, 50, 2.0f, 100, new ChaseAI(), eventBus);

            // Act: Gây sát thương chí tử cho quái
            enemy.TakeDamage(new DamageInfo(60));

            // Assert: Sự kiện được bắn lên EventBus chính xác 100%
            Assert.That(enemy.IsDead, Is.True);
            Assert.That(receivedEvent, Is.Not.Null);
            Assert.That(receivedEvent.EnemyId, Is.EqualTo(enemy.Id));
            Assert.That(receivedEvent.EnemyType, Is.EqualTo("OrcWarlord"));
            Assert.That(receivedEvent.Position, Is.EqualTo(new GridPos(3, 4)));
            Assert.That(receivedEvent.ExperienceReward, Is.EqualTo(100));
        }

        private class DummyStationaryAI : IEnemyAI
        {
            public string BehaviorName => "Stationary";

            public GridPos DecideNextCell(Actor enemy, Actor target, ITileQuery tileQuery)
            {
                return enemy.GridPosition;
            }
        }
    }
}
