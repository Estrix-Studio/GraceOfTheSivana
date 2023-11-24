using Battle.Core;
using Battle.DataHolders;
using UnityEngine;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Doge")]
    public class Doge : Ability
    {
        public override void Use(Character owner, Character target)
        {
            Debug.Log($"Character {owner} used Doge on {target}");
        }
    }
}