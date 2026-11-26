using System;
using System.Collections.Generic;
using Game.Core.Entities;
using Game.Core.State;
using Game.Core.ValueObjects;

namespace Game.Core.Combat
{
    public enum StatusEffectType
    {
        Burn = 0,
        Poison = 1,
        Freeze = 2,
        Regen = 3
    }

    /// <summary>
    /// Lớp cơ sở trừu tượng cho các hiệu ứng trạng thái theo thời gian (Luật R-06).
    /// Thực thi chu kỳ tick 1.0s/lần bằng đa hình (Dynamic Binding), tuyệt đối không dùng is/as.
    /// </summary>
    public abstract class StatusEffect
    {
        private const float DefaultTickInterval = 1.0f;
        private const float TimeTolerance = 0.0001f;

        private float _tickAccumulator;

        public abstract StatusEffectType EffectType { get; }
        public float RemainingDuration { get; protected set; }
        public float TickInterval { get; }
        public bool IsExpired => RemainingDuration <= 0.0f;

        protected StatusEffect(float duration, float tickInterval = DefaultTickInterval)
        {
            if (duration <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Thời lượng hiệu ứng phải lớn hơn 0.");
            }

            if (tickInterval <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(tickInterval), "Chu kỳ kích hoạt hiệu ứng phải lớn hơn 0.");
            }

            RemainingDuration = duration;
            TickInterval = tickInterval;
            _tickAccumulator = 0.0f;
        }

        public virtual void OnApply(Actor target)
        {
        }

        public virtual void OnExpire(Actor target)
        {
        }

        /// <summary>
        /// Cập nhật bộ đếm thời gian và gọi OnTick() đa hình mỗi khi đủ 1 chu kỳ.
        /// </summary>
        public void Tick(Actor target, float deltaTime)
        {
            if (target == null || IsExpired || deltaTime <= 0.0f)
            {
                return;
            }

            float effectiveDelta = Math.Min(deltaTime, RemainingDuration);
            _tickAccumulator += effectiveDelta;
            RemainingDuration -= effectiveDelta;

            while (_tickAccumulator >= TickInterval - TimeTolerance)
            {
                _tickAccumulator -= TickInterval;
                if (!target.IsDead)
                {
                    OnTick(target);
                }
            }

            if (RemainingDuration <= TimeTolerance)
            {
                RemainingDuration = 0.0f;
                OnExpire(target);
            }
        }

