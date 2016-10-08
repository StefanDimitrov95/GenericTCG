using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class DrawHand : MonoBehaviour {

	const int drawAmount =7;

	public List<GameObject> cards = new List<GameObject>();
	public 
	// Use this for initialization
	void Start () {    


		for (int i = 0; i < drawAmount; i++)
		{
			cards.Add(Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject);
			cards[i].transform.SetParent(GameObject.Find("Hand").transform);
		}

	}
	
	void AddCard(int id)
	{
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
