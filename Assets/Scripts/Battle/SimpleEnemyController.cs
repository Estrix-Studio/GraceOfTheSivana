using System;
using System.Collections;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour, IBattleController
{
    [SerializeField] private float turnDelay = 1f;
    [SerializeField] private Ability dummyAbility;
    
    public event Action OnTurnEnd;
    
    private Character _character;
    private Character _player;
    
    public Character ControlledCharacter => _character;

    private void Awake()
    {
        _character = new Character(new Health(100), new Stats());
    }

    public void StartBattle(Character enemyCharacter)
    {
        _player = enemyCharacter;
    }

    public void StartTurn()
    { 
        print("Enemy turn started");
        StartCoroutine(PassTurn());
    }
    
    private IEnumerator PassTurn()
    {
        yield return new WaitForSeconds(turnDelay);
        dummyAbility.Use(_character, _player);
        print("Enemy turn ended");
        OnTurnEnd?.Invoke();
    }
}