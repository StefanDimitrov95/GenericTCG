using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class PlayerDeck : MonoBehaviour
{

    const int drawAmount = 20;

    public List<Card> Deck;
    private CardDatabase CardDatabase;

    void Start()
    {
        CardDatabase = GetComponent<CardDatabase>();
        Deck = CardDatabase.Database.Take(drawAmount).ToList();
        Deck.Sort((x, y) => Random.value < 0.5f ? -1 : 1);

        UpdateDeckLabel();
    }


    public void UpdateDeckLabel()
    {
        this.GetComponent<Text>().text = Deck.Count.ToString();
    }
}
