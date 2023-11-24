using DataHolders;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/DoubleAttack")]
    public class DoubleAttack : Attack
    {
        public override void Use(Character owner, Character target)
        {
            base.Use(owner, target);
            base.Use(owner, target);
        }
    }
}