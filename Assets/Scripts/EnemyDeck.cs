using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class EnemyDeck : MonoBehaviour
    {

        const int drawAmount = 20;

        public List<Card> Deck;
        private CardDatabase CardDatabase;

        void Start()
        {
            CardDatabase = GameObject.Find("CardDatabase").GetComponent<CardDatabase>();
            ShuffleDeck(CardDatabase.EnemyDatabase);
            Deck = CardDatabase.EnemyDatabase.Take(drawAmount).ToList();
        }


        public void UpdateDeckLabel()
        {
            this.GetComponent<Text>().text = Deck.Count.ToString();
        }

        private void ShuffleDeck(List<Card> toShuffle)
        {
            System.Random rnd = new System.Random(DateTime.Now.Millisecond);
            int count = toShuffle.Count;
            while (count > 1)
            {
                int randomDraw = rnd.Next(0, count);
                Card tmp = toShuffle[randomDraw];
                toShuffle[randomDraw] = toShuffle[count - 1];
                toShuffle[count - 1] = tmp;
                count--;
            }
        }
    }
}
