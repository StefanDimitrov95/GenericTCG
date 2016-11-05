using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Utils;
using Assets.Scripts.Interfaces;

public class PlayerDeck : MonoBehaviour, IDeck
{

    const int drawAmount = 20;

    public List<Card> Deck { get; set; }
    private CardDatabase CardDatabase;

    void Start()
    {
        CardDatabase = GameObject.Find("CardDatabase").GetComponent<CardDatabase>();
        Extensions.Shuffle(CardDatabase.PlayerDatabase);
        Deck = CardDatabase.PlayerDatabase.Take(drawAmount).ToList();
    }


    public void UpdateDeckLabel()
    {
        this.GetComponent<Text>().text = Deck.Count.ToString();
    }
}
