using System;

namespace Battle.DataHolders
{
    public interface IReadOnlyMana
    {
        float Current { get; }
        float Max { get; }
        public event Action OnManaChanged;

        bool CanSpend(float amount);
    }


    /// <summary>
    ///     Manages mana of character.
    ///     RegenTick should be called from outside.
    ///     Can be extended with future requirements.
    /// </summary>
    public class Mana : IReadOnlyMana
    {
        private readonly float _regenRate;

        //Starts at max mana
        public Mana(float max, float regenRate)
        {
            Max = max;
            Current = max;
            _regenRate = regenRate;
        }

        public float Current { get; private set; }

        public float Max { get; }

        public event Action OnManaChanged;

        public bool CanSpend(float amount)
        {
            return Current >= amount;
        }

        public void RegenTick()
        {
            Current += _regenRate;
            if (Current > Max) Current = Max;
            OnManaChanged?.Invoke();
        }

        public void Spend(float amount)
        {
            if (!CanSpend(amount)) throw new Exception("Cannot spend more mana than character have");

            Current -= amount;
            OnManaChanged?.Invoke();
        }
    }
}