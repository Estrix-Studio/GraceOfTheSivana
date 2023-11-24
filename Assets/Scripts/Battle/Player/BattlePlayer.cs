using System.Collections.Generic;
using Battle.Core;
using Battle.DataHolders;
using UnityEngine;

namespace Battle.Player
{
    /// <summary>
    /// Handles logic for player character in battle.\
    /// sets up character and abilities.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BattlePlayer : MonoBehaviour
    {
        [SerializeField] private CharacterAbilities playerAbilities;
    
        private readonly List<AnimatedAbility> _activeAbilities = new List<AnimatedAbility>();
        public IReadOnlyList<Ability> Abilities => _activeAbilities.ConvertAll(a => a.ability);
    
        private Character _character;
        public Character Character => _character;
    
        private Animator _animator;
    
        public void UseAbility(int index, Character target)
        {
            _activeAbilities[index].ability.Use(_character, target);
            _animator.Play(_activeAbilities[index].animation.name);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = new Character(new Health(100), new Stats(), new Mana(100, 10));
            _activeAbilities.AddRange(playerAbilities.animatedAbilities);
        }
    }
}
