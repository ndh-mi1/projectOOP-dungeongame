using System;
using NUnit.Framework;
using Game.Core.ValueObjects;

namespace Game.Core.Tests.Combat
{
    [TestFixture]
    public class HealthTests
    {
        [Test]
        public void Constructor_ValidMaxHealth_InitializesMaxAndCurrentCorrectly()
        {
            // Arrange & Act
            var health = new Health(max: 100);

            // Assert
            Assert.AreEqual(100, health.Max);
            Assert.AreEqual(100, health.Current);
            Assert.IsFalse(health.IsDead);
            Assert.AreEqual(1.0f, health.Percentage);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void Constructor_ZeroOrNegativeMax_ThrowsArgumentOutOfRangeException(int invalidMax)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Health(invalidMax));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(150)]
        public void Constructor_InvalidInitialCurrent_ThrowsArgumentOutOfRangeException(int invalidCurrent)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Health(max: 100, current: invalidCurrent));
        }

        [Test]
        public void Damage_NormalAmount_ReducesCurrent()
        {
            // Arrange
            var health = new Health(max: 100);

            // Act
            health.Damage(35);

            // Assert
            Assert.AreEqual(65, health.Current);
            Assert.IsFalse(health.IsDead);
            Assert.AreEqual(0.65f, health.Percentage, 0.001f);
        }

        [Test]
        public void Damage_ExcessiveAmount_ClampsToZeroAndMarksDead()
        {
            // Arrange
            var health = new Health(max: 100);

            // Act
            health.Damage(150);

            // Assert
            Assert.AreEqual(0, health.Current);
            Assert.IsTrue(health.IsDead);
            Assert.AreEqual(0f, health.Percentage);
        }

        [Test]
        public void Damage_NegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var health = new Health(max: 100);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => health.Damage(-10));
        }

        [Test]
        public void Heal_NormalAmount_IncreasesCurrent()
        {
            // Arrange
            var health = new Health(max: 100, current: 40);

            // Act
            health.Heal(25);

            // Assert
            Assert.AreEqual(65, health.Current);
        }

        [Test]
        public void Heal_ExcessiveAmount_ClampsToMax()
        {
            // Arrange
            var health = new Health(max: 100, current: 80);

            // Act
            health.Heal(50);

            // Assert
            Assert.AreEqual(100, health.Current);
        }

        [Test]
        public void Heal_WhenAlreadyDead_DoesNotHeal()
        {
            // Arrange
            var health = new Health(max: 100, current: 0);
            Assert.IsTrue(health.IsDead);

            // Act
            health.Heal(50);

            // Assert
            Assert.AreEqual(0, health.Current);
            Assert.IsTrue(health.IsDead);
        }

        [Test]
        public void Heal_NegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var health = new Health(max: 100);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => health.Heal(-5));
        }
    }
}
