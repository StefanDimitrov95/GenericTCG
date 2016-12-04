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
    private List<HeroUnit> heroCards;
    private List<ClearSkies> clearSkiesCards;

    private EnemyDeck EnemyDeck;
    private Text HandLabel;
    const int AmountOfCardsToDraw = 10;

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
        heroCards = new List<HeroUnit>();
        clearSkiesCards = new List<ClearSkies>();

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
        scorchUnitCards = CardsInHand.OfType<ScorchUnit>().ToList();
        spyCards = CardsInHand.OfType<SpyUnit>().ToList();
        moraleBoostCards = CardsInHand.OfType<MoraleBoostUnit>().ToList();
        tightBondCards = CardsInHand.OfType<TightBondUnit>().ToList();
        musterCards = CardsInHand.OfType<MusterUnit>().ToList();
        medicCards = CardsInHand.OfType<MedicUnit>().ToList();
        normalUnitCards = CardsInHand.OfType<NormalUnit>().ToList();
        weatherMagicCards = CardsInHand.OfType<Weather>().ToList();
        heroCards = CardsInHand.OfType<HeroUnit>().ToList();
        clearSkiesCards = CardsInHand.OfType<ClearSkies>().ToList();
    }

    public UnitCard GetLowestCard()
    {
        List<UnitCard> unitsInHand = CardsInHand.FindAll(c => (c is UnitCard)).Cast<UnitCard>().ToList();
        UnitCard lowestCard = null;
        int lowestValue = int.MaxValue;
        foreach (UnitCard card in unitsInHand)
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

    public bool IsAnyWeatherInHand()
    {
        return weatherMagicCards.Any();
    }

    public bool IsAnyWeakNormalUnitInHand()
    {
        return normalUnitCards.Any(c => c.AttackValue < 8);
    }

    public bool IsAnyHeroUnitInHand()
    {
        return heroCards.Any();
    }

    public bool IsAnyClearSkiesInHand()
    {
        return clearSkiesCards.Any();
    }

    public ClearSkies GetClearSkiesCard()
    {
        ClearSkies cardToGive = clearSkiesCards.First();
        clearSkiesCards.Remove(cardToGive);
        return cardToGive;
    }

    public HeroUnit GetHeroCard()
    {
        HeroUnit cardToGive = heroCards.First();
        heroCards.Remove(cardToGive);
        return cardToGive;
    }

    public UnitCard GetWeakestNormalUnit()
    {
        List<NormalUnit> weakCards = normalUnitCards.FindAll(c => c.AttackValue < 8);
        UnitCard lowestCard = null;
        int lowestValue = int.MaxValue;
        foreach (UnitCard card in weakCards)
        {
            if (card.AttackValue < lowestValue)
            {
                lowestValue = card.AttackValue;
                lowestCard = card;
            }
        }
        return lowestCard;
    }

    public SpyUnit GetSpyUnit()
    {
        SpyUnit cardToGive = spyCards.First();
        spyCards.Remove(cardToGive);
        return cardToGive;
    }

    public ScorchUnit DecideWhichScorchToPlay(List<UnitCard> units)
    {
        foreach (ScorchUnit scorch in scorchUnitCards)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (scorch.CanDestroyUnit(units[i]))
                {
                    scorchUnitCards.Remove(scorch);
                    return scorch;
                }
            }
        }
        return null;
    }

    public Weather DecideWhichWeatherToPlay(List<UnitCard> units)
    {
        foreach (Weather weather in weatherMagicCards)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (weather.CanAffectUnit(units[i]))
                {
                    weatherMagicCards.Remove(weather);
                    return weather;
                }
            }
        }
        return null;
    }

    public MedicUnit GetMedicUnit()
    {
        MedicUnit cardToGive = medicCards.First();
        medicCards.Remove(cardToGive);
        return cardToGive;
    }

    public MusterUnit GetMusterUnit()
    {
        MusterUnit musterCard = musterCards.First();
        musterCards.Remove(musterCard);
        return musterCard;
    }

    public bool AreTighBondCardsSame()
    {
        var tightBondGroups = tightBondCards.GroupBy(c => c.Title);
        foreach (var tightBondGroup in tightBondGroups)
        {
            if (tightBondGroup.Count() > 1)
            {
                //tightBondGroup.ToList().ForEach(c => Debug.Log(c.ToString()));
                return true;
            }
        }
        return false;
    }

    public TightBondUnit GetTightBondUnit(string name)
    {
        TightBondUnit tightBondUnit = null;
        var tightBondGroups = tightBondCards.GroupBy(c => c.Title);
        foreach (var tightBondGroup in tightBondGroups)
        {
            if (tightBondGroup.Key == name)
            {
                tightBondUnit = tightBondGroup.First();
            }
        }
        if (tightBondUnit == null)
        {
            tightBondUnit = tightBondCards.First();
        }
        tightBondCards.Remove(tightBondUnit);
        return tightBondUnit;
    }
}