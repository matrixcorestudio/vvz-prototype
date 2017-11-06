using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Player;

public class ActionLogger : MonoBehaviour 
{
	void Start()
	{
		Player[] players = FindObjectsOfType(typeof(Player)) as Player[];
		foreach(Player player in players)
		{
			player.GetComponent<PlayerDiceRoll>().DiceRollEvent += LogAction;
			player.GetComponent<PlayerDrawCard>().drawCardEvent += LogAction;
		}
	}

	void LogAction(Player player, string action)
	{
		Debug.Log(player.name+ " -> "+action);
	}
}
