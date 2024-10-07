using System;
using System.Collections.Generic;
using Battle.Core;
using Battle.DataHolders;
using Battle.UI;
using UnityEngine;

namespace Battle.Player
{
    /// <summary>
    ///     Player controller for battle scene.
    ///     Connects UI input to character actions.
    ///     Used by battle manager to control battle flow.
    /// </summary>
    [RequireComponent(typeof(BattlePlayer))]
    public class PlayerBattleController : MonoBehaviour, IBattleController
    {
        private bool _canCastAbility = true;
        private Character _enemyCharacter;

        private IHealthDisplay _healthDisplay;
        private IManaDisplay _manaDisplay;

        private BattlePlayer _player;
        private UIInputManager _uiManager;

        public IReadOnlyList<Ability> Abilities => _player.Abilities;

        private void Awake()
        {
            _player = GetComponent<BattlePlayer>();
            _uiManager = FindObjectOfType<UIInputManager>();
            _healthDisplay = GetComponentInChildren<IHealthDisplay>();
            _manaDisplay = GetComponentInChildren<IManaDisplay>();
        }

        private void OnDisable()
        {
            _player.Character.Health.OnDeath -= OnPlayerDeath;
        }

        public event Action OnTurnEnd;
        public Character ControlledCharacter => _player.Character;

        public void StartBattle(Character enemyCharacter)
        {
            _uiManager.SetUpUI(this);
            _uiManager.TurnOffUI();

            _healthDisplay.SetUp(ControlledCharacter.Health);
            _player.Character.Health.OnDeath += OnPlayerDeath;

            _manaDisplay.SetUp(ControlledCharacter.Mana);

            _enemyCharacter = enemyCharacter;
        }

        public void StartTurn()
        {
            // TODO turn UI on
            _uiManager.OnEndTurnButtonPressed += EndTurn;
            _uiManager.OnSkillButtonPressed += UseAbility;

            _uiManager.TurnOnUI();
        }

        public void FullCirclePassed()
        {
            _player.Character.RegenMana();
        }

        private void EndTurn()
        {
            // TODO turn UI off
            _uiManager.OnEndTurnButtonPressed -= EndTurn;
            _uiManager.OnSkillButtonPressed -= UseAbility;

            _uiManager.TurnOffUI();
            OnTurnEnd?.Invoke();
        }

        private void UseAbility(int index)
        {
            if (!_canCastAbility) return;

            var ability = _player.AbilityContexts[index];
            var abilityCost = ability.manaCost;

            if (!_player.Character.Mana.CanSpend(abilityCost)) return;

            _player.Character.SpendMana(abilityCost);

            _player.UseAbility(index, _enemyCharacter);
            _canCastAbility = false;
            _uiManager.TurnOffUI();
        }

        public void AbilityAnimationFinished()
        {
            _canCastAbility = true;
            _uiManager.TurnOnUI();
        }

        private void OnPlayerDeath()
        {
            _player.Character.Health.OnDeath -= OnPlayerDeath;
            _uiManager.TurnOffUI();
        }
    }
}