﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.CardClasses.Magic
{
    class Weather : MagicCard
    {
        public Weather(int id, string title, CardType type, Faction faction, string slug, Ability ability, AffectRow rowToAffect, string description)
            : base(id, title, type, faction, slug, ability, rowToAffect, description)
        {
        }

        public override void OnDropEffect()
        {
            string enemyRowName = this.ToRow.name.Contains("Enemy") ? this.ToRow.name.Substring(5) : "Enemy" + this.ToRow.name;
            DropZone enemyRow = GameObject.Find(enemyRowName).GetComponent<DropZone>();
            Debuff(this.ToRow);
            Debuff(enemyRow);
            ToRow.currentRow.AddWeatherEffectToRow();
        }

        public override void OnPlay()
        {
            string discardPileName = this.ToRow.name.Contains("Enemy") ? "EnemyDiscardPile" : "PlayerDiscardPile";
            DiscardPile dp = GameObject.Find(discardPileName).GetComponent<DiscardPile>();
            dp.AddToDiscardPile(this, this.ToRow.gameObject);
        }

        private void Debuff(DropZone row)
        {
            row.currentRow.AddWeatherEffectToRow();
            row.currentRow.SetAttackValueOfRow();
        }
    }
}
