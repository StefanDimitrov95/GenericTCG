using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class PlayHeroState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public PlayHeroState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }

        public void PlayTurn()
        {
            if (endGameState.stateMachine.computerHand.IsAnyHeroUnitInHand())
            {
                EnemyTurn.PlaceCardOnBoard(endGameState.stateMachine.enemyHand, endGameState.stateMachine.computerHand.GetHeroCard());
                endGameState.stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
                Debug.Log("IMMA GONNA PLAY HERO CARD!!!");
                ChangeState(endGameState.checkOpponentState);
            }
            else
            {
                //Check for imba card 
                ChangeState(endGameState.isStronUnitOnBoard);
            }
        }
    }
}
