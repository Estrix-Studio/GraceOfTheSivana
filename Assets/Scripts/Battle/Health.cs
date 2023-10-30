using System;

public interface IReadOnlyHealth
{
    float Current { get; }
    float Max { get; }
    
    bool IsAlive { get; }
    
    event Action OnDeath;
    event Action OnHealthChanged;
}

public class Health : IReadOnlyHealth
{
    private float _current;
    private float _max;
    private bool _isAlive = true;
    
    public float Current => _current;
    public float Max => _max;
    public bool IsAlive => _isAlive;

    public event Action OnDeath;
    public event Action OnHealthChanged;
    
    public Health(float max)
    {
        _max = max;
        _current = max;
    }
    
    public Health(float max, float current)
    {
        _max = max;
        _current = current;
    }
    
    public void TakeDamage(float amount)
    {
        if (!_isAlive) return;
        
        _current -= amount;
        OnHealthChanged?.Invoke();
        if (_current <= 0)
        {
            _current = 0;
            _isAlive = false;
            OnDeath?.Invoke();
        }
    }
    
    public void Heal(float amount)
    {
        if (!_isAlive) return;
        
        _current += amount;
        OnHealthChanged?.Invoke();
        if (_current > _max)
        {
            _current = _max;
        }
    }
}