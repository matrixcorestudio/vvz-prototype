using UnityEngine;
using UnityEngine.UI;

public class CardDrawUI : Singleton<CardDrawUI> 
{
	public Text drawingPlayerName;
	public Text lastCardTypeText;
	public Text lastCardIndexText;
	public Text lastCardDescriptionText;
	public Text remainingBlessingsText;
	public Text remainingCursesText;

	public void UpdateDeckStatusUI (string cardType, int index, string description, int blessings, int curses, string playerName)
	{
		lastCardTypeText.text = cardType;
		lastCardIndexText.text = index.ToString();
		lastCardDescriptionText.text = description;
		remainingBlessingsText.text = blessings.ToString();
		remainingCursesText.text = curses.ToString();
		drawingPlayerName.text = playerName;
	}
}
