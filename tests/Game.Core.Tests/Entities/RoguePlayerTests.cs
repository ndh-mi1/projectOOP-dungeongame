using System;
using Game.Core.Entities;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Entities
{
    [TestFixture]
    public class RoguePlayerTests
    {
        [Test]
        public void Constructor_ValidArguments_InitializesPlayerProperties()
        {
            // Arrange & Act
            var player = new RoguePlayer("Knight", 1.0f, 2.0f, 120, 5.0f, 60);

            // Assert
            Assert.That(player.Name, Is.EqualTo("Knight"));
            Assert.That(player.MaxHealth, Is.EqualTo(120));
            Assert.That(player.CurrentHealth, Is.EqualTo(120));
            Assert.That(player.MaxMana, Is.EqualTo(60));
            Assert.That(player.CurrentMana, Is.EqualTo(60));
            Assert.That(player.Level, Is.EqualTo(1));
            Assert.That(player.Experience, Is.EqualTo(0));
            Assert.That(player.ExperienceToNextLevel, Is.EqualTo(100));
        }

        [Test]
        public void Constructor_ZeroOrNegativeMana_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new RoguePlayer("Knight", 0, 0, 100, 5.0f, 0));
            Assert.Throws<ArgumentException>(() => new RoguePlayer("Knight", 0, 0, 100, 5.0f, -10));
        }

        [Test]
        public void ConsumeMana_SufficientMana_ReturnsTrueAndDecreasesCurrentMana()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 100, 5.0f, 50);

            // Act
            bool result = player.ConsumeMana(20);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(player.CurrentMana, Is.EqualTo(30));
        }

        [Test]
        public void ConsumeMana_InsufficientMana_ReturnsFalseAndLeavesManaUnchanged()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 100, 5.0f, 30);

            // Act
            bool result = player.ConsumeMana(50);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(player.CurrentMana, Is.EqualTo(30));
        }

        [Test]
        public void ConsumeMana_WhenDead_ReturnsFalse()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 20, 5.0f, 50);
            player.TakeDamage(new DamageInfo(25));
            Assert.That(player.IsDead, Is.True);

            // Act
            bool result = player.ConsumeMana(10);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RestoreMana_WhenAlive_IncreasesManaUpToMax()
        {
            // Arrange
            var player = new RoguePlayer("Knight", 0, 0, 100, 5.0f, 50);
            player.ConsumeMana(30);
            Assert.That(player.CurrentMana, Is.EqualTo(20));

            // Act
            player.RestoreMana(15);

            // Assert
            Assert.That(player.CurrentMana, Is.EqualTo(35));

            // Act 2: Hồi vượt trần MaxMana
            player.RestoreMana(50);
            Assert.That(player.CurrentMana, Is.EqualTo(50));
        }

        [Test]
        public void GainExperience_AccumulatesAndTriggersLevelUp()
        {
            // Arrange: Level 1 cần 100 EXP
            var player = new RoguePlayer("Knight", 0, 0, 100, 5.0f, 50);
            player.TakeDamage(new DamageInfo(40));
            player.ConsumeMana(20);

            // Act: Nhận 150 EXP -> Lên Level 2, thừa 50 EXP, hồi đầy máu và mana
            player.GainExperience(150);

            // Assert
            Assert.That(player.Level, Is.EqualTo(2));
            Assert.That(player.Experience, Is.EqualTo(50));
            Assert.That(player.ExperienceToNextLevel, Is.EqualTo(200)); // Level 2 * 100
            Assert.That(player.CurrentHealth, Is.EqualTo(100)); // Hồi phục máu khi LevelUp
            Assert.That(player.CurrentMana, Is.EqualTo(50));    // Hồi phục mana khi LevelUp
        }
    }
}
