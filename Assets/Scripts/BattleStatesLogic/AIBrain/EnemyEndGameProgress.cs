using Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain
{
    public class EnemyEndGameProgress : IGameProgress
    {
        public readonly EnemyLogicStateMachine stateMachine;

        public IGameStates currentState;
        public PlaySpyState spyState;
        public OpponentPassedState checkOpponentState;
        public PlayMedicState medicState;
        public PlayWeakestCardState weakestCardState;
        public CheckPlayerPointsState playerPointsState;
        public PlayLowestCardState playLowestCardState;
        public PassTurnState passTurnState;
        public PlayHeroState playHeroState;
        public IsStrongUnitOnBoardState isStronUnitOnBoard;
        public PlayScorchState playScorchState;
        public PlayWeatherEffectState playWeatherState;
        public PlayMusterState playMusterState;
        public PlayTightBondState playTightBondState;
        public PlayClearSkiesState playClearSkiesState;

        public EnemyEndGameProgress(EnemyLogicStateMachine stateMachine)
        {
            spyState = new PlaySpyState(this);
            checkOpponentState = new OpponentPassedState(this);
            medicState = new PlayMedicState(this);
            weakestCardState = new PlayWeakestCardState(this);
            playerPointsState = new CheckPlayerPointsState(this);
            playLowestCardState = new PlayLowestCardState(this);
            passTurnState = new PassTurnState(this);
            playHeroState = new PlayHeroState(this);
            isStronUnitOnBoard = new IsStrongUnitOnBoardState(this);
            playScorchState = new PlayScorchState(this);
            playWeatherState = new PlayWeatherEffectState(this);
            playMusterState = new PlayMusterState(this);
            playTightBondState = new PlayTightBondState(this);
            playClearSkiesState = new PlayClearSkiesState(this);

            this.stateMachine = stateMachine;
            currentState = checkOpponentState;
        }

        public void DecideTurn()
        {
            currentState.PlayTurn();
        }
    }
}
