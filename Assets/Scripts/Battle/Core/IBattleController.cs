using System;
using Battle.DataHolders;

namespace Battle.Core
{
    /// <summary>
    ///     Connects battle manager and character.
    ///     OnTurnEnd should be called within controller work flow.
    /// </summary>
    public interface IBattleController
    {
        Character ControlledCharacter { get; }
        event Action OnTurnEnd;

        void StartBattle(Character enemyCharacter);

        void StartTurn();

        void FullCirclePassed();
    }
}