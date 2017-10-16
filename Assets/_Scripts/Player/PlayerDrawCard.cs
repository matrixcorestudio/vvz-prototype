﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.Player;

[RequireComponent(typeof(Player))]
public class PlayerDrawCard : NetworkBehaviour 
{
	int xpos = 10;
	int ypos = 310;
	int buttonWidth = 80;
	int buttonHeight = 20;

	Player player;

	void Start ()
	{
		player = GetComponent<Player>();
	}

	[ClientCallback]
	void OnGUI ()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		int _ypos = ypos;
		int _xpos = xpos;
		if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Blessing"))
		{
			CmdDrawBlessing();
		}
		_ypos += buttonHeight + 10;
		if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Curse"))
		{
			CmdDrawCurse();
		}
		_ypos += buttonHeight + 10;
		if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Random"))
		{
			CmdDrawRandom();
		}
	}

	[Command]
	void CmdDrawBlessing ()
	{
		ServerCardDealer.Instance.DrawBlessing();
		RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, player.name);
	}

	[Command]
	void CmdDrawCurse ()
	{
		ServerCardDealer.Instance.DrawCurse();
		RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, player.name);
	}

	[Command]
	void CmdDrawRandom ()
	{
		ServerCardDealer.Instance.DrawRandom();
		RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, player.name);
	}

	[ClientRpc]
	void RpcUpdateDeckStatusInfo (ServerCardDealer.DealerStatus dealerStatus, string playerName)
	{
		CardDrawUI.Instance.UpdateDeckStatusUI(dealerStatus, playerName);
	}
}
