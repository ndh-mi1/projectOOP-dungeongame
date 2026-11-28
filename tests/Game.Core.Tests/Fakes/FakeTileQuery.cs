using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.ValueObjects;

namespace Game.Core.Tests.Fakes
{
    /// <summary>
    /// Lớp Fake đóng giả hợp đồng ITileQuery để kiểm thử độc lập thuật toán A* và AI
    /// mà không cần phụ thuộc vào lớp Floor thật của TV2 (Tiêu chí 5.1).
    /// </summary>
    public class FakeTileQuery : ITileQuery
    {
        private readonly HashSet<GridPos> _blockedTiles = new HashSet<GridPos>();
        private readonly HashSet<GridPos> _opaqueTiles = new HashSet<GridPos>();

        public void Block(int x, int y)
        {
            _blockedTiles.Add(new GridPos(x, y));
        }

        public void SetOpaque(int x, int y)
        {
            _opaqueTiles.Add(new GridPos(x, y));
        }

        public bool IsWalkable(int x, int y)
        {
            return !_blockedTiles.Contains(new GridPos(x, y));
        }

        public bool IsOpaque(int x, int y)
        {
            return _opaqueTiles.Contains(new GridPos(x, y));
        }
    }
}
