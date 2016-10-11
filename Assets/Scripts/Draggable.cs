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
    private Tooltip tooltip;
    private GameObject placeholder;

    static readonly Vector3 cardPopUpScale = new Vector3(0.5F, 0.5F);

    void Start()
    {
        tooltip = GameObject.Find("Deck").GetComponent<Tooltip>();
        //Debug.Log(currentCard.ToString());
        FindToRowField();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);

        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;


        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        this.transform.localScale += cardPopUpScale;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(parentToReturnTo.parent);

        AnimateRowOnBeginDrag();

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.localScale -= cardPopUpScale;
        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        Destroy(placeholder);
        
        AnimateRowOnEndDrag();

        if (cardPlayed)
        {
            Destroy(this);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale += cardPopUpScale;

        tooltip.Activate(currentCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        this.transform.localScale -= cardPopUpScale;
        tooltip.Deactivate();   
    }

    private void AnimateRowOnBeginDrag()
    {
        if (currentCard.Type == "MonsterCard")
        {
            toRow.GetComponent<DropZone>().animator.enabled = true;
            toRow.GetComponent<DropZone>().animator.Play("OnBeginDrag");
        }       
    }

    private void AnimateRowOnEndDrag()
    {
        toRow.GetComponent<DropZone>().animator.Play("OnEndDrag");
    }

    private void FindToRowField()
    {
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
}