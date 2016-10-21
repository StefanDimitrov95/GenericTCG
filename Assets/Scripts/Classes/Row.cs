using Assets.Scripts.Classes;
using Assets.Scripts.Classes.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<Card> CardsOnRow { get; set; }

        public List<MonsterAbility> AbilityEffectOnRow;

        public Row(string name)
        {
            AbilityEffectOnRow = new List<MonsterAbility>();
            CardsOnRow = new List<Card>();
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

            foreach (UnitCard unitCard in CardsOnRow)
            {
                totalPowerOfRow += unitCard.AttackValue;
            }

            RowAttackValue = totalPowerOfRow;
        }

        private void UpdateTextOfRow()
        {
            TextBoxOfRow.GetComponent<Text>().text = RowAttackValue.ToString();
        }     
        
        public void AddMoraleBoostToRow()
        {
            AbilityEffectOnRow.Add(MonsterAbility.MoraleBoost);
        }  
    }
}
