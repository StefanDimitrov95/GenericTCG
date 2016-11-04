using Assets.Scripts.Classes.EnumClasses;

namespace Assets.Scripts.Classes
{
    public class MoraleBoostUnit : UnitCard
    {
        public MoraleBoostUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
            
        }

        public override void OnDeath()
        {
            base.OnDeath();
            ToRow.GetComponent<DropZone>().currentRow.RemoveEffectFromRow(MonsterAbility.MoraleBoost);
            ToRow.GetComponent<DropZone>().currentRow.SetAttackValueOfRow();
        }

        public override void OnDropEffect()
        {                                
            AddCardToRow(this);
            UpdateAttackForMoraleBoost();
            base.ToRow.currentRow.AddMoraleBoostToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
