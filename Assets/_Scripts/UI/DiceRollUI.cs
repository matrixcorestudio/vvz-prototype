using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;

public class DiceRollUI : Singleton<DiceRollUI> 
{
	[SerializeField] GameObject rollPanel;
	[SerializeField] Text rollTypeText;
	[SerializeField] Text rollValueText;
	[SerializeField] Text rollStatusText;
    [SerializeField] Text[] charNameTexts;
	[SerializeField] float randomizerTime = 0.2f;

	int lastRollValue = 0;
	string lastRollType = string.Empty;
    bool isSingle;

    private void Start()
    {
        rollStatusText.text = string.Empty;
        rollTypeText.text = string.Empty;
        rollValueText.text = string.Empty;
        for (int i = 0; i < charNameTexts.Length; ++i)
        {
            charNameTexts[i].text = string.Empty;
        }
    }

    public void RollDice (int rollvalue, string diceType, Enums.RollType rollType)
	{
		lastRollValue = rollvalue;
		lastRollType = diceType;
		rollPanel.SetActive(true);
		rollTypeText.text = rollType.ToString();
		StartCoroutine(VisualRandomizerRoutine(rollvalue));
		UpdateRollStatusUI();
	}

	void UpdateRollStatusUI ()
	{
		rollStatusText.text = "Last roll value: "+ lastRollValue+"; "+
			"With dice type: " + lastRollType+"; "+
			"By Player: "+ rollTypeText.text;
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
