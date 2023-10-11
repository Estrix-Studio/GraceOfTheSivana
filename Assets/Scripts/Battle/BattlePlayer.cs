using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattlePlayer : MonoBehaviour
{
    [SerializeField] private CharacterAbilities playerAbilities;
    
    private readonly List<AnimatedAbility> _activeAbilities = new List<AnimatedAbility>();
    
    private Character _character;

    private Animator _animator;
    
    private void UseAbility(int index, Character target)
    {
        _activeAbilities[index].ability.Use(_character, target);
        _animator.Play(_activeAbilities[index].animation.name);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _character = new Character(new Health(100), new Stats());
        _activeAbilities.AddRange(playerAbilities.animatedAbilities);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseAbility(0, null);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseAbility(1, null);
        }
    }
}
