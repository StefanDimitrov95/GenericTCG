using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform parentToReturnTo;
    public bool cardPlayed = false;
    public Card currentCard;

    static readonly Vector3 popUpVector = new Vector3(0.5F, 0.5F);

    void Start()
    {
        Debug.Log(currentCard.ToString());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.localScale += popUpVector;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(parentToReturnTo.parent);

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
