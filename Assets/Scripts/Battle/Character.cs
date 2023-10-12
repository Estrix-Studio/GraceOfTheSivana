using UnityEngine;

public class Character
{
    private readonly Health _health;
    private readonly Stats _stats;
    public float CurrentHealth => _health.Current;
    
    public Character(Health health, Stats stats)
    {
        _health = health;
        _stats = stats;
    }

}
