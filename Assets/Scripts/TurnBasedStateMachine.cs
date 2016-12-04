using Assets.Scripts.BattleStatesLogic;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts
{
    public class TurnBasedStateMachine : MonoBehaviour
    {
        public BattleState currentState;
        private GameObject playerHand;
        private GameObject board;

        public void Start()
        {
            playerHand = GameObject.Find("Hand");
            board = GameObject.Find("Board");
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
                        PlayerTurn.Logic(ref playerHand, ref currentState);
                        Debug.Log("Is turn passed: " + PlayerTurn.IsTurnPassed);
                        break;
                    }
                case BattleState.EnemyTurn:
                    {
                        if (EnemyTurn.IsTurnPassed)
                        {
                            currentState = BattleState.PlayerTurn;
                            if (PlayerTurn.IsTurnPassed)
                            {
                                currentState = BattleState.CalculateTurn;
                            }
                        }

                        break;
                    }
                case BattleState.CalculateTurn:
                    {
                        // only if both players have passed or have nothing to play
                        //Check if round is over
                        //If round is over check if player has won or lost
                        //reset passed condition
                        int playerTotal = board.GetComponent<Board>().GetPlayerTotalAttack();
                        int enemyTotal = board.GetComponent<Board>().GetEnemyTotalAttack();
                        if (playerTotal > enemyTotal)
                        {
                            currentState = BattleState.Win;
                        }
                        else
                        {
                            currentState = BattleState.Lose;
                        }
                        ResetTurn();
                        Debug.Log("Ending turn... Setting passedTurns to default " + PlayerTurn.IsTurnPassed + " " + EnemyTurn.IsTurnPassed);
                        board.GetComponent<Board>().ResetCardsOnBoard();
                        break;
                    }
                case BattleState.Win:
                    {
                        Debug.Log(" YOU HAVE WON !!!!!");
                        currentState = BattleState.Start;
                        board.GetComponent<Board>().enemyTurnsLeft--;
                        board.GetComponent<Board>().UpdateEnemyTurnsText();
                        break;
                    }
                case BattleState.Lose:
                    {
                        Debug.Log(" YOU HAVE LOST !!!!!");
                        board.GetComponent<Board>().playerTurnsLeft--;
                        board.GetComponent<Board>().UpdatePlayerTurnsText();
                        currentState = BattleState.Start;
                        break;
                    }
                default:
                    break;
            }
        }

        private void ResetTurn()
        {
            PlayerTurn.ResetTurn();
            EnemyTurn.ResetTurn();
        }

        private static BattleState DecideWhoGoesFirst()
        {
            System.Random rnd = new System.Random();
            return rnd.NextDouble() >= 0.5 ? BattleState.PlayerTurn : BattleState.EnemyTurn;
        }
    }
}
