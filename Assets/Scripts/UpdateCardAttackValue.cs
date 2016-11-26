using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public class UpdateCardAttackValue : MonoBehaviour
{
    public Card CurrentCard;
    private Text AttackComponent;

    void Start()
    {
        AttackComponent = GetComponent<Text>();
        if (!(CurrentCard is UnitCard))
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        AttackComponent.text = ((UnitCard)CurrentCard).AttackValue.ToString();

        if (((UnitCard)CurrentCard).AttackChanged() < 0)
        {
            AttackComponent.color = Color.red;
        }
        else if (((UnitCard)CurrentCard).AttackChanged() > 0)
        {
            AttackComponent.color = Color.green;
        }
    }
}
