using System;
using System.Collections;
using Battle.Core;
using Battle.DataHolders;
using Battle.UI;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.AI
{
    public class BaseEnemyAiController : MonoBehaviour, IBattleController
    {
        [SerializeField] private AbilityContext healAbility;
        [SerializeField] private AbilityContext attackAbility;

        [SerializeField] private float timeBetweenMoves = 1.0f;

        private Animator _animator;

        private IHealthDisplay _healthDisplay;
        private IManaDisplay _manaDisplay;
        private Character _player;

        private bool _turnEnded;

        private void Awake()
        {
            ControlledCharacter = new Character(new Health(100), new Stats(), new Mana(50, 5));
            _animator = GetComponent<Animator>();
            _healthDisplay = GetComponentInChildren<IHealthDisplay>();
            _manaDisplay = GetComponentInChildren<IManaDisplay>();
        }

        public event Action OnTurnEnd;
        public Character ControlledCharacter { get; private set; }

        public void StartBattle(Character enemyCharacter)
        {
            _player = enemyCharacter;

            _healthDisplay.SetUp(ControlledCharacter.Health);
            _manaDisplay.SetUp(ControlledCharacter.Mana);
        }

        public void StartTurn()
        {
            StartCoroutine(TurnLoop());
        }

        public void FullCirclePassed()
        {
            ControlledCharacter.RegenMana();
        }

        private IEnumerator TurnLoop()
        {
            _turnEnded = false;
            var moves = 0;
            while (!_turnEnded)
            {
                print($"enemy thinking... + {moves}");
                yield return new WaitForSeconds(timeBetweenMoves);
                DoMove();
                moves++;
                if (!_turnEnded)
                {
                    // chance to do another move is 0.75 from previous for each next move
                    // 1 -> 0.75 -> -> 0.5625 -> 0.421
                    var chanceToDoAnotherMove = math.pow(0.75, moves);

                    // if random number is bigger than chance -> end turn
                    if (Random.Range(0f, 1f) > chanceToDoAnotherMove) _turnEnded = true;
                }
            }

            yield return new WaitForSeconds(timeBetweenMoves);
            OnTurnEnd?.Invoke();
        }

        private void DoMove()
        {
            // TODO add more skills and logic for choosing them
            // TODO Ideas: add mana regen, add defense

            // logic for choosing ability
            // if no mana for both -> skip turn (0)
            // if health is full -> attack  (1)
            // if can kill player -> attack (2)
            // if health is lese than 20% -> heal (4)
            // if health is not full, chance to heal is 20% else attack (5)
            // else attack

            var canHeal = ControlledCharacter.Mana.Current >= healAbility.manaCost;
            var canAttack = ControlledCharacter.Mana.Current >= attackAbility.manaCost;

            // if no mana for both -> skip turn (0)
            if (!canHeal && !canAttack)
            {
                _turnEnded = true;
                return;
            }

            // if health is full -> attack  (1)
            if (ControlledCharacter.Health.IsFull && canAttack)
            {
                UseAttack();
                return;
            }

            // if can kill player -> attack (2)
            // damage is fixed for now TODO get data from stats
            var damage = 10;
            if (_player.Health.Current <= damage && canAttack)
            {
                UseAttack();
                return;
            }

            // if health is lese than 20% -> heal (4)
            if (ControlledCharacter.Health.Current < ControlledCharacter.Health.Max * 0.2f && canHeal)
            {
                UseHeal();
                return;
            }

            // if health is not full, chance to heal is 25% else attack (5)
            if (ControlledCharacter.Health.Current < ControlledCharacter.Health.Max && canHeal &&
                Random.Range(0, 100) < 25)
            {
                UseHeal();
                return;
            }

            // else attack
            if (canAttack)
            {
                UseAttack();
                return;
            }

            // if here smth went wrong -> end Turn
            _turnEnded = true;
        }

        private void UseAttack()
        {
            print("Enemy attacked");
            ControlledCharacter.SpendMana(attackAbility.manaCost);
            attackAbility.ability.Use(ControlledCharacter, _player);

            if (attackAbility.animation != null)
                _animator.Play(attackAbility.animation.name);

            if (attackAbility.particle != null)
                PlayAbilityParticle(attackAbility.particle);
        }

        private void UseHeal()
        {
            print("Enemy healed");
            ControlledCharacter.SpendMana(healAbility.manaCost);
            healAbility.ability.Use(ControlledCharacter, ControlledCharacter);

            if (healAbility.animation != null)
                _animator.Play(healAbility.animation.name);

            if (healAbility.particle != null)
                PlayAbilityParticle(healAbility.particle);
        }

        private void PlayAbilityParticle(ParticleSystem particle)
        {
            var newParticle = Instantiate(particle, transform);
            newParticle.transform.localPosition = Vector3.zero;
            newParticle.Play();
        }
    }
}