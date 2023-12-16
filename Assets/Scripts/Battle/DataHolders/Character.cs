namespace Battle.DataHolders
{
    /// <summary>
    ///     Non mono-behaviour character class.
    ///     Contains character data for any character in the game
    /// </summary>
    public class Character
    {
        private readonly Health _health;
        private readonly Mana _mana;

        public Character(Health health, Stats stats, Mana mana)
        {
            _health = health;
            Stats = stats;
            _mana = mana;
        }

        public IReadOnlyHealth Health => _health;
        public Stats Stats { get; }

        public IReadOnlyMana Mana => _mana;

        public virtual void TakeDamage(float amount)
        {
            _health.TakeDamage(amount);
        }

        public virtual void Heal(float amount)
        {
            _health.Heal(amount);
        }

        public virtual void SpendMana(float amount)
        {
            _mana.Spend(amount);
        }

        public virtual void RegenMana()
        {
            _mana.RegenTick();
        }
    }
}