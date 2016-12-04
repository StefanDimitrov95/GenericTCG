using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PassTurnState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PassTurnState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }
        public void PlayTurn()
        {
            if (endGameState.stateMachine.board.enemyTurnsLeft == 2)
            {
                EnemyTurn.PassTurn();
                Debug.Log("I HAVE 2 TURNS LEFT, IMMA GONNA PASS THIS!!!");
            }
            else
            {
                //Play Hero State
                ChangeState(endGameState.playHeroState);
            }
        }
    }
}
