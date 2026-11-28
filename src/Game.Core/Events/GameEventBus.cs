using System;
using System.Collections.Generic;
using Game.Core.Contracts;

namespace Game.Core.Events
{
    /// <summary>
    /// Triển khai trung tâm phân phối sự kiện (GoF Observer Pattern / EventBus).
    /// Hỗ trợ đăng ký đa kênh, chống lỗi sửa đổi danh sách trong vòng lặp (Collection Modified)
    /// và cung cấp cơ chế hủy đăng ký triệt để chống rò rỉ bộ nhớ.
    /// </summary>
    public class GameEventBus : IGameEventBus
    {
        private readonly object _lock = new object();
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<T> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "Handler không được null.");
            }

            lock (_lock)
            {
                Type eventType = typeof(T);
                if (!_subscribers.TryGetValue(eventType, out List<Delegate> handlers))
                {
                    handlers = new List<Delegate>();
                    _subscribers[eventType] = handlers;
                }

                if (!handlers.Contains(handler))
                {
                    handlers.Add(handler);
                }
            }
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            if (handler == null)
            {
                return;
            }

            lock (_lock)
            {
                Type eventType = typeof(T);
                if (_subscribers.TryGetValue(eventType, out List<Delegate> handlers))
                {
                    handlers.Remove(handler);
                    if (handlers.Count == 0)
                    {
                        _subscribers.Remove(eventType);
                    }
                }
            }
        }

        public void Publish<T>(T @event)
        {
            if (@event == null)
            {
                return;
            }

            Delegate[] handlersToInvoke;

            lock (_lock)
            {
                Type eventType = typeof(T);
                if (!_subscribers.TryGetValue(eventType, out List<Delegate> handlers) || handlers.Count == 0)
                {
                    return;
                }

                // Sao chép mảng handler để tránh ngoại lệ Collection Was Modified nếu handler tự hủy đăng ký
                handlersToInvoke = handlers.ToArray();
            }

            foreach (Delegate handler in handlersToInvoke)
            {
                if (handler is Action<T> action)
                {
                    action(@event);
                }
            }
        }

        /// <summary>
        /// Xóa sạch toàn bộ người nghe sự kiện (thường dùng khi reset phiên game).
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _subscribers.Clear();
            }
        }
    }
}
