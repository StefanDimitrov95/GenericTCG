using Assets.Scripts.Classes.EnumClasses;


namespace Assets.Scripts.Classes
{
    public class UnitCardFactory
    {
        public UnitCard CreateUnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
        {
            UnitCard unitCard = null;
            switch (ability)
            {
                case Ability.None:
                    {
                        unitCard = new NormalUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }                   
                case Ability.Spy:
                    {
                        unitCard = new SpyUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case Ability.MoraleBoost:
                    {
                        unitCard = new MoraleBoostUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case Ability.Medic:
                    {
                        unitCard = new MedicUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case Ability.TightBond:
                    {
                        unitCard = new TightBondUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case Ability.Hero:
                    {
                        break;
                    }
                case Ability.Agile:
                    {
                        break;
                    }
                case Ability.Muster:
                    {
                        unitCard = new MusterUnit(id, title, type, faction, slug, attackValue, ability);
                        break;
                    }
                case Ability.Scorch:
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
