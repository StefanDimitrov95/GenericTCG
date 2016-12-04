using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain.EnemyEndGameStates
{
    public interface IGameStates
    {
        void PlayTurn();

        void ChangeState(IGameStates newState);
    }
}
