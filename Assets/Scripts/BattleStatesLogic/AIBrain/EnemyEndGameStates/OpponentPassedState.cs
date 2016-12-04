using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public class OpponentPassedState : IGameStates
    {
        private EnemyEndGameProgress endGameState;

        public OpponentPassedState(EnemyEndGameProgress endGameState)
        {
            this.endGameState = endGameState;
        }

        public void ChangeState(IGameStates newState)
        {
            endGameState.currentState = newState;
        }  

        public void PlayTurn()
        {
            if (PlayerTurn.IsTurnPassed)
            {
                ChangeState(endGameState.playHeroState);
            }
            else
            {
                ChangeState(endGameState.spyState);
            }
        }
    }
}
