using System;
using Game.Core.AI;
using Game.Core.Entities;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.AI
{
    [TestFixture]
    public class KiteAITests
    {
        [Test]
        public void DecideNextCell_TargetTooClose_RetreatsAwayFromTarget()
        {
            // Arrange: Cự ly ưu tiên [4, 7]. Player tại (1,0), Quái tại (3,0) -> d = 2 < 4 -> Lùi về (4,0)
            var tileQuery = new FakeTileQuery();
            var ai = new KiteAI(minPreferredDistance: 4.0f, maxAttackRange: 7.0f);
            var spider = new Enemy("Spider", "Beast", 3, 0, 40, 3.0f, 20, ai);
            var player = new RoguePlayer("Knight", 1, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(spider, player, tileQuery);

            // Assert
            Assert.That(next, Is.EqualTo(new GridPos(4, 0)));
        }

        [Test]
        public void DecideNextCell_TargetInOptimalRangeWithLOS_HoldsPositionAndCanShoot()
        {
            // Arrange: Player tại (0,0), Quái tại (5,0) -> d = 5 nằm gọn trong [4, 7] và thông thoáng
            var tileQuery = new FakeTileQuery();
            var ai = new KiteAI(minPreferredDistance: 4.0f, maxAttackRange: 7.0f);
            var mage = new Enemy("FrostMage", "Undead", 5, 0, 40, 3.0f, 25, ai);
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(mage, player, tileQuery);
            bool canAttack = ai.ShouldAttack(mage, player, tileQuery);

            // Assert: Đứng giữ vị trí (5,0) và kích hoạt bắn
            Assert.That(next, Is.EqualTo(new GridPos(5, 0)));
            Assert.That(canAttack, Is.True);
        }

        [Test]
        public void DecideNextCell_TargetBeyondMaxRange_AdvancesCloserViaAStar()
        {
            // Arrange: Player tại (0,0), Quái tại (9,0) -> d = 9 > 7 -> Tiến về (8,0)
            var tileQuery = new FakeTileQuery();
            var ai = new KiteAI(minPreferredDistance: 4.0f, maxAttackRange: 7.0f);
            var mage = new Enemy("FrostMage", "Undead", 9, 0, 40, 3.0f, 25, ai);
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(mage, player, tileQuery);
            bool canAttack = ai.ShouldAttack(mage, player, tileQuery);

            // Assert
            Assert.That(next, Is.EqualTo(new GridPos(8, 0)));
            Assert.That(canAttack, Is.False);
        }

        [Test]
        public void DecideNextCell_InRangeButWallBlocksLOS_MovesAroundObstacleInsteadOfHolding()
        {
            // Arrange: Player tại (0,0), Quái tại (5,0) (d=5) nhưng tường chắn sáng & đi lại tại (2,0)
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 0);
            tileQuery.SetOpaque(2, 0);

            var ai = new KiteAI(minPreferredDistance: 4.0f, maxAttackRange: 7.0f);
            var mage = new Enemy("FrostMage", "Undead", 5, 0, 40, 3.0f, 25, ai);
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(mage, player, tileQuery);
            bool canAttack = ai.ShouldAttack(mage, player, tileQuery);

            // Assert: Không bắn vào tường, chủ động di chuyển tìm góc ngắm
            Assert.That(canAttack, Is.False);
            Assert.That(next, Is.Not.EqualTo(new GridPos(5, 0)));
        }

        [Test]
        public void DecideNextCell_CorneredWhenTooClose_StaysAtSafeCurrentTile()
        {
            // Arrange: Player tại (1,0), Quái tại (2,0) (d=1 < 4), bị tường bao các hướng rút (3,0), (2,1), (2,-1)
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(3, 0);
            tileQuery.Block(2, 1);
            tileQuery.Block(2, -1);

            var ai = new KiteAI(minPreferredDistance: 4.0f, maxAttackRange: 7.0f);
            var spider = new Enemy("Spider", "Beast", 2, 0, 40, 3.0f, 20, ai);
            var player = new RoguePlayer("Knight", 1, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(spider, player, tileQuery);

            // Assert: Không có hướng tăng khoảng cách -> giữ nguyên vị trí, không đâm đầu vào player
            Assert.That(next, Is.EqualTo(new GridPos(2, 0)));
        }
    }
}
