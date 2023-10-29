using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerAbilityManager : MonoBehaviour
{
    public event Action OnFleeButtonPressed;
    public event Action OnReappearButtonPressed;
    public event Action<int> OnSkillButtonPressed;
    
    [SerializeField] private Button fleeButton;
    [SerializeField] private Button reappearButton;
    [Space]
    [SerializeField]private GameObject skillButtonPrefab;
    
    private Button[] skillButtons;

    private PlayerBattleController _playerBattleController;
    
    public void SetUpUI(PlayerBattleController battleController)
    {
        _playerBattleController = battleController;
        var abilities = battleController.Abilities;
        var index = 0;
        foreach (var ability in abilities)
        {
            var button = Instantiate(skillButtonPrefab, transform).GetComponent<Button>();
            button.transform.position += new Vector3((index+2) * 160, 0, 0);
            //TODO remove listener on destroy
            var abilityIndex = index;
            button.onClick.AddListener(() => OnSkillButtonPressed?.Invoke(abilityIndex));
            var text = button.GetComponentInChildren<TMP_Text>();
            text.text = ability.Name;
            index++;
        }
        
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
