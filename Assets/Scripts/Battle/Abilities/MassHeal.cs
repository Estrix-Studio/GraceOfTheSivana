using Battle.Core;
using Battle.DataHolders;
using UnityEngine;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/MassHeal")]
    public class MassHeal : Ability
    {
        public override void Use(Character owner, Character target)
        {
            // todo add stats dependency
            target.Heal(25);
        }
    }
}