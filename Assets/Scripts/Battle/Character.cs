public class Character
{
    private readonly Health _health;
    private readonly Mana _mana;
    private readonly Stats _stats;
    
    public IReadOnlyHealth Health => _health;
    public Stats Stats => _stats;
    public Mana Mana => _mana;
    
    public Character(Health health, Stats stats, Mana mana)
    {
        _health = health;
        _stats = stats;
        _mana = mana;
    }

    public virtual void TakeDamage(float amount)
    {
        _health.TakeDamage(amount);
    }
    
    public virtual void Heal(float amount)
    {
        _health.Heal(amount);
    }
}
