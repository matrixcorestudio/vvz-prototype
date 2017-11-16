using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{
	public List<CardData> cards = new List<CardData>();
	public int space = 5;

    public delegate void OnInventoryChange();
    public event OnInventoryChange InventoryChangeEvent;

    private void Start()
    {
        if (isLocalPlayer)
        {
            InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
            if (inventoryUI != null)
            {
                inventoryUI.Init(this);
            }
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
		cards.Remove(card);
        if (InventoryChangeEvent != null)
        {
            InventoryChangeEvent();
        }
    }
}
