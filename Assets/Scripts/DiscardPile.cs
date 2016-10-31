using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour
{
    public List<GameObject> CardPile { get; set; }

    void Start()
    {
        CardPile = new List<GameObject>();
    }

    public void AddToDiscardPile(GameObject card)
    {
        this.CardPile.Add(card);

        Image DiscardPileImage = this.gameObject.GetComponent<Image>();
        DiscardPileImage.enabled = true;
        DiscardPileImage.sprite = card.GetComponent<Image>().sprite;

        Destroy(card);
    }
}
