using UnityEngine;

[CreateAssetMenu(menuName = "Character/Abilities")]
public class CharacterAbilities : ScriptableObject
{
    public AnimatedAbility[] attackAbilities;
    public AnimatedAbility[] defendAbilities;
}
