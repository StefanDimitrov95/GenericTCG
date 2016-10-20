using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class DrawHand : MonoBehaviour
{

    const int drawAmount = 7;
    public List<Card> cardHand;
    public CardDatabase cardDeck;

    void Start()
    {
        cardDeck = GetComponent<CardDatabase>();
        cardDeck.database.Sort((x, y) => Random.value < 0.5f ? -1 : 1);
        cardHand = cardDeck.database.Take(drawAmount).ToList();
        cardDeck.database.RemoveAll(x => cardHand.Any(y => y.ID == x.ID));

        foreach (Card card in cardHand)
        {
            InstanciateCardToHand(card);
        }

        UpdateDeckLabel();
    }

    public void DrawExtraCards(int amountOfCardsToDraw)
    {
        cardDeck = GameObject.Find("Deck").GetComponent<CardDatabase>();
        List<Card> cardsToDraw = new List<Card>();
        cardsToDraw = cardDeck.database.Take(amountOfCardsToDraw).ToList();
        cardHand.AddRange(cardsToDraw);
        cardDeck.database.RemoveAll(x => cardsToDraw.Any(y => y.ID == x.ID));

        foreach (Card card in cardsToDraw)
        {
            InstanciateCardToHand(card);
        }

        UpdateDeckLabel();
    }

    private static void InstanciateCardToHand(Card cardToBeInstanciated)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        cardObj.transform.SetParent(GameObject.Find("Hand").transform);
        cardObj.GetComponent<Image>().sprite = cardToBeInstanciated.Sprite;
        cardObj.name = cardToBeInstanciated.Title;
        cardObj.GetComponent<Draggable>().currentCard = cardToBeInstanciated;
        cardObj.GetComponent<PointerHandler>().currentCard = cardToBeInstanciated;
    }

    private void UpdateDeckLabel()
    {
        this.GetComponent<Text>().text = cardDeck.database.Count.ToString();
    }
}
