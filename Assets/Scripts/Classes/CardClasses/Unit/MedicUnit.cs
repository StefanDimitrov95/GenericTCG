using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class MedicUnit : UnitCard
    {
        public MedicUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {

        }

        public override void OnDropEffect()
        {
            AddCardToRow(this);
            UpdateAttackForAbilitiesOnRow();
            base.ToRow.currentRow.SetAttackValueOfRow();
            DiscardPile pile;

            if (this.ToRow.name.Contains("Enemy"))
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
                returnedCard.Key.OnResurrect();
                returnedCard.Value.SetActive(true);
            }

            base.ToRow.currentRow.SetAttackValueOfRow();
        }
    }
}
