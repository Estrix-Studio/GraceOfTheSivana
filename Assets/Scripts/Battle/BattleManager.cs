using System;
using System.Collections;
using UnityEngine;

public enum EBattleState
{
    PreBattle,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleManager : MonoBehaviour
{
    [SerializeField] private float preBattleDelay = 2f;
    
    
    private IBattleController _playerController;
    private IBattleController _enemyController;
    
    private EBattleState _state;
    private bool _isPlayerTurnFirst;
    
    private IBattleController _currentController;
    
    private void Start()
    {
        _isPlayerTurnFirst = true;
        _state = EBattleState.PreBattle;
        ContinueBattle();
    }
    
    private IEnumerator PreBattle()
    {
        _playerController.StartBattle();
        _enemyController.StartBattle();
        yield return new WaitForSeconds(preBattleDelay);
        _state = _isPlayerTurnFirst ? EBattleState.PlayerTurn : EBattleState.EnemyTurn;
    }

    private void ContinueBattle()
    {
        if (_currentController!= null)
        {
            _currentController.OnTurnEnd -= ContinueBattle;
            _currentController = null;
        }
        
        switch (_state)
        {
            case EBattleState.PreBattle:
                StartCoroutine(PreBattle());
                break;
            case EBattleState.PlayerTurn:
                StartTurn(_playerController);
                break;
            case EBattleState.EnemyTurn:
                StartTurn(_enemyController);
                break;
            case EBattleState.Won:
                // TODO show win screen
                break;
            case EBattleState.Lost:
                // TODO show lose screen
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void StartTurn(IBattleController controller)
    {
        controller.OnTurnEnd += ContinueBattle;
        controller.StartTurn();
    }
}
