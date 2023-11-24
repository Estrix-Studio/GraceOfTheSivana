using System;
using System.Collections;
using Core;
using DataHolders;
using UnityEngine;

namespace AI
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
        
        private void Start()
        {
            _character = new Character(new Health(100), new Stats(), new Mana(50, 5));
            _animator = GetComponent<Animator>();
        }

        public void StartBattle(Character enemyCharacter)
        {
            _player = enemyCharacter;
        }

        public void StartTurn()
        {
            StartCoroutine(TurnDelay());
        }

        private IEnumerator TurnDelay()
        {
            print("enemy thinking...");
            yield return new WaitForSeconds(0.5f);
            // logic for choosing ability
            // if no mana for both -> skip turn (0)
            // if health is full -> attack  (1)
            // if can kill player -> attack (2)
            // if mana is not enough to heal -> attack (3)
            // if health is lese than 20% -> heal (4)
            // if health is not full, chance to heal is 20% else attack (5)
            // else attack
            
            // if no mana for both -> skip turn (0)
            if (_character.Mana.Current < healAbility.manaCost && 
                _character.Mana.Current < attackAbility.manaCost)
            {
                print("Enemy skipped turn");
                OnTurnEnd?.Invoke();
                yield break;
            }
            // if health is full -> attack  (1)
            if (_character.Health.IsFull)
            {
                UseAttack();
                print("Enemy attacked");
                OnTurnEnd?.Invoke();
                yield break;
            }
            // if can kill player -> attack (2)
            // damage is fixed for now TODO get data from stats
            var damage = 10;
            if (_player.Health.Current <= damage)
            {
                UseAttack();
                print("Enemy attacked");
                OnTurnEnd?.Invoke();
                yield break;
            }   
            
            // (3) need think about the condition
            
            // if health is lese than 20% -> heal (4)
            if (_character.Health.Current < _character.Health.Max * 0.2f)
            {
                UseHeal();
                print("Enemy healed");
                OnTurnEnd?.Invoke();
                yield break;
            }
            
            // if health is not full, chance to heal is 20% else attack (5)
            if (_character.Health.Current < _character.Health.Max && UnityEngine.Random.Range(0, 100) < 20)
            {
                UseHeal();
                print("Enemy healed");
                OnTurnEnd?.Invoke();
                yield break;
            }   
            
            // else attack
            UseAttack();
            print("Enemy attacked");
            OnTurnEnd?.Invoke();
            yield break;
        }
        
        public void FullCirclePassed()
        {
            _character.RegenMana();
        }
        
        private void UseAttack()
        {
            _character.SpendMana(attackAbility.manaCost);
            attackAbility.ability.Use(_character, _player);
            _animator.Play(attackAbility.animation.name);
        }
        
        private void UseHeal()
        {
            _character.SpendMana(healAbility.manaCost);
            healAbility.ability.Use(_character, _character);
            _animator.Play(healAbility.animation.name);
        }
    }
}