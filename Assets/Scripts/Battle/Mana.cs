using System;

public class Mana
{
    public float Current { get; }
    public float Max { get; }
    
    private float _current;
    private float _max;
    private float _regenRate;
    
    public event Action OnManaChanged;
    
    //Starts at max mana
    public Mana(float max, float regenRate)
    {
        _max = max;
        _current = max;
        _regenRate = regenRate;
    }

    public void RegenTick()
    {
        _current += _regenRate;
        if (_current > _max)
        {
            _current = _max;
        }
        OnManaChanged?.Invoke();
    }

    public bool CanSpend(float amount)
    {
        return _current >= amount;
    }
    
    public void Spend(float amount)
    {
        if (!CanSpend(amount)) throw new Exception("Cannot spend more mana than character have");
        
        _current -= amount;
        OnManaChanged?.Invoke();
    }
}