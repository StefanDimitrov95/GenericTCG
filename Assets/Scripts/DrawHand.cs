using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class DrawHand : MonoBehaviour {

	const int drawAmount =7;

	public List<Card> cards = new List<Card>();
    public CardDatabase database;
	// Use this for initialization
	void Start () {
        database = GetComponent<CardDatabase>();

		for (int i = 1; i <= drawAmount; i++)
		{
            Card cardToAdd = database.FetchCardById(i);
            if (cardToAdd == null)
            {
                break;
            }
            Debug.Log(cardToAdd.ToString());
            cards.Add(cardToAdd);
            GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
            cardObj.transform.SetParent(GameObject.Find("Hand").transform);
            cardObj.GetComponent<Image>().sprite = cardToAdd.Sprite;
            cardObj.name = cardToAdd.Title;
            //cards.Add(Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject);
            //cards[i].transform.SetParent(GameObject.Find("Hand").transform);
        }

	}
	
	void AddCard(int id)
	{
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
