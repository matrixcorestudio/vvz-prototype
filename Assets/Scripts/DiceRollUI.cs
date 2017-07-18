using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollUI : MonoBehaviour {

	static DiceRollUI instance;
	public static DiceRollUI Instance{get {return instance;}}

	[SerializeField] GameObject rollPanel;
	[SerializeField] Text rollingPlayerNameText;
	[SerializeField] Text rollValueText;
	[SerializeField] Text rollStatusText;
	[SerializeField] float randomizerTime = 0.2f;

	int lastRollValue = 0;
	string lastDiceType = "";

	void Awake ()
	{
		instance = this;
	}

	public void RollDice (int rollvalue, string diceType, string playerName)
	{
		lastRollValue = rollvalue;
		lastDiceType = diceType;
		rollPanel.SetActive(true);
		rollingPlayerNameText.text = playerName;
		StartCoroutine(VisualRandomizerRoutine(rollvalue));
		UpdateRollStatusUI();
	}

	void UpdateRollStatusUI ()
	{
		rollStatusText.text = "Last roll value: "+ lastRollValue+"; "+
			"With dice type: " + lastDiceType+"; "+
			"By Player: "+ rollingPlayerNameText.text;
	}

	IEnumerator VisualRandomizerRoutine (int rollvalue)
	{
		float elapsedTime = 0;
		while (elapsedTime < randomizerTime)
		{
			yield return new WaitForSeconds(0.1f);
			rollValueText.text = Random.Range(1,12).ToString();
			elapsedTime += Time.deltaTime;
		}
		rollValueText.text = rollvalue.ToString();
	}
}
