using Assets.Scripts.BattleStatesLogic;
using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TurnBasedStateMachine : MonoBehaviour
    {
        public BattleState currentState;

        public GameObject statsPanel;

        private GameObject playerHand;
        private GameObject board;
        private int playerTotal = 0;
        private int enemyTotal = 0;
        private List<int> playerStats;
        private List<int> enemyStats;
        private BattleState finalState;

        public void Start()
        {
            playerStats = new List<int>();
            enemyStats = new List<int>();
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
                        playerTotal = board.GetComponent<Board>().GetPlayerTotalAttack();
                        enemyTotal = board.GetComponent<Board>().GetEnemyTotalAttack();

                        playerStats.Add(playerTotal);
                        enemyStats.Add(enemyTotal);


                        if (playerTotal > enemyTotal)
                        {
                            currentState = BattleState.Win;
                        }
                        else if (playerTotal < enemyTotal)
                        {
                            currentState = BattleState.Lose;
                        }
                        else
                        {
                            currentState = BattleState.Draw;
                        }

                        ResetTurn();
                        Debug.Log("Ending turn... Setting passedTurns to default " + PlayerTurn.IsTurnPassed + " " + EnemyTurn.IsTurnPassed);
                        board.GetComponent<Board>().ResetCardsOnBoard();
                        break;
                    }
                case BattleState.Win:
                    {
                        Debug.Log(" YOU HAVE WON !!!!!");
                        board.GetComponent<Board>().enemyTurnsLeft--;
                        board.GetComponent<Board>().UpdateEnemyTurnsText();
                        finalState = currentState;
                        currentState = GetNextState();
                        break;
                    }
                case BattleState.Lose:
                    {
                        Debug.Log(" YOU HAVE LOST !!!!!");
                        board.GetComponent<Board>().playerTurnsLeft--;
                        board.GetComponent<Board>().UpdatePlayerTurnsText();
                        finalState = currentState;
                        currentState = GetNextState();
                        break;
                    }
                case BattleState.Draw:
                    {
                        Debug.Log(" IT'S A DRAW !!!!!");
                        board.GetComponent<Board>().playerTurnsLeft--;
                        board.GetComponent<Board>().enemyTurnsLeft--;
                        board.GetComponent<Board>().UpdatePlayerTurnsText();
                        board.GetComponent<Board>().UpdateEnemyTurnsText();
                        finalState = currentState;
                        currentState = GetNextState();
                        break;
                    }
                case BattleState.End:
                    {
                        if (statsPanel.activeInHierarchy)
                        {
                            break;
                        }
                        statsPanel.SetActive(true);
                        SetStatsPanel(statsPanel);
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

        private BattleState GetNextState()
        {
            if (board.GetComponent<Board>().playerTurnsLeft == 0 || board.GetComponent<Board>().enemyTurnsLeft == 0)
            {
                return BattleState.End;
            }
            return BattleState.Start;
        }

        private void SetStatsPanel(GameObject panel)
        {
            Text outcome = panel.transform.GetChild(0).GetComponent<Text>();
            string finalResult = this.finalState.ToString();

            outcome.text = finalResult;
            outcome.color = outcome.text == "Win" ? Color.green : Color.red;

            for (int i = 0; i < playerStats.Count; i++)
            {
                panel.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().text = "Round " + (i + 1);
                panel.transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<Text>().text = playerStats[i].ToString();
                panel.transform.GetChild(1).GetChild(i).GetChild(2).GetComponent<Text>().text = enemyStats[i].ToString();

            }
        }
    }
}
