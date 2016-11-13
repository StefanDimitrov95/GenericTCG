using Assets.Scripts.Classes.EnumClasses;
using System;

namespace Assets.Scripts.Classes
{
    public class MoraleBoostUnit : UnitCard
    {
        public MoraleBoostUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
            
        }

        public override void OnDeath()
        {
            base.OnDeath();
            ToRow.GetComponent<DropZone>().currentRow.RemoveEffectFromRow(Ability.MoraleBoost);
            ToRow.GetComponent<DropZone>().currentRow.SetAttackValueOfRow();
        }

        public override void UpdateAttackForAbilitiesOnRow()
        {
            UpdateAttackForWeatherEffect();
            if (!ToRow.currentRow.IsWeatherEffectOnRow())
            {
                UpdateAttackForMoraleBoost();
            }
            else
            {
                int attackToAdd = ToRow.currentRow.cardsOnRow.FindAll(card => card.Ability == Ability.MoraleBoost).Count;
                AttackValue = attackToAdd;
            }
        }

        public override void OnDropEffect()
        {                                
            AddCardToRow(this);
            UpdateAttackForAbilitiesOnRow();
            base.ToRow.currentRow.AddMoraleBoostToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
