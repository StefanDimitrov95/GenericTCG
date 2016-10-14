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
        public Class RowClass { get; set; }

        public int RowAttackValue { get; set; }

        public GameObject TextBoxOfRow { get; set; }

        public Row(string name)
        {
            RowAttackValue = 0;
            switch (name)
            {
                case "MeleeRow":
                    {
                        RowClass = Class.Melee;
                        TextBoxOfRow = GameObject.Find("MeleeRowValue"); 
                        break;
                    }
                case "RangedRow":
                    {
                        RowClass = Class.Ranged;
                        TextBoxOfRow = GameObject.Find("RangedRowValue");
                        break;
                    }
                case "SiegeRow":
                    {
                        RowClass = Class.Siege;
                        TextBoxOfRow = GameObject.Find("SiegeRowValue");
                        break;
                    }
                case "EnemyMeleeRow":
                    {
                        RowClass = Class.EnemyMelee;
                        TextBoxOfRow = GameObject.Find("EnemyMeleeRowValue");
                        break;
                    }
                case "EnemyRangedRow":
                    {
                        RowClass = Class.EnemyRanged;
                        TextBoxOfRow = GameObject.Find("EnemyRangedRowValue");
                        break;
                    }
                case "EnemySiegeRow":
                    {
                        RowClass = Class.EnemySiege;
                        TextBoxOfRow = GameObject.Find("EnemySiegeRowValue");
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

        public void UpdateAttackValueOfRow(Card draggedCard)
        {
            if (draggedCard.Type == "MonsterCard")
            {
                int draggedCardAttackValue = (draggedCard as MonsterCard).AttackValue;
                RowAttackValue += draggedCardAttackValue;
                TextBoxOfRow.GetComponent<Text>().text = RowAttackValue.ToString();
                Debug.Log(TextBoxOfRow.GetComponent<Text>().text);
            }
        }
    }
}
