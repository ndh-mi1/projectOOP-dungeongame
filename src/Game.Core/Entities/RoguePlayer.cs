using System;

namespace Game.Core.Entities
{
    /// <summary>
    /// Thực thể người chơi chính trong hầm ngục.
    /// Quản lý tài nguyên Năng lượng (Mana), Cấp độ (Level) và Điểm kinh nghiệm (Exp).
    /// </summary>
    public class RoguePlayer : Actor
    {
        private const int DefaultInitialLevel = 1;
        private const int DefaultInitialExperience = 0;
        private const int BaseExperiencePerLevel = 100;
        private const int MinResourceValue = 0;

        public int CurrentMana { get; private set; }
        public int MaxMana { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }

        public int ExperienceToNextLevel => Level * BaseExperiencePerLevel;

        public RoguePlayer(
            Guid id,
            string name,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed,
            int maxMana)
            : base(id, name, initialX, initialY, maxHealth, baseSpeed)
        {
            if (maxMana <= MinResourceValue)
            {
                throw new ArgumentException("Năng lượng tối đa phải lớn hơn 0.", nameof(maxMana));
            }

            MaxMana = maxMana;
            CurrentMana = maxMana;
            Level = DefaultInitialLevel;
            Experience = DefaultInitialExperience;
        }

        public RoguePlayer(
            string name,
            float initialX,
            float initialY,
            int maxHealth,
            float baseSpeed,
            int maxMana)
            : this(Guid.NewGuid(), name, initialX, initialY, maxHealth, baseSpeed, maxMana)
        {
        }

        /// <summary>
        /// Tiêu hao điểm Mana khi người chơi thi triển kỹ năng.
        /// </summary>
        /// <param name="amount">Lượng mana cần dùng (phải lớn hơn 0).</param>
        /// <returns>True nếu tiêu hao thành công, False nếu không đủ mana.</returns>
        public bool ConsumeMana(int amount)
        {
            if (amount <= MinResourceValue)
            {
                throw new ArgumentException("Lượng mana tiêu hao phải lớn hơn 0.", nameof(amount));
            }

            if (IsDead || CurrentMana < amount)
            {
                return false;
            }

            CurrentMana -= amount;
            return true;
        }

        /// <summary>
        /// Hồi phục điểm Mana cho người chơi.
        /// </summary>
        /// <param name="amount">Lượng mana hồi phục.</param>
        public void RestoreMana(int amount)
        {
            if (IsDead || amount <= MinResourceValue)
            {
                return;
            }

            CurrentMana = Math.Min(MaxMana, CurrentMana + amount);
        }

        /// <summary>
        /// Nhận điểm kinh nghiệm khi tiêu diệt quái vật hoặc hoàn thành mục tiêu.
        /// Tự động thăng cấp khi tích lũy đủ điểm.
        /// </summary>
        public void GainExperience(int amount)
        {
            if (IsDead || amount <= MinResourceValue)
            {
                return;
            }

            Experience += amount;

            while (Experience >= ExperienceToNextLevel)
            {
                Experience -= ExperienceToNextLevel;
                LevelUp();
            }
        }

        /// <summary>
        /// Cập nhật trạng thái người chơi theo chu kỳ Tick thời gian.
        /// </summary>
        public override void Update(float deltaTime)
        {
            if (IsDead || deltaTime <= 0.0f)
            {
                return;
            }

            base.Update(deltaTime);
        }

        private void LevelUp()
        {
            Level++;
            // Khôi phục tài nguyên khi thăng cấp
            CurrentMana = MaxMana;
            Heal(MaxHealth);
        }
    }
}
