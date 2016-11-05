using Assets.Scripts.BattleStatesLogic;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts
{
    class TurnBasedStateMachine : MonoBehaviour
    {
        private BattleState currentState;
        private GameObject playerHand;
        private GameObject enemyHand;
        private float timer = 4;

        public void Start()
        {
            playerHand = GameObject.Find("Hand");
            enemyHand = GameObject.Find("EnemyDeck");
            currentState = BattleState.Start;
        }

        public void Update()
        {
            Debug.Log(currentState);
            switch (currentState)
            {
                case BattleState.Start:
                    {
                        //Initialize enemy player deck, name, etc
                        //Decide who goes first
                        currentState = DecideWhoGoesFirst();
                        break;
                    }
                case BattleState.PlayerTurn:
                    {
                        //player plays a card  
                        timer = 0;                      
                        PlayerTurn.Logic(ref playerHand, ref currentState);
                        break;
                    }
                case BattleState.EnemyTurn:
                    {
                        //enemy turn logic
                        timer -= Time.deltaTime;

                        if (timer <= 2)
                        {
                            timer = 0;
                            EnemyTurn.Logic(ref enemyHand, ref currentState);
                        }
                        
                        break;
                    }
                case BattleState.CalculateTurn:
                    {
                        //Check if round is over
                        //If round is over check if player has won or lost
                        break;
                    }
                case BattleState.Win:
                    {
                        break;
                    }
                case BattleState.Lose:
                    {
                        break;
                    }
                default:
                    break;
            }
        }

        private static BattleState DecideWhoGoesFirst()
        {
            System.Random rnd = new System.Random();
            return rnd.NextDouble() >= 0.5 ? BattleState.PlayerTurn : BattleState.EnemyTurn;
        }
    }
}
