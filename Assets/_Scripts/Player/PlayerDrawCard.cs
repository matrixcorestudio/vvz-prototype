using UnityEngine;
using UnityEngine.Networking;
using Prototype.Player;

[RequireComponent(typeof(Player))]
public class PlayerDrawCard : NetworkBehaviour 
{
	//int xpos = 10;
	//int ypos = 310;
	//int buttonWidth = 80;
	//int buttonHeight = 20;

	Player m_player;
    Inventory m_inventory;
	public delegate void OnDrawCard(Player player, string result);
	public event OnDrawCard DrawCardEvent;

	void Start ()
	{
		m_player = GetComponent<Player>();
        m_inventory = GetComponent<Inventory>();
	}

    //[ClientCallback]
    //void OnGUI()
    //{
    //    if (!isLocalPlayer)
    //    {
    //        return;
    //    }
    //    int _ypos = ypos;
    //    int _xpos = xpos;
    //    if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Blessing"))
    //    {
    //        CmdDrawBlessing();
    //    }
    //    _ypos += buttonHeight + 10;
    //    if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Curse"))
    //    {
    //        CmdDrawCurse();
    //    }
    //    _ypos += buttonHeight + 10;
    //    if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Random"))
    //    {
    //        CmdDrawRandom();
    //    }
    //}

    [Command]
	void CmdDrawBlessing ()
	{
		CardData card = ServerCardDealer.Instance.DrawBlessing();
        m_inventory.Add(card);
		RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, m_player.name);
		if(DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

	[Command]
	void CmdDrawCurse ()
	{
		CardData card = ServerCardDealer.Instance.DrawCurse();
        m_inventory.Add(card);
        RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, m_player.name);
		if(DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

	[Command]
	void CmdDrawRandom ()
	{
		CardData card = ServerCardDealer.Instance.DrawRandom();
        m_inventory.Add(card);
		RpcUpdateDeckStatusInfo(ServerCardDealer.Instance.CardDealerStatus, m_player.name);
		if(DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

	[ClientRpc]
	void RpcUpdateDeckStatusInfo (ServerCardDealer.DealerStatus dealerStatus, string playerName)
	{
		//CardDrawUI.Instance.UpdateDeckStatusUI(dealerStatus, playerName);
	}
}
