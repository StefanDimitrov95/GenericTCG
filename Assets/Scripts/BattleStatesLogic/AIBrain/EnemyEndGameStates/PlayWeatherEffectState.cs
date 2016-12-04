using Assets.Scripts.Classes.CardClasses.Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayWeatherEffectState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayWeatherEffectState(EnemyEndGameProgress endGameState)
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
            if (endGameState.stateMachine.computerHand.IsAnyWeatherInHand())
            {
                Weather weatherCard = endGameState.stateMachine.computerHand.DecideWhichWeatherToPlay(strongCards);
                if (weatherCard != null)
                {
                    Debug.Log("Gonna play weather unit!");
                    EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, weatherCard);
                    endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                    ChangeState(endGameState.checkOpponentState);
                }
                else
                {
                    ChangeState(endGameState.playMusterState);
                }
            }
            else
            {
                ChangeState(endGameState.playMusterState);
            }
       }
    }
}
