namespace Game.Core.Contracts
{
    /// <summary>
    /// Hợp đồng trừu tượng định nghĩa khả năng di chuyển và vận tốc trong không gian game.
    /// Tuân thủ nguyên lý Phân tách Giao diện (Interface Segregation Principle - ISP).
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Tốc độ di chuyển cơ sở của thực thể (đơn vị: ô/giây).
        /// </summary>
        float BaseSpeed { get; }

        /// <summary>
        /// Di chuyển thực thể theo hướng chuẩn hóa và khoảng thời gian delta.
        /// </summary>
        /// <param name="directionX">Thành phần hướng X (-1.0 đến 1.0).</param>
        /// <param name="directionY">Thành phần hướng Y (-1.0 đến 1.0).</param>
        /// <param name="deltaTime">Khoảng thời gian trôi qua giữa 2 chu kỳ cập nhật (giây).</param>
        void Move(float directionX, float directionY, float deltaTime);
    }
}
