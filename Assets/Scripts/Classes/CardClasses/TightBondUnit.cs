using Assets.Scripts.Classes.EnumClasses;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using Assets.Scripts.Classes;
using UnityEngine;

public class TightBondUnit : UnitCard
{

    public TightBondUnit(int id, string title, CardType type, Faction faction, string slug, int attackValue, MonsterAbility ability)
        :base(id, title, type, faction, slug, attackValue, ability)
    {
       
    }
  
    public override void OnDropEffect()
    {
        AddCardToRow(this);
        UpdateAttackForMoraleBoost();
        UpdateAttackForTightBond();
        base.ToRow.currentRow.SetAttackValueOfRow();
    }

    public override Transform MoveToRow()
    {
        string enemyRowName = "Enemy" + this.ToRow.name;
        base.ToRow = GameObject.Find(enemyRowName).GetComponent<DropZone>();

        OnDropEffect();

        return (GameObject.Find(enemyRowName).transform);
    }

    private void UpdateAttackForTightBond()
    {
        List<UnitCard> foundCards = ToRow.GetCardsByNameFromRow(this.Title, this.Ability);
        if (foundCards.Count > 1)
        {
            foreach (UnitCard card in foundCards)
            {
                if (card.Ability == MonsterAbility.TightBond)
                {
                    card.AttackValue = (foundCards.Count) * card.originalAttack;
                }
            }
        }
    }
}
