using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Transform cardPrefab = enemyRowObj.transform.FindChild(card.ID + "," + card.Title);
                pile.AddToDiscardPile(cardPrefab.gameObject);
                enemyRow.currentRow.CardsOnRow.Remove(card);
            }

            enemyRow.currentRow.SetAttackValueOfRow();
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
