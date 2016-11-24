using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain
{
    public class EnemyStartGameProgress : IGameProgress
    {
        private readonly EnemyLogicStateMachine stateMachine;

        public EnemyStartGameProgress(EnemyLogicStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void DecideTurn()
        {
            if (stateMachine.computerHand.IsAnySpyInHand())
            {
                // Debug.Log("Playing Spy");
                PlayTurn(stateMachine.enemyHand, stateMachine.computerHand.GetSpyUnit());
                return;
            }
            if (PlayerTurn.IsTurnPassed)
            {
                DecideBasedOnComputerTotal();
            }
            else
            {
                if (IsComputerHandBigger())
                {
                    //Play lowest value card State
                    PlayTurn(stateMachine.enemyHand, stateMachine.computerHand.GetLowestCard());
                }
                else
                {
                    DecideBasedOnComputerTotal();
                }
            }
        }

        private void DecideBasedOnComputerTotal()
        {
            if (IsComputerTotalBigger())
            {
                //PassTurn
                PassTurn();
            }
            else
            {
                TryToCatchUpPlayerTotal();
            }
        }

        private void TryToCatchUpPlayerTotal()
        {
            if (CanComputerCatchUp())
            {
                PlayTurn(stateMachine.enemyHand, stateMachine.computerHand.GetLowestCard());
            }
            else
            {
                //pass turn
                PassTurn();
            }
        }

        private void PassTurn()
        {
            EnemyTurn.PassTurn();
        }

        private void PlayTurn(GameObject enemyHand, Card cardToPlay)
        {
            EnemyTurn.PlaceCardOnBoard(enemyHand, cardToPlay);
            stateMachine.turnState.currentState = Classes.EnumClasses.BattleState.PlayerTurn;
        }

        private bool CanComputerCatchUp()
        {
            int lowestValue = stateMachine.computerHand.GetLowestCard().AttackValue;
            if (stateMachine.board.GetEnemyTotalAttack() + lowestValue >= stateMachine.board.GetPlayerTotalAttack())
            {
                return true;
            }
            return false;
        }

        private bool IsComputerHandBigger()
        {
            if (stateMachine.computerHand.CardsInHand.Count > stateMachine.playerHand.CardsInHand.Count)
            {
                return true;
            }
            return false;
        }

        private bool IsComputerTotalBigger()
        {
            if (stateMachine.board.GetEnemyTotalAttack() > stateMachine.board.GetPlayerTotalAttack())
            {
                return true;
            }
            return false;
        }
    }
}
