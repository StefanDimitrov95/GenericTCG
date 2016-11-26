using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using System;
using UnityEngine;

namespace Assets.Scripts
{

    public abstract class UnitCard : Card, IMonster
    {
        public int AttackValue { get; set; }

        public Ability Ability { get; set; }

        internal int originalAttack;

        public UnitCard(int id, string title, CardType type, Faction faction, string slug, int attackValue, Ability ability)
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

        public virtual void OnDeath()
        {
            this.AttackValue = this.originalAttack;
            GameObject unitCardObject = GameObject.Find(String.Format("{0},{1}", this.ID, this.Title));
            unitCardObject.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
            this.ToRow.GetComponent<DropZone>().currentRow.RemoveUnitCardFromRow(this);
        }

        public override string ConstructCardData()
        {
            char modifiedAttack = this.AttackValue != originalAttack ? '*' : ' ';
            string data = "<color=#acb939><b> \t\t" + this.Title + "</b></color>" +
            "\n\nAttack Power: " + "<color=#e14c43><b>" + this.AttackValue + modifiedAttack + "</b></color>" +
            "\nType: " + "<color=#3770d2>" + this.Type + "</color>" +
            "\nAbility: " + "<color=#3770d2>" + this.Ability + "</color>";
            return data;
        }
        
        public int AttackChanged()
        {
            return this.AttackValue.CompareTo(this.originalAttack);
        }

        public override void OnResurrect()
        {
            OnDropEffect();
        }

        public virtual void UpdateAttackForAbilitiesOnRow()
        {
            UpdateAttackForWeatherEffect();
            UpdateAttackForMoraleBoost();
        }

        protected void AddCardToRow(UnitCard card)
        {
            ToRow.currentRow.AddUnitCardToRow(card);
            Debug.Log(ToRow.currentRow.ToString());
        }

        protected void UpdateAttackForMoraleBoost()
        {
            this.AttackValue += ToRow.currentRow.AbilityEffectOnRow.Count;
        }

        protected void UpdateAttackForWeatherEffect()
        {
            if (ToRow.currentRow.IsWeatherEffectOnRow())
            {
                this.AttackValue = 1;
            }
        }
    }
}