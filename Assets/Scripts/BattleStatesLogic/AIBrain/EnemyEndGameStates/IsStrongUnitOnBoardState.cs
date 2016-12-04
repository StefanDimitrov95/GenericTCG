using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class IsStrongUnitOnBoardState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public IsStrongUnitOnBoardState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            List<UnitCard> strongCards = this.endGameState.stateMachine.board.GetStrongestUnitsOnPlayerBoard();
            if (strongCards.Any())
            {
                Debug.Log("There is a beast on the board!!!! " + strongCards.Count);
                strongCards.ForEach(x => Debug.Log(x.ConstructCardData()));
                ChangeState(endGameState.playScorchState);
            }
            else
            {
                ChangeState(endGameState.playLowestCardState);
            }
        }
    }
}
