using System;
using System.Collections.Generic;
using Game.Core.Physics;
using Game.Core.Tests.Fakes;
using NUnit.Framework;

namespace Game.Core.Tests.Physics
{
    [TestFixture]
    public class CollisionDetectorTests
    {
        [Test]
        public void SweptAABB_MovingIntoWall_ReturnsExactHitTimeAndNormal()
        {
            // Arrange: Hộp 1x1 tại [0,0], tường 1x1 tại [2,0], dịch chuyển sang phải 4 đơn vị
            // Khoảng cách hở là 1.0 -> va chạm xảy ra tại t = 1.0 / 4.0 = 0.25
            var box = new AABB2D(0.0f, 0.0f, 1.0f, 1.0f);
            var wall = AABB2D.ForTile(2, 0);

            // Act
            CollisionHit hit = CollisionDetector.SweptAABB(box, 4.0f, 0.0f, wall);

            // Assert
            Assert.That(hit.HasHit, Is.True);
            Assert.That(hit.HitTime, Is.EqualTo(0.25f).Within(0.0001f));
            Assert.That(hit.NormalX, Is.EqualTo(-1.0f));
            Assert.That(hit.NormalY, Is.EqualTo(0.0f));
        }

        [Test]
        public void SweptAABB_ExactTouchAtEndOfFrame_ReturnsHitAtOne()
        {
            // Arrange (Diệt đột biến #2): [0,0] rộng 1, tường [3,0], đi 2.0 -> chạm mép phải x=3 tại đúng t = 1.0
            var box = new AABB2D(0.0f, 0.0f, 1.0f, 1.0f);
            var wall = AABB2D.ForTile(3, 0);

            // Act
            CollisionHit hit = CollisionDetector.SweptAABB(box, 2.0f, 0.0f, wall);

            // Assert
            Assert.That(hit.HasHit, Is.True);
            Assert.That(hit.HitTime, Is.EqualTo(1.0f).Within(0.0001f));
            Assert.That(hit.NormalX, Is.EqualTo(-1.0f));
        }

        [Test]
        public void SweptAABB_TouchingAtStartAndMovingInward_ReturnsHitAtZero()
        {
            // Arrange: Đang chạm mép tường x=2 và tiếp tục đâm sang phải
            var box = new AABB2D(1.0f, 0.0f, 1.0f, 1.0f);
            var wall = AABB2D.ForTile(2, 0);

            // Act
            CollisionHit hit = CollisionDetector.SweptAABB(box, 2.0f, 0.0f, wall);

            // Assert
            Assert.That(hit.HasHit, Is.True);
            Assert.That(hit.HitTime, Is.EqualTo(0.0f).Within(0.0001f));
        }

        [Test]
        public void SweptAABB_MovingAwayFromWall_ReturnsNoHit()
        {
            // Arrange: Đang chạm mép x=2 nhưng đi sang trái
            var box = new AABB2D(1.0f, 0.0f, 1.0f, 1.0f);
            var wall = AABB2D.ForTile(2, 0);

            // Act
            CollisionHit hit = CollisionDetector.SweptAABB(box, -2.0f, 0.0f, wall);

            // Assert
            Assert.That(hit.HasHit, Is.False);
        }

        [Test]
        public void SweptAABB_ParallelSlideAlongTouchingSeam_DoesNotSnag()
        {
            // Arrange: Thực thể đặt sát trên đỉnh tường [1,0] (Y=1.0 == wall.MaxY) và trượt ngang
            var box = new AABB2D(0.0f, 1.0f, 0.8f, 0.8f);
            var wall = AABB2D.ForTile(1, 0);

            // Act
            CollisionHit hit = CollisionDetector.SweptAABB(box, 5.0f, 0.0f, wall);

            // Assert: Không bị kẹt vào mép gạch khi trượt song song
            Assert.That(hit.HasHit, Is.False);
        }

        [Test]
        public void ResolveTileMovement_HighSpeedBulletIntoThinWall_StopsBeforeWall()
        {
            // Arrange (TC-09 & INT-10): Hộp 0.5x0.5 tại Pos=(1.5, 0.2), Vel=(5.0, 0), Tường dày 1 ô tại (3, 0)
            // Cạnh phải xuất phát = 2.0, mặt tường = 3.0 -> t_hit = (3.0 - 2.0) / 5.0 = 0.2
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(3, 0);
            var bulletBox = new AABB2D(1.5f, 0.2f, 0.5f, 0.5f);

            // Act
            MovementResult result = CollisionDetector.ResolveTileMovement(bulletBox, 5.0f, 0.0f, tileQuery);

            // Assert: Không bị xuyên tường sang x=6.5; bị chặn đứng trước x=3.0
            Assert.That(result.FirstHit.HasHit, Is.True);
            Assert.That(result.FirstHit.HitTime, Is.EqualTo(0.2f).Within(0.0001f));
            Assert.That(result.FinalX, Is.LessThanOrEqualTo(2.99f));
            Assert.That(result.FinalX + bulletBox.Width, Is.LessThanOrEqualTo(3.0f));
        }

