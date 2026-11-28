using System;
using System.Collections.Generic;
using Game.Core.AI;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.AI
{
    [TestFixture]
    public class PathfinderTests
    {
        [Test]
        public void FindPath_StartEqualsTarget_ReturnsSingleNodePath()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var pos = new GridPos(2, 3);

            // Act
            IReadOnlyList<GridPos> path = Pathfinder.FindPath(pos, pos, tileQuery);

            // Assert
            Assert.That(path.Count, Is.EqualTo(1));
            Assert.That(path[0], Is.EqualTo(pos));
        }

        [Test]
        public void FindPath_ClearStraightLine_ReturnsDirectConsecutivePath()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var start = new GridPos(0, 0);
            var target = new GridPos(3, 0);

            // Act
            IReadOnlyList<GridPos> path = Pathfinder.FindPath(start, target, tileQuery);

            // Assert: Độ dài 4 ô: (0,0) -> (1,0) -> (2,0) -> (3,0)
            Assert.That(path.Count, Is.EqualTo(4));
            Assert.That(path[0], Is.EqualTo(start));
            Assert.That(path[3], Is.EqualTo(target));
        }

        [Test]
        public void FindPath_ObstacleInBetween_NavigatesAroundWall()
        {
            // Arrange: Đặt chướng ngại vật tại (1, 0)
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(1, 0);

            var start = new GridPos(0, 0);
            var target = new GridPos(2, 0);

            // Act: Tìm đường vòng qua (1, 0)
            IReadOnlyList<GridPos> path = Pathfinder.FindPath(start, target, tileQuery);

            // Assert: Tìm thấy đường đi, điểm cuối là target và không đi qua ô (1, 0)
            Assert.That(path.Count, Is.GreaterThan(0));
            Assert.That(path[path.Count - 1], Is.EqualTo(target));
            Assert.That(path, Does.Not.Contain(new GridPos(1, 0)));
        }

        [Test]
        public void FindPath_TargetInsideWall_ReturnsPathToNearestWalkableCell()
        {
            // Arrange (TC-10): Mục tiêu (3, 3) là tường, các ô lân cận (2, 3) hoặc (3, 2) đi được
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(3, 3);

            var start = new GridPos(1, 1);
            var wallTarget = new GridPos(3, 3);

            // Act
            IReadOnlyList<GridPos> path = Pathfinder.FindPath(start, wallTarget, tileQuery);

            // Assert: Đường đi tìm tới ô lân cận của (3, 3)
            Assert.That(path.Count, Is.GreaterThan(0));
            GridPos destination = path[path.Count - 1];
            Assert.That(destination.ManhattanDistance(wallTarget), Is.EqualTo(1));
            Assert.That(tileQuery.IsWalkable(destination.X, destination.Y), Is.True);
        }

        [Test]
        public void FindPath_TargetCompletelyEnclosed_ReturnsEmptyList()
        {
            // Arrange: Mục tiêu (5, 5) bị vây kín 4 hướng
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(5, 5);
            tileQuery.Block(4, 5); // Trái
            tileQuery.Block(6, 5); // Phải
            tileQuery.Block(5, 4); // Dưới
            tileQuery.Block(5, 6); // Trên

            var start = new GridPos(0, 0);
            var target = new GridPos(5, 5);

            // Act
            IReadOnlyList<GridPos> path = Pathfinder.FindPath(start, target, tileQuery);

            // Assert: Không thể tới được
            Assert.That(path, Is.Empty);
        }

        [Test]
        public void FindPath_NullTileQuery_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => Pathfinder.FindPath(new GridPos(0, 0), new GridPos(1, 1), null));
        }
    }
}
