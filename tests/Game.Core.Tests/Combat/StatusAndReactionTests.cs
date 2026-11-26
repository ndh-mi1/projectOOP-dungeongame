using System.Collections.Generic;
using Game.Core.AI;
using Game.Core.Combat;
using Game.Core.Entities;
using Game.Core.Events;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Combat
{
    [TestFixture]
    public class StatusAndReactionTests
    {
        [Test]
        public void BurnEffect_ThreeDiscreteOneSecondTicks_ReducesHpFrom100To85()
        {
            // Arrange (TC-12): Burn 5 dmg/s trong 3s -> sau 3 tick HP còn 100 - 15 = 85
            var target = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            var effects = new StatusEffectManager(target);
            effects.ApplyEffect(new BurnEffect(duration: 3.0f, damagePerTick: 5));

            // Act: Tick 3 lần 1.0s + thêm 1 lần thừa kiểm tra hết hạn
            effects.TickAll(1.0f);
            effects.TickAll(1.0f);
            effects.TickAll(1.0f);
            effects.TickAll(1.0f);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(85));
            Assert.That(effects.HasEffect(StatusEffectType.Burn), Is.False);
        }

        [Test]
        public void BurnEffect_FixedSixtyHzFramesOverThreeSeconds_ReducesHpToExact85()
        {
            // Arrange: Kiểm chứng cộng dồn thời gian 60Hz (180 frame * 1/60s = 3.0s) không trôi số thực
            var target = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            var effects = new StatusEffectManager(target);
            effects.ApplyEffect(new BurnEffect(duration: 3.0f, damagePerTick: 5));
            float fixedDt = 1.0f / 60.0f;

            // Act
            for (int frame = 0; frame < 180; frame++)
            {
                effects.TickAll(fixedDt);
            }

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(85));
            Assert.That(effects.HasEffect(StatusEffectType.Burn), Is.False);
        }

        [Test]
        public void PoisonAndRegenEffects_ApplyExpectedDeltaPolitelyOverTime()
        {
            // Arrange: Độc 3 dmg/s trong 6s = 18 dmg; Regen 5 HP/s trong 2s = +10 HP -> 100 - 18 + 10 = 92
            var target = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            var effects = new StatusEffectManager(target);
            effects.ApplyEffect(new PoisonEffect(duration: 6.0f, damagePerTick: 3));

            for (int sec = 0; sec < 6; sec++)
            {
                effects.TickAll(1.0f);
            }

            Assert.That(target.CurrentHealth, Is.EqualTo(82));

            // Act: Ăn thực phẩm hồi phục 2 giây
            effects.ApplyEffect(new RegenEffect(duration: 2.0f, healPerTick: 5));
            effects.TickAll(1.0f);
            effects.TickAll(1.0f);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(92));
        }

        [Test]
        public void FreezeEffect_ParalysesActorAndRecoversWhenExpired()
        {
            // Arrange
            var target = new RoguePlayer("Knight", 0, 0, 100, 4.0f, 50);
            var effects = new StatusEffectManager(target);

            // Act 1: Đóng băng 2s
            effects.ApplyEffect(new FreezeEffect(duration: 2.0f));

            // Assert 1: Không thể di chuyển hay tấn công
            Assert.That(target.TryPerformAttack(), Is.False);
            Assert.That(target.StateMachine.CanMove, Is.False);

            // Act 2: Sau 2s tan băng
            target.Update(2.1f);
            effects.TickAll(2.1f);

            // Assert 2: Hoạt động lại bình thường
            Assert.That(effects.HasEffect(StatusEffectType.Freeze), Is.False);
            Assert.That(target.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
        }

        [Test]
        public void ReactionR1_Extinguish_ClearsBurnOnceAndGuardsAgainstSpammingSmoke()
        {
            // Arrange (BUG-06 & Luật R-16): Đang cháy bước vào ô nước
            var bus = new GameEventBus();
            var triggeredEvents = new List<ReactionTriggeredEvent>();
            bus.Subscribe<ReactionTriggeredEvent>(e => triggeredEvents.Add(e));

            var resolver = new ReactionResolver(bus);
            var player = new RoguePlayer("Knight", 2, 2, 100, 4.0f, 50);
            var effects = new StatusEffectManager(player);
            effects.ApplyEffect(new BurnEffect());

            // Act: Chạm ô Nước frame 1 và frame 2
            bool frame1 = resolver.TryResolveExtinguish(player, effects, SurfaceKind.Water);
            bool frame2 = resolver.TryResolveExtinguish(player, effects, SurfaceKind.Water);

            // Assert: Burn tắt ở frame 1, chỉ phát đúng 1 cụm khói (không lặp 60 lần/s)
            Assert.That(frame1, Is.True);
            Assert.That(frame2, Is.False);
            Assert.That(effects.HasEffect(StatusEffectType.Burn), Is.False);
            Assert.That(triggeredEvents.Count, Is.EqualTo(1));
            Assert.That(triggeredEvents[0].Type, Is.EqualTo(ReactionType.Extinguish));
            Assert.That(triggeredEvents[0].SmokeDuration, Is.EqualTo(2.0f));
        }

        [Test]
        public void ReactionR2_Electrocute_DamagesAndStunsAllActorsOnConnectedWaterCluster()
        {
            // Arrange (INT-04): Cụm nước nối liền gồm (1,1), (2,1), (3,1); quái 1 tại (1,1), quái 2 tại (3,1), quái khô tại (5,5)
            var bus = new GameEventBus();
            ReactionTriggeredEvent captured = null;
            bus.Subscribe<ReactionTriggeredEvent>(e => captured = e);

            var resolver = new ReactionResolver(bus);
            var enemyWater1 = new Enemy("Slime1", "Water", 1, 1, 50, 2.0f, 10, new ChaseAI());
            var enemyWater2 = new Enemy("Slime2", "Water", 3, 1, 50, 2.0f, 10, new ChaseAI());
            var enemyDry = new Enemy("Goblin", "Ground", 5, 5, 50, 2.0f, 10, new ChaseAI());

            SurfaceKind Lookup(int x, int y) => (y == 1 && x >= 1 && x <= 3) ? SurfaceKind.Water : SurfaceKind.None;

            // Act: Tia sét đánh trúng ô (1,1)
            int hitCount = resolver.ResolveElectrocute(
                new GridPos(1, 1),
                DamageType.Lightning,
                Lookup,
                new[] { enemyWater1, enemyWater2, enemyDry });

            // Assert: Cả 2 quái đứng trong nước nhận 25 dmg + choáng; quái đứng đất khô an toàn
            Assert.That(hitCount, Is.EqualTo(2));
            Assert.That(enemyWater1.CurrentHealth, Is.EqualTo(25));
            Assert.That(enemyWater1.StateMachine.CurrentState.StateName, Is.EqualTo("Stunned"));
            Assert.That(enemyWater2.CurrentHealth, Is.EqualTo(25));
            Assert.That(enemyWater2.StateMachine.CurrentState.StateName, Is.EqualTo("Stunned"));
            Assert.That(enemyDry.CurrentHealth, Is.EqualTo(50));
            Assert.That(enemyDry.StateMachine.CurrentState.StateName, Is.EqualTo("Idle"));
            Assert.That(captured, Is.Not.Null);
            Assert.That(captured.AffectedTiles.Count, Is.EqualTo(3));
        }

        [Test]
        public void ReactionR4_Combustion_DealsSixtyExplosionDamageWithinBlastRadius()
        {
            // Arrange: Tâm nổ (2,2), bán kính 2.5 ô. Target A ở (4,2) d=2 <= 2.5; Target B ở (6,2) d=4 > 2.5
            var resolver = new ReactionResolver();
            var nearActor = new Enemy("Orc", "Beast", 4, 2, 100, 2.0f, 20, new ChaseAI());
            var farActor = new Enemy("Skeleton", "Undead", 6, 2, 100, 2.0f, 20, new ChaseAI());

            // Act
            int hitCount = resolver.ResolveCombustion(
                new GridPos(2, 2),
                DamageType.Fire,
                hasPoisonSource: true,
                new Actor[] { nearActor, farActor });

            // Assert
            Assert.That(hitCount, Is.EqualTo(1));
            Assert.That(nearActor.CurrentHealth, Is.EqualTo(40));
            Assert.That(farActor.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void ReactionR5_Solidify_ConvertsLavaTileToBasaltForFiveSeconds()
        {
            // Arrange
            var bus = new GameEventBus();
            ReactionTriggeredEvent evt = null;
            bus.Subscribe<ReactionTriggeredEvent>(e => evt = e);

            var resolver = new ReactionResolver(bus);
            var surfaceMap = new Dictionary<GridPos, SurfaceKind> { { new GridPos(3, 4), SurfaceKind.Lava } };

            // Act
            bool solidified = resolver.TryResolveSolidify(
                new GridPos(3, 4),
                DamageType.Ice,
                (x, y) => surfaceMap.TryGetValue(new GridPos(x, y), out SurfaceKind kind) ? kind : SurfaceKind.None,
                (pos, newKind) => surfaceMap[pos] = newKind);

            // Assert
            Assert.That(solidified, Is.True);
            Assert.That(surfaceMap[new GridPos(3, 4)], Is.EqualTo(SurfaceKind.Basalt));
            Assert.That(evt, Is.Not.Null);
            Assert.That(evt.Type, Is.EqualTo(ReactionType.Solidify));
            Assert.That(evt.TransformDuration, Is.EqualTo(5.0f));
        }
    }
}
