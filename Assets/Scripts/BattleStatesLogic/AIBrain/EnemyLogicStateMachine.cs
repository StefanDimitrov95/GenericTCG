using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BattleStatesLogic.AIBrain
{
    public class EnemyLogicStateMachine : MonoBehaviour
    {
        public Board board;
        public TurnBasedStateMachine turnState;
        public DiscardPile discardPile;

        public PlayerHand playerHand;
        public EnemyHand computerHand;
        public GameObject enemyHand;
        public IGameProgress gameProgress;
        public EnemyStartGameProgress startGame;
        public EnemyEndGameProgress endGame;

        void Awake()
        {
            startGame = new EnemyStartGameProgress(this);
            endGame = new EnemyEndGameProgress(this);  
        }

        void Start()
        {
            board = GameObject.Find("Board").GetComponent<Board>();
            playerHand = GameObject.Find("Hand").GetComponent<PlayerHand>();
            enemyHand = GameObject.Find("EnemyDeck");
            computerHand = GameObject.Find("EnemyDeck").GetComponent<EnemyHand>();
            turnState = GameObject.Find("TurnBasedMachine").GetComponent<TurnBasedStateMachine>();
            discardPile = GameObject.Find("EnemyDiscardPile").GetComponent<DiscardPile>();
            gameProgress = startGame;
        }

        void Update()
        {
            Debug.Log("Enemy Is thinking");
            if (turnState.currentState != Classes.EnumClasses.BattleState.EnemyTurn || EnemyTurn.IsTurnPassed)
            {
                return;
            }
            if (computerHand.CardsInHand.Count == 0)
            {
                Debug.Log("Computer hand is empty");
                EnemyTurn.PassTurn();
                return;
            }
            gameProgress.DecideTurn();         
        }       
    }
}
