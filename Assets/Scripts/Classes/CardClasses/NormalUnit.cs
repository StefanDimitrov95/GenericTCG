using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Classes
{
    public class NormalUnit : UnitCard
    {
        public NormalUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue)
            : base(id, title, type, faction, slug, attackValue)
        {
            Ability = MonsterAbility.None;
        }

        public override void OnDropEffect()
        {
            UpdateAttackForMoraleBoost();
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
