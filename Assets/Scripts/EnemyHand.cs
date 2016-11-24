using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.CardClasses.Magic;

public class EnemyHand : MonoBehaviour, IHand
{
    public List<Card> CardsInHand { get; set; }

    private List<ScorchUnit> scorchUnitCards;
    private List<SpyUnit> spyCards;
    private List<MoraleBoostUnit> moraleBoostCards;
    private List<TightBondUnit> tightBondCards;
    private List<MusterUnit> musterCards;
    private List<MedicUnit> medicCards;
    private List<NormalUnit> normalUnitCards;
    private List<Weather> weatherMagicCards;

    private EnemyDeck EnemyDeck;
    private Text HandLabel;
    const int AmountOfCardsToDraw = 5;

    void Start()
    {
        EnemyDeck = GetComponent<EnemyDeck>();
        CardsInHand = EnemyDeck.Deck.Take(AmountOfCardsToDraw).ToList();
        EnemyDeck.Deck.RemoveAll(x => CardsInHand.Any(y => y.ID == x.ID));
        UpdateHandLabel();
        EnemyDeck.UpdateDeckLabel();
        CardsInHand.ForEach(enemyCard => Debug.Log("!!!ENEMY!!! " + enemyCard));

        scorchUnitCards = new List<ScorchUnit>();
        spyCards = new List<SpyUnit>();
        moraleBoostCards = new List<MoraleBoostUnit>();
        tightBondCards = new List<TightBondUnit>();
        musterCards = new List<MusterUnit>();
        medicCards = new List<MedicUnit>();
        normalUnitCards = new List<NormalUnit>();
        weatherMagicCards = new List<Weather>();

        RefreshMemory();
    }

    public void DrawExtraCards(int amountOfCardsToDraw)
    {
        List<Card> cardsToDraw = new List<Card>();
        cardsToDraw = EnemyDeck.Deck.Take(amountOfCardsToDraw).ToList();
        cardsToDraw.ForEach(card => Debug.Log("!!!ENEMY DRAW!!! " + card));
        CardsInHand.AddRange(cardsToDraw);
        EnemyDeck.Deck.RemoveAll(x => cardsToDraw.Any(y => y.ID == x.ID));
    }

    public void UpdateHandLabel()
    {
        HandLabel = GameObject.Find("EnemyHandLabel").GetComponent<Text>();
        HandLabel.text = CardsInHand.Count.ToString();
    }

    public void PlayCard(Card cardToBePlayed)
    {
        //Card cardToBePlayed = this.CardsInHand[0];
        InstantiateEnemyCard(cardToBePlayed);
        UpdateHandLabel();
        EnemyDeck.UpdateDeckLabel();
        GameObject.Find("Board").GetComponent<Board>().UpdateAttackLabels();

        if (cardToBePlayed is MagicCard)
        {
            (cardToBePlayed as MagicCard).OnPlay();
        }

        Debug.Log("ENEMY PLAYED: " + cardToBePlayed.ToString());
    }

    private void InstantiateEnemyCard(Card card)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        Extensions.Instantiate(cardObj, card, card.PlayEnemyCard());
        CardsInHand.Remove(card);
        RefreshMemory();
    }

    private void RefreshMemory()
    {
        List<Card> cardsInHand = GameObject.Find("EnemyDeck").GetComponent<EnemyHand>().CardsInHand;

        scorchUnitCards = cardsInHand.OfType<ScorchUnit>().ToList();
        spyCards = cardsInHand.OfType<SpyUnit>().ToList();
        moraleBoostCards = cardsInHand.OfType<MoraleBoostUnit>().ToList();
        tightBondCards = cardsInHand.OfType<TightBondUnit>().ToList();
        musterCards = cardsInHand.OfType<MusterUnit>().ToList();
        medicCards = cardsInHand.OfType<MedicUnit>().ToList();
        normalUnitCards = cardsInHand.OfType<NormalUnit>().ToList();
        weatherMagicCards = cardsInHand.OfType<Weather>().ToList();
    }

    public UnitCard GetLowestCard()
    {
        UnitCard lowestCard = null;
        int lowestValue = int.MaxValue;
        foreach (UnitCard card in CardsInHand)
        {
            if (card.AttackValue < lowestValue)
            {
                lowestValue = card.AttackValue;
                lowestCard = card;
            }
        }
        return lowestCard;
    }

    public bool IsAnySpyInHand()
    {
        return spyCards.Any();
    }

    public bool IsAnyMedicInHand()
    {
        return medicCards.Any();
    }

    public bool IsAnyScorchUnitInHand()
    {
        return scorchUnitCards.Any();
    }

    public bool IsAnyMoraleBoostInHand()
    {
        return moraleBoostCards.Any();
    }

    public bool IsAnyTightBondInHand()
    {
        return tightBondCards.Any();
    }

    public bool IsAnyMusterInHand()
    {
        return musterCards.Any();
    }

    public bool IsAnyNormalUnitInHand()
    {
        return normalUnitCards.Any();
    }

    public bool IsAnyWeatherUnitInHand()
    {
        return weatherMagicCards.Any();
    }

    public SpyUnit GetSpyUnit()
    {
        SpyUnit cardToGive = spyCards.First();
        spyCards.Remove(cardToGive);
        return cardToGive;
    }

    public MedicUnit GetMedicUnit()
    {
        MedicUnit cardToGive = medicCards.First();
        medicCards.Remove(cardToGive);
        return cardToGive;
    }
}