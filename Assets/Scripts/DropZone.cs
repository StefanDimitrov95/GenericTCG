using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Row currentRow;
    public Animator animator;

    void Start()
    {
        currentRow = new Row(this.name);
       
        animator = this.GetComponent<Animator>();
        animator.enabled = false;                          
    }

    public List<UnitCard> GetCardsByNameFromRow(string title, MonsterAbility ability)
    {
        return currentRow.GetCardsByNameFromRow(title, ability);
    }

    public List<UnitCard> GetStrongestUnitCards()
    {
        return currentRow.GetStrongestUnitCards();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        Draggable draggedCard = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggedCard.gameObject.transform.parent == GameObject.Find("Hand").transform)
        {
            return;
        }
        CardEffectOnDrop(draggedCard);
        DestroyCard(draggedCard);        
    }

    void CardEffectOnDrop(Draggable draggedCard)
    {       
        string draggedCardRow = draggedCard.currentCard.GetToRowName();

        if (draggedCardRow == this.name)
        {
            draggedCard.parentToReturnTo = this.transform;
            
            draggedCard.currentCard.OnDropEffect();
            GameObject.Find("Board").GetComponent<Board>().UpdateAttackLabels();
        }
        
    }

    void DestroyCard(Draggable draggedCard)
    {
        if (draggedCard.parentToReturnTo.gameObject != GameObject.Find("Hand").gameObject
            && draggedCard.parentToReturnTo.gameObject == GameObject.Find(this.name).gameObject)
        {
            GameObject.Find("Hand").GetComponent<PlayerHand>().cardPlayed = true;
            draggedCard.cardPlayed = true;
        }
    }
    
}
