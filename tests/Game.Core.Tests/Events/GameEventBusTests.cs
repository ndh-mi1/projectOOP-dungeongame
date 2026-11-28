using System;
using Game.Core.Events;
using Game.Core.ValueObjects;
using NUnit.Framework;

namespace Game.Core.Tests.Events
{
    [TestFixture]
    public class GameEventBusTests
    {
        [Test]
        public void Publish_SingleSubscriber_ReceivesEventPayload()
        {
            // Arrange
            var bus = new GameEventBus();
            EnemyKilledEvent received = null;

            bus.Subscribe<EnemyKilledEvent>(e => received = e);

            var sampleEvent = new EnemyKilledEvent(Guid.NewGuid(), "Slime", new GridPos(1, 2), 25);

            // Act
            bus.Publish(sampleEvent);

            // Assert
            Assert.That(received, Is.Not.Null);
            Assert.That(received.EnemyType, Is.EqualTo("Slime"));
            Assert.That(received.Position, Is.EqualTo(new GridPos(1, 2)));
            Assert.That(received.ExperienceReward, Is.EqualTo(25));
        }

        [Test]
        public void Publish_MultipleSubscribers_InvokesAllRegisteredHandlers()
        {
            // Arrange
            var bus = new GameEventBus();
            int countA = 0;
            int countB = 0;

            bus.Subscribe<EnemyKilledEvent>(_ => countA++);
            bus.Subscribe<EnemyKilledEvent>(_ => countB++);

            var sampleEvent = new EnemyKilledEvent(Guid.NewGuid(), "Bat", new GridPos(0, 0), 10);

            // Act
            bus.Publish(sampleEvent);

            // Assert
            Assert.That(countA, Is.EqualTo(1));
            Assert.That(countB, Is.EqualTo(1));
        }

        [Test]
        public void Unsubscribe_HandlerStopsReceivingSubsequentEvents()
        {
            // Arrange
            var bus = new GameEventBus();
            int callCount = 0;
            Action<EnemyKilledEvent> handler = _ => callCount++;

            bus.Subscribe(handler);
            bus.Publish(new EnemyKilledEvent(Guid.NewGuid(), "Goblin", new GridPos(0, 0), 10));
            Assert.That(callCount, Is.EqualTo(1));

            // Act: Hủy đăng ký
            bus.Unsubscribe(handler);
            bus.Publish(new EnemyKilledEvent(Guid.NewGuid(), "Goblin", new GridPos(0, 0), 10));

            // Assert: Không tăng thêm lần gọi nào nữa
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void Publish_WhenNoSubscribers_DoesNotThrowException()
        {
            // Arrange
            var bus = new GameEventBus();

            // Act & Assert: Không văng lỗi
            Assert.DoesNotThrow(() => bus.Publish(new EnemyKilledEvent(Guid.NewGuid(), "Ghost", new GridPos(0, 0), 10)));
        }

        [Test]
        public void Clear_RemovesAllSubscribersAcrossAllEventTypes()
        {
            // Arrange
            var bus = new GameEventBus();
            int calls = 0;
            bus.Subscribe<EnemyKilledEvent>(_ => calls++);

            // Act: Xóa sạch toàn bộ
            bus.Clear();
            bus.Publish(new EnemyKilledEvent(Guid.NewGuid(), "Orc", new GridPos(0, 0), 50));

            // Assert
            Assert.That(calls, Is.EqualTo(0));
        }
    }
}
