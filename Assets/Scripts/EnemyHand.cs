using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.Scripts.Interfaces;

public class EnemyHand : MonoBehaviour, IHand
{
    public List<Card> CardsInHand;

    private EnemyDeck EnemyDeck;
    private Text HandLabel;
    const int AmountOfCardsToDraw = 7;

    void Start()
    {
        EnemyDeck = GetComponent<EnemyDeck>();
        CardsInHand = EnemyDeck.Deck.Take(AmountOfCardsToDraw).ToList();
        EnemyDeck.Deck.RemoveAll(x => CardsInHand.Any(y => y.ID == x.ID));
        UpdateHandLabel();
        EnemyDeck.UpdateDeckLabel();
        CardsInHand.ForEach(enemyCard => Debug.Log("!!!ENEMY!!! " + enemyCard));
    }

    public void DrawExtraCards(int amountOfCardsToDraw)
    {
        List<Card> cardsToDraw = new List<Card>();
        cardsToDraw = EnemyDeck.Deck.Take(amountOfCardsToDraw).ToList();
        CardsInHand.AddRange(cardsToDraw);
        EnemyDeck.Deck.RemoveAll(x => cardsToDraw.Any(y => y.ID == x.ID));
    }

    public void UpdateHandLabel()
    {
        HandLabel = GameObject.Find("EnemyHandLabel").GetComponent<Text>();
        HandLabel.text = CardsInHand.Count.ToString();
    }

    public void PlayCard()
    {
        Card cardToBePlayed = this.CardsInHand[0];
        if (cardToBePlayed is UnitCard)
        {
            InstantiateEnemyUnitCard((UnitCard)cardToBePlayed);
        }
        UpdateHandLabel();
        EnemyDeck.UpdateDeckLabel();
        GameObject.Find("Board").GetComponent<Board>().UpdateAttackLabels();
    }

    private void InstantiateEnemyUnitCard(UnitCard card)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        cardObj.GetComponent<Image>().sprite = card.Sprite;
        cardObj.transform.SetParent(card.MoveToRow());
        cardObj.name = String.Format("{0},{1}", card.ID, card.Title);
        cardObj.GetComponent<PointerHandler>().CurrentCard = card;
        CardsInHand.Remove(card);
    }
}