namespace Game.Core.Contracts
{
    /// <summary>
    /// Hợp đồng truy vấn ô địa hình giữa phân hệ Combat/AI (Đình Huy) và phân hệ World (Công Huy).
    /// Giúp Combat và AI kiểm tra chướng ngại vật mà không phụ thuộc trực tiếp vào lớp Floor.
    /// </summary>
    public interface ITileQuery
    {
        /// <summary>
        /// Kiểm tra xem ô tại tọa độ (x, y) có thể bước vào được không (không có tường/vật cản).
        /// </summary>
        bool IsWalkable(int x, int y);

        /// <summary>
        /// Kiểm tra xem ô tại tọa độ (x, y) có chắn tầm nhìn không (dùng cho thuật toán Line of Sight/Bresenham).
        /// </summary>
        bool IsOpaque(int x, int y);
    }
}
