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

	void Start ()
	{
	  
	}
	
	public void UpdateAttackLabels()
	{
		UpdatePlayerAttackLabel();
		UpdateEnemyAttackLabel();
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
}
