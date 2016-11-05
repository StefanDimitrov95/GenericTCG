using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class EnemyDeck : MonoBehaviour, IDeck
    {

        const int drawAmount = 20;

        public List<Card> Deck { get; set; }
        private CardDatabase CardDatabase;

        void Start()
        {
            CardDatabase = GameObject.Find("CardDatabase").GetComponent<CardDatabase>();
            Extensions.Shuffle(CardDatabase.EnemyDatabase);
            Deck = CardDatabase.EnemyDatabase.Take(drawAmount).ToList();
        }

        public void UpdateDeckLabel()
        {
            this.GetComponent<Text>().text = Deck.Count.ToString();
        }
    }
}
