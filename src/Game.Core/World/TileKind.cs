using System;
using System.Collections.Generic;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.World
{
    /// <summary>
    /// Enumeration of all tile types in the dungeon grid.
    /// </summary>
    public enum TileType
    {
        Void = 0,
        Floor = 1,
        Wall = 2,
        Door = 3,
        StairsDown = 4,
        Water = 5,
        Lava = 6,
        Trap = 7
    }

    /// <summary>
    /// Flyweight class representing shared intrinsic properties and step behavior of a terrain tile.
    /// Eliminates per-cell memory overhead across large dungeon grids.
    /// </summary>
    public sealed class TileKind
    {
        public TileType Type { get; }
        public string Name { get; }
        public bool IsWalkable { get; }
        public bool IsOpaque { get; }
        public float MovementCost { get; }
        public int StepDamage { get; }
        public DamageType StepDamageType { get; }

        private TileKind(
            TileType type,
            string name,
            bool isWalkable,
            bool isOpaque,
            float movementCost = 1.0f,
            int stepDamage = 0,
            DamageType stepDamageType = DamageType.Physical)
        {
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsWalkable = isWalkable;
            IsOpaque = isOpaque;
            MovementCost = movementCost;
            StepDamage = stepDamage;
            StepDamageType = stepDamageType;
        }

        public static readonly TileKind Void = new TileKind(TileType.Void, "Void", isWalkable: false, isOpaque: true, movementCost: float.PositiveInfinity);
        public static readonly TileKind Floor = new TileKind(TileType.Floor, "Stone Floor", isWalkable: true, isOpaque: false, movementCost: 1.0f);
        public static readonly TileKind Wall = new TileKind(TileType.Wall, "Stone Wall", isWalkable: false, isOpaque: true, movementCost: float.PositiveInfinity);
        public static readonly TileKind Door = new TileKind(TileType.Door, "Dungeon Door", isWalkable: true, isOpaque: false, movementCost: 1.0f);
        public static readonly TileKind StairsDown = new TileKind(TileType.StairsDown, "Stairs Down", isWalkable: true, isOpaque: false, movementCost: 1.0f);
        public static readonly TileKind Water = new TileKind(TileType.Water, "Shallow Water", isWalkable: true, isOpaque: false, movementCost: 1.5f);
        public static readonly TileKind Lava = new TileKind(TileType.Lava, "Molten Lava", isWalkable: true, isOpaque: false, movementCost: 2.0f, stepDamage: 15, stepDamageType: DamageType.Fire);
        public static readonly TileKind Trap = new TileKind(TileType.Trap, "Spike Trap", isWalkable: true, isOpaque: false, movementCost: 1.0f, stepDamage: 10, stepDamageType: DamageType.Physical);

        private static readonly Dictionary<TileType, TileKind> Registry = new Dictionary<TileType, TileKind>
        {
            { TileType.Void, Void },
            { TileType.Floor, Floor },
            { TileType.Wall, Wall },
            { TileType.Door, Door },
            { TileType.StairsDown, StairsDown },
            { TileType.Water, Water },
            { TileType.Lava, Lava },
            { TileType.Trap, Trap }
        };

        /// <summary>
        /// Retrieves the shared Flyweight instance for the specified <see cref="TileType"/>.
        /// </summary>
        public static TileKind FromType(TileType type)
        {
            if (Registry.TryGetValue(type, out TileKind kind))
            {
                return kind;
            }

            throw new ArgumentOutOfRangeException(nameof(type), $"Unknown TileType: {type}");
        }

        /// <summary>
        /// Applies terrain step effects (e.g. lava burn or spike trap damage) to an actor entering this tile.
        /// </summary>
        public void OnStep(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }

            if (StepDamage > 0 && !actor.IsDead)
            {
                actor.TakeDamage(new DamageInfo(StepDamage, StepDamageType));
            }
        }
    }
}
