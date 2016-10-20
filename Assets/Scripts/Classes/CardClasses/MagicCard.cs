using Assets.Scripts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class  MagicCard : Card, IMagic
    {
        public string Effect { get; set; }

        public MagicCard(int id, string title, CardType type, Faction faction, string slug, string effect) :
            base(id, title, type, faction, slug)
        {
            this.Effect = effect;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}", Effect);
        }

        public override void OnDropEffect()
        {
            throw new NotImplementedException();
        }
    }
}
