using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;

public class EnemyHand : MonoBehaviour
{
    public List<Card> CardsInHand;

    private PlayerDeck EnemyDeck;
    private Text HandLabel;
    const int AmountOfCardsToDraw = 7;

    void Start()
    {
        EnemyDeck = GetComponent<PlayerDeck>();
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
}