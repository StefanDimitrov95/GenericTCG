using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class CheckPlayerPointsState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public CheckPlayerPointsState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.board.GetPlayerTotalAttack() + 8 > endGameState.stateMachine.board.GetEnemyTotalAttack())
            {
                ChangeState(endGameState.passTurnState);
                Debug.Log("MAY BE I SHOUD PASS?!!!");
            }
            else
            {     
                ChangeState(endGameState.playLowestCardState);              
            }
        }
    }
}
