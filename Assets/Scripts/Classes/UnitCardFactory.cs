using Assets.Scripts.Classes.EnumClasses;


namespace Assets.Scripts.Classes
{
    public class UnitCardFactory
    {
        public UnitCard CreateUnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
        {
            UnitCard unitCard = null;
            switch (ability)
            {
                case MonsterAbility.None:
                    {
                        unitCard = new NormalUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }                   
                case MonsterAbility.Spy:
                    {
                        unitCard = new SpyUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case MonsterAbility.MoraleBoost:
                    {
                        unitCard = new MoraleBoostUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case MonsterAbility.Medic:
                    {
                        unitCard = new MedicUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case MonsterAbility.TightBond:
                    {
                        unitCard = new TightBondUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case MonsterAbility.Hero:
                    {
                        break;
                    }
                case MonsterAbility.Agile:
                    {
                        break;
                    }
                case MonsterAbility.Muster:
                    {
                        break;
                    }
                case MonsterAbility.Scorch:
                    {
                        unitCard = new ScorchUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                default:
                    break;
            }
           
            return unitCard;
        }
    }
}
