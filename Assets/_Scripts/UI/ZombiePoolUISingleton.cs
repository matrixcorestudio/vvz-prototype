using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePoolUISingleton : Singleton<ZombiePoolUISingleton>
{
    [SerializeField] Text RoundsCountText;

    private string zombiePoolPath;
    private string[] zombiePoolCSVPaths = new string[2];

    private List<ZombieCard> zombiePool;

    private void Start()
    {
        zombiePoolPath = Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Resources\Zombie Cards");
        zombiePoolCSVPaths[0] = Path.Combine(zombiePoolPath, "Zombie Pool - Regular.csv");
        zombiePoolCSVPaths[1] = Path.Combine(zombiePoolPath, "Zombie Pool - Obligatory.csv");
        Debug.Log("Zombie Pool directory path: " + zombiePoolPath);
        Debug.Log("Regular path: " + zombiePoolCSVPaths[0]);
        Debug.Log("Obligatory path: " + zombiePoolCSVPaths[1]);
        InitZombiePool();
        RoundsCountText.text = "0";
    }

    public void AddRound()
    {
        RoundsCountText.text = (int.Parse(RoundsCountText.text) + 1).ToString();
    }

    public void Next4Cards()
    {
        Debug.Log("Next 4 cards dealed!");
    }

    public void UseCard(int index)
    {
        Debug.Log("Used card at index " + index);
    }

    private void InitZombiePool()
    {
        zombiePool = new List<ZombieCard>();
        for (int i = 0; i < 2; ++i)
        {
            string[] allLines = File.ReadAllLines(zombiePoolCSVPaths[i]);
            foreach (var line in allLines)
            {
                string[] splitted = line.Split(',');
                int id = int.Parse(splitted[0]);
                bool isForced = (id < 1000 ? false : true);
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

                zombiePool.Add(new ZombieCard(isForced, id, cost, splitted[1], splitted[3]));
                Debug.LogFormat("ID: {0}, Is Forced: {1}, Name: {2}, Cost: {3}, Description: {4}", 
                    id, isForced, splitted[1], cost, splitted[3]);
            }
        }
    }
}
