using System;
using System.Collections;
using Battle.Core;
using Battle.DataHolders;
using Battle.UI;
using UnityEngine;

namespace Battle.AI
{
    public class SimpleEnemyController : MonoBehaviour, IBattleController
    {
        [SerializeField] private float turnDelay = 1f;
        [SerializeField] private Ability dummyAbility;

        private IHealthDisplay _healthDisplay;
        private Character _player;

        private void Awake()
        {
            ControlledCharacter = new Character(new Health(100), new Stats(), new Mana(50, 5));
            _healthDisplay = GetComponentInChildren<IHealthDisplay>();
        }

        public event Action OnTurnEnd;

        public Character ControlledCharacter { get; private set; }

        public void StartBattle(Character enemyCharacter)
        {
            _player = enemyCharacter;
            _healthDisplay.SetUp(ControlledCharacter.Health);
        }

        public void StartTurn()
        {
            print("Enemy turn started");
            StartCoroutine(PassTurn());
        }

        public void FullCirclePassed()
        {
            ControlledCharacter.RegenMana();
        }

        private IEnumerator PassTurn()
        {
            yield return new WaitForSeconds(turnDelay);
            dummyAbility.Use(ControlledCharacter, _player);
            print("Enemy turn ended");
            OnTurnEnd?.Invoke();
        }
    }
}