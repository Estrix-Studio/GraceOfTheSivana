using System;
using System.Collections;
using Battle.Core;
using Battle.DataHolders;
using Battle.UI;
using Unity.Mathematics;
using UnityEngine;

namespace Battle.AI
{
    public class BaseEnemyAiController : MonoBehaviour, IBattleController
    {
        public event Action OnTurnEnd;
        public Character ControlledCharacter => _character;

        private Character _character;
        private Character _player;
        
        [SerializeField] private AbilityContext healAbility;
        [SerializeField] private AbilityContext attackAbility;

        private Animator _animator;
        
        private IHealthDisplay _healthDisplay;
        private IManaDisplay _manaDisplay;
        
        private bool _turnEnded;
        
        private void Awake()
        {
            _character = new Character(new Health(100), new Stats(), new Mana(50, 5));
            _animator = GetComponent<Animator>();
            _healthDisplay = GetComponentInChildren<IHealthDisplay>();
            _manaDisplay = GetComponentInChildren<IManaDisplay>();
        }

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

        private IEnumerator TurnLoop()
        {
            _turnEnded = false;
            var moves = 0;
            while(!_turnEnded)
            {
                print($"enemy thinking... + {moves}");
                yield return new WaitForSeconds(0.5f);
                DoMove();
                moves++;
                if (!_turnEnded)
                {
                    // chance to do another move is 0.75 from previous for each next move
                    // 1 -> 0.75 -> -> 0.5625 -> 0.421
                    var chanceToDoAnotherMove = math.pow(0.75, moves);

                    // if random number is bigger than chance -> end turn
                    if (UnityEngine.Random.Range(0f, 1f) > chanceToDoAnotherMove)
                    {
                        _turnEnded = true;
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
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

            var canHeal = _character.Mana.Current >= healAbility.manaCost;
            var canAttack = _character.Mana.Current >= attackAbility.manaCost;
            
            // if no mana for both -> skip turn (0)
            if (!canHeal && !canAttack)
            {
                _turnEnded = true;
                return;
            }
            // if health is full -> attack  (1)
            if (_character.Health.IsFull && canAttack)
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
            if (_character.Health.Current < _character.Health.Max * 0.2f && canHeal)
            {
                UseHeal();
                return;
            }
            
            // if health is not full, chance to heal is 25% else attack (5)
            if (_character.Health.Current < _character.Health.Max && canHeal && UnityEngine.Random.Range(0, 100) < 25)
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
        
        public void FullCirclePassed()
        {
            _character.RegenMana();
        }
        
        private void UseAttack()
        {
            print("Enemy attacked");
            _character.SpendMana(attackAbility.manaCost);
            attackAbility.ability.Use(_character, _player);
            _animator.Play(attackAbility.animation.name);
        }
        
        private void UseHeal()
        {
            print("Enemy healed");
            _character.SpendMana(healAbility.manaCost);
            healAbility.ability.Use(_character, _character);
            _animator.Play(healAbility.animation.name);
        }
    }
}