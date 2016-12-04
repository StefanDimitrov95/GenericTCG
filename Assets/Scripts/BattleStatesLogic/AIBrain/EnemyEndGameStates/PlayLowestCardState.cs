using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayLowestCardState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayLowestCardState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }
      
        public void PlayTurn()
        {
            //NEEDS IMPROVEMENT
            EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetLowestCard());
            endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
            Debug.Log("IMMA THROW RUBISH CARD!!!");
            ChangeState(endGameState.checkOpponentState);
        }
    }
}
