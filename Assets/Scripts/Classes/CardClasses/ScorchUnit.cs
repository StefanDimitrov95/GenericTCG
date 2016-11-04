﻿using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    class ScorchUnit : UnitCard
    {
        public ScorchUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {

        }

        public override void OnDropEffect()
        {
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
            GameObject enemyRowObj;
            DiscardPile pile;
            if (this.ToRow.name.StartsWith("Enemy"))
            {
                enemyRowObj = GameObject.Find(this.ToRow.name.Substring(5));
                pile = GameObject.Find("PlayerDiscardPile").GetComponent<DiscardPile>();
            }
            else
            {
                enemyRowObj = GameObject.Find("Enemy" + this.ToRow.name);
                pile = GameObject.Find("EnemyDiscardPile").GetComponent<DiscardPile>();
            }
            DropZone enemyRow = enemyRowObj.GetComponent<DropZone>();
            List<UnitCard> strongestCards = enemyRow.GetStrongestUnitCards();

            if (!strongestCards.Any())
            {
                return;
            }

            foreach (UnitCard card in strongestCards)
            {
                pile.AddToDiscardPile(card, enemyRowObj);
                enemyRow.currentRow.RemoveUnitCardFromRow(card);
            }
            UpdateAttackForMoraleBoost();
            enemyRow.currentRow.SetAttackValueOfRow();
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
