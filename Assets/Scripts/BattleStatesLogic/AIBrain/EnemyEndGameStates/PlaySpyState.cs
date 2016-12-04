using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlaySpyState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlaySpyState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnySpyInHand())//check if spy is in hand
            {
                EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetSpyUnit());
                endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
            }
            else
            {
                ChangeState(endGameState.medicState);
            }           
        }
    }
}
