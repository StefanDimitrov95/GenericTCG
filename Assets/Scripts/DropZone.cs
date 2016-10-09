using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Class rowClass;
    public Sprite oldPanel;
    public Sprite newPanel;

    void Start()
    {
        oldPanel = this.GetComponent<Image>().sprite;
        
        switch (this.name)
        {
            case "MeleeRow":
                {
                    rowClass = Class.Melee;
                    newPanel = Resources.Load<Sprite>("UI/MeleeRow_selected");
                    break;
                }
            case "RangedRow":
                {
                    rowClass = Class.Ranged;
                    newPanel = Resources.Load<Sprite>("UI/RangedRow_selected");
                    break;
                }
            case "SiegeRow":
                {
                    rowClass = Class.Siege;
                    newPanel = Resources.Load<Sprite>("UI/SiegeRow_selected");
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
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (d.currentCard.Type == "MonsterCard" && (d.currentCard as  MonsterCard).CardClass == this.rowClass)
        {
            if (d != null)
            {
                d.parentToReturnTo = this.transform;
            }
        }

        if (d.currentCard.Type == "MagicCard")
        {
            //To be implemented 
        }

        if (d.parentToReturnTo.gameObject != GameObject.Find("Hand").gameObject && d.parentToReturnTo.gameObject == GameObject.Find(this.name).gameObject )
        {            
            d.cardPlayed = true;
        }
    }
}
