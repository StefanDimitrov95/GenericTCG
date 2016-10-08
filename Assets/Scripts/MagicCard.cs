using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class MagicCard : Card, IMagic
    {
        public string Effect { get; set; }

        public MagicCard(int id, string title, string type, string slug, string effect) :
            base(id, title, "MagicCard", slug)
        {
            this.Effect = effect;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}", Effect);
        }
    }
}
