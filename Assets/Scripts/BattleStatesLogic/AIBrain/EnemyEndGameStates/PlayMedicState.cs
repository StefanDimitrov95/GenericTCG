using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayMedicState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayMedicState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyMedicInHand()
                && (endGameState.stateMachine.discardPile.IsContainingSpy()
                || endGameState.stateMachine.discardPile.IsContainingStrongUnit()))
            {
                EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetMedicUnit());
                endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
            }
            else
            {
                //Play Weakest Card State
                ChangeState(endGameState.weakestCardState);
            }
        }
    }
}
