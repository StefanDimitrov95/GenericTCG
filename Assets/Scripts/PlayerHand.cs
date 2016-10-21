using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;

public class PlayerHand : MonoBehaviour
{
	public List<Card> CardsInHand;

	private PlayerDeck PlayerDeck;
	private Text HandLabel;
	const int AmountOfCardsToDraw = 7;

	void Start()
	{
		PlayerDeck = GameObject.Find("Deck").GetComponent<PlayerDeck>();

		CardsInHand = PlayerDeck.Deck.Take(AmountOfCardsToDraw).ToList();
		PlayerDeck.Deck.RemoveAll(x => CardsInHand.Any(y => y.ID == x.ID));

		foreach (Card card in this.CardsInHand)
		{
			InstanciateCardToHand(card);
		}

		UpdateHandLabel();
		PlayerDeck.UpdateDeckLabel();
	}

	public void DrawExtraCards(int amountOfCardsToDraw)
	{
		List<Card> cardsToDraw = new List<Card>();
		cardsToDraw = PlayerDeck.Deck.Take(amountOfCardsToDraw).ToList();
		CardsInHand.AddRange(cardsToDraw);
		PlayerDeck.Deck.RemoveAll(x => cardsToDraw.Any(y => y.ID == x.ID));

		foreach (Card card in cardsToDraw)
		{
			InstanciateCardToHand(card);
		}

		UpdateHandLabel();
		PlayerDeck.UpdateDeckLabel();
	}


	private static void InstanciateCardToHand(Card cardToBeInstanciated)
	{
		GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
		cardObj.transform.SetParent(GameObject.Find("Hand").transform);
		cardObj.GetComponent<Image>().sprite = cardToBeInstanciated.Sprite;
		cardObj.name = cardToBeInstanciated.Title;
		cardObj.GetComponent<Draggable>().currentCard = cardToBeInstanciated;
		cardObj.GetComponent<PointerHandler>().CurrentCard = cardToBeInstanciated;
	}

	public void UpdateHandLabel()
	{
		HandLabel = GameObject.Find("HandLabel").GetComponent<Text>();
		HandLabel.text = CardsInHand.Count.ToString();
	}
}
