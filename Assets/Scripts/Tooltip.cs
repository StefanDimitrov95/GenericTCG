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

    public void ConstructTooltip(Card card, GameObject cardObj)
    {
        this.card = card;
        ConstructDataString();
        SetImageOfTooltip(card);
        SetAttackTextField(cardObj);
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

    private void SetAttackTextField(GameObject cardObj)
    {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = cardObj.transform.GetChild(0).GetComponent<Text>().text;
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = cardObj.transform.GetChild(0).GetComponent<Text>().color;
    }
}
