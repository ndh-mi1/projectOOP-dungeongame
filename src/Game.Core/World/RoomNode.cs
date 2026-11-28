using System;
using System.Collections.Generic;
using Game.Core.ValueObjects;

namespace Game.Core.World
{
    /// <summary>
    /// Semantic role of a room within a dungeon floor.
    /// </summary>
    public enum RoomType
    {
        Start = 0,
        Normal = 1,
        Treasure = 2,
        Shop = 3,
        Boss = 4
    }

    /// <summary>
    /// Lifecycle state of a dungeon room (Room State FSM).
    /// </summary>
    public enum RoomState
    {
        Locked = 0,
        Active = 1,
        Cleared = 2
    }

    /// <summary>
    /// Represents a rectangular room node inside a dungeon floor.
    /// Enforces room coordinate/size invariants and RoomState FSM transitions.
    /// </summary>
    public sealed class RoomNode
    {
        private readonly List<Guid> _enemyIds = new List<Guid>();

        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public RoomType Type { get; }
        public RoomState State { get; private set; }

        public GridPos Center => new GridPos(X + (Width / 2), Y + (Height / 2));
        public int Right => X + Width - 1;
        public int Top => Y + Height - 1;
        public IReadOnlyList<Guid> EnemyIds => _enemyIds.AsReadOnly();

        public RoomNode(int id, int x, int y, int width, int height, RoomType type = RoomType.Normal)
        {
            if (width < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Room width must be at least 3 tiles.");
            }

            if (height < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Room height must be at least 3 tiles.");
            }

            Id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Type = type;
            State = type == RoomType.Start ? RoomState.Cleared : RoomState.Locked;
        }

        /// <summary>
        /// Checks whether grid coordinate (x, y) lies inside this room's interior bounds.
        /// </summary>
        public bool Contains(int x, int y)
        {
            return x >= X && x < X + Width && y >= Y && y < Y + Height;
        }

        /// <summary>
        /// Checks whether this room overlaps another room (with optional padding).
        /// </summary>
        public bool Intersects(RoomNode other, int padding = 0)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return X - padding < other.X + other.Width
                && X + Width + padding > other.X
                && Y - padding < other.Y + other.Height
                && Y + Height + padding > other.Y;
        }

        /// <summary>
        /// Registers an enemy belonging to this room.
        /// </summary>
        public void AddEnemy(Guid enemyId)
        {
            if (enemyId == Guid.Empty)
            {
                throw new ArgumentException("Enemy ID cannot be empty.", nameof(enemyId));
            }

            if (!_enemyIds.Contains(enemyId))
            {
                _enemyIds.Add(enemyId);
            }
        }

        /// <summary>
        /// Transitions the room from Locked to Active when combat begins.
        /// Guard: Cleared rooms cannot be reactivated.
        /// </summary>
        public bool Activate()
        {
            if (State != RoomState.Locked)
            {
                return false;
            }

            State = RoomState.Active;
            return true;
        }

        /// <summary>
        /// Marks an enemy as defeated and automatically clears the room when none remain.
        /// </summary>
        public bool MarkEnemyDefeated(Guid enemyId)
        {
            bool removed = _enemyIds.Remove(enemyId);
            if (removed && _enemyIds.Count == 0 && State == RoomState.Active)
            {
                State = RoomState.Cleared;
            }

            return removed;
        }

        /// <summary>
        /// Transitions an Active room to Cleared.
        /// Guard: Only Active rooms can be cleared.
        /// </summary>
        public bool Clear()
        {
            if (State != RoomState.Active)
            {
                return false;
            }

            _enemyIds.Clear();
            State = RoomState.Cleared;
            return true;
        }
    }
}
