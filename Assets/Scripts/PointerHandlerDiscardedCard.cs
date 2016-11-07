using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PointerHandlerDiscardedCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card discardedCard;

    private GameObject textDesription;
    // Use this for initialization
    void Start () {
        textDesription = GameObject.Find("Description");
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover Discared card");
        textDesription.GetComponent<Text>().enabled = true;
        textDesription.GetComponent<Text>().text = discardedCard.ConstructCardData();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textDesription.GetComponent<Text>().text = string.Empty;
        textDesription.GetComponent<Text>().enabled = true;        
    }

}
