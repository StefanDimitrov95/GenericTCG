using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class MonsterCard : Card, IMonster
    {
        public int AttackValue { get; set; }

        public Class CardClass { get; set; }

        public MonsterCard(int id, string title, string type, string slug, int attackValue, Class cardClass)
            : base(id, title, "MonsterCard", slug)
        {
            this.AttackValue = attackValue;
            this.CardClass = cardClass;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}, {1}", AttackValue, CardClass.ToString());
        }
    }
}
