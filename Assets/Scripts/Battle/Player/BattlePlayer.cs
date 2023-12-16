using System;
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
    
        private readonly List<AbilityContext> _activeAbilities = new List<AbilityContext>();
        public IReadOnlyList<Ability> Abilities => _activeAbilities.ConvertAll(a => a.ability);
    
        public IReadOnlyList<AbilityContext> AbilityContexts => _activeAbilities;
        
        private Character _character;
        public Character Character => _character;
    
        private Animator _animator;
        
        public void UseAbility(int index, Character target)
        {
            var abilityContext = _activeAbilities[index];
            
            abilityContext.ability.Use(_character, target);
            
            if (abilityContext.animation != null)
                _animator.Play(abilityContext.animation.name);
            
            if (abilityContext.particle != null)
                PlayAbilityParticle(abilityContext.particle);
        }

        private void PlayAbilityParticle(ParticleSystem particle)
        {
            var newParticle = Instantiate(particle, transform);
            newParticle.transform.localPosition = Vector3.zero;
            newParticle.Play();
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = new Character(new Health(100), new Stats(), new Mana(100, 10));
            _activeAbilities.AddRange(playerAbilities.animatedAbilities);
        }
    }
}
