using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public abstract void Use(Character owner, Character target);
}