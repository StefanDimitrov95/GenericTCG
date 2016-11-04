using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Classes.EnumClasses;

public class DiscardPile : MonoBehaviour
{
    public List<KeyValuePair<Card, GameObject>> CardPile { get; set; }
    private Image DiscardPileImage { get; set; }


    void Start()
    {
        CardPile = new List<KeyValuePair<Card, GameObject>>();
    }

    public void AddToDiscardPile(Card card, GameObject enemyRowObj)
    {
        if (card != null)
        {
            card.OnDeath();
        }

        GameObject cardPrefab = enemyRowObj.transform.FindChild(card.ID + "," + card.Title).gameObject;

        DiscardPileImage = this.gameObject.GetComponent<Image>();
        cardPrefab.SetActive(false);
        this.CardPile.Add(new KeyValuePair<Card, GameObject>(card, cardPrefab));
        UpdateCardPileImage();
    }

    public KeyValuePair<Card, GameObject> GetRandomCard()
    {
        System.Random rnd = new System.Random();
        KeyValuePair<Card, GameObject> randomCardKvPair = CardPile.ElementAt(rnd.Next(0, CardPile.Count));
        CheckIfSpyCard(randomCardKvPair);
        CardPile.Remove(randomCardKvPair);
        UpdateCardPileImage();
        return randomCardKvPair;       
    }

    private void CheckIfSpyCard(KeyValuePair<Card, GameObject> ressuructPair)
    {
        if (ressuructPair.Key is UnitCard && (ressuructPair.Key as UnitCard).Ability == MonsterAbility.Spy)
        {
            ChangeSpyUnitParrent(ressuructPair);
        }
    }

    private void ChangeSpyUnitParrent(KeyValuePair<Card, GameObject> ressuructPair)
    {
        if (ressuructPair.Key.ToRow.name.StartsWith("Enemy"))
        {
            GameObject newToRow = GameObject.Find(ressuructPair.Key.ToRow.name.Substring(5));
            ressuructPair.Key.ToRow = newToRow.GetComponent<DropZone>();
            ressuructPair.Value.transform.SetParent(newToRow.transform);
        }
        else
        {
            GameObject newToRow = GameObject.Find("Enemy" + ressuructPair.Key.ToRow.name);
            ressuructPair.Key.ToRow = newToRow.GetComponent<DropZone>();
            ressuructPair.Value.transform.SetParent(newToRow.transform);
        }
    }

    private void UpdateCardPileImage()
    {
        if (!CardPile.Any())
        {
            DiscardPileImage.enabled = false;
            return;
        }
        DiscardPileImage.enabled = true;
        DiscardPileImage.sprite = CardPile[CardPile.Count - 1].Value.GetComponent<Image>().sprite;
    }
}