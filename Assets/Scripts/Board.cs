using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.Utils;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject playerMeleeRowText;
    public GameObject playerRangedRowText;
    public GameObject playerSiegeRowText;
    public GameObject enemyMeleeRowText;
    public GameObject enemyRangedRowText;
    public GameObject enemySiegeRowText;
    public GameObject playerAttackLabel;
    public GameObject enemyAttackLabel;
    public GameObject playerTurnsLeftText;
    public GameObject enemyTurnsLeftText;
    public GameObject playerPassedTurnPanel;
    public GameObject enemyPassedTurnPanel;
    public GameObject playerMeleeRow;
    public GameObject playerRangedRow;
    public GameObject playerSiegeRow;
    public GameObject enemyMeleeRow;
    public GameObject enemyRangedRow;
    public GameObject enemySiegeRow;
    public GameObject playerDiscardPile;
    public GameObject enemyDiscardPile;

    [HideInInspector]
    public byte playerTurnsLeft;
    [HideInInspector]
    public byte enemyTurnsLeft;

	void Start ()
	{
        playerTurnsLeft = 2;
        enemyTurnsLeft = 2;
	}

    public void ResetCardsOnBoard()
    {
        ResetEachSide(playerMeleeRow, playerRangedRow, playerSiegeRow, playerDiscardPile);
        ResetEachSide(enemyMeleeRow, enemyRangedRow, enemySiegeRow, enemyDiscardPile);
    }

    private void ResetEachSide(GameObject meleeRow, GameObject rangedRow, GameObject siegeRow, GameObject discardPile)
    {
        List<UnitCard> cardsOnMeleeRow = meleeRow.GetComponent<DropZone>().currentRow.cardsOnRow;
        List<UnitCard> cardsOnRangedRow = rangedRow.GetComponent<DropZone>().currentRow.cardsOnRow;
        List<UnitCard> cardsOnSiegeRow = siegeRow.GetComponent<DropZone>().currentRow.cardsOnRow;

        AddCardsFromRowToDiscardPile(discardPile, cardsOnMeleeRow);
        AddCardsFromRowToDiscardPile(discardPile, cardsOnRangedRow);
        AddCardsFromRowToDiscardPile(discardPile, cardsOnSiegeRow);

        meleeRow.GetComponent<DropZone>().ResetRow();
        rangedRow.GetComponent<DropZone>().ResetRow();
        siegeRow.GetComponent<DropZone>().ResetRow();

        meleeRow.GetComponent<DropZone>().currentRow.SetAttackValueOfRow();
        rangedRow.GetComponent<DropZone>().currentRow.SetAttackValueOfRow();
        siegeRow.GetComponent<DropZone>().currentRow.SetAttackValueOfRow();

        UpdateAttackLabels();
    }

    private void AddCardsFromRowToDiscardPile(GameObject discardPile, List<UnitCard> cardsOnRow)
    {
        for (int i = 0; i < cardsOnRow.Count; i++)
        {
            discardPile.GetComponent<DiscardPile>().AddToDiscardPile(cardsOnRow[i]);
        }
    }

    public void UpdateAttackLabels()
    {
        UpdatePlayerAttackLabel();
        UpdateEnemyAttackLabel();
    }

    public int GetPlayerTotalAttack()
    {
        return int.Parse(playerAttackLabel.GetComponent<Text>().text);
    }

    public int GetEnemyTotalAttack()
    {
        return int.Parse(enemyAttackLabel.GetComponent<Text>().text);
    }

    private void UpdatePlayerAttackLabel()
    {
        List<int> rowsAttackValue = new List<int>();
        rowsAttackValue.Add(int.Parse(playerMeleeRowText.GetComponent<Text>().text));
        rowsAttackValue.Add(int.Parse(playerRangedRowText.GetComponent<Text>().text));
        rowsAttackValue.Add(int.Parse(playerSiegeRowText.GetComponent<Text>().text));

        int totalValue = 0;
        foreach (int value in rowsAttackValue)
        {
            totalValue += value;
        }

        playerAttackLabel.GetComponent<Text>().text = totalValue.ToString();
    }

    private void UpdateEnemyAttackLabel()
    {
        List<int> rowsAttackValue = new List<int>();
        rowsAttackValue.Add(int.Parse(enemyMeleeRowText.GetComponent<Text>().text));
        rowsAttackValue.Add(int.Parse(enemyRangedRowText.GetComponent<Text>().text));
        rowsAttackValue.Add(int.Parse(enemySiegeRowText.GetComponent<Text>().text));

        int totalValue = 0;
        foreach (int value in rowsAttackValue)
        {
            totalValue += value;
        }

        enemyAttackLabel.GetComponent<Text>().text = totalValue.ToString();
    }

    public static void Instantiate(Card cardToBeInstanciated, Transform rowToInstantiateOn)
    {
        GameObject cardObj = Instantiate(Resources.Load("Card", typeof(GameObject))) as GameObject;
        Extensions.Instantiate(cardObj, cardToBeInstanciated, rowToInstantiateOn);
    }

    public void UpdatePlayerTurnsText()
    {
        playerTurnsLeftText.GetComponent<Text>().text = string.Format("{0}/2", playerTurnsLeft.ToString());
    }

    public void UpdateEnemyTurnsText()
    {
        enemyTurnsLeftText.GetComponent<Text>().text = string.Format("{0}/2", enemyTurnsLeft.ToString());
    }

    public void ActivatePlayerPassPanel()
    {
        playerPassedTurnPanel.SetActive(true);
    }

    public void DeactivatePlayerPassPanel()
    {
        playerPassedTurnPanel.SetActive(false);
    }

    public void ActivateComputerPassPanel()
    {
        enemyPassedTurnPanel.SetActive(true);
    }

    public void DeactivateComputerPassPanel()
    {
        enemyPassedTurnPanel.SetActive(false);
    }

    public List<UnitCard> GetStrongestUnitsOnPlayerBoard()
    {
        List<UnitCard> strongUnits = new List<UnitCard>();

        List<UnitCard> meleeUnits = GetStrongestUnitsOnMeleeRow();
        List<UnitCard> rangedUnits = GetStrongestUnitsOnRangedRow();
        List<UnitCard> siegeUnits = GetStrongestUnitsOnSiegeRow();

        if (meleeUnits.Any())
        {
            strongUnits.AddRange(meleeUnits);
        }
        if (rangedUnits.Any())
        {
            strongUnits.AddRange(rangedUnits);
        }
        if (siegeUnits.Any())
        {
            strongUnits.AddRange(siegeUnits);
        }

        return strongUnits;
    }

    private List<UnitCard> GetStrongestUnitsOnMeleeRow()
    {
        List<UnitCard> meleeUnits = new List<UnitCard>();
        foreach (UnitCard unit in GameObject.Find("MeleeRow").GetComponent<DropZone>().currentRow.GetStrongestUnitCards())
        {
            if (unit.AttackValue > 10)
            {
                meleeUnits.Add(unit);
            }
        }

        return meleeUnits;
    }

    private List<UnitCard> GetStrongestUnitsOnRangedRow()
    {
        List<UnitCard> rangedUnits = new List<UnitCard>();
        foreach (UnitCard unit in GameObject.Find("RangedRow").GetComponent<DropZone>().currentRow.GetStrongestUnitCards())
        {
            if (unit.AttackValue > 10)
            {
                rangedUnits.Add(unit);
            }
        }
        return rangedUnits;
    }

    private List<UnitCard> GetStrongestUnitsOnSiegeRow()
    {
        List<UnitCard> siegeUnits = new List<UnitCard>();
        foreach (UnitCard unit in GameObject.Find("SiegeRow").GetComponent<DropZone>().currentRow.GetStrongestUnitCards())
        {
            if (unit.AttackValue > 10)
            {
                siegeUnits.Add(unit);
            }
        }
        return siegeUnits;
    }
}
