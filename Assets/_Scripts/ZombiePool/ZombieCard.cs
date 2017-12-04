using System.Collections;
using System.Collections.Generic;
using System.Text;

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

    //    Debug.LogFormat("ID: {0}, Is Forced: {1}, Name: {2}, Cost: {3}, Description: {4}", 
    //                    id, i == 1, splitted[1], cost, splitted[3]);

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("ID: ");
        sb.Append(ID.ToString());
        sb.Append("\n");
        sb.Append("Name: ");
        sb.Append(Name);
        sb.Append("\n");
        sb.Append("Is Forced: ");
        sb.Append(IsForced ? "Yes" : "No");
        sb.Append("\n");
        sb.Append("Cost: ");
        sb.Append(Cost.ToString());
        sb.Append(" RP\n\n");
        sb.Append("Description: \n");
        sb.Append(Description);

        return sb.ToString();
    }
}
