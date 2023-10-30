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

    private IHealthDisplay _healthDisplay;
    
    private void Awake()
    {
        _character = new Character(new Health(100), new Stats(), new Mana(50, 5));
        _healthDisplay = GetComponentInChildren<IHealthDisplay>();
    }

    public void StartBattle(Character enemyCharacter)
    {
        _player = enemyCharacter;
        _healthDisplay.SetUp(_character.Health);
    }

    public void StartTurn()
    { 
        print("Enemy turn started");
        StartCoroutine(PassTurn());
    }

    public void FullCirclePassed()
    {
        _character.Mana.RegenTick();
    }

    private IEnumerator PassTurn()
    {
        yield return new WaitForSeconds(turnDelay);
        dummyAbility.Use(_character, _player);
        print("Enemy turn ended");
        OnTurnEnd?.Invoke();
    }
}