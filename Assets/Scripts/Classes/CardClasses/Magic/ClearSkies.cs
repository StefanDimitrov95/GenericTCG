using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.CardClasses.Magic
{
    public class ClearSkies : MagicCard
    {
        public DropZone rangedRow;
        public DropZone siegeRow;
        public ClearSkies(int id, string title, CardType type, Faction faction, string slug, Ability ability, AffectRow rowToAffect, string description)
            : base(id, title, type, faction, slug, ability, rowToAffect, description)
        {
        }

        public override void OnDropEffect()
        {
            foreach (GameObject row in GameObject.FindGameObjectsWithTag("Row"))
            {
                row.GetComponent<DropZone>().currentRow.RemoveWeatherEffectFromRow();
            }
        }
    }
}
