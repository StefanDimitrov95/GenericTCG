using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class MagicCard : Card, IMagic
    {
        public AffectRow RowToAffect { get; set; }
        public Ability Ability { get; set; }
        public string Description { get; set;
        }
        public MagicCard(int id, string title, CardType type, Faction faction, string slug, Ability ability, AffectRow rowToAffect, string description) :
            base(id, title, type, faction, slug)
        {
            this.Ability = ability;
            this.RowToAffect = rowToAffect;
            this.Description = description;
            FindToRowField();
        }

        public override string ConstructCardData()
        {
            string data = "<color=#acb939><b> \t\t" + this.Title + "</b></color>" +
            "\n\n<color=#33ccff>" + this.Description + "</color>";
            return data;
        }

        public override void OnResurrect()
        {
            OnDropEffect();
        }

        public abstract void OnPlay();

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}", (object)Ability);
        }

        private void FindToRowField()
        {
            switch (RowToAffect)
            {
                case AffectRow.Melee:
                    base.ToRow = GameObject.Find("MeleeRow").GetComponent<DropZone>();
                    break;
                case AffectRow.Ranged:
                    base.ToRow = GameObject.Find("RangedRow").GetComponent<DropZone>();
                    break;
                case AffectRow.Siege:
                    base.ToRow = GameObject.Find("SiegeRow").GetComponent<DropZone>();
                    break;
                default:
                    break;
            }
        }
    }
}
