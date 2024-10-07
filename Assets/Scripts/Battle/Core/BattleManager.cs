using System;
using System.Collections;
using Adventure;
using Battle.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle.Core
{
    public enum EBattleState
    {
        PreBattle,
        PlayerTurn,
        EnemyTurn,
        FullCircle,
        Won,
        Lost
    }

    /// <summary>
    ///     Handles battle game flow.
    ///     Scene should have only one battle manager.
    ///     Scene should have two characters with IBattleController tagged with appropriate tags.
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private float preBattleDelay = 2f;
        [SerializeField] private bool _isPlayerTurnFirst;

        private IBattleController _currentController;
        private IBattleController _enemyController;

        private EBattleState _nextState;


        private IBattleController _playerController;

        private void Start()
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerBattleController>();
            _enemyController = GameObject.FindWithTag("Enemy").GetComponent<IBattleController>();
            _nextState = EBattleState.PreBattle;
            ContinueBattle();
        }

        private IEnumerator PreBattle()
        {
            _playerController.ControlledCharacter.Health.OnDeath += () => _nextState = EBattleState.Lost;
            _enemyController.ControlledCharacter.Health.OnDeath += () => _nextState = EBattleState.Won;

            _playerController.StartBattle(_enemyController.ControlledCharacter);
            _enemyController.StartBattle(_playerController.ControlledCharacter);

            yield return new WaitForSeconds(preBattleDelay);

            // if player turn first: player -> enemy -> fullCircle -> player...
            // if enemy turn first: enemy -> player -> fullCircle -> enemy...
            _nextState = _isPlayerTurnFirst ? EBattleState.PlayerTurn : EBattleState.EnemyTurn;
            ContinueBattle();
        }

        private void ContinueBattle()
        {
            if (_currentController != null)
            {
                _currentController.OnTurnEnd -= ContinueBattle;
                _currentController = null;
            }

            switch (_nextState)
            {
                case EBattleState.PreBattle:
                    StartCoroutine(PreBattle());
                    break;
                case EBattleState.PlayerTurn:
                    // if player turn first: *player* -> enemy -> fullCircle.
                    // if enemy turn first: enemy -> *player* -> fullCircle. 
                    _nextState = _isPlayerTurnFirst ? EBattleState.EnemyTurn : EBattleState.FullCircle;
                    StartTurn(_playerController);
                    break;
                case EBattleState.EnemyTurn:
                    // if player turn first: player -> *enemy* -> fullCircle
                    // if enemy turn first: *enemy* -> player -> fullCircle
                    _nextState = _isPlayerTurnFirst ? EBattleState.FullCircle : EBattleState.PlayerTurn;
                    StartTurn(_enemyController);
                    break;
                case EBattleState.Won:
                    PlayerWin();
                    break;
                case EBattleState.Lost:
                    PlayerLose();
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
            _currentController = controller;
            _currentController.OnTurnEnd += ContinueBattle;
            _currentController.StartTurn();
        }

        private void FullCirclePassed()
        {
            _nextState = _isPlayerTurnFirst ? EBattleState.PlayerTurn : EBattleState.EnemyTurn;

            _playerController.FullCirclePassed();
            _enemyController.FullCirclePassed();

            ContinueBattle();
        }

        private void PlayerWin()
        {
            StaticContext.DoLoad = true;
            SceneManager.LoadScene("SampleScene");
        }

        private void PlayerLose()
        {
            SceneManager.LoadScene("EncounterScene");
        }
    }
}