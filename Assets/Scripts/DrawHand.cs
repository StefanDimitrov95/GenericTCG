using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class DrawHand : MonoBehaviour
{

    const int drawAmount = 7;
    public List<Card> cardHand = new List<Card>();
    public CardDatabase cardDeck;

    // Use this for initialization
    void Start()
    {
        cardDeck = GetComponent<CardDatabase>();
        cardDeck.database.Sort((x, y) => Random.value < 0.5f ? -1 : 1);
        cardHand = cardDeck.database.Take(drawAmount).ToList();
        foreach (Card card in cardHand)
        {
            //Debug.Log(card.ToString());
            GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
            cardObj.transform.SetParent(GameObject.Find("Hand").transform);
            cardObj.GetComponent<Image>().sprite = card.Sprite;
            cardObj.name = card.Title;
            cardObj.GetComponent<Draggable>().currentCard = card;
        }
    }

    //update is called once per frame
    void Update()
    {

    }


}
