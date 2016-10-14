using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Row currentRow;
    //public GameObject mainTextBox;
    public Animator animator;

    void Start()
    {
        currentRow = new Row(this.name);
        if (this.name != "Hand")
        {
            animator = this.GetComponent<Animator>();
            animator.enabled = false;
        }               
        //mainTextBox = GameObject.Find("AllRowsValue");  
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        Draggable draggedCard = eventData.pointerDrag.GetComponent<Draggable>();

        CardEffectOnDrop(draggedCard);
        DestroyCard(draggedCard);
    }

    void CardEffectOnDrop(Draggable draggedCard)
    {
        if (draggedCard.currentCard.Type == "MonsterCard"
            && (draggedCard.currentCard as MonsterCard).CardClass == this.currentRow.RowClass
            && draggedCard != null)
        {
            draggedCard.parentToReturnTo = this.transform;
            UpdateRowValue(draggedCard);
        }

        if (draggedCard.currentCard.Type == "MagicCard")
        {
            //To be implemented 
        }       
    }

    void UpdateRowValue(Draggable draggedCard)
    {
        if (draggedCard.parentToReturnTo.gameObject != GameObject.Find("Hand"))
        {
            currentRow.UpdateAttackValueOfRow(draggedCard.currentCard);
            //int totalAttackValue = int.Parse(mainTextBox.GetComponent<Text>().text);
            //totalAttackValue += draggedCardAttackValue;
            //mainTextBox.GetComponent<Text>().text = totalAttackValue.ToString();
        }     
    }

    void DestroyCard(Draggable draggedCard)
    {
        if (draggedCard.parentToReturnTo.gameObject != GameObject.Find("Hand").gameObject
            && draggedCard.parentToReturnTo.gameObject == GameObject.Find(this.name).gameObject)
        {
            draggedCard.cardPlayed = true;
        }
    }
    
}
