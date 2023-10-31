using UnityEngine;

/// <summary>
/// Contains information about ability.
/// Children of this class contain the logic for the ability.
/// </summary>
public abstract class Ability : ScriptableObject
{
    public string Name;
    public abstract void Use(Character owner, Character target);
}