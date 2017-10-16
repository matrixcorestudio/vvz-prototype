using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCardDealer : Singleton<ServerCardDealer> 
{
	[System.Serializable]
	public struct DealerStatus
	{
		public int lastCardId;
		public string lastCardName;
		public string lastCardType;
		public string lastCardDescription;

		public int remainingBlessings;
		public int remainingCurses;

		public void SetLastCard(CardData card)
		{
			lastCardId = card.id;
			lastCardName = card.name;
			lastCardType = card.type.ToString();
			lastCardDescription = card.description;
		}
	}
		
	public List<CardData> blessingCards = new List<CardData>();
	public List<CardData> curseCards = new List<CardData>();

	List<int> m_blessings;
	List<int> m_curses;

	CardData m_lastDrawnCard;
	DealerStatus m_cardDealerStatus;
	public DealerStatus CardDealerStatus {
		get {
			m_cardDealerStatus.SetLastCard(m_lastDrawnCard);
			m_cardDealerStatus.remainingBlessings = m_blessings.Count;
			m_cardDealerStatus.remainingCurses = m_curses.Count;
			return m_cardDealerStatus;
		}
	}


	public override void Awake ()
	{
		base.Awake();
		m_lastDrawnCard = ScriptableObject.CreateInstance<CardData>();
	}

	void Start()
	{
		InitBlessings();
		InitCurses();
	}

	public void DrawBlessing ()
	{
		m_lastDrawnCard = blessingCards[m_blessings[0]];
		RemoveCard(m_blessings, CardData.CardType.Blessing);
	}

	public void DrawCurse ()
	{
		m_lastDrawnCard = curseCards[m_curses[0]];
		RemoveCard(m_curses, CardData.CardType.Curse);
	}

	public void DrawRandom ()
	{
		if(Random.Range(0,100)%2 == 0)
		{
			DrawBlessing();
		}
		else
		{
			DrawCurse();
		}
	}

	void InitBlessings()
	{
		m_blessings = new List<int>();
		for (int i = 0; i < blessingCards.Count; i++)
		{
			m_blessings.Add(i);
		}
		ShuffleDeck(m_blessings, Random.Range(4, 7));
	}

	void InitCurses()
	{
		m_curses = new List<int>();
		for (int i = 0; i < curseCards.Count; i++)
		{
			m_curses.Add(i);
		}
		ShuffleDeck(m_curses, Random.Range(4, 7));
	}
		
	void ShuffleDeck(List<int> deck, int iterations)
	{
		if (iterations == 0)
		{
			//PrintDeckToConsolini(deck);
			return;
		}

		List<int> half1 = new List<int>();
		List<int> half2 = new List<int>();

		for (int i = 0; i < deck.Count; ++i)
		{
			if (i < deck.Count / 2)
			{
				half1.Add(deck[i]);
			}
			else
			{
				half2.Add(deck[i]);
			}
		}

		for (int i = 0; i < deck.Count; ++i)
		{
			int rand = (Random.Range(0, 100) % 2);
			int next = -1;
			if (rand != 0 && half1.Count > 0)
			{
				next = half1[0];
				half1.RemoveAt(0);
			}
			else if (rand == 0 && half2.Count > 0)
			{
				next = half2[0];
				half2.RemoveAt(0);
			}
			else
			{
				if (half1.Count > 0)
				{
					next = half1[0];
					half1.RemoveAt(0);
				}
				else
				{
					next = half2[0];
					half2.RemoveAt(0);
				}
			}

			deck[i] = next;
		}
		ShuffleDeck(deck, iterations - 1);
	}

	void RemoveCard(List<int> deck, CardData.CardType type)
	{
		deck.RemoveAt(0);
		if(deck.Count == 0)
		{
			if(type == CardData.CardType.Blessing)
			{
				InitBlessings();
			}
			else
			{
				InitCurses();	
			}
		}
	}
}
