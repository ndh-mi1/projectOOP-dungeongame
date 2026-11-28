using System;
using Game.Core.Contracts;
using Game.Core.Entities;
using Game.Core.ValueObjects;

namespace Game.Core.Combat
{
    /// <summary>
    /// Bộ phân giải sát thương trung tâm (Luật R-01, R-02, R-15).
    /// Tính toán giáp, hệ số chí mạng, sát thương tối thiểu (Min = 1),
    /// kiểm tra bất tử i-frames và kích hoạt phản ứng Băng Toái (Shatter 2.5x).
    /// </summary>
    public class CombatResolver
    {
        private const int MinimumDamage = 1;
        private const float DefaultCritMultiplier = 1.5f;
        private const float ShatterDamageMultiplier = 2.5f;

        /// <summary>
        /// Tính toán sát thương cuối cùng sau khi trừ giáp và áp hệ số chí mạng / vỡ băng.
        /// Luật R-01: DmgActual = max(1, DmgRaw - ArmorValue) khi có đòn đánh (rawDamage > 0).
        /// </summary>
        public int CalculateDamage(
            int rawDamage,
            int armorValue,
            bool isCritical = false,
            float criticalMultiplier = DefaultCritMultiplier,
            bool applyShatter = false)
        {
            if (rawDamage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rawDamage), "Sát thương gốc không được âm.");
            }

            if (criticalMultiplier < 1.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(criticalMultiplier), "Hệ số chí mạng phải từ 1.0 trở lên.");
            }

            if (rawDamage == 0)
            {
                return 0;
            }

            float scaled = rawDamage;
            if (isCritical)
            {
                scaled *= criticalMultiplier;
            }

            if (applyShatter)
            {
                scaled *= ShatterDamageMultiplier;
            }

            int effectiveArmor = Math.Max(0, armorValue);
            int reduced = (int)Math.Round(scaled) - effectiveArmor;
            return Math.Max(MinimumDamage, reduced);
        }

        /// <summary>
        /// Thực thi đòn đánh lên một Actor có kiểm tra bất tử i-frames (Luật R-02) và hiệu ứng đóng băng (R-15).
        /// </summary>
        public int ExecuteAttack(
            Actor target,
            DamageInfo baseDamage,
            int targetArmor = 0,
            bool isBluntWeapon = false,
            StatusEffectManager targetEffects = null,
            float criticalMultiplier = DefaultCritMultiplier)
        {
            if (target == null || target.IsDead || !target.StateMachine.CanTakeDamage)
            {
                return 0;
            }

            return ApplyResolvedAttack(target, baseDamage, targetArmor, isBluntWeapon, targetEffects, criticalMultiplier);
        }

        /// <summary>
        /// Thực thi đòn đánh qua hợp đồng IDamageable (cho phép tương tác Cross-Module với mọi vật thể/Fake stub).
        /// </summary>
        public int ExecuteAttack(
            IDamageable target,
            DamageInfo baseDamage,
            int targetArmor = 0,
            bool isBluntWeapon = false,
            StatusEffectManager targetEffects = null,
            float criticalMultiplier = DefaultCritMultiplier)
        {
            if (target == null || target.IsDead)
            {
                return 0;
            }

            return ApplyResolvedAttack(target, baseDamage, targetArmor, isBluntWeapon, targetEffects, criticalMultiplier);
        }

        private int ApplyResolvedAttack(
            IDamageable target,
            DamageInfo baseDamage,
            int targetArmor,
            bool isBluntWeapon,
            StatusEffectManager targetEffects,
            float criticalMultiplier)
        {
            bool shatterTriggered = isBluntWeapon &&
                                    targetEffects != null &&
                                    targetEffects.HasEffect(StatusEffectType.Freeze);

            int finalDamage = CalculateDamage(
                baseDamage.Amount,
                targetArmor,
                baseDamage.IsCritical,
                criticalMultiplier,
                shatterTriggered);

            if (finalDamage <= 0)
            {
                return 0;
            }

            if (shatterTriggered)
            {
                targetEffects.RemoveEffect(StatusEffectType.Freeze);
            }

            bool resultingCritical = baseDamage.IsCritical || shatterTriggered;
            int hpBefore = target.CurrentHealth;
            target.TakeDamage(new DamageInfo(finalDamage, baseDamage.Type, resultingCritical));

            // Nếu target chặn sát thương nội bộ (VD: HurtState), lượng máu trừ sẽ bằng 0
            return target.CurrentHealth == hpBefore ? 0 : finalDamage;
        }
    }
}
