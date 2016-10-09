using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform parentToReturnTo;
    public bool cardPlayed = false;
    public Card currentCard;

    private GameObject toRow; 

    static readonly Vector3 popUpVector = new Vector3(0.5F, 0.5F);

    void Start()
    {
        Debug.Log(currentCard.ToString());
        if (currentCard.Type == "MonsterCard" && (currentCard as MonsterCard).CardClass == Class.Melee)
        {
            toRow = GameObject.Find("MeleeRow");
        }
        else if (currentCard.Type == "MonsterCard" && (currentCard as MonsterCard).CardClass == Class.Ranged)
        {
            toRow = GameObject.Find("RangedRow");
        }
        else if (currentCard.Type == "MonsterCard" && (currentCard as MonsterCard).CardClass == Class.Siege)
        {
            toRow = GameObject.Find("SiegeRow");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.localScale += popUpVector;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(parentToReturnTo.parent);

        if (currentCard.Type == "MonsterCard")
        {
            Sprite newPanel = toRow.GetComponent<DropZone>().newPanel;
            toRow.GetComponent<Image>().sprite = newPanel;
        }     

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.localScale -= new Vector3(0.5F, 0.5F);
        this.transform.SetParent(parentToReturnTo);

        Sprite oldPanel = toRow.GetComponent<DropZone>().oldPanel;
        toRow.GetComponent<Image>().sprite = oldPanel;

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (cardPlayed)
        {
            Destroy(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale += popUpVector;
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        this.transform.localScale -= popUpVector;
    }
}
