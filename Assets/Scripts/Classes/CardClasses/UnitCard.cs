using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using UnityEngine;

namespace Assets.Scripts
{

    public abstract class UnitCard : Card, IMonster
    {
        public int AttackValue { get; set; }

        public MonsterAbility Ability { get; set; }

        internal int originalAttack;

        public UnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug)
        {
            originalAttack = attackValue;
            this.AttackValue = attackValue;            
            this.Ability = ability;
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

        public override string ConstructCardData()
        {
            char modifiedAttack = this.AttackValue != originalAttack ? '*' : ' ';
            string data = "<color=#acb939><b> \t\t\t\t" + this.Title + "</b></color>" +
            "\n\nAttack Power: " + "<color=#e14c43><b>" + this.AttackValue + modifiedAttack + "</b></color>" +
            "\nType: " + "<color=#3770d2>" + this.Type + "</color>" +
            "\nAbility: " + "<color=#3770d2>" + this.Ability + "</color>";
            return data;
        }

        protected void AddCardToRow(UnitCard card)
        {
            ToRow.currentRow.CardsOnRow.Add(card);
            Debug.Log(ToRow.currentRow.CurrentRow.name + " has " + ToRow.currentRow.CardsOnRow.Count + " cards");
        }

        protected void UpdateAttackForMoraleBoost()
        {
            this.AttackValue += ToRow.currentRow.AbilityEffectOnRow.Count;
        }

        public virtual Transform PlayEnemyUnitCard()
        {
            string enemyRowName = "Enemy" + this.ToRow.name;
            base.ToRow = GameObject.Find(enemyRowName).GetComponent<DropZone>();

            OnDropEffect();

            return (GameObject.Find(enemyRowName).transform);
        }
    }
}
