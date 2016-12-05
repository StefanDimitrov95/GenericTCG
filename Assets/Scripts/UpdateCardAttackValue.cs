using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public class UpdateCardAttackValue : MonoBehaviour
{
    public Card CurrentCard;
    private Text AttackComponent;

    void Start()
    {
        if (!(CurrentCard is UnitCard))
        {
            this.enabled = false;
            this.gameObject.SetActive(false);
            return;
        }
        AttackComponent = GetComponent<Text>();
    }

    void Update()
    {
        if (!(CurrentCard is UnitCard))
        {
            return;
        }

        AttackComponent.text = ((UnitCard)CurrentCard).AttackValue.ToString();

        if (((UnitCard)CurrentCard).AttackChanged() < 0)
        {
            AttackComponent.color = Color.red;
        }
        else if (((UnitCard)CurrentCard).AttackChanged() > 0)
        {
            AttackComponent.color = Color.green;
        }
        else
        {
            AttackComponent.color = Color.white;
        }
    }
}
