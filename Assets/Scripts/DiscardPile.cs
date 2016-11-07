using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Classes.EnumClasses;
using Assets.Scripts.Utils;

public class DiscardPile : MonoBehaviour
{
    public List<KeyValuePair<Card, GameObject>> CardPile { get; set; }
    public GameObject playerDiscardPanel;
    public GameObject enemyPanel;
    private Image DiscardPileImage { get; set; }
    

    void Start()
    {
        CardPile = new List<KeyValuePair<Card, GameObject>>();
    }

    public void OnMouseDown()
    {
        if (enemyPanel.activeInHierarchy)
        {
            DeactivatePanel(enemyPanel);
        }

        if (!playerDiscardPanel.activeInHierarchy)
        {
            ActivatePanel();
        }
        else
        {
            DeactivatePanel(playerDiscardPanel);
        }

        Debug.Log("DiscardPile Was Clicked!!!");
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
        KeyValuePair<Card, GameObject> randomCardKvPair = CardPile[rnd.Next(0, CardPile.Count)];
        CheckIfSpyCard(randomCardKvPair);
        CardPile.Remove(randomCardKvPair);
        UpdateCardPileImage();
        return randomCardKvPair;       
    }

    private void ActivatePanel()
    {
        foreach (KeyValuePair<Card, GameObject> item in CardPile)
        {
            AddToDiscardPilePanel(item.Key);

        }
        playerDiscardPanel.SetActive(true);
    }

    private void DeactivatePanel(GameObject discardPanel)
    {
        GameObject scrollCardPanel = discardPanel.transform.GetChild(1).GetChild(0).gameObject;

        foreach (Transform child in scrollCardPanel.transform)
        {
            child.gameObject.GetComponent<PointerHandlerDiscardedCard>().enabled = false;
            Destroy(child.gameObject);
        }
        discardPanel.SetActive(false);
    }

    private void AddToDiscardPilePanel(Card card)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        Extensions.InstantiateToDiscardPanel(cardObj, card, playerDiscardPanel.transform.GetChild(1).GetChild(0).transform);
        cardObj.GetComponent<PointerHandlerDiscardedCard>().enabled = true;
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