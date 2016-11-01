using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Classes;

public class TightBondUnit : UnitCard
{

    public TightBondUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
        :base(id, title, type, faction, slug, attackValue, ability)
    {
       
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
        List<UnitCard> foundCards = ToRow.GetCardsByNameFromRow(this.Title, this.Ability);
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
