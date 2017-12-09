using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{
	public List<CardData> cards = new List<CardData>();
    public int space = 6;

    public delegate void OnInventoryChange();
    public event OnInventoryChange InventoryChangeEvent;

    InventoryUI m_inventoryUI;

    private void Start()
    {
        m_inventoryUI = FindObjectOfType<InventoryUI>();
        if (m_inventoryUI != null)
        {
            m_inventoryUI.Init(this, isLocalPlayer);
        }
        else
        {
            Debug.LogWarning("Could not find Inventory UI");
        }
    }

    public bool Add(CardData card)
	{
		if(cards.Count >= space)
		{
			Debug.LogWarning("No space in inventory");
			return false;
		}
		cards.Add(card);
        if (InventoryChangeEvent != null)
        {
            InventoryChangeEvent();
        }
		return true;
	}

	public void Remove(CardData card)
	{
        CmdRemove(card.id);
    }

    [Command]
    void CmdRemove(int cardId)
    {
        RpcRemove(cardId);
    }

    [ClientRpc]
    void RpcRemove(int cardId)
    {
        cards.Remove(cards.Find(card => card.id == cardId));
        if (InventoryChangeEvent != null)
        {
            InventoryChangeEvent();
        }
    }
}
