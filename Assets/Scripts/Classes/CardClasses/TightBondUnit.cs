using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using Assets.Scripts.Classes;

public class TightBondUnit : UnitCard {

    public TightBondUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue)
        :base(id, title, type, faction, slug, attackValue)
    {
        Ability = MonsterAbility.TightBond;
    }

    public override string ConstructCardData()
    {
        char modifiedAttack = this.AttackValue != originalAttack ? '*' : ' ';
        string data = "<color=#acb939><b> \t\t\t\t" + this.Title + "</b></color>" +
        "\n\nAttack Power: " + "<color=#e14c43><b>" + this.AttackValue + modifiedAttack + "</b></color>" +
        "\nType: " + "<color=#3770d2>" + this.Type + "</color>" +
        "\nAbility: " + "<color=#3770d2>" + this.Ability + "</color>";
        return data;
    }

    public override void OnDropEffect()
    {
        AddCardToRow(this);
        UpdateAttackForMoraleBoost();
        UpdateAttackForTightBond();
        base.ToRow.currentRow.SetAttackValueOfRow();
    }

    private void UpdateAttackForTightBond()
    {
        List<Card> foundCards = ToRow.GetCardsByNameFromRow(this.Title);
        if (foundCards.Count > 1)
        {
            foreach (UnitCard card in foundCards)
            {
                if (card.Ability == MonsterAbility.TightBond)
                {
                    card.AttackValue = (foundCards.Count) * card.originalAttack;
                }
            }
        }
    }
}
