using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiscardPileTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private DiscardPile discardPile;
    public GameObject tooltip;
    private string data;

    void Start()
    {
        discardPile = this.gameObject.GetComponent<DiscardPile>();
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void ConstructDataString()
    {
        data = string.Format("<color=#e14c43>{0}</color>\n<size=8>Click here to view cards...</size>", discardPile.CardPile.Count);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ConstructDataString();
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}