using System.Collections;
using System.Collections.Generic;

public struct ZombieCard
{
    public bool IsForced;
    public int ID;
    public int Cost;
    public string Name;
    public string Description;

    public ZombieCard(bool isForced, int id, int cost, string name, string description)
    {
        IsForced = isForced;
        ID = id;
        Cost = cost;
        Name = name;
        Description = description;
    }
}
