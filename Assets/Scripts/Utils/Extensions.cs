using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
    static class Extensions
    {
        public static void Shuffle(List<Card> toShuffle)
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

        public static Card Find(IDeck deck, int id)
        {
            return deck.Deck.Single(c => c.ID == id);
        }

        public static void Instantiate(GameObject cardObj, Card cardToBeInstanciated, Transform parent)
        {
            cardObj.transform.SetParent(parent);
            cardObj.GetComponent<Image>().sprite = cardToBeInstanciated.Sprite;
            cardObj.name = string.Format("{0},{1}", cardToBeInstanciated.ID, cardToBeInstanciated.Title);
            if (parent == GameObject.Find("Hand"))
            {
                cardObj.GetComponent<Draggable>().currentCard = cardToBeInstanciated;
                cardObj.GetComponent<Draggable>().enabled = true;
            }
            cardObj.GetComponent<PointerHandler>().CurrentCard = cardToBeInstanciated;
        }
    }
}
