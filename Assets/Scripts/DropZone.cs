using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite oldPanel;
    public Sprite newPanel;
    public GameObject textBoxOfRow;
    public GameObject mainTextBox;

    private Class rowClass;
    private int rowAttackValue = 0;

    void Start()
    {
        oldPanel = this.GetComponent<Image>().sprite;
        mainTextBox = GameObject.Find("AllRowsValue");
        switch (this.name)
        {
            case "MeleeRow":
                {
                    rowClass = Class.Melee;
                    newPanel = Resources.Load<Sprite>("UI/MeleeRow_selected");
                    textBoxOfRow = GameObject.Find("MeleeRowValue");
                    break;
                }
            case "RangedRow":
                {
                    rowClass = Class.Ranged;
                    newPanel = Resources.Load<Sprite>("UI/RangedRow_selected");
                    textBoxOfRow = GameObject.Find("RangedRowValue");
                    break;
                }
            case "SiegeRow":
                {
                    rowClass = Class.Siege;
                    newPanel = Resources.Load<Sprite>("UI/SiegeRow_selected");
                    textBoxOfRow = GameObject.Find("SiegeRowValue");
                    break;
                }
            default:
                break;
        }      
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = oldPanel;
        Debug.Log("Dropped");
        Draggable draggedCard = eventData.pointerDrag.GetComponent<Draggable>();

        CardEffectOnDrop(draggedCard);
        DestroyingCard(draggedCard);
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

    void DestroyingCard(Draggable draggedCard)
    {
        if (draggedCard.parentToReturnTo.gameObject != GameObject.Find("Hand").gameObject
            && draggedCard.parentToReturnTo.gameObject == GameObject.Find(this.name).gameObject)
        {
            draggedCard.cardPlayed = true;
        }
    }
}
