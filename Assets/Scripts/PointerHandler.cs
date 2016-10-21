using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Card CurrentCard;

	private Tooltip Tooltip;
	
	
	void Start () {
		Tooltip = GameObject.Find("Deck").GetComponent<Tooltip>();
	}
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		CardScaling.UpscaleCard(this);
		Tooltip.Activate(CurrentCard);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CardScaling.DownscaleCard(this);
		Tooltip.Deactivate();
	}

	
}
