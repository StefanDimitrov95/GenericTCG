using Assets.Scripts.Classes.CardClasses.Magic;
using Assets.Scripts.Classes.EnumClasses;


namespace Assets.Scripts.Classes
{
    public class MagicCardFactory
    {
        public MagicCard CreateMagicCard(int id, string title, CardType type, Faction faction, string slug, Ability ability, AffectRow rowToAffect, string description)
        {
            MagicCard magicCard = null;
            switch (ability)
            {
                case Ability.Weather:
                    magicCard = new Weather(id, title, type, faction, slug, ability, rowToAffect, description);
                    break;
                case Ability.ClearSkies:
                    magicCard = new ClearSkies(id, title, type, faction, slug, ability, rowToAffect, description);
                    break;
                default:
                    break;
            }
            return magicCard;
        }
    }
}
