using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Row
    {
        public GameObject CurrentRow { get; set; }

        public RowClass Class { get; set; }

        public int RowAttackValue { get; set; }

        public GameObject TextBoxOfRow { get; set; }

        public List<UnitCard> cardsOnRow { get; private set; }

        public List<Ability> AbilityEffectOnRow;

        private bool isDebuffed = false;

        public Row(string name)
        {
            AbilityEffectOnRow = new List<Ability>();
            cardsOnRow = new List<UnitCard>();
            RowAttackValue = 0;
            switch (name)
            {
                case "MeleeRow":
                    {
                        Class = RowClass.Melee;
                        TextBoxOfRow = GameObject.Find("MeleeRowValue");
                        CurrentRow = GameObject.Find("MeleeRow");
                        break;
                    }
                case "RangedRow":
                    {
                        Class = RowClass.Ranged;
                        TextBoxOfRow = GameObject.Find("RangedRowValue");
                        CurrentRow = GameObject.Find("RangedRow");
                        break;
                    }
                case "SiegeRow":
                    {
                        Class = RowClass.Siege;
                        TextBoxOfRow = GameObject.Find("SiegeRowValue");
                        CurrentRow = GameObject.Find("SiegeRow");
                        break;
                    }
                case "EnemyMeleeRow":
                    {
                        Class = RowClass.EnemyMelee;
                        TextBoxOfRow = GameObject.Find("EnemyMeleeRowValue");
                        CurrentRow = GameObject.Find("EnemyMeleeRow");
                        break;
                    }
                case "EnemyRangedRow":
                    {
                        Class = RowClass.EnemyRanged;
                        TextBoxOfRow = GameObject.Find("EnemyRangedRowValue");
                        CurrentRow = GameObject.Find("EnemyRangedRow");
                        break;
                    }
                case "EnemySiegeRow":
                    {
                        Class = RowClass.EnemySiege;
                        TextBoxOfRow = GameObject.Find("EnemySiegeRowValue");
                        CurrentRow = GameObject.Find("EnemySiegeRow");
                        break;
                    }
                case "Hand":
                    {
                        break;
                    }

                default:
                    break;
            }
        }

        public void AddUnitCardToRow(UnitCard unit)
        {
            cardsOnRow.Add(unit);
        }

        public bool RemoveUnitCardFromRow(UnitCard unit)
        {
            return cardsOnRow.Remove(unit);
        }

        public List<UnitCard> GetCardsByNameFromRow(string title, Ability ability)
        {
            List<UnitCard> cards = new List<UnitCard>();
            cards = cardsOnRow.FindAll(x => x.Title == title && x.Ability == ability);
            return cards;
        }

        public List<UnitCard> GetStrongestUnitCards()
        {
            IEnumerable<UnitCard> sortedByAttack = cardsOnRow.OrderByDescending(x => x.AttackValue);
            return cardsOnRow.OrderByDescending(x => x.AttackValue).Where(x => (sortedByAttack.FirstOrDefault().AttackValue == x.AttackValue)).ToList();
        }

        //public void UpdateAttackValueOfRow(Card draggedCard)
        //{
        //    if (draggedCard.Type != CardType.Special)
        //    {
        //        int draggedCardAttackValue = (draggedCard as UnitCard).AttackValue;
        //        RowAttackValue += draggedCardAttackValue;
        //        UpdateTextOfRow();
        //        Debug.Log(TextBoxOfRow.GetComponent<Text>().text);
        //    }
        //}

        public void SetAttackValueOfRow()
        {
            UpdateRowAttackValue();
            UpdateTextOfRow();
        }

        private void UpdateRowAttackValue()
        {
            int totalPowerOfRow = 0;

            foreach (UnitCard unitCard in cardsOnRow)
            {
                totalPowerOfRow += unitCard.AttackValue;
            }

            RowAttackValue = totalPowerOfRow;
        }

        private void UpdateTextOfRow()
        {
            TextBoxOfRow.GetComponent<Text>().text = RowAttackValue.ToString();
        }

        public void AddMoraleBoostToRow(UnitCard droppedCard)
        {
            AbilityEffectOnRow.Add(Ability.MoraleBoost);
            for (int i = 0; i < cardsOnRow.Count; i++)
            {
                if (cardsOnRow[i] != droppedCard)
                {
                    cardsOnRow[i].AttackValue++;
                }
            }
        }

        public void AddWeatherEffectToRow()
        {
            isDebuffed = true;
            cardsOnRow.ForEach(card => card.AttackValue = 1);
        }

        public override string ToString()
        {
            return CurrentRow.name + " has " + cardsOnRow.Count + " cards";
        }

        public bool RemoveEffectFromRow(Ability effect)
        {
            for (int i = 0; i < cardsOnRow.Count; i++)
            {
                cardsOnRow[i].AttackValue--;
            }
            return AbilityEffectOnRow.Remove(effect);
        }
    }
}
