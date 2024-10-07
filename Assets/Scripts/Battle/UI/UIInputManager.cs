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
        [SerializeField] private Button[] skillButtons;

        private readonly List<Button> _allButtons = new();

        private PlayerBattleController _playerBattleController;

        private List<Button> _skillButtons = new();

        private void Awake()
        {
            _allButtons.Add(endTurnButton);
            _allButtons.AddRange(skillButtons);
        }

        private void OnDisable()
        {
            endTurnButton.onClick.RemoveAllListeners();
            foreach (var button in skillButtons) button.onClick.RemoveAllListeners();
        }

        public event Action OnEndTurnButtonPressed;
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
            
            endTurnButton.onClick.AddListener(() => OnEndTurnButtonPressed?.Invoke());
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