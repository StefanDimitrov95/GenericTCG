using Assets.Scripts.Classes.EnumClasses;


namespace Assets.Scripts.Classes
{
    public class UnitCardFactory
    {
        public UnitCard CreateUnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
        {
            UnitCard unitCard = null;

            if (ability == MonsterAbility.None)
            {
                unitCard = new NormalUnit(id, title, type, faction, slug, attackValue, ability);
            }
            if (ability == MonsterAbility.Spy)
            {
                unitCard = new SpyUnit(id, title, type, faction, slug, attackValue, ability);
            }
            if (ability == MonsterAbility.MoraleBoost)
            {
                unitCard = new MoraleBoostUnit(id, title, type, faction, slug, attackValue, ability);
            }
            if (ability == MonsterAbility.TightBond)
            {
                unitCard = new TightBondUnit(id, title, type, faction, slug, attackValue, ability);
            }
            if (ability == MonsterAbility.Scorch)
            {
                unitCard = new ScorchUnit(id, title, type, faction, slug, attackValue, ability);
            }
            return unitCard;
        }
    }
}
