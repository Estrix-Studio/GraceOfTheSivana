using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattlePlayer))]
public class PlayerBattleController : MonoBehaviour, IBattleController
{
    public event Action OnTurnEnd;
    
    private BattlePlayer _player;
    private UIPlayerAbilityManager _uiManager;
    
    public IReadOnlyList<Ability> Abilities => _player.Abilities;
    
    private void Awake()
    {
        _player = GetComponent<BattlePlayer>();
        _uiManager = FindObjectOfType<UIPlayerAbilityManager>();
    }
    
    public void StartBattle()
    {
        _uiManager.SetUpUI(this);        
    }

    public void StartTurn()
    {
        // TODO turn UI on
        _uiManager.OnEndTurnButtonPressed += EndTurn;
        _uiManager.OnAttackButtonPressed += () => UseAbility(0);
        _uiManager.OnDogeButtonPressed += () => UseAbility(1);
        _uiManager.OnFleeButtonPressed += Flee;
        _uiManager.OnReappearButtonPressed += Reappear; 
        _uiManager.OnSkillButtonPressed += UseAbility;
     
        _uiManager.TurnOnUI();
    }
    
    public void EndTurn()
    {
        // TODO turn UI off
        _uiManager.OnEndTurnButtonPressed -= EndTurn;
        _uiManager.OnAttackButtonPressed -= () => UseAbility(0);
        _uiManager.OnDogeButtonPressed -= () => UseAbility(1);
        _uiManager.OnFleeButtonPressed -= Flee;
        _uiManager.OnReappearButtonPressed -= Reappear;
        _uiManager.OnSkillButtonPressed -= UseAbility;
        
        _uiManager.TurnOffUI();
        OnTurnEnd?.Invoke();
    }
    
    public void UseAbility(int index)
    {
        _player.UseAbility(index, null);
    }

    public void Flee()
    {
        print("Fleeing");
    }
    
    public void Reappear()
    {
        Debug.Log("Reappearing");   
    }
}
