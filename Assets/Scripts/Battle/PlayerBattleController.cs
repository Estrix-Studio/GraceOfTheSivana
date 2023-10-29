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
    
    private void Start()
    {
        _uiManager.SetUpUI(this);
        StartTurn();
    }
    
    public void StartBattle()
    {
        // TODO setup UI here
        
    }

    public void StartTurn()
    {
        // TODO turn UI on
        _uiManager.OnSkillButtonPressed += UseAbility;
        _uiManager.OnFleeButtonPressed += Flee;
        _uiManager.OnReappearButtonPressed += Reappear;
    }
    
    public void EndTurn()
    {
        // TODO turn UI off
        _uiManager.OnSkillButtonPressed -= UseAbility;
        _uiManager.OnFleeButtonPressed -= Flee;
        _uiManager.OnReappearButtonPressed -= Reappear;
        
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
