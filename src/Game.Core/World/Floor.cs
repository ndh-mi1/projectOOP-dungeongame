using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.ValueObjects;

namespace Game.Core.World
{
    /// <summary>
    /// Represents a complete dungeon floor composed of a 2D tile grid and room nodes.
    /// Implements <see cref="ITileQuery"/> to decouple Combat/AI pathfinding from world storage.
    /// </summary>
    public sealed class Floor : ITileQuery
    {
        private readonly TileKind[,] _tiles;
        private readonly List<RoomNode> _rooms = new List<RoomNode>();

        public int FloorNumber { get; }
        public int Width { get; }
        public int Height { get; }
        public GridPos StartPosition { get; private set; }
        public GridPos ExitPosition { get; private set; }
        public IReadOnlyList<RoomNode> Rooms => _rooms.AsReadOnly();

        public Floor(int floorNumber, int width, int height)
        {
            if (floorNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(floorNumber), "Floor number must be at least 1.");
            }

            if (width < 5)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Floor width must be at least 5.");
            }

            if (height < 5)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Floor height must be at least 5.");
            }

            FloorNumber = floorNumber;
            Width = width;
            Height = height;
            _tiles = new TileKind[width, height];

            Fill(TileKind.Wall);
        }

        /// <summary>
        /// Checks whether (x, y) is within valid grid boundaries.
        /// </summary>
        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Returns the <see cref="TileKind"/> at (x, y), or <see cref="TileKind.Void"/> if out of bounds.
        /// </summary>
        public TileKind GetTile(int x, int y)
        {
            if (!IsInBounds(x, y))
            {
                return TileKind.Void;
            }

            return _tiles[x, y];
        }

        /// <summary>
        /// Sets the <see cref="TileKind"/> at (x, y). Throws if out of bounds or null.
        /// </summary>
        public void SetTile(int x, int y, TileKind tileKind)
        {
            if (tileKind == null)
            {
                throw new ArgumentNullException(nameof(tileKind));
            }

            if (!IsInBounds(x, y))
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"Coordinates ({x}, {y}) are outside floor bounds ({Width}x{Height}).");
            }

            _tiles[x, y] = tileKind;
        }

        /// <summary>
        /// Fills the entire grid with the specified <see cref="TileKind"/>.
        /// </summary>
        public void Fill(TileKind tileKind)
        {
            if (tileKind == null)
            {
                throw new ArgumentNullException(nameof(tileKind));
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _tiles[x, y] = tileKind;
                }
            }
        }

        /// <summary>
        /// Adds a room node to this floor and carves its interior with <see cref="TileKind.Floor"/>.
        /// </summary>
        public void AddRoom(RoomNode room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            _rooms.Add(room);
            CarveRect(room.X, room.Y, room.Width, room.Height, TileKind.Floor);
        }

        /// <summary>
        /// Carves a rectangular region with the given tile kind, clamped to floor bounds.
        /// </summary>
        public void CarveRect(int startX, int startY, int width, int height, TileKind tileKind)
        {
            if (tileKind == null)
            {
                throw new ArgumentNullException(nameof(tileKind));
            }

            int endX = Math.Min(Width - 1, startX + width - 1);
            int endY = Math.Min(Height - 1, startY + height - 1);
            int minX = Math.Max(0, startX);
            int minY = Math.Max(0, startY);

            for (int x = minX; x <= endX; x++)
            {
                for (int y = minY; y <= endY; y++)
                {
                    _tiles[x, y] = tileKind;
                }
            }
        }

        public void SetStartPosition(GridPos pos)
        {
            if (!IsInBounds(pos.X, pos.Y))
            {
                throw new ArgumentOutOfRangeException(nameof(pos), "Start position must be within floor bounds.");
            }

            StartPosition = pos;
        }

        public void SetExitPosition(GridPos pos)
        {
            if (!IsInBounds(pos.X, pos.Y))
            {
                throw new ArgumentOutOfRangeException(nameof(pos), "Exit position must be within floor bounds.");
            }

            ExitPosition = pos;
            _tiles[pos.X, pos.Y] = TileKind.StairsDown;
        }

        /// <summary>
        /// Finds the room containing grid coordinates (x, y), or null if in a corridor/wall.
        /// </summary>
        public RoomNode GetRoomAt(int x, int y)
        {
            foreach (RoomNode room in _rooms)
            {
                if (room.Contains(x, y))
                {
                    return room;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public bool IsWalkable(int x, int y)
        {
            return GetTile(x, y).IsWalkable;
        }

        /// <inheritdoc />
        public bool IsOpaque(int x, int y)
        {
            return GetTile(x, y).IsOpaque;
        }
    }
}
