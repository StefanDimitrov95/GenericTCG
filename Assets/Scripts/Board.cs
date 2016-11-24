using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.Utils;

public class Board : MonoBehaviour
{
	public GameObject playerMeleeRow;
	public GameObject playerRangedRow;
	public GameObject playerSiegeRow;
	public GameObject enemyMeleeRow;
	public GameObject enemyRangedRow;
	public GameObject enemySiegeRow;
	public GameObject playerAttackLabel;
	public GameObject enemyAttackLabel;
    public GameObject playerTurnsLeftText;
    public GameObject enemyTurnsLeftText;
    public GameObject playerPassedTurnPanel;
    public GameObject enemyPassedTurnPanel;

    [HideInInspector]
    public byte playerTurnsLeft;
    [HideInInspector]
    public byte enemyTurnsLeft;

	void Start ()
	{
        playerTurnsLeft = 2;
        enemyTurnsLeft = 2;
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
		rowsAttackValue.Add(int.Parse(playerMeleeRow.GetComponent<Text>().text));
		rowsAttackValue.Add(int.Parse(playerRangedRow.GetComponent<Text>().text));
		rowsAttackValue.Add(int.Parse(playerSiegeRow.GetComponent<Text>().text));

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
		rowsAttackValue.Add(int.Parse(enemyMeleeRow.GetComponent<Text>().text));
		rowsAttackValue.Add(int.Parse(enemyRangedRow.GetComponent<Text>().text));
		rowsAttackValue.Add(int.Parse(enemySiegeRow.GetComponent<Text>().text));

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
}
