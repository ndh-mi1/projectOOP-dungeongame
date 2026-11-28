using System;
using Game.Core.Physics;
using Game.Core.Tests.Fakes;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Physics
{
    [TestFixture]
    public class LineOfSightTests
    {
        [Test]
        public void HasLineOfSight_SameTile_ReturnsTrue()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();
            var pos = new GridPos(3, 3);

            // Act
            bool visible = LineOfSight.HasLineOfSight(pos, pos, tileQuery);

            // Assert
            Assert.That(visible, Is.True);
        }

        [Test]
        public void HasLineOfSight_ClearHorizontalAndDiagonal_ReturnsTrue()
        {
            // Arrange
            var tileQuery = new FakeTileQuery();

            // Act & Assert
            Assert.That(LineOfSight.HasLineOfSight(new GridPos(0, 0), new GridPos(5, 0), tileQuery), Is.True);
            Assert.That(LineOfSight.HasLineOfSight(new GridPos(0, 0), new GridPos(4, 3), tileQuery), Is.True);
        }

        [Test]
        public void HasLineOfSight_OpaqueWallInMiddle_ReturnsFalse()
        {
            // Arrange: Đặt tường chắn sáng tại (2, 1) giữa (0, 1) và (4, 1)
            var tileQuery = new FakeTileQuery();
            tileQuery.SetOpaque(2, 1);

            // Act
            bool visible = LineOfSight.HasLineOfSight(new GridPos(0, 1), new GridPos(4, 1), tileQuery);

            // Assert
            Assert.That(visible, Is.False);
        }

        [Test]
        public void HasLineOfSight_DiagonalPinchCornerBothOpaque_ReturnsFalse()
        {
            // Arrange (Ca biên 02_OOP_BLUEPRINT): Tia nhìn chéo (0,0) -> (1,1) nhưng cả (1,0) và (0,1) đều chắn sáng
            var tileQuery = new FakeTileQuery();
            tileQuery.SetOpaque(1, 0);
            tileQuery.SetOpaque(0, 1);

            // Act
            bool visible = LineOfSight.HasLineOfSight(new GridPos(0, 0), new GridPos(1, 1), tileQuery);

            // Assert
            Assert.That(visible, Is.False);
        }

        [Test]
        public void HasLineOfSight_DiagonalWithOnlyOneCornerOpaque_ReturnsTrue()
        {
            // Arrange: Chỉ có (1,0) chắn sáng, góc (0,1) thoáng -> vẫn nhìn thấy (1,1)
            var tileQuery = new FakeTileQuery();
            tileQuery.SetOpaque(1, 0);

            // Act
            bool visible = LineOfSight.HasLineOfSight(new GridPos(0, 0), new GridPos(1, 1), tileQuery);

            // Assert
            Assert.That(visible, Is.True);
        }

        [Test]
        public void HasLineOfSight_NullTileQuery_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => LineOfSight.HasLineOfSight(GridPos.Zero, new GridPos(1, 1), null));
        }
    }
}
