using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;

public class DiceRollUI : Singleton<DiceRollUI> 
{
	[SerializeField] GameObject rollPanel;
	[SerializeField] Text rollTypeText;
	[SerializeField] Text rollStatusText;
    [SerializeField] Text[] charNameTexts;
    [SerializeField] Text[] rollValueTexts;
    [SerializeField] float randomizerTime = 0.2f;

	int lastRollValue = 0;
    int[] lastRollValues = new int[4];
    string lastRollType = string.Empty;
	string lastDiceType = string.Empty;
    string[] lastDiceTypes = new string[4];

    private void Start()
    {
        rollStatusText.text = string.Empty;
        rollTypeText.text = string.Empty;
        for (int i = 0; i < charNameTexts.Length; ++i)
        {
            charNameTexts[i].text = string.Empty;
            rollValueTexts[i].text = string.Empty;
            lastDiceTypes[i] = string.Empty;
            lastRollValues[i] = 0;
        }
    }

    public void RollSingleDice (int rollValue, string diceType)
	{
		lastRollValue = rollValue;
		lastDiceType = diceType;
		rollPanel.SetActive(true);
        rollTypeText.text = Enums.RollType.SingleRoll.ToString();
        lastRollType = Enums.RollType.SingleRoll.ToString();
        charNameTexts[0].text = "Single Roll (" + diceType + ") : ";
        for (int i = 1; i < charNameTexts.Length; ++i)
        {
            charNameTexts[i].text = string.Empty;
            rollValueTexts[i].text = string.Empty;
        }
        StartCoroutine(SingleVisualRandomizerRoutine());
	}

    public void RollMultipleDice(int[] rollValues, string[] diceTypes, string[] charNames, Enums.RollType rollType)
    {
        lastRollValues = rollValues;
        lastDiceTypes = diceTypes;
        rollPanel.SetActive(true);
        rollTypeText.text = rollType.ToString();
        lastRollType = rollType.ToString();
        for (int i = 0; i < charNameTexts.Length; ++i)
        {
            charNameTexts[i].text = charNames[i] + " (" + diceTypes[i] + ") : ";
        }
        StartCoroutine(MultipleVisualRandomizerRoutine());
    }

	void UpdateRollStatusUI (bool isMultipleRoll)
	{
        if (isMultipleRoll)
        {
            rollStatusText.text = "Last roll values: ";
            for (int i = 0; i < rollValueTexts.Length; ++i)
            {
                rollValueTexts[i].text = (lastRollValues[i] < 0 ? "None" : lastRollValues[i].ToString());
                if (lastDiceTypes[i] == Enums.DiceType.CoinFlip.ToString())
                {
                    rollValueTexts[i].text = (lastRollValues[i] == 2 ? "Good" : "Bad");
                }
                rollStatusText.text += rollValueTexts[i].text + ", ";

                if (rollTypeText.text == Enums.RollType.VikingRoll.ToString())
                {
                    HUDUISingleton.Instance.UpdateMovementValue(i, rollValueTexts[i].text);
                }
                else
                {
                    HUDUISingleton.Instance.UpdateMovementValue(i + 4, rollValueTexts[i].text);
                }
            }
            rollStatusText.text += "with roll type: " + lastRollType;            
        }
        else
        {
            rollValueTexts[0].text = (lastRollValue < 0 ? "None" : lastRollValue.ToString());
            if (lastDiceType == Enums.DiceType.CoinFlip.ToString())
            {
                rollValueTexts[0].text = (lastRollValue == 2 ? "Good" : "Bad");
            }
            for (int i = 1; i < rollValueTexts.Length; ++i)
            {
                rollValueTexts[i].text = string.Empty;
            }
            rollStatusText.text = string.Format("Last roll value: {0}, with dice type: {1}, with " +
                "roll type: {2}", rollValueTexts[0].text, lastDiceType, lastRollType);
        }
	}

	IEnumerator SingleVisualRandomizerRoutine ()
	{
		float elapsedTime = 0;
		while (elapsedTime < randomizerTime)
		{
			yield return new WaitForSeconds(0.1f);
			rollValueTexts[0].text = Random.Range(1,12).ToString();
			elapsedTime += Time.deltaTime;
		}
        UpdateRollStatusUI(false);
    }

    IEnumerator MultipleVisualRandomizerRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < randomizerTime)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < rollValueTexts.Length; ++i)
            {
                rollValueTexts[i].text = Random.Range(1, 12).ToString(); 
            }
            elapsedTime += Time.deltaTime;
        }
        UpdateRollStatusUI(true);
    }
}
