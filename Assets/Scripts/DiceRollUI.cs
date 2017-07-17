using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollUI : MonoBehaviour {

	[SerializeField] GameObject rollPanel;
	[SerializeField] Text vikingRollText;
	[SerializeField] Text zombieRollText;
	[SerializeField] Text diceRollInfoText;
	[SerializeField] float randomizerTime = 0.2f;

	static DiceRollUI instance;
	public static DiceRollUI Instance{get {return instance;}}
	public int lastVikingRollValue = 0;
	public int lastZombieRollValue = 0;
	public string lastDiceType = "";

	void Awake ()
	{
		instance = this;
	}

	public void RollDice (int vRoll, int zRoll)
	{
		rollPanel.SetActive(true);
		StartCoroutine(VisualRandomizerRoutine(vRoll,zRoll));
	}

	public void UpdateInfo ()
	{
		diceRollInfoText.text = "Last Viking roll value: "+ lastVikingRollValue+"; "+
			"Last Zombie roll value: " + lastZombieRollValue+"; "+
			"With dice type: " + lastDiceType;
	}

	IEnumerator VisualRandomizerRoutine (int vRoll, int zRoll)
	{
		float elapsedTime = 0;
		while (elapsedTime < randomizerTime)
		{
			yield return new WaitForSeconds(0.1f);
			vikingRollText.text = Random.Range(1,12).ToString();
			zombieRollText.text = Random.Range(1,12).ToString();

			elapsedTime += Time.deltaTime;
		}
		vikingRollText.text = vRoll.ToString();
		zombieRollText.text = zRoll.ToString();
	}
}
