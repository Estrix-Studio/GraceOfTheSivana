public class Character
{
    private readonly Health _health;
    private readonly Stats _stats;
    
    public IReadOnlyHealth Health => _health;
    public Stats Stats => _stats;
    
    public Character(Health health, Stats stats)
    {
        _health = health;
        _stats = stats;
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
