using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts
{

    public abstract class UnitCard : Card, IMonster
    {
        public int AttackValue { get; set; }

        public MonsterAbility Ability { get; set; }

        public UnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue)
            : base(id, title, type, faction, slug)
        {
            this.AttackValue = attackValue;            
            //this.Ability = ability;
            FindToRowField();
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", {0}, {1}", AttackValue, Ability.ToString());
        }

        private void FindToRowField()
        {
            if (this.Type == CardType.CloseCombat)
            {
                base.ToRow = GameObject.Find("MeleeRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.RangedCombat)
            {
                base.ToRow = GameObject.Find("RangedRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.SiegeCombat)
            {
                base.ToRow = GameObject.Find("SiegeRow").GetComponent<DropZone>();
            }
        }

        protected void AddCardToRow(Card card)
        {
            ToRow.currentRow.CardsOnRow.Add(card);
            Debug.Log(ToRow.currentRow.CurrentRow.name + " has " + ToRow.currentRow.CardsOnRow.Count + " cards");
        }

        protected void UpdateAttackForMoraleBoost()
        {
            this.AttackValue += ToRow.currentRow.AbilityEffectOnRow.Count;
        }
    }
}
