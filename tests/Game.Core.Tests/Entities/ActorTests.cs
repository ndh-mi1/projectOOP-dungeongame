using System;
using Game.Core.Entities;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Entities
{
    [TestFixture]
    public class ActorTests
    {
        private class TestActor : Actor
        {
            public int KilledCount { get; private set; }

            public TestActor(
                string name,
                float initialX,
                float initialY,
                int maxHealth,
                float baseSpeed)
                : base(name, initialX, initialY, maxHealth, baseSpeed)
            {
            }

            public override void Update(float deltaTime)
            {
            }

            protected override void OnKilled()
            {
                base.OnKilled();
                KilledCount++;
            }
        }

        [Test]
        public void Constructor_ValidArguments_InitializesPropertiesCorrectly()
        {
            // Arrange & Act
            var actor = new TestActor("Hero", 2.0f, 3.0f, 100, 4.0f);

            // Assert
            Assert.That(actor.Name, Is.EqualTo("Hero"));
            Assert.That(actor.PositionX, Is.EqualTo(2.0f));
            Assert.That(actor.PositionY, Is.EqualTo(3.0f));
            Assert.That(actor.CurrentHealth, Is.EqualTo(100));
            Assert.That(actor.MaxHealth, Is.EqualTo(100));
            Assert.That(actor.BaseSpeed, Is.EqualTo(4.0f));
            Assert.That(actor.IsDead, Is.False);
            Assert.That(actor.GridPosition, Is.EqualTo(new GridPos(2, 3)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new TestActor(invalidName, 0, 0, 100, 1.0f));
        }

        [Test]
        public void Constructor_NegativeSpeed_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new TestActor("Hero", 0, 0, 100, -1.0f));
        }

        [Test]
        public void TakeDamage_NormalDamage_ReducesCurrentHealth()
        {
            // Arrange
            var actor = new TestActor("Hero", 0, 0, 100, 2.0f);
            var damage = new DamageInfo(35, DamageType.Physical);

            // Act
            actor.TakeDamage(damage);

            // Assert
            Assert.That(actor.CurrentHealth, Is.EqualTo(65));
            Assert.That(actor.IsDead, Is.False);
            Assert.That(actor.KilledCount, Is.EqualTo(0));
        }

        [Test]
        public void TakeDamage_LethalDamage_SetsIsDeadAndTriggersOnKilledOnce()
        {
            // Arrange
            var actor = new TestActor("Hero", 0, 0, 50, 2.0f);
            var lethalDamage = new DamageInfo(60, DamageType.Physical);

            // Act
            actor.TakeDamage(lethalDamage);

            // Assert
            Assert.That(actor.CurrentHealth, Is.EqualTo(0));
            Assert.That(actor.IsDead, Is.True);
            Assert.That(actor.KilledCount, Is.EqualTo(1));

            // Act 2: Nhận thêm sát thương khi đã chết không kích hoạt lại OnKilled
            actor.TakeDamage(new DamageInfo(10, DamageType.Fire));
            Assert.That(actor.KilledCount, Is.EqualTo(1));
        }

        [Test]
        public void Heal_WhenAlive_RestoresHealthWithoutExceedingMax()
        {
            // Arrange
            var actor = new TestActor("Hero", 0, 0, 100, 2.0f);
            actor.TakeDamage(new DamageInfo(40));

            // Act
            actor.Heal(25);

            // Assert
            Assert.That(actor.CurrentHealth, Is.EqualTo(85));

            // Act 2: Hồi quá máu tối đa
            actor.Heal(50);
            Assert.That(actor.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void Heal_WhenDead_DoesNotReviveActor()
        {
            // Arrange
            var actor = new TestActor("Hero", 0, 0, 50, 2.0f);
            actor.TakeDamage(new DamageInfo(50));
            Assert.That(actor.IsDead, Is.True);

            // Act
            actor.Heal(30);

            // Assert
            Assert.That(actor.CurrentHealth, Is.EqualTo(0));
            Assert.That(actor.IsDead, Is.True);
        }

        [Test]
        public void Move_WhenAlive_UpdatesPositionCorrectly()
        {
            // Arrange
            var actor = new TestActor("Hero", 0.0f, 0.0f, 100, 4.0f);

            // Act: Đi sang phải theo trục X trong 0.5s (4.0 * 0.5 = 2.0)
            actor.Move(1.0f, 0.0f, 0.5f);

            // Assert
            Assert.That(actor.PositionX, Is.EqualTo(2.0f));
            Assert.That(actor.PositionY, Is.EqualTo(0.0f));
        }

        [Test]
        public void Move_DiagonalDirection_NormalizesVectorToPreventSpeedBoosting()
        {
            // Arrange
            var actor = new TestActor("Hero", 0.0f, 0.0f, 100, 10.0f);

            // Act: Đi chéo (1.0, 1.0) trong 1.0s
            actor.Move(1.0f, 1.0f, 1.0f);

            // Assert: Quãng đường di chuyển = sqrt(dx^2 + dy^2) phải xấp xỉ 10.0f (không phải 14.14f)
            double distance = Math.Sqrt((actor.PositionX * actor.PositionX) + (actor.PositionY * actor.PositionY));
            Assert.That(distance, Is.EqualTo(10.0).Within(0.001));
        }

        [Test]
        public void Move_WhenDead_DoesNotChangePosition()
        {
            // Arrange
            var actor = new TestActor("Hero", 5.0f, 5.0f, 20, 4.0f);
            actor.TakeDamage(new DamageInfo(30));
            Assert.That(actor.IsDead, Is.True);

            // Act
            actor.Move(1.0f, 0.0f, 1.0f);

            // Assert: Tọa độ giữ nguyên vị trí lúc chết
            Assert.That(actor.PositionX, Is.EqualTo(5.0f));
            Assert.That(actor.PositionY, Is.EqualTo(5.0f));
        }
    }
}
