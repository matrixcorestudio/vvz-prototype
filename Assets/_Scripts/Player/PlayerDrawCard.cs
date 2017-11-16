using UnityEngine;
using UnityEngine.Networking;
using Prototype.Player;

[RequireComponent(typeof(Player))]
public class PlayerDrawCard : NetworkBehaviour 
{
    public delegate void OnDrawCard(Player player, string result);
    public event OnDrawCard DrawCardEvent;

    Player m_player;
    Inventory m_inventory;

	void Start ()
	{
		m_player = GetComponent<Player>();
        m_inventory = GetComponent<Inventory>();
	}

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.C))
        {
            CmdDrawRandom();
        }
    }

    [Command]
	void CmdDrawBlessing ()
	{
		CardData card = ServerCardDealer.Instance.DrawBlessing();
        RpcAddToInventory(card.id);
		if(DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

	[Command]
	void CmdDrawCurse ()
	{
		CardData card = ServerCardDealer.Instance.DrawCurse();
        RpcAddToInventory(card.id);
        if (DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

	[Command]
	void CmdDrawRandom ()
	{
		CardData card = ServerCardDealer.Instance.DrawRandom();
        RpcAddToInventory(card.id);
        if (DrawCardEvent != null)
		{
			DrawCardEvent(m_player, "Draw card: "+ ServerCardDealer.Instance.CardDealerStatus.lastCardName);
		}
	}

    [ClientRpc]
    void RpcAddToInventory(int cardId)
    {
        CardData newCard = ServerCardDealer.Instance.blessingCards.Find(card => card.id == cardId);
        if (newCard == null)
        {
            newCard = ServerCardDealer.Instance.curseCards.Find(card => card.id == cardId);
        }
        m_inventory.Add(newCard);
    }

	[ClientRpc]
	void RpcUpdateDeckStatusInfo (ServerCardDealer.DealerStatus dealerStatus, string playerName)
	{
		//CardDrawUI.Instance.UpdateDeckStatusUI(dealerStatus, playerName);
	}
}
