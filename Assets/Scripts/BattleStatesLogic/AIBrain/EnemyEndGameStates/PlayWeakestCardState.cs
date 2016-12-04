using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayWeakestCardState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayWeakestCardState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyWeakNormalUnitInHand())
            {
                EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetWeakestNormalUnit());
                endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
            }
            else
            {
                //CheckPlayerPointsState
                ChangeState(endGameState.playerPointsState);
            }
        }
    }
}
