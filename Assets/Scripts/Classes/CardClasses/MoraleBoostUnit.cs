using Assets.Scripts.Classes.EnumClasses;

namespace Assets.Scripts.Classes
{
    public class MoraleBoostUnit : UnitCard
    {
        public MoraleBoostUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
            
        }   

        public override void OnDropEffect()
        {                                
            for (int i = 0; i < ToRow.currentRow.CardsOnRow.Count; i++)
            {
               (ToRow.currentRow.CardsOnRow[i] as UnitCard).AttackValue++;
            }

            AddCardToRow(this);
            UpdateAttackForMoraleBoost();
            base.ToRow.currentRow.AddMoraleBoostToRow();
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
