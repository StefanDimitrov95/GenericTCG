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

        public override string ConstructCardData()
        {
            char modifiedAttack = this.AttackValue != originalAttack ? '*' : ' ';
            string data = "<color=#acb939><b> \t\t\t\t" + this.Title + "</b></color>" +
            "\n\nAttack Power: " + "<color=#e14c43><b>" + this.AttackValue + modifiedAttack + "</b></color>" +
            "\nType: " + "<color=#3770d2>" + this.Type + "</color>" +
            "\nAbility: " + "<color=#3770d2>" + this.Ability + "</color>";
            return data;
        }

        public override void OnDropEffect()
        {
            PlayerHand hnd = GameObject.Find("Hand").GetComponent<PlayerHand>();
            hnd.DrawExtraCards(2);

            UpdateAttackForMoraleBoost();
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }

        public override Transform MoveToRow()
        {
            EnemyHand ehnd = GameObject.Find("EnemyDeck").GetComponent<EnemyHand>();
            ehnd.DrawExtraCards(2);

            string spyRowName = this.ToRow.name.Substring(5);
            GameObject enemySpyRow = GameObject.Find(spyRowName);
            this.ToRow = enemySpyRow.GetComponent<DropZone>();

            UpdateAttackForMoraleBoost();
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();

            return enemySpyRow.transform;
        }
    }
}
