using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card CurrentCard;
    private Tooltip tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Deck").GetComponent<Tooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardScaling.UpscaleCard(this);
        tooltip.ConstructTooltip(CurrentCard);
        tooltip.Activate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardScaling.DownscaleCard(this);
        tooltip.Deactivate();
    }
}
