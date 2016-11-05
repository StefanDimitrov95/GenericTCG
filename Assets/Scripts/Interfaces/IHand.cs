using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IHand
    {
        List<Card> CardsInHand { get; set; }
        void DrawExtraCards(int amount);
        void UpdateHandLabel();
    }
}
