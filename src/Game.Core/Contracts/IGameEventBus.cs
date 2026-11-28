using System;

namespace Game.Core.Contracts
{
    /// <summary>
    /// Hợp đồng cầu nối phân phối sự kiện trung tâm (GoF Observer Pattern / Event Aggregator).
    /// Triệt tiêu hoàn toàn sự phụ thuộc trực tiếp và phụ thuộc vòng giữa phân hệ Combat/AI (TV1) và World/Items (TV2).
    /// </summary>
    public interface IGameEventBus
    {
        /// <summary>
        /// Phát tán một thông điệp sự kiện tới tất cả các thành phần đang đăng ký lắng nghe.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu sự kiện.</typeparam>
        /// <param name="event">Đối tượng chứa dữ liệu sự kiện.</param>
        void Publish<T>(T @event);

        /// <summary>
        /// Đăng ký phương thức xử lý khi có sự kiện thuộc kiểu T phát sinh.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu sự kiện.</typeparam>
        /// <param name="handler">Hàm ủy quyền (delegate) tiếp nhận sự kiện.</param>
        void Subscribe<T>(Action<T> handler);

        /// <summary>
        /// Hủy đăng ký phương thức xử lý sự kiện để giải phóng bộ nhớ (chống rò rỉ Memory Leak).
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu sự kiện.</typeparam>
        /// <param name="handler">Hàm ủy quyền cần gỡ bỏ.</param>
        void Unsubscribe<T>(Action<T> handler);
    }
}
