using System;
using Game.Core.ValueObjects;

namespace Game.Core.Entities
{
    /// <summary>
    /// Lớp cơ sở trừu tượng cho mọi thực thể tồn tại trong thế giới game.
    /// Cung cấp định danh duy nhất (Id), tọa độ không gian thực và tọa độ lưới.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; }
        public float PositionX { get; protected set; }
        public float PositionY { get; protected set; }

        public GridPos GridPosition => new GridPos((int)Math.Floor(PositionX), (int)Math.Floor(PositionY));

        protected Entity(Guid id, float initialX, float initialY)
        {
            if (float.IsNaN(initialX) || float.IsInfinity(initialX))
            {
                throw new ArgumentException("Tọa độ X ban đầu không hợp lệ (NaN hoặc Infinity).", nameof(initialX));
            }

            if (float.IsNaN(initialY) || float.IsInfinity(initialY))
            {
                throw new ArgumentException("Tọa độ Y ban đầu không hợp lệ (NaN hoặc Infinity).", nameof(initialY));
            }

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            PositionX = initialX;
            PositionY = initialY;
        }

        protected Entity(float initialX, float initialY)
            : this(Guid.NewGuid(), initialX, initialY)
        {
        }

        /// <summary>
        /// Cập nhật logic của thực thể qua từng chu kỳ thời gian (Tick).
        /// </summary>
        /// <param name="deltaTime">Khoảng thời gian trôi qua giữa 2 chu kỳ (giây).</param>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Đặt lại tọa độ không gian cho thực thể với kiểm tra tính hợp lệ.
        /// </summary>
        public virtual void SetPosition(float x, float y)
        {
            if (float.IsNaN(x) || float.IsInfinity(x))
            {
                throw new ArgumentException("Tọa độ X không hợp lệ.", nameof(x));
            }

            if (float.IsNaN(y) || float.IsInfinity(y))
            {
                throw new ArgumentException("Tọa độ Y không hợp lệ.", nameof(y));
            }

            PositionX = x;
            PositionY = y;
        }
    }
}
