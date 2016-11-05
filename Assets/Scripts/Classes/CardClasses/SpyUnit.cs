using Assets.Scripts.Classes.EnumClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class SpyUnit : UnitCard
    {
        public SpyUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
            : base(id, title, type, faction, slug, attackValue, ability)
        {          
            if (this.Type == CardType.CloseCombat)
            {
                base.ToRow = GameObject.Find("EnemyMeleeRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.RangedCombat)
            {
                base.ToRow = GameObject.Find("EnemyRangedRow").GetComponent<DropZone>();
            }
            if (this.Type == CardType.SiegeCombat)
            {
                base.ToRow = GameObject.Find("EnemySiegeRow").GetComponent<DropZone>();
            }
        }
        
        public override void OnDropEffect()
        {
            PlayerHand hand = GameObject.Find("Hand").GetComponent<PlayerHand>();
            SpyEffect(hand);
        }

        public void SpyEffect(IHand hand)
        {           
            hand.DrawExtraCards(2);

            UpdateAttackForMoraleBoost();
            AddCardToRow(this);
            base.ToRow.currentRow.SetAttackValueOfRow();
        }
     
        public override Transform PlayEnemyUnitCard()
        {      
            string spyRowName = this.ToRow.name.Substring(5);
            GameObject enemySpyRow = GameObject.Find(spyRowName);
            this.ToRow = enemySpyRow.GetComponent<DropZone>();

            EnemyHand ehnd = GameObject.Find("EnemyDeck").GetComponent<EnemyHand>();
            SpyEffect(ehnd);
            
            return enemySpyRow.transform;
        }
    }
}