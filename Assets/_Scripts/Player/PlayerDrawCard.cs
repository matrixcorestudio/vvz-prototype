using UnityEngine;
using UnityEngine.Networking;
using Prototype.Player;
using System.Linq;

[RequireComponent(typeof(Player))]
public class PlayerDrawCard : NetworkBehaviour 
{
    public delegate void OnDrawCard(Player player, string result);
    public event OnDrawCard DrawCardEvent;

    delegate CardData DrawCardTypeDelegate();

    Player m_player;
    Inventory m_inventory;
    ServerCardDealer m_dealer;

	void Start ()
	{
		m_player = GetComponent<Player>();
        m_inventory = GetComponent<Inventory>();
        m_dealer = ServerCardDealer.Instance;
	}

    void DrawCard(DrawCardTypeDelegate cardTypeDelegate)
    {
        if (m_inventory.cards.Count < m_inventory.space)
        {
            CardData card = cardTypeDelegate();
            RpcAddToInventory(card.id);
            if (DrawCardEvent != null)
            {
                DrawCardEvent(m_player, "Draw card: " + card.name);
            }
        }
    }

    [Command]
	public void CmdDrawBlessing ()
	{
        DrawCard(m_dealer.DrawBlessing);
	}

	[Command]
	public void CmdDrawCurse ()
	{
        DrawCard(m_dealer.DrawCurse);
    }

	[Command]
	public void CmdDrawRandom ()
	{
        DrawCard(m_dealer.DrawRandom);
    }

    [ClientRpc]
    void RpcAddToInventory(int cardId)
    {
        CardData newCard = m_dealer.blessingCards.Union(m_dealer.curseCards).ToList().Find(card => card.id == cardId);
        if (newCard != null)
        {
            m_inventory.Add(newCard);
        }
        else
        {
            Debug.LogWarning("Could not find card in dealer!");
        }
    }
}
