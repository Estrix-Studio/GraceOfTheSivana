using System;
using System.Collections;
using UnityEngine;

public enum EBattleState
{
    PreBattle,
    PlayerTurn,
    EnemyTurn,
    FullCircle,
    Won,
    Lost
}

public class BattleManager : MonoBehaviour
{
    [SerializeField] private float preBattleDelay = 2f;
    [SerializeField] private bool _isPlayerTurnFirst;
    
    
    private IBattleController _playerController;
    private IBattleController _enemyController;
    
    private EBattleState _state;
    
    private IBattleController _currentController;
    
    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerBattleController>();
        _enemyController = GameObject.FindWithTag("Enemy").GetComponent<IBattleController>();
        _state = EBattleState.PreBattle;
        ContinueBattle();
    }
    
    private IEnumerator PreBattle()
    {
        _playerController.ControlledCharacter.Health.OnDeath += () => _state = EBattleState.Lost;
        _enemyController.ControlledCharacter.Health.OnDeath += () => _state = EBattleState.Won;
        
        _playerController.StartBattle(_enemyController.ControlledCharacter);
        _enemyController.StartBattle(_playerController.ControlledCharacter);
        yield return new WaitForSeconds(preBattleDelay);
        _state = _isPlayerTurnFirst ? EBattleState.PlayerTurn : EBattleState.EnemyTurn;
        ContinueBattle();
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
                _state = _isPlayerTurnFirst ? EBattleState.EnemyTurn : EBattleState.FullCircle;
                StartTurn(_playerController);
                break;
            case EBattleState.EnemyTurn:
                _state = _isPlayerTurnFirst ? EBattleState.FullCircle : EBattleState.PlayerTurn;
                StartTurn(_enemyController);
                break;
            case EBattleState.Won:
                Debug.Log("You won!");
                break;
            case EBattleState.Lost:
                Debug.Log("You lost!");
                break;
            case EBattleState.FullCircle:
                FullCirclePassed();
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

    private void FullCirclePassed()
    {
        _state = _isPlayerTurnFirst ? EBattleState.PlayerTurn : EBattleState.EnemyTurn;
        ContinueBattle();
    }
}
