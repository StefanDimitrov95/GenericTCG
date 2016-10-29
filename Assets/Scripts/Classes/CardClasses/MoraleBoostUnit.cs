﻿using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class MoraleBoostUnit : UnitCard
    {
        public MoraleBoostUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue)
            : base(id, title, type, faction, slug, attackValue)
        {
            Ability = MonsterAbility.MoraleBoost;
        }

        public override string ConstructCardData()
        {
            char modifiedAttack = this.AttackValue != originalAttack ? '*' : ' ';
            string data= "<color=#acb939><b> \t\t\t\t" + this.Title + "</b></color>" +
            "\n\nAttack Power: " + "<color=#e14c43><b>" + this.AttackValue + modifiedAttack + "</b></color>" +
            "\nType: " + "<color=#3770d2>" + this.Type + "</color>" +
            "\nAbility: " + "<color=#3770d2>" + this.Ability + "</color>";
            return data;
        }

        public override void OnDropEffect()
        {                                
            for (int i = 0; i < ToRow.currentRow.CardsOnRow.Count; i++)
            {
               (ToRow.currentRow.CardsOnRow[i] as UnitCard).AttackValue++;
            }

            AddCardToRow(this);
            UpdateAttackForMoraleBoost();
            base.ToRow.currentRow.AddMoraleBoostToRow();
            base.ToRow.currentRow.SetAttackValueOfRow();
        }

        public override Transform MoveToRow()
        {
            string enemyRowName = "Enemy" + this.ToRow.name;
            base.ToRow = GameObject.Find(enemyRowName).GetComponent<DropZone>();

            OnDropEffect();

            return (GameObject.Find(enemyRowName).transform);
        }
    }
}
