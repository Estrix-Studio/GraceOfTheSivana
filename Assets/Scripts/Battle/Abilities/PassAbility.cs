using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Pass")]
    public class PassAbility : Ability
    {
        public override void Use(Character owner, Character target)
        {
            Debug.Log($"Character {owner} passed their turn.");
        }
    }
}