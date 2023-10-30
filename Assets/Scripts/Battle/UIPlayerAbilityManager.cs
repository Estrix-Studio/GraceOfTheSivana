using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerAbilityManager : MonoBehaviour
{
    public event Action OnFleeButtonPressed;
    public event Action OnReappearButtonPressed;
    public event Action<int> OnSkillButtonPressed;
    
    [SerializeField] private Button endTurnButton;
    [SerializeField] private Button fleeButton;
    [SerializeField] private Button reappearButton;
    
    [SerializeField] private Button[] skillButtons;

    private PlayerBattleController _playerBattleController;
    
    public void SetUpUI(PlayerBattleController battleController)
    {
        _playerBattleController = battleController;
        var abilities = battleController.Abilities;
        var index = 0;
        foreach (var ability in abilities)
        {
            var abilityIndex = index;
            var button = skillButtons[index];
            button.onClick.AddListener(() => OnSkillButtonPressed?.Invoke(abilityIndex));
            var text = button.GetComponentInChildren<TMP_Text>();
            text.text = ability.Name;
            index++;
        }
        
        endTurnButton.onClick.AddListener(() => _playerBattleController.EndTurn());
        fleeButton.onClick.AddListener(() => OnFleeButtonPressed?.Invoke());
        reappearButton.onClick.AddListener(() => OnReappearButtonPressed?.Invoke());
        
    }
    
    public void TurnOnUI()
    {
    }
    
    public void TurnOffUI()
    {
        
    }
    
    private void Start()
    {
        TurnOffUI();
    }
    
}
