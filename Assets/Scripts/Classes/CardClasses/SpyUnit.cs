using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class SpyUnit : UnitCard
    {
        public SpyUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue)
            : base(id, title, type, faction, slug, attackValue)
        {
            Ability = MonsterAbility.Spy;

            if (this.Type == CardType.CloseCombat)
            {
                base.ToRow = GameObject.Find("EnemyMeleeRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.RangedCombat)
            {
                base.ToRow = GameObject.Find("EnemyRangedRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.SiegeCombat)
            {
                base.ToRow = GameObject.Find("EnemySiegeRow").GetComponent<DropZone>();
            }
        }      

        public override void OnDropEffect()
        {
            PlayerHand hnd = GameObject.Find("Hand").GetComponent<PlayerHand>();
            hnd.DrawExtraCards(2);

            UpdateAttackForMoraleBoost();
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
