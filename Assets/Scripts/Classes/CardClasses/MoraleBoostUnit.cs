using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class MoraleBoostUnit : UnitCard
    {
        public MoraleBoostUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
            
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
