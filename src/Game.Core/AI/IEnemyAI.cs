using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.AI
{
    /// <summary>
    /// Giao diện chiến thuật AI (GoF Strategy Pattern) ra quyết định cho quái vật.
    /// Cho phép hoán đổi thuật toán hành vi trong thời gian thực (Runtime Strategy Swap).
    /// </summary>
    public interface IEnemyAI
    {
        /// <summary>
        /// Tên định danh của chiến thuật hành vi (VD: "Chase", "Patrol", "Kite").
        /// </summary>
        string BehaviorName { get; }

        /// <summary>
        /// Quyết định ô lưới tiếp theo quái vật cần di chuyển tới để tiếp cận mục tiêu.
        /// </summary>
        /// <param name="enemy">Bản thân quái vật thực thi.</param>
        /// <param name="target">Mục tiêu nhắm tới (thường là RoguePlayer).</param>
        /// <param name="tileQuery">Hợp đồng truy vấn tính vật cản của địa hình.</param>
        /// <returns>Tọa độ ô lưới tiếp theo nên bước tới; nếu không có đường đi trả về tọa độ hiện tại.</returns>
        GridPos DecideNextCell(Actor enemy, Actor target, ITileQuery tileQuery);
    }
}
