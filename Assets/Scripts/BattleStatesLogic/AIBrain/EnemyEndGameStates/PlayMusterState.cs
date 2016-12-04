using Assets.Scripts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayMusterState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayMusterState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyMusterInHand())
            {
                MusterUnit musterCard = endGameState.stateMachine.computerHand.GetMusterUnit();
                if (!musterCard.ToRow.currentRow.IsWeatherEffectOnRow())
                {
                    Debug.Log("Gonna play Muster unit!");
                    EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, musterCard);
                    endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                    ChangeState(endGameState.checkOpponentState);
                }
                else
                {
                    //PlayClearSkyState
                    ChangeState(endGameState.playClearSkiesState);
                }
            }
            else
            {
                //Play TightBond State
                ChangeState(endGameState.playTightBondState);
            }
        }
    }
}
