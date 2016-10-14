using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    enum MonsterPerk
    {
        Normal,Spy
    }

    class MonsterCard : Card, IMonster
    {
        public int AttackValue { get; set; }

        public Class CardClass { get; set; }

        public MonsterPerk CardPerk { get; set; }

        public MonsterCard(int id, string title, string type, string slug, int attackValue, Class cardClass, MonsterPerk cardPerk)
            : base(id, title, "MonsterCard", slug)
        {
            this.AttackValue = attackValue;
            this.CardClass = cardClass;
            this.CardPerk = cardPerk;
            FindToRowField();
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}, {1}", AttackValue, CardClass.ToString());
        }

        private void FindToRowField()
        {
            if (this.CardPerk == MonsterPerk.Normal)
            {
                if (this.CardClass == Class.Melee)
                {
                    base.ToRow = GameObject.Find("MeleeRow");
                }
                if (this.CardClass == Class.Ranged)
                {
                    base.ToRow = GameObject.Find("RangedRow");
                }
                if (this.CardClass == Class.Siege)
                {
                    base.ToRow = GameObject.Find("SiegeRow");
                }
            }
            if (this.CardPerk == MonsterPerk.Spy)
            {
                if (this.CardClass == Class.EnemyMelee)
                {
                    base.ToRow = GameObject.Find("EnemyMeleeRow");
                }
                if (this.CardClass == Class.EnemyRanged)
                {
                    base.ToRow = GameObject.Find("EnemyRangedRow");
                }
                if (this.CardClass == Class.EnemySiege)
                {
                    base.ToRow = GameObject.Find("EnemySiegeRow");
                }
            }
           
        }
    }
}
