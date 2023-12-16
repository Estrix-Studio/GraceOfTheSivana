using Battle.DataHolders;
using UnityEngine;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/DoubleDoge")]
    public class DoubleDoge : Doge
    {
        public override void Use(Character owner, Character target)
        {
            base.Use(owner, target);
            base.Use(owner, target);
        }
    }
}