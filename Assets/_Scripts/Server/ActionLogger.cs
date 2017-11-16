using UnityEngine;
using Prototype.Player;

public class ActionLogger : MonoBehaviour 
{
	void Start()
	{
		Player[] players = FindObjectsOfType(typeof(Player)) as Player[];
		foreach(Player player in players)
		{
            PlayerDrawCard cardDrawer = player.GetComponent<PlayerDrawCard>();
            if (cardDrawer != null)
            {
                cardDrawer.DrawCardEvent += LogAction;
            }
        }
	}

	void LogAction(Player player, string action)
	{
		Debug.Log(player.name+ " -> "+action);
	}
}
