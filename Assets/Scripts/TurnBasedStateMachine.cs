using Assets.Scripts.BattleStatesLogic;
using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class TurnBasedStateMachine : MonoBehaviour
    {
        private BattleState currentState;
        private GameObject playerHand;

        public void Start()
        {
            playerHand = GameObject.Find("Hand");
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
                        currentState = BattleState.PlayerTurn;
                        break;
                    }
                case BattleState.PlayerTurn:
                    {
                        //player plays a card                        
                        PlayerTurn.Logic(ref playerHand, ref currentState);
                        break;
                    }
                case BattleState.EnemyTurn:
                    {
                        //enemy turn logic
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
    }
}
