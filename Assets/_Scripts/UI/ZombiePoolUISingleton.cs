using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePoolUISingleton : Singleton<ZombiePoolUISingleton>
{
    [SerializeField] TextAsset RegularZombiePoolAsset;
    [SerializeField] TextAsset ObligatoryZombiepoolAsset;
    [SerializeField] Text RoundsCountText;
    [SerializeField] Text[] PoolSlotTexts;

    private List<ZombieCard> regularZombiePool;
    private List<ZombieCard> obligatoryZombiePool;
    private List<ZombieCard> zombiePool;
    private List<ZombieCard> usedCards;
    private List<ZombieCard> next4List;

    private void Start()
    {
        InitZombiePool();
        RoundsCountText.text = "0";
    }

    public void AddRound()
    {
        RoundsCountText.text = (int.Parse(RoundsCountText.text) + 1).ToString();
    }

    public void Next4Cards()
    {
        if (regularZombiePool.Count < 4)
        {
            for (int i = 0; i < regularZombiePool.Count; ++i)
            {
                usedCards.Add(regularZombiePool[i]);
            }
            regularZombiePool = usedCards;
            usedCards.Clear();
            ShuffleIndividualDeck(regularZombiePool);
        }

        next4List.Clear();
        NextCard(false);
        NextCard(false);

        if (obligatoryZombiePool.Count > 1)
        {
            NextCard((Random.Range(0, 999) % 4 == 0)); //25% of a forced card for the 3rd slot.
            NextCard((Random.Range(0, 999) % 2 == 0)); //50% of a forced card for the 4th slot.
        }
        else if(obligatoryZombiePool.Count == 1)
        {
            NextCard((Random.Range(0, 999) % 2 == 0)); //50% of a forced card for the 4th slot.
        }
        else
        {
            NextCard(false);
            NextCard(false);
        }

        for (int i = 0; i < PoolSlotTexts.Length; ++i)
        {
            PoolSlotTexts[i].color = Color.white;
            PoolSlotTexts[i].fontStyle = FontStyle.Normal;
            PoolSlotTexts[i].text = next4List[i].ToString();
        }

        Debug.Log("Next 4 cards dealed!");
    }

    public void UseCard(int index)
    {
        if (PoolSlotTexts[index].color == Color.black)
        {
            return;
        }
        PoolSlotTexts[index].color = Color.black;
        PoolSlotTexts[index].fontStyle = FontStyle.Italic;

        Debug.Log("Used card at index " + index);
    }

    private void NextCard(bool obligatory)
    {
        if (!obligatory)
        {
            next4List.Add(regularZombiePool[regularZombiePool.Count - 1]);
            usedCards.Add(regularZombiePool[regularZombiePool.Count - 1]);
            regularZombiePool.RemoveAt(regularZombiePool.Count - 1);
        }
        else
        {
            next4List.Add(obligatoryZombiePool[obligatoryZombiePool.Count - 1]);
            obligatoryZombiePool.RemoveAt(obligatoryZombiePool.Count - 1);
        }
    }

    private void InitZombiePool()
    {
        regularZombiePool = new List<ZombieCard>();
        obligatoryZombiePool = new List<ZombieCard>();
        zombiePool = new List<ZombieCard>();
        usedCards = new List<ZombieCard>();
        next4List = new List<ZombieCard>();

        for (int i = 0; i < 2; ++i)
        {
            string[] allLines = (i == 0 ? RegularZombiePoolAsset.text.Split('\n') : ObligatoryZombiepoolAsset.text.Split('\n'));
            foreach (var line in allLines)
            {
                string[] splitted = line.Split(',');
                int id = int.Parse(splitted[0]);
                int cost = int.Parse(splitted[2].Remove(splitted[2].Length - 3)); //Remove " RP" from cost

                if (splitted.Length > 4) // There are commas in the description
                {
                    List<string> remaining = new List<string>();
                    for (int j = splitted.Length - (splitted.Length - 4); j < splitted.Length; ++j)
                    {
                        remaining.Add(splitted[j]);
                    }
                    splitted[3] += "," + string.Join(",", remaining.ToArray()); //Restore the rest of the description.
                }

                var zc = new ZombieCard(i == 1, id, cost, splitted[1], splitted[3]);

                if (i == 0) { regularZombiePool.Add(zc); }
                else { obligatoryZombiePool.Add(zc); }

                Debug.Log(zc.ToString());
            }
        }
        ShuffleIndividualDeck(regularZombiePool);
        ShuffleIndividualDeck(obligatoryZombiePool);

    }

    private void ShuffleIndividualDeck(List<ZombieCard> deck)
    {
        int N = deck.Count;
        int iterations = Random.Range(N + 50, N + 100);
        for (int i = 0; i < iterations; ++i)
        {
            int swap1 = Random.Range(0, N - 1);
            int swap2 = Random.Range(0, N - 1);
            var temp = deck[swap1];
            deck[swap1] = deck[swap2];
            deck[swap2] = temp;
        }

    }
}
