using UnityEngine;

namespace DataHolders
{
    /// <summary>
    /// Contains Information about current character ability set.
    /// </summary>
    [CreateAssetMenu(menuName = "Character/Abilities")]
    public class CharacterAbilities : ScriptableObject
    {
        public AnimatedAbility[] animatedAbilities;
    }
}