        [Test]
        public void ResolveTileMovement_MovingLeftIntoWallAtOrigin_StopsCleanly()
        {
            // Arrange: Ô tường tại (-1, 0) có mép phải x=0.0; thực thể tại (1.0, 0.1) lao sang trái
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(-1, 0);
            var box = new AABB2D(1.0f, 0.1f, 0.8f, 0.8f);

            // Act
            MovementResult result = CollisionDetector.ResolveTileMovement(box, -3.0f, 0.0f, tileQuery);

            // Assert
            Assert.That(result.FirstHit.HasHit, Is.True);
            Assert.That(result.FinalX, Is.GreaterThanOrEqualTo(0.0f));
            Assert.That(result.FirstHit.NormalX, Is.EqualTo(1.0f));
        }

        [Test]
        public void ResolveTileMovement_DiagonalIntoWall_SlidesAlongOpenAxis()
        {
            // Arrange: Dãy tường dọc tại X=2 (các ô y=1, 2, 3) chặn hướng phải, trục Y thông thoáng
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 1);
            tileQuery.Block(2, 2);
            tileQuery.Block(2, 3);
            var playerBox = new AABB2D(0.2f, 1.2f, 0.5f, 0.5f);

            // Act: Di chuyển chéo mạnh sang phải & lên trên
            MovementResult result = CollisionDetector.ResolveTileMovement(playerBox, 3.0f, 2.0f, tileQuery);

            // Assert: Chặn trên trục X trước mặt tường 2.0, nhưng vẫn trượt mượt lên trục Y
            Assert.That(result.FirstHit.HasHit, Is.True);
            Assert.That(result.FinalX + playerBox.Width, Is.LessThanOrEqualTo(2.0f));
            Assert.That(result.FinalY, Is.GreaterThan(1.2f));
        }

        [Test]
        public void ResolveTileMovement_DiagonalClearsCorner_DoesNotGhostCollide()
        {
            // Arrange: Chỉ có 1 ô tường (2, 1) [y: 1..2]; hộp tại (0.2, 1.2) đi (3.0, 2.0)
            // Thời điểm thoát Y=2.0 là t=0.40 < thời điểm chạm X=2.0 là t=0.433 -> bay qua góc an toàn
            var tileQuery = new FakeTileQuery();
            tileQuery.Block(2, 1);
            var playerBox = new AABB2D(0.2f, 1.2f, 0.5f, 0.5f);

            // Act
            MovementResult result = CollisionDetector.ResolveTileMovement(playerBox, 3.0f, 2.0f, tileQuery);

            // Assert: Không bắt nhầm va chạm ma
            Assert.That(result.FirstHit.HasHit, Is.False);
            Assert.That(result.FinalX, Is.EqualTo(3.2f).Within(0.001f));
            Assert.That(result.FinalY, Is.EqualTo(3.2f).Within(0.001f));
        }

        [Test]
        public void SpatialHashGrid_InsertAndQuery_ReturnsOnlyMatchingBucketItems()
        {
            // Arrange: Lưới băm 4x4 ô
            var grid = new SpatialHashGrid<string>(cellSize: 4);
            grid.Insert("EnemyNear", new AABB2D(1.0f, 1.0f, 0.8f, 0.8f));
            grid.Insert("EnemyFar", new AABB2D(20.0f, 20.0f, 0.8f, 0.8f));

            // Act
            IReadOnlyCollection<string> candidates = grid.Query(new AABB2D(0.0f, 0.0f, 3.0f, 3.0f));

            // Assert
            Assert.That(candidates, Does.Contain("EnemyNear"));
            Assert.That(candidates, Does.Not.Contain("EnemyFar"));
        }

        [Test]
        public void AABB2D_ZeroOrNegativeSize_ThrowsArgumentOutOfRangeException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new AABB2D(0, 0, 0.0f, 1.0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AABB2D(0, 0, 1.0f, -0.5f));
        }
    }
}
