using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IHand
    {
        List<Card> CardsInHand { get; set; }
        void DrawExtraCards(int amount);
        void UpdateHandLabel();
    }
}
