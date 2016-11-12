using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;

public class PlayerHand : MonoBehaviour, IHand
{
    public List<Card> CardsInHand { get; set; }
    public bool cardPlayed = false;

    private PlayerDeck PlayerDeck;
    private Text HandLabel;
    const int AmountOfCardsToDraw = 8;

    void Start()
    {
        PlayerDeck = GameObject.Find("Deck").GetComponent<PlayerDeck>();

        CardsInHand = PlayerDeck.Deck.Take(AmountOfCardsToDraw).ToList();
        PlayerDeck.Deck.RemoveAll(x => CardsInHand.Any(y => y.ID == x.ID));

        foreach (Card card in this.CardsInHand)
        {
            InstanciateCardToHand(card);
            Debug.Log(card);
        }

        UpdateHandLabel();
        PlayerDeck.UpdateDeckLabel();
    }

    public void DrawExtraCards(int amountOfCardsToDraw)
    {
        List<Card> cardsToDraw = new List<Card>();
        cardsToDraw = PlayerDeck.Deck.Take(amountOfCardsToDraw).ToList();
        CardsInHand.AddRange(cardsToDraw);
        PlayerDeck.Deck.RemoveAll(x => cardsToDraw.Any(y => y.ID == x.ID));

        foreach (Card card in cardsToDraw)
        {
            InstanciateCardToHand(card);
        }

        UpdateHandLabel();
        PlayerDeck.UpdateDeckLabel();
    }


    public static void InstanciateCardToHand(Card cardToBeInstanciated)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        Extensions.Instantiate(cardObj, cardToBeInstanciated, GameObject.Find("Hand").transform);
    }

    public void UpdateHandLabel()
    {
        HandLabel = GameObject.Find("HandLabel").GetComponent<Text>();
        HandLabel.text = CardsInHand.Count.ToString();
    }

    public void EnableDraggableComponent()
    {
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if (CardsInHand[i] != null)
            {
                Draggable cardPrefab = GameObject.Find(CardsInHand[i].ID + "," + CardsInHand[i].Title).GetComponent<Draggable>();
                cardPrefab.enabled = true;
            }
        }
    }

    public void DisableDraggableComponent()
    {
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if (CardsInHand[i] != null)
            {
                Draggable cardPrefab = GameObject.Find(CardsInHand[i].ID + "," + CardsInHand[i].Title).GetComponent<Draggable>();
                cardPrefab.enabled = false;
            }
        }
    }

    public void ReturnCardInHand()
    {
        foreach (var card in CardsInHand)
        {
            Draggable cardPrefab = GameObject.Find(card.ID + "," + card.Title).GetComponent<Draggable>();
            if (cardPrefab.isBeingDragged)
            {
                cardPrefab.ReturnCardToHand();
            }
        }
    }
}
