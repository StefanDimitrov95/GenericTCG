﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo;
    public bool cardPlayed = false;
    public Card currentCard;
   
    private GameObject placeholder;

    void Start()
    {       
        //Debug.Log(currentCard.ToString());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);

        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;


        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        CardScaling.UpscaleCard(this);

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
        CardScaling.DownscaleCard(this);
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

    private void AnimateRowOnBeginDrag()
    {
        if (currentCard.Type != CardType.Special)
        {
           currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.enabled = true;
           currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.Play("OnBeginDrag");
        }       
    }

    private void AnimateRowOnEndDrag()
    {
        currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.Play("OnEndDrag");
    }
  
    
  
}