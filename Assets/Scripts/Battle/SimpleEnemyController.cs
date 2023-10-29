using System;
using System.Collections;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour, IBattleController
{
    [SerializeField] private float turnDelay = 1f;
    
    public event Action OnTurnEnd;
    public void StartBattle()
    {
        // TODO setup here
    }

    public void StartTurn()
    { 
        print("Enemy turn started");
        StartCoroutine(PassTurn());
    }
    
    private IEnumerator PassTurn()
    {
        yield return new WaitForSeconds(turnDelay);
        OnTurnEnd?.Invoke();
    }
}