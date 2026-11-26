using System;
using System.Collections.Generic;
using Game.Core.ValueObjects;

namespace Game.Core.Combat
{
    public enum ReactionType
    {
        Extinguish = 1,
        Electrocute = 2,
        Shatter = 3,
        Combustion = 4,
        Solidify = 5
    }

    public enum SurfaceKind
    {
        None = 0,
        Water = 1,
        Lava = 2,
        PoisonCloud = 3,
        Basalt = 4
    }

    /// <summary>
    /// Sự kiện thông báo phản ứng nguyên tố đã kích hoạt, giúp phân hệ TV2 phát FX, âm thanh và biến đổi ô gạch.
    /// </summary>
    public sealed class ReactionTriggeredEvent
    {
        public ReactionType Type { get; }
        public GridPos Origin { get; }
        public IReadOnlyList<GridPos> AffectedTiles { get; }
        public int DamageDealt { get; }
        public float SmokeDuration { get; }
        public float TransformDuration { get; }

        public ReactionTriggeredEvent(
            ReactionType type,
            GridPos origin,
            IReadOnlyList<GridPos> affectedTiles,
            int damageDealt,
            float smokeDuration = 0.0f,
            float transformDuration = 0.0f)
        {
            Type = type;
            Origin = origin;
            AffectedTiles = affectedTiles ?? Array.Empty<GridPos>();
            DamageDealt = Math.Max(0, damageDealt);
            SmokeDuration = Math.Max(0.0f, smokeDuration);
            TransformDuration = Math.Max(0.0f, transformDuration);
        }
    }
}
