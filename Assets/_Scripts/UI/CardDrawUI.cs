using UnityEngine;
using UnityEngine.UI;

public class CardDrawUI : Singleton<CardDrawUI> 
{
	public Text playerNameText;
	public Text cardIdText;
	public Text cardNameText;
	public Text cardTypeText;
	public Text cardDescriptiontText;
	public Text remainingBlessingsText;
	public Text remainingCursesText;

	public void UpdateDeckStatusUI (ServerCardDealer.DealerStatus dealerStatus, string playerName)
	{
		playerNameText.text = "Drawn By: "+playerName;

		cardIdText.text = "ID: "+ dealerStatus.lastCardId;
		cardNameText.text = "Last Card: "+dealerStatus.lastCardName;
		cardTypeText.text = "Type: "+dealerStatus.lastCardType;
		cardDescriptiontText.text = "Description: "+dealerStatus.lastCardDescription;

		remainingBlessingsText.text = "Remaining Blessings: "+dealerStatus.remainingBlessings.ToString();
		remainingCursesText.text = "Remaining Curses: "+dealerStatus.remainingCurses.ToString();

	}
}
