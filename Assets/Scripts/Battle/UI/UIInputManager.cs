using System;
using System.Collections.Generic;
using Battle.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    /// <summary>
    ///     Used to translate button events to code actions
    ///     relative to battle scene
    /// </summary>
    public class UIInputManager : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
        [SerializeField] private Button fleeButton;
        [SerializeField] private Button attackButton;
        [SerializeField] private Button dogeButton;
        [SerializeField] private Button reappearButton;
        [SerializeField] private Button[] skillButtons;

        private readonly List<Button> _allButtons = new();

        private PlayerBattleController _playerBattleController;

        private List<Button> _skillButtons = new();

        private void Awake()
        {
            _allButtons.Add(endTurnButton);
            _allButtons.Add(fleeButton);
            _allButtons.Add(reappearButton);
            _allButtons.Add(attackButton);
            _allButtons.Add(dogeButton);
            _allButtons.AddRange(skillButtons);
        }

        private void OnDisable()
        {
            endTurnButton.onClick.RemoveAllListeners();
            fleeButton.onClick.RemoveAllListeners();
            reappearButton.onClick.RemoveAllListeners();
            attackButton.onClick.RemoveAllListeners();
            dogeButton.onClick.RemoveAllListeners();
            foreach (var button in skillButtons) button.onClick.RemoveAllListeners();
        }

        public event Action OnEndTurnButtonPressed;
        public event Action OnFleeButtonPressed;
        public event Action OnAttackButtonPressed;
        public event Action OnDogeButtonPressed;
        public event Action OnReappearButtonPressed;
        public event Action<int> OnSkillButtonPressed;

        public void SetUpUI(PlayerBattleController battleController)
        {
            _playerBattleController = battleController;
            var abilities = battleController.Abilities;

            for (var index = 0; index < 4; index++)
            {
                var ability = abilities[index];
                var button = skillButtons[index];
                var indexToInvoke = index;
                button.onClick.AddListener(() => OnSkillButtonPressed?.Invoke(indexToInvoke));
                var text = button.GetComponentInChildren<TMP_Text>();
                text.text = ability.Name;
            }

            attackButton.onClick.AddListener(() => OnAttackButtonPressed?.Invoke());
            dogeButton.onClick.AddListener(() => OnDogeButtonPressed?.Invoke());
            endTurnButton.onClick.AddListener(() => OnEndTurnButtonPressed?.Invoke());
            fleeButton.onClick.AddListener(() => OnFleeButtonPressed?.Invoke());
            reappearButton.onClick.AddListener(() => OnReappearButtonPressed?.Invoke());
        }

        public void TurnOnUI()
        {
            foreach (var button in _allButtons) button.interactable = true;
        }

        public void TurnOffUI()
        {
            foreach (var button in _allButtons) button.interactable = false;
        }
    }
}