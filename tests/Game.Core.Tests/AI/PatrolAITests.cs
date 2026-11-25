using System;
using Game.Core.AI;
using Game.Core.Entities;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.AI
{
    [TestFixture]
    public class PatrolAITests
    {
        [Test]
        public void Constructor_EmptyWaypoints_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new PatrolAI(Array.Empty<GridPos>()));
        }

        [Test]
        public void DecideNextCell_NoTargetInSight_FollowsWaypointsCyclically()
        {
            // Arrange: Tuần tra qua 2 điểm (0,0) và (2,0), player ở ngoài tầm 20 ô
            var tileQuery = new FakeTileQuery();
            var waypoints = new[] { new GridPos(0, 0), new GridPos(2, 0) };
            var ai = new PatrolAI(waypoints, detectionRange: 7.0f);
            var skeleton = new Enemy("Skeleton", "Undead", 0, 0, 50, 2.5f, 15, ai);
            var player = new RoguePlayer("Knight", 20, 0, 100, 4.0f, 50);

            // Act 1: Đang ở (0,0) đã chạm điểm 0 -> tự động hướng tới điểm 1 (2,0), bước kế là (1,0)
            GridPos step1 = ai.DecideNextCell(skeleton, player, tileQuery);

            // Assert 1
            Assert.That(ai.IsAlerted, Is.False);
            Assert.That(step1, Is.EqualTo(new GridPos(1, 0)));

            // Act 2: Khi đã tới (2,0) -> tự động xoay vòng về điểm 0 (0,0), bước kế là (1,0)
            skeleton.SetPosition(2.0f, 0.0f);
            GridPos stepBack = ai.DecideNextCell(skeleton, player, tileQuery);

            // Assert 2
            Assert.That(stepBack, Is.EqualTo(new GridPos(1, 0)));
            Assert.That(ai.CurrentWaypointIndex, Is.EqualTo(0));
        }

        [Test]
        public void DecideNextCell_PlayerInsideRangeWithClearLOS_BecomesAlertedAndChases()
        {
            // Arrange: Skeleton đang tuần tra hướng xuống (0,3), player xuất hiện ở (3,0) trong tầm 7 ô
            var tileQuery = new FakeTileQuery();
            var waypoints = new[] { new GridPos(0, 0), new GridPos(0, 3) };
            var ai = new PatrolAI(waypoints, detectionRange: 7.0f);
            var skeleton = new Enemy("Skeleton", "Undead", 0, 0, 50, 2.5f, 15, ai);
            var player = new RoguePlayer("Knight", 3, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(skeleton, player, tileQuery);

            // Assert: Phát hiện và chuyển hướng đuổi theo player sang (1,0) thay vì đi xuống (0,1)
            Assert.That(ai.IsAlerted, Is.True);
            Assert.That(next, Is.EqualTo(new GridPos(1, 0)));
        }

        [Test]
        public void DecideNextCell_PlayerInsideRangeBehindOpaqueWall_ContinuesPatrolWithoutAlert()
        {
            // Arrange: Player ở (3,0) trong tầm 7 ô nhưng có tường chắn sáng tại (2,0)
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 0);
            tileQuery.SetOpaque(2, 0);

            var waypoints = new[] { new GridPos(0, 0), new GridPos(0, 3) };
            var ai = new PatrolAI(waypoints, detectionRange: 7.0f);
            var skeleton = new Enemy("Skeleton", "Undead", 0, 0, 50, 2.5f, 15, ai);
            var player = new RoguePlayer("Knight", 3, 0, 100, 4.0f, 50);

            // Act
            GridPos next = ai.DecideNextCell(skeleton, player, tileQuery);

            // Assert: Không nhìn thấy xuyên tường -> giữ nguyên lịch trình tuần tra xuống (0,1)
            Assert.That(ai.IsAlerted, Is.False);
            Assert.That(next, Is.EqualTo(new GridPos(0, 1)));
        }
    }
}
