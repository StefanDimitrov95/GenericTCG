using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayTightBondState : IGameStates
    {
        private EnemyEndGameProgress endGameState;
        private string lastPlayedTightBond;

        public PlayTightBondState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
            lastPlayedTightBond = string.Empty;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyTightBondInHand())
            {

                if (endGameState.stateMachine.computerHand.AreTighBondCardsSame())
                {
                    TightBondUnit tightBondUnit = endGameState.stateMachine.computerHand.GetTightBondUnit(lastPlayedTightBond);
                    if (!tightBondUnit.ToRow.currentRow.IsWeatherEffectOnRow())
                    {
                        Debug.Log("Imma Gonna play TightBond!");
                        lastPlayedTightBond = tightBondUnit.Title;
                        EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, tightBondUnit);
                        endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                        ChangeState(endGameState.playTightBondState);
                    }
                    else
                    {
                        //PlayClearWeather
                        ChangeState(endGameState.playClearSkiesState);
                    }
                }
                else
                {
                    //PlayLowestCardState
                    ChangeState(endGameState.playLowestCardState);
                }
            }
            else
            {
                //PlayLowestCardState
                ChangeState(endGameState.playLowestCardState);
            }
        }
    }
}
