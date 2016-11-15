using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    private Card card;
    private GameObject tooltip;
    private Animator anim;
    private string data;

    void Start()
    {
        tooltip = GameObject.Find("TooltipPanel");
        anim = tooltip.GetComponent<Animator>();
        tooltip.SetActive(false);
    }

    public void Activate()
    {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(1).gameObject.SetActive(true);
        anim.ResetTrigger("isHoverEnded");
        anim.SetTrigger("isHover");
    }

    public void Deactivate()
    {
        anim.ResetTrigger("isHover");
        anim.SetTrigger("isHoverEnded");
        tooltip.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = card.ConstructCardData();
    }

    public void ConstructTooltip(Card card)
    {
        this.card = card;
        ConstructDataString();
        SetImageOfTooltip(card);
        DisplayData();
    }
    void DisplayData()
    {
        tooltip.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = data;
    }

    private void SetImageOfTooltip(Card card)
    {
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite;
    }
}