        protected abstract void OnTick(Actor target);
    }

    /// <summary>
    /// Hiệu ứng Thiêu đốt (Luật R-06): Gây 5 sát thương hỏa mỗi giây trong 3s.
    /// </summary>
    public sealed class BurnEffect : StatusEffect
    {
        private const float DefaultDuration = 3.0f;
        private const int DefaultDamagePerTick = 5;

        public override StatusEffectType EffectType => StatusEffectType.Burn;
        public int DamagePerTick { get; }

        public BurnEffect(float duration = DefaultDuration, int damagePerTick = DefaultDamagePerTick)
            : base(duration)
        {
            if (damagePerTick <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damagePerTick), "Sát thương đốt phải lớn hơn 0.");
            }

            DamagePerTick = damagePerTick;
        }

        protected override void OnTick(Actor target)
        {
            target.TakeDamage(new DamageInfo(DamagePerTick, DamageType.Fire));
        }
    }

    /// <summary>
    /// Hiệu ứng Trúng độc (Luật R-06): Gây 3 sát thương độc mỗi giây trong 6s.
    /// </summary>
    public sealed class PoisonEffect : StatusEffect
    {
        private const float DefaultDuration = 6.0f;
        private const int DefaultDamagePerTick = 3;

        public override StatusEffectType EffectType => StatusEffectType.Poison;
        public int DamagePerTick { get; }

        public PoisonEffect(float duration = DefaultDuration, int damagePerTick = DefaultDamagePerTick)
            : base(duration)
        {
            if (damagePerTick <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damagePerTick), "Sát thương độc phải lớn hơn 0.");
            }

            DamagePerTick = damagePerTick;
        }

        protected override void OnTick(Actor target)
        {
            target.TakeDamage(new DamageInfo(DamagePerTick, DamageType.Poison));
        }
    }

    /// <summary>
    /// Hiệu ứng Đóng băng (Luật R-06): Làm tê liệt hoàn toàn thực thể trong 2s.
    /// Khi bị đánh bằng vũ khí nặng sẽ kích hoạt phản ứng Băng Toái (Shatter - R-15).
    /// </summary>
    public sealed class FreezeEffect : StatusEffect
    {
        private const float DefaultDuration = 2.0f;
        private StunnedState _appliedStun;

        public override StatusEffectType EffectType => StatusEffectType.Freeze;

        public FreezeEffect(float duration = DefaultDuration)
            : base(duration)
        {
        }

        public override void OnApply(Actor target)
        {
            if (target == null || target.IsDead)
            {
                return;
            }

            _appliedStun = new StunnedState(RemainingDuration);
            target.StateMachine.TryTransitionTo(_appliedStun);
        }

        public override void OnExpire(Actor target)
        {
            // Nếu bị phá băng sớm (Shatter), trả tự do ngay cho Actor nếu không có trạng thái khác thay thế
            if (target != null && !target.IsDead && _appliedStun != null &&
                ReferenceEquals(target.StateMachine.CurrentState, _appliedStun))
            {
                target.StateMachine.TryTransitionTo(new IdleState());
            }
        }

        protected override void OnTick(Actor target)
        {
            // Đóng băng không gây sát thương theo giây mà giữ trạng thái bất động tới khi hết hạn hoặc vỡ băng
        }
    }

    /// <summary>
    /// Hiệu ứng Hồi phục dần (Regen từ thực phẩm tiêu hao): Hồi máu định kỳ mỗi giây.
    /// </summary>
    public sealed class RegenEffect : StatusEffect
    {
        private const float DefaultDuration = 6.0f;
        private const int DefaultHealPerTick = 5;

        public override StatusEffectType EffectType => StatusEffectType.Regen;
        public int HealPerTick { get; }

        public RegenEffect(float duration = DefaultDuration, int healPerTick = DefaultHealPerTick)
            : base(duration)
        {
            if (healPerTick <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(healPerTick), "Lượng hồi phục mỗi giây phải lớn hơn 0.");
            }

            HealPerTick = healPerTick;
        }

        protected override void OnTick(Actor target)
        {
            target.Heal(HealPerTick);
        }
    }

    /// <summary>
    /// Bộ quản lý các trạng thái đang gắn trên một Actor. Bảo vệ bất biến qua IReadOnlyList.
    /// </summary>
    public sealed class StatusEffectManager
    {
        private readonly Actor _owner;
        private readonly List<StatusEffect> _effects = new List<StatusEffect>();

        public IReadOnlyList<StatusEffect> ActiveEffects => _effects.AsReadOnly();

        public StatusEffectManager(Actor owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public void ApplyEffect(StatusEffect effect)
        {
            if (effect == null)
            {
                throw new ArgumentNullException(nameof(effect));
            }

            if (_owner.IsDead)
            {
                return;
            }

            RemoveEffect(effect.EffectType);
            _effects.Add(effect);
            effect.OnApply(_owner);
        }

        public bool HasEffect(StatusEffectType type)
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effects[i].EffectType == type && !_effects[i].IsExpired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RemoveEffect(StatusEffectType type)
        {
            bool removed = false;
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                if (_effects[i].EffectType == type)
                {
                    StatusEffect existing = _effects[i];
                    _effects.RemoveAt(i);
                    existing.OnExpire(_owner);
                    removed = true;
                }
            }

            return removed;
        }

        public void TickAll(float deltaTime)
        {
            if (_owner.IsDead || deltaTime <= 0.0f || _effects.Count == 0)
            {
                return;
            }

            StatusEffect[] snapshot = _effects.ToArray();
            for (int i = 0; i < snapshot.Length; i++)
            {
                StatusEffect effect = snapshot[i];
                effect.Tick(_owner, deltaTime);

                if (effect.IsExpired)
                {
                    _effects.Remove(effect);
                }
            }
        }

        public void Clear()
        {
            _effects.Clear();
        }
    }
}
