using UnityEngine;
using Assets.Scripts.Classes.EnumClasses;

namespace Assets.Scripts.BattleStatesLogic
{
    public static class EnemyTurn 
    {
        private static bool isTurnPassed = false;
        public static bool IsTurnPassed { get { return isTurnPassed; } }

        public static void Logic(ref GameObject enemyHand, ref BattleState currentState, Card cardToBePlayed)
        {
            PlaceCardOnBoard(enemyHand, cardToBePlayed);
            currentState = BattleState.PlayerTurn;
        }

        public static void PlaceCardOnBoard(GameObject enemyHand, Card cardToBePlayed)
        {
            try
            {
                if (cardToBePlayed == null)
                {
                    PassTurn();
                    Debug.Log("I AM STUCK SO ME GONNA PASS");
                    return;
                }
                enemyHand.GetComponent<EnemyHand>().PlayCard(cardToBePlayed);               
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }

        public static void PassTurn()
        {
            isTurnPassed = true;
            GameObject.Find("Board").GetComponent<Board>().ActivateComputerPassPanel();
           //GameObject.Find("Board").GetComponent<Board>().enemyTurnsLeft--;
            GameObject.Find("Board").GetComponent<Board>().UpdateEnemyTurnsText();
        }

        public static void ResetTurn()
        {
            isTurnPassed = false;
            GameObject.Find("Board").GetComponent<Board>().DeactivateComputerPassPanel();
        }
    }
}