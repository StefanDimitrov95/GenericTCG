using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayClearSkiesState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayClearSkiesState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyClearSkiesInHand())
            {
                EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetClearSkiesCard());
                endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                ChangeState(endGameState.checkOpponentState);
            }
            else
            {
                ChangeState(endGameState.weakestCardState);
            }
        }
    }
}
