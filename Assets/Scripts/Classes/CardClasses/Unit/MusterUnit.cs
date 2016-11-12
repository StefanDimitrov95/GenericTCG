using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes
{
    class MusterUnit : UnitCard
    {
        public MusterUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {
        }

        public override void OnDropEffect()
        {
            AddCardToRow(this);

            if (base.ToRow.name.StartsWith("Enemy"))
            {
                MusterEffect(true);
            }

            else
            {
                MusterEffect(false);
            }

            base.ToRow.currentRow.SetAttackValueOfRow();
        }

        private void MusterEffect(bool isEnemy)
        {
            List<UnitCard> namesakes = GetMusterCards(isEnemy);

            if (isEnemy)
            {
                foreach (UnitCard card in namesakes)
                {
                    Board.Instantiate(card, GameObject.Find("Enemy" + card.ToRow.name).transform);
                    AddCardToRow(card);
                }
            }

            else
            {
                foreach (UnitCard card in namesakes)
                {
                    Board.Instantiate(card, card.ToRow.transform);
                    AddCardToRow(card);
                }
            }
        }

        private List<UnitCard> GetMusterCards(bool isEnemy)
        {
            string musterTag = this.Title.Split(':')[0];
            IDeck deck = isEnemy ? (IDeck)GameObject.Find("EnemyDeck").GetComponent<EnemyDeck>() : GameObject.Find("Deck").GetComponent<PlayerDeck>();
                                   
            List<Card> namesakes = deck.Deck.Where(c => c.Title.StartsWith(musterTag)).ToList();
            deck.Deck.RemoveAll(i => namesakes.Contains(i));

            return namesakes.OfType<UnitCard>().ToList();
        }
    }
}
