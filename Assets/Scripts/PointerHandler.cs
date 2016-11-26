using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts;
using UnityEngine.UI;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card CurrentCard;
    private Tooltip tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Board").GetComponent<Tooltip>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        CardScaling.UpscaleCard(this);
        tooltip.ConstructTooltip(CurrentCard, this.gameObject);
        tooltip.Activate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardScaling.DownscaleCard(this);
        tooltip.Deactivate();
    }
}
