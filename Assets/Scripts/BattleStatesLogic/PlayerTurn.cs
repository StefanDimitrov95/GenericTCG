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

        public static void Logic(ref GameObject playerHand, ref BattleState currentState)
        {
            EnableDraggableComponent(playerHand);
            HandlePassedTurn(ref playerHand, ref currentState);
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
                ReturnCardsInHand(ref playerHand);
                DisableDraggableComponent(playerHand);
                currentState = BattleState.EnemyTurn;
            }
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
