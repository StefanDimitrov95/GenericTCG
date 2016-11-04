using UnityEngine;
using Assets.Scripts.Classes.EnumClasses;

namespace Assets.Scripts.BattleStatesLogic
{
    public static class EnemyTurn 
    {
      
        public static void Logic(ref GameObject enemyHand, ref BattleState currentState)
        {         
            PlaceCardOnBoard(enemyHand);
            currentState = BattleState.PlayerTurn;
        }

        public static void PlaceCardOnBoard(GameObject enemyHand)
        {
            try
            {
                enemyHand.GetComponent<EnemyHand>().PlayCard();               
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }
    }
}