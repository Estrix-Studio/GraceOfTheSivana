using Core;
using DataHolders;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Attack")]
    public class Attack : Ability
    {
        public override void Use(Character owner, Character target)
        {
            target.TakeDamage(10);
            Debug.Log($"Character {owner} used Attack on {target}");
        }
    }
}
