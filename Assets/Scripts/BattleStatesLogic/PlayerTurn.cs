using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic
{
    public static class PlayerTurn
    {
        private static bool isTurnPassed = false;
        public static bool IsTurnPassed { get { return isTurnPassed; } }

        public static void Logic(ref GameObject playerHand, ref BattleState currentState)
        {
            if (isTurnPassed)
            {
                EndTurn(ref playerHand, ref currentState);
                if (EnemyTurn.IsTurnPassed)
                {
                    currentState = BattleState.CalculateTurn;
                }
                return;
            }
            EnableDraggableComponent(playerHand);
            HandlePassedTurn(ref playerHand, ref currentState);
            if (playerHand.GetComponent<PlayerHand>().cardPlayed)
            {
                EndTurn(ref playerHand, ref currentState);
            }
        }
        
        public static void ResetTurn()
        {
            isTurnPassed = false;
            GameObject.Find("Board").GetComponent<Board>().DeactivatePlayerPassPanel();
        }

        private static void EndTurn(ref GameObject playerHand, ref BattleState currentState)
        {
            DisableDraggableComponent(playerHand);
            currentState = BattleState.EnemyTurn;
            playerHand.GetComponent<PlayerHand>().cardPlayed = false;
        }

        private static void EnableDraggableComponent(GameObject playerHand)
        {
            playerHand.GetComponent<PlayerHand>().EnableDraggableComponent();
        }

        private static void DisableDraggableComponent(GameObject playerHand)
        {
            playerHand.GetComponent<PlayerHand>().DisableDraggableComponent();
        }

        private static void HandlePassedTurn(ref GameObject playerHand, ref BattleState currentState)
        {
            if (IsPlayerPassingTurn())
            {
                isTurnPassed = true;
                ShowPassTurnPanel();
                GameObject.Find("Board").GetComponent<Board>().playerTurnsLeft--;
                GameObject.Find("Board").GetComponent<Board>().UpdatePlayerTurnsText();
                ReturnCardsInHand(ref playerHand);
                DisableDraggableComponent(playerHand);
                currentState = BattleState.EnemyTurn;
            }
        }

        private static void ShowPassTurnPanel()
        {
            isTurnPassed = true;
            GameObject.Find("Board").GetComponent<Board>().ActivatePlayerPassPanel();
        }

        private static void ReturnCardsInHand(ref GameObject playerHand)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("MB down");
                playerHand.GetComponent<PlayerHand>().ReturnCardInHand();
            }
        }

        private static bool IsPlayerPassingTurn()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("X is pressed");
                return true;
            }
            return false;
        }
    }
}
