using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
	public List<CardData> cards = new List<CardData>();
	public int space = 4;

	public bool Add(CardData card)
	{
		if(cards.Count >= space)
		{
			Debug.LogWarning("Sin espacio en inventario");
			return false;
		}
		cards.Add(card);
		return true;
	}

	public void Remove(CardData card)
	{
		cards.Remove(card);
	}
}