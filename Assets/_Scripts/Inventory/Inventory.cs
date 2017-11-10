using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{
	public List<CardData> cards = new List<CardData>();
	public int space = 5;

    //Envento para cuando se presente un cambio en el inventario, a�adir, usar, o remover cartas.
    public delegate void OnInventoryChange();
    public event OnInventoryChange inventoryChangeEvent;

    public bool Add(CardData card)
	{
		if(cards.Count >= space)
		{
			Debug.LogWarning("No space in inventory");
			return false;
		}
		cards.Add(card);
        if (inventoryChangeEvent != null)
        {
            inventoryChangeEvent();
        }
		return true;
	}

	public void Remove(CardData card)
	{
		cards.Remove(card);
        if (inventoryChangeEvent != null)
        {
            inventoryChangeEvent();
        }
    }
}
