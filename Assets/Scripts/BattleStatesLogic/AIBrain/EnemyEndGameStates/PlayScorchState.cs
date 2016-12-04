using Assets.Scripts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayScorchState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayScorchState(EnemyEndGameProgress endGameState)
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
            if (endGameState.stateMachine.computerHand.IsAnyScorchUnitInHand())
            {
                ScorchUnit scorch = endGameState.stateMachine.computerHand.DecideWhichScorchToPlay(strongCards);
                if (scorch != null)
                {
                    Debug.Log("Gonna play scorch unit!");
                    EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, scorch);
                    endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                    ChangeState(endGameState.checkOpponentState);
                }
                else
                {
                    ChangeState(endGameState.playWeatherState);
                }
            }
            else
            {
                //Play weather state
                ChangeState(endGameState.playWeatherState);
            }          
        }
    }
}
