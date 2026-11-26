using Game.Core.Combat;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Combat
{
    [TestFixture]
    public class CombatResolverTests
    {
        private class FakeDamageableTarget : IDamageable
        {
            public int CurrentHealth { get; private set; }
            public int MaxHealth { get; }
            public bool IsDead => CurrentHealth <= 0;
            public DamageInfo LastReceivedDamage { get; private set; }

            public FakeDamageableTarget(int maxHealth)
            {
                MaxHealth = maxHealth;
                CurrentHealth = maxHealth;
            }

            public void TakeDamage(DamageInfo damageInfo)
            {
                LastReceivedDamage = damageInfo;
                CurrentHealth = damageInfo.Amount >= CurrentHealth ? 0 : CurrentHealth - damageInfo.Amount;
            }

            public void Heal(int amount)
            {
                CurrentHealth += amount;
            }
        }

        [Test]
        public void CalculateDamage_NormalArmorReduction_ReturnsExpectedActualDamage()
        {
            // Arrange (TC-04): RawDmg=25, Armor=10 -> 25 - 10 = 15
            var resolver = new CombatResolver();

            // Act
            int actual = resolver.CalculateDamage(rawDamage: 25, armorValue: 10);

            // Assert
            Assert.That(actual, Is.EqualTo(15));
        }

        [Test]
        public void CalculateDamage_ArmorExceedsDamage_ReturnsMinimumOneDamage()
        {
            // Arrange (TC-05): RawDmg=5, Armor=20 -> chặn dưới tối thiểu bằng 1
            var resolver = new CombatResolver();

            // Act
            int actual = resolver.CalculateDamage(rawDamage: 5, armorValue: 20);

            // Assert
            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void CalculateDamage_CriticalStrike_AppliesMultiplierBeforeArmor()
        {
            // Arrange: RawDmg=20, Crit x1.5 = 30, Armor=5 -> 25
            var resolver = new CombatResolver();

            // Act
            int actual = resolver.CalculateDamage(rawDamage: 20, armorValue: 5, isCritical: true, criticalMultiplier: 1.5f);

            // Assert
            Assert.That(actual, Is.EqualTo(25));
        }

        [Test]
        public void CalculateDamage_ZeroRawDamage_ReturnsZero()
        {
            // Arrange
            var resolver = new CombatResolver();

            // Act
            int actual = resolver.CalculateDamage(rawDamage: 0, armorValue: 0);

            // Assert
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void ExecuteAttack_DuringHurtInvulnerability_IgnoredByIframesGuard()
        {
            // Arrange (TC-08): Actor đang trong 0.5s bất tử
            var resolver = new CombatResolver();
            var player = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            player.TriggerInvulnerability(0.5f);

            // Act
            int dealt = resolver.ExecuteAttack(player, new DamageInfo(40));

            // Assert: Máu giữ nguyên 100, sát thương trả về 0
            Assert.That(dealt, Is.EqualTo(0));
            Assert.That(player.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void ExecuteAttack_ThroughIDamageableContract_DecoupledFromConcreteEntities()
        {
            // Arrange: Truyền Fake Stub qua IDamageable (0 phụ thuộc Actor cụ thể)
            var resolver = new CombatResolver();
            IDamageable fakeProp = new FakeDamageableTarget(maxHealth: 80);

            // Act
            int dealt = resolver.ExecuteAttack(fakeProp, new DamageInfo(30), targetArmor: 5);

            // Assert: 30 - 5 = 25 sát thương
            Assert.That(dealt, Is.EqualTo(25));
            Assert.That(fakeProp.CurrentHealth, Is.EqualTo(55));
        }

        [Test]
        public void ExecuteAttack_BluntWeaponHitsFrozenActor_TriggersShatter250PercentAndFreesActor()
        {
            // Arrange (Luật R-15): Búa 20 dmg đánh mục tiêu đang đóng băng -> 20 * 2.5 = 50 dmg
            var resolver = new CombatResolver();
            var target = new RoguePlayer("Target", 0, 0, 100, 4.0f, 50);
            var effects = new StatusEffectManager(target);
            effects.ApplyEffect(new FreezeEffect(duration: 2.0f));
            Assert.That(target.StateMachine.CurrentState.StateName, Is.EqualTo("Stunned"));

            // Act
            int dealt = resolver.ExecuteAttack(
                target,
                new DamageInfo(20, DamageType.Physical),
                targetArmor: 0,
                isBluntWeapon: true,
                targetEffects: effects);

            // Assert
            Assert.That(dealt, Is.EqualTo(50));
            Assert.That(target.CurrentHealth, Is.EqualTo(50));
            Assert.That(effects.HasEffect(StatusEffectType.Freeze), Is.False);
            Assert.That(target.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
        }
    }
}
