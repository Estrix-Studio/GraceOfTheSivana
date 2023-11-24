using System;
using DataHolders;

namespace Core
{
    /// <summary>
    /// Connects battle manager and character.
    /// OnTurnEnd should be called within controller work flow.
    /// </summary>
    public interface IBattleController
    {
        event Action OnTurnEnd;
    
        Character ControlledCharacter { get; }
    
        void StartBattle(Character enemyCharacter);
    
        void StartTurn();

        void FullCirclePassed();
    }
}
