using System.Collections.Generic;
using Battle.Core;
using Battle.DataHolders;
using Data;
using UnityEngine;

namespace Battle.Player
{
    /// <summary>
    ///     Handles logic for player character in battle.\
    ///     sets up character and abilities.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BattlePlayer : MonoBehaviour
    {
        [SerializeField] private CharacterAbilities playerAbilities;

        private readonly List<AbilityContext> _activeAbilities = new();

        private Animator _animator;

        public IReadOnlyList<Ability> Abilities => _activeAbilities.ConvertAll(a => a.ability);

        public IReadOnlyList<AbilityContext> AbilityContexts => _activeAbilities;
        public Character Character { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            var stats = new PlayerStats();
            Character = new Character(new Health(100), stats, new Mana(stats.mana, stats.manaRegen));
            _activeAbilities.AddRange(playerAbilities.animatedAbilities);
        }

        public void UseAbility(int index, Character target)
        {
            var abilityContext = _activeAbilities[index];

            abilityContext.ability.Use(Character, target);

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
    }
}