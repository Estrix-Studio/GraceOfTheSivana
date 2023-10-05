using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : Character
{
    [SerializeField] private List<Ability> activeAbilities;
    
    public void UseAbility(int index, Character target)
    {
        activeAbilities[index].Use(this, target);
    }
}
