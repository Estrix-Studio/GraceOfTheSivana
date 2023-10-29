using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string Name;
    public abstract void Use(Character owner, Character target);
}