using System;

public interface IBattleController
{
    event Action OnTurnEnd;
    
    Character ControlledCharacter { get; }
    
    void StartBattle(Character enemyCharacter);
    
    void StartTurn();

    void FullCirclePassed();
}
