using System;
using System.Collections.Generic;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.Combat
{
    /// <summary>
    /// Bộ phân giải 5 phản ứng nguyên tố liên phân hệ (R1–R5 trong 01_CONTENT_BIBLE & Luật R-15, R-16).
    /// Phát sự kiện ReactionTriggeredEvent lên IGameEventBus để phối hợp hiệu ứng với phân hệ World/UI (TV2).
    /// </summary>
    public class ReactionResolver
    {
        private const int ElectrocuteDamage = 25;
        private const float ElectrocuteStunSeconds = 1.0f;
        private const int MaxConductiveWaterTiles = 64;

        private const int CombustionDamage = 60;
        private const float CombustionRadius = 2.5f;

        private const float ShatterDamageMultiplier = 2.5f;
        private const float ExtinguishSmokeDuration = 2.0f;
        private const float SolidifyDuration = 5.0f;

        private readonly IGameEventBus _eventBus;

        public ReactionResolver(IGameEventBus eventBus = null)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// R1: Dập Hỏa (Extinguish - Luật R-16 & khóa hồi quy BUG-06).
        /// Cháy (Burn) gặp ô Nước -> xóa Burn ngay frame đầu tiên và tạo 1 cụm khói 2s (chống phát khói 60 lần/s).
        /// </summary>
        public bool TryResolveExtinguish(Actor target, StatusEffectManager targetEffects, SurfaceKind surface)
        {
            if (target == null || target.IsDead || targetEffects == null || surface != SurfaceKind.Water)
            {
                return false;
            }

            if (!targetEffects.RemoveEffect(StatusEffectType.Burn))
            {
                return false;
            }

            _eventBus?.Publish(new ReactionTriggeredEvent(
                ReactionType.Extinguish,
                target.GridPosition,
                new[] { target.GridPosition },
                damageDealt: 0,
                smokeDuration: ExtinguishSmokeDuration));

            return true;
        }

        /// <summary>
        /// R2: Thủy Điện Kích (Electrocute - INT-04).
        /// Kỹ năng Sét trúng ô Nước -> lan truyền qua cụm ô nước kề cận, gây 25 sát thương lôi + choáng 1s.
        /// </summary>
        public int ResolveElectrocute(
            GridPos impactCell,
            DamageType element,
            Func<int, int, SurfaceKind> surfaceLookup,
            IReadOnlyList<Actor> candidates)
        {
            if (element != DamageType.Lightning || surfaceLookup == null || surfaceLookup(impactCell.X, impactCell.Y) != SurfaceKind.Water)
            {
                return 0;
            }

            List<GridPos> waterCluster = CollectConnectedTiles(impactCell, SurfaceKind.Water, surfaceLookup, MaxConductiveWaterTiles);
            var waterSet = new HashSet<GridPos>(waterCluster);
            int affectedActors = 0;

            if (candidates != null)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    Actor actor = candidates[i];
                    if (actor == null || actor.IsDead || !waterSet.Contains(actor.GridPosition))
                    {
                        continue;
                    }

                    actor.TakeDamage(new DamageInfo(ElectrocuteDamage, DamageType.Lightning));
                    if (!actor.IsDead)
                    {
                        actor.ApplyStun(ElectrocuteStunSeconds);
                    }

                    affectedActors++;
                }
            }

            _eventBus?.Publish(new ReactionTriggeredEvent(
                ReactionType.Electrocute,
                impactCell,
                waterCluster.AsReadOnly(),
                damageDealt: ElectrocuteDamage));

            return affectedActors;
        }

        /// <summary>
        /// R3: Băng Toái (Shatter - Luật R-15).
        /// Vũ khí nặng đánh trúng thực thể đang bị Freeze -> nhân 2.5x sát thương chí mạng và giải trừ Freeze.
        /// </summary>
        public int TryResolveShatter(
            Actor target,
            StatusEffectManager targetEffects,
            int baseWeaponDamage,
            bool isBluntImpact)
        {
            if (target == null || target.IsDead || targetEffects == null || !isBluntImpact || baseWeaponDamage <= 0)
            {
                return 0;
            }

            if (!targetEffects.HasEffect(StatusEffectType.Freeze))
            {
                return 0;
            }

            targetEffects.RemoveEffect(StatusEffectType.Freeze);
            int shatterDamage = Math.Max(1, (int)Math.Round(baseWeaponDamage * ShatterDamageMultiplier));
            target.TakeDamage(new DamageInfo(shatterDamage, DamageType.Physical, isCritical: true));

            _eventBus?.Publish(new ReactionTriggeredEvent(
                ReactionType.Shatter,
                target.GridPosition,
                new[] { target.GridPosition },
                damageDealt: shatterDamage));

            return shatterDamage;
        }

        /// <summary>
        /// R4: Độc Hỏa Nổ (Combustion).
        /// Đám mây khí độc hoặc trạng thái Poison gặp sát thương Fire -> nổ bán kính 2.5 ô gây 60 dmg nổ.
        /// </summary>
        public int ResolveCombustion(
            GridPos origin,
            DamageType triggerElement,
            bool hasPoisonSource,
            IReadOnlyList<Actor> candidates)
        {
            if (triggerElement != DamageType.Fire || !hasPoisonSource)
            {
                return 0;
            }

            int affectedActors = 0;
            if (candidates != null)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    Actor actor = candidates[i];
                    if (actor == null || actor.IsDead)
                    {
                        continue;
                    }

                    if (origin.EuclideanDistance(actor.GridPosition) <= CombustionRadius)
                    {
                        actor.TakeDamage(new DamageInfo(CombustionDamage, DamageType.Fire));
                        affectedActors++;
                    }
                }
            }

            _eventBus?.Publish(new ReactionTriggeredEvent(
                ReactionType.Combustion,
                origin,
                new[] { origin },
                damageDealt: CombustionDamage));

            return affectedActors;
        }

        /// <summary>
        /// R5: Hóa Đá Dung Nham (Solidify).
        /// Kỹ năng Băng trúng ô Dung nham -> làm nguội thành đá basalt đi được trong 5s và bốc hơi khói 2s.
        /// </summary>
        public bool TryResolveSolidify(
            GridPos targetCell,
            DamageType element,
            Func<int, int, SurfaceKind> surfaceLookup,
            Action<GridPos, SurfaceKind> onTransformTile = null)
        {
            if (element != DamageType.Ice || surfaceLookup == null || surfaceLookup(targetCell.X, targetCell.Y) != SurfaceKind.Lava)
            {
                return false;
            }

            onTransformTile?.Invoke(targetCell, SurfaceKind.Basalt);

            _eventBus?.Publish(new ReactionTriggeredEvent(
                ReactionType.Solidify,
                targetCell,
                new[] { targetCell },
                damageDealt: 0,
                smokeDuration: ExtinguishSmokeDuration,
                transformDuration: SolidifyDuration));

            return true;
        }

        private static List<GridPos> CollectConnectedTiles(
            GridPos origin,
            SurfaceKind targetKind,
            Func<int, int, SurfaceKind> surfaceLookup,
            int maxTiles)
        {
            var result = new List<GridPos>();
            var visited = new HashSet<GridPos>();
            var queue = new Queue<GridPos>();

            visited.Add(origin);
            queue.Enqueue(origin);

            while (queue.Count > 0 && result.Count < maxTiles)
            {
                GridPos current = queue.Dequeue();
                result.Add(current);

                foreach (GridPos dir in GridPos.CardinalDirections)
                {
                    GridPos neighbor = current + dir;
                    if (!visited.Contains(neighbor) && surfaceLookup(neighbor.X, neighbor.Y) == targetKind)
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }
    }
}
