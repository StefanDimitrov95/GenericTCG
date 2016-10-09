using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Class rowClass;

    void Start()
    {       
        switch (this.name)
        {
            case "MeleeRow":
                {
                    rowClass = Class.Melee;
                    break;
                }
            case "RangedRow":
                {
                    rowClass = Class.Ranged;
                    break;
                }
            case "SiegeRow":
                {
                    rowClass = Class.Siege;
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
