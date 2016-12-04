using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class NormalUnit : UnitCard
    {
        public NormalUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {  
        }    

        public override void OnDropEffect()
        {
            AddCardToRow(this);
            UpdateAttackForAbilitiesOnRow(); 
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
