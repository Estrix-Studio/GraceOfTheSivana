using System;

public interface IBattleController
{
    event Action OnTurnEnd;
    
    void StartBattle();
    
    void StartTurn();
}
