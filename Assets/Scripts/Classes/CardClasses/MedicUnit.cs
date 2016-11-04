using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    class MedicUnit : UnitCard
    {
        public MedicUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {

        }

        public override void OnDropEffect()
        {
            AddCardToRow(this);
            UpdateAttackForMoraleBoost();
            base.ToRow.currentRow.SetAttackValueOfRow();
            DiscardPile pile;

            if (this.ToRow.name.StartsWith("Enemy"))
            {
                pile = GameObject.Find("EnemyDiscardPile").GetComponent<DiscardPile>();
            }
            else
            {
                pile = GameObject.Find("PlayerDiscardPile").GetComponent<DiscardPile>();
            }

            if (!pile.CardPile.Any())
            {
                return;
            }

            KeyValuePair<Card, GameObject> returnedCard = pile.GetRandomCard();

            if (returnedCard.Key is UnitCard)
            {
                //returnedCard.Key.ToRow.currentRow.AddUnitCardToRow((UnitCard)returnedCard.Key);
                //returnedCard.Key.ToRow.currentRow.SetAttackValueOfRow();
                returnedCard.Key.OnRessuruct();
                returnedCard.Value.SetActive(true);
            }

            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
