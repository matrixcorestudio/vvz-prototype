using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
	public List<CardData> cards = new List<CardData>();
	public int space = 4;

    public delegate void OnCardChange();
    public event OnCardChange cardChangeEvent ;

    public bool Add(CardData card)
	{
		if(cards.Count >= space)
		{
			Debug.LogWarning("No space in inventory");
			return false;
		}
		cards.Add(card);
        if (cardChangeEvent != null)
        {
            cardChangeEvent();
        }
		return true;
	}

	public void Remove(CardData card)
	{
		cards.Remove(card);
	}
}
