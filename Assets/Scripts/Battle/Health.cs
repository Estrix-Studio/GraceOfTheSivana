using System;

public class Health
{
    private float _current;
    private float _max;
    
    public float Current => _current;
    
    public Action OnDeath;
    
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
        _current -= amount;
        if (_current <= 0)
        {
            _current = 0;
            OnDeath?.Invoke();
        }
    }
    
    public void Heal(float amount)
    {
        _current += amount;
        if (_current > _max)
        {
            _current = _max;
        }
    }
}