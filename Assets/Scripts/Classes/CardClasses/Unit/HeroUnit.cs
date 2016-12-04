using Assets.Scripts.Classes.EnumClasses;

namespace Assets.Scripts.Classes
{
    public class HeroUnit : UnitCard
    {
        public HeroUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
        }

        public override void OnDropEffect()
        {
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
