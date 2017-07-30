using UnityEngine;
using UnityEngine.UI;

public class CardDrawUI : MonoBehaviour 
{
	static CardDrawUI instance;
	public static CardDrawUI Instance{get {return instance;}}
	public Text drawingPlayerName;
	public Text lastCardTypeText;
	public Text lastCardIndexText;
	public Text lastCardDescriptionText;
	public Text remainingBlessingsText;
	public Text remainingCursesText;

	void Awake ()
	{
		instance = this;
	}

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
