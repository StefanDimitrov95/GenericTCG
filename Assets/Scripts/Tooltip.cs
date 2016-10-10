using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private Card card;
    private GameObject tooltip;
    private string data;

	// Use this for initialization
	void Start () {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
	}
	
	public void Activate(Card card)
    {
        this.card = card;
        ConstructDataString();
        DisplayData();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        if (card.Type == "MonsterCard")
        {
            data = "<color=#acb939><b> \t\t\t\t" + card.Title + "</b></color>" +
            "\n\nAttack Power: " + "<color=#e14c43><b>" + (card as MonsterCard).AttackValue + "</b></color>" +
            "\nClass: " + "<color=#3770d2>" + (card as MonsterCard).CardClass + "</color>";
        }
        if (card.Type == "MagicCard")
        {
            data = "<color=#acb939><b> \t\t\t\t" + card.Title + "</b></color>" +
               "\n\nEffect: " + "<color=#e14c43><b>" + (card as MagicCard).Effect + "</color>";
        }
    }

    void DisplayData()
    {
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
