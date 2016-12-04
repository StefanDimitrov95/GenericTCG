using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Classes;

public class TightBondUnit : UnitCard
{

    public TightBondUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
        :base(id, title, type, faction, slug, attackValue, ability)
    {
       
    }

    public override void UpdateAttackForAbilitiesOnRow()
    {
        UpdateAttackForWeatherEffect();
        UpdateAttackForTightBond();
        UpdateAttackForMoraleBoost();
    }

    public override void ResetAttackAfterClearWeather()
    {
        base.ResetAttackAfterClearWeather();
        UpdateAttackForTightBond();
    }

    public override void OnDropEffect()
    {
        AddCardToRow(this);
        UpdateAttackForAbilitiesOnRow();
        base.ToRow.currentRow.SetAttackValueOfRow();
    }

    private void UpdateAttackForTightBond()
    {
        List<UnitCard> foundCards = ToRow.GetCardsByNameFromRow(this.Title, this.Ability);
        if (foundCards.Count > 1)
        {
            foreach (UnitCard card in foundCards)
            {
                if (ToRow.currentRow.IsWeatherEffectOnRow())
                {
                    card.AttackValue = foundCards.Count * 1;
                }
                else 
                {
                    //Needs to be tested
                    //int buffedAttack = card.AttackValue - card.originalAttack;
                    card.AttackValue = (foundCards.Count) * card.originalAttack;
                    //card.AttackValue += buffedAttack;
                }
            }
        }
    }
}
