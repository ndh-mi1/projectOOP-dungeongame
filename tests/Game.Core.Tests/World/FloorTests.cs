using System;
using Game.Core.AI;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.Physics;
using Game.Core.ValueObjects;
using Game.Core.World;
using NUnit.Framework;

namespace Game.Core.Tests.World
{
    [TestFixture]
    public class FloorTests
    {
        [Test]
        public void TileKind_FromType_ReturnsSameFlyweightInstance()
        {
            // Arrange & Act
            TileKind floor1 = TileKind.FromType(TileType.Floor);
            TileKind floor2 = TileKind.FromType(TileType.Floor);

            // Assert
            Assert.AreSame(floor1, floor2);
            Assert.IsTrue(floor1.IsWalkable);
            Assert.IsFalse(floor1.IsOpaque);
            Assert.IsFalse(TileKind.Wall.IsWalkable);
            Assert.IsTrue(TileKind.Wall.IsOpaque);
        }

        [Test]
        public void TileKind_OnStep_LavaAppliesFireDamageToActor()
        {
            // Arrange
            var player = new RoguePlayer("Hero", 0f, 0f, maxHealth: 100, baseSpeed: 5f, maxMana: 50);

            // Act
            TileKind.Lava.OnStep(player);

            // Assert
            Assert.AreEqual(85, player.CurrentHealth);
        }

        [Test]
        public void RoomNode_StateTransitions_EnforceGuards()
        {
            // Arrange
            var room = new RoomNode(1, 2, 2, 6, 6, RoomType.Normal);
            Guid enemyId = Guid.NewGuid();
            room.AddEnemy(enemyId);

            // Act & Assert: Locked -> Clear is blocked
            Assert.AreEqual(RoomState.Locked, room.State);
            Assert.IsFalse(room.Clear());

            // Act & Assert: Locked -> Active succeeds
            Assert.IsTrue(room.Activate());
            Assert.AreEqual(RoomState.Active, room.State);

            // Act & Assert: Defeating last enemy auto-clears room
            Assert.IsTrue(room.MarkEnemyDefeated(enemyId));
            Assert.AreEqual(RoomState.Cleared, room.State);

            // Act & Assert: Cleared -> Active is blocked
            Assert.IsFalse(room.Activate());
        }

        [Test]
        public void Floor_ImplementsITileQuery_AndHandlesOutOfBoundsAsVoid()
        {
            // Arrange
            var floor = new Floor(1, 10, 10);
            var room = new RoomNode(0, 2, 2, 5, 5, RoomType.Start);
            floor.AddRoom(room);
            ITileQuery tileQuery = floor;

            // Act & Assert
            Assert.IsTrue(tileQuery.IsWalkable(3, 3));
            Assert.IsFalse(tileQuery.IsOpaque(3, 3));
            Assert.IsFalse(tileQuery.IsWalkable(0, 0));
            Assert.IsTrue(tileQuery.IsOpaque(0, 0));
            Assert.IsFalse(tileQuery.IsWalkable(-1, -1));
            Assert.IsTrue(tileQuery.IsOpaque(99, 99));
        }

        [Test]
        public void Floor_IntegratesWithTV1PathfinderAndLineOfSight()
        {
            // Arrange
            var floor = new Floor(1, 12, 12);
            floor.AddRoom(new RoomNode(0, 1, 1, 10, 10, RoomType.Normal));
            var start = new GridPos(2, 2);
            var target = new GridPos(8, 2);

            // Act
            GridPos nextStep = Pathfinder.GetNextStep(start, target, floor);
            bool hasLos = LineOfSight.HasLineOfSight(start, target, floor);

            // Assert
            Assert.AreEqual(new GridPos(3, 2), nextStep);
            Assert.IsTrue(hasLos);
        }

        [Test]
        public void Floor_InvalidDimensions_ThrowsArgumentOutOfRangeException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Floor(0, 10, 10));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Floor(1, 4, 10));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Floor(1, 10, 4));
        }
    }
}
