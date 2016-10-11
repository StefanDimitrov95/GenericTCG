using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{    
    public GameObject textBoxOfRow;
    public GameObject mainTextBox;
    public Animator animator;

    private Class rowClass;
    private int rowAttackValue = 0;
    private List<GameObject> cardsOnPanel= new List<GameObject>(7);
    private int children = 0;

    void Start()
    {
        if (this.name != "Hand")
        {
            animator = this.GetComponent<Animator>();
            animator.enabled = false;
        }
               
        mainTextBox = GameObject.Find("AllRowsValue");

        InitializeFiedData();
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
            && (draggedCard.currentCard as MonsterCard).CardClass == this.rowClass
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
            int draggedCardAttackValue = (draggedCard.currentCard as MonsterCard).AttackValue;
            rowAttackValue += draggedCardAttackValue;
            textBoxOfRow.GetComponent<Text>().text = rowAttackValue.ToString();
            int totalAttackValue = int.Parse(mainTextBox.GetComponent<Text>().text);
            totalAttackValue += draggedCardAttackValue;
            mainTextBox.GetComponent<Text>().text = totalAttackValue.ToString();
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
    void InitializeFiedData()
    {
        switch (this.name)
        {
            case "MeleeRow":
                {
                    rowClass = Class.Melee;
                    textBoxOfRow = GameObject.Find("MeleeRowValue");
                    break;
                }
            case "RangedRow":
                {
                    rowClass = Class.Ranged;
                    textBoxOfRow = GameObject.Find("RangedRowValue");
                    break;
                }
            case "SiegeRow":
                {
                    rowClass = Class.Siege;
                    textBoxOfRow = GameObject.Find("SiegeRowValue");
                    break;
                }
            case "Hand":
                {
                    foreach (Transform child in this.transform)
                    {
                        cardsOnPanel.Add(child.gameObject);
                        children++;
                    }
                    break;
                }
            default:
                break;
        }
    }
}
