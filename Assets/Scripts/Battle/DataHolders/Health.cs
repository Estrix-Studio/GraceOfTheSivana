using System;

namespace Battle.DataHolders
{
    /// <summary>
    ///     Used to pass health outside of character.
    /// </summary>
    public interface IReadOnlyHealth
    {
        float Current { get; }
        float Max { get; }

        bool IsAlive { get; }

        bool IsFull { get; }

        event Action OnDeath;
        event Action OnHealthChanged;
    }

    /// <summary>
    ///     Manages health of character.
    ///     Can be extended with future requirements.
    /// </summary>
    public class Health : IReadOnlyHealth
    {
        public Health(float max)
        {
            Max = max;
            Current = max;
        }

        public Health(float max, float current)
        {
            Max = max;
            Current = current;
        }

        public float Current { get; private set; }

        public float Max { get; }

        public bool IsAlive { get; private set; } = true;

        public bool IsFull => Current == Max;

        public event Action OnDeath;
        public event Action OnHealthChanged;

        public void TakeDamage(float amount)
        {
            if (!IsAlive) return;

            Current -= amount;
            OnHealthChanged?.Invoke();
            if (Current <= 0)
            {
                Current = 0;
                IsAlive = false;
                OnDeath?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            Current += amount;
            if (Current > Max) Current = Max;
            OnHealthChanged?.Invoke();
        }
    }
}