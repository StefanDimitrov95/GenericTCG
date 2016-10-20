using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card currentCard;

    private Tooltip tooltip;
    
    
    void Start () {
        tooltip = GameObject.Find("Deck").GetComponent<Tooltip>();
    }
	
    public void OnPointerEnter(PointerEventData eventData)
    {
        CardScaling.UpscaleCard(this);
        tooltip.Activate(currentCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardScaling.DownscaleCard(this);
        tooltip.Deactivate();
    }

    
}
