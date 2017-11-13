using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;
using Prototype.Player;

public class DiceRollUIManager : NetworkBehaviour
{
    public Dropdown singleDiceDropdown;
    public Dropdown[] charDiceDropdowns;

    public delegate void OnDiceRoll(string result);
    public event OnDiceRoll DiceRollEvent;

    private SyncListInt vikingDiceTypes = new SyncListInt();
    private SyncListInt zombieDiceTypes = new SyncListInt();
    private string[] vikingNamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone" };
    private string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };

    private Player player;
    public Player Player { get; set; }

    private void Start()
    {
        List<string> diceOptions = new List<string>();
        foreach (var type in Enum.GetValues(typeof(Enums.DiceType)))
        {
            diceOptions.Add(type.ToString());
        }
        singleDiceDropdown.ClearOptions();
        singleDiceDropdown.AddOptions(diceOptions);
        for (int i = 0; i < charDiceDropdowns.Length; i++)
        {
            charDiceDropdowns[i].ClearOptions();
            charDiceDropdowns[i].AddOptions(diceOptions);
        }
        
        InitSyncLists();
    }

    [Server]
    void InitSyncLists()
    {
        //Hardcoded for flight 1
        vikingDiceTypes.Add((int)Enums.DiceType.D6Plus2);
        vikingDiceTypes.Add((int)Enums.DiceType.D6Plus2);
        vikingDiceTypes.Add((int)Enums.DiceType.D12Min3);
        vikingDiceTypes.Add((int)Enums.DiceType.D10Max8);

        //Hardcoded for flight 1
        zombieDiceTypes.Add((int)Enums.DiceType.D6);
        zombieDiceTypes.Add((int)Enums.DiceType.D4X2);
        zombieDiceTypes.Add((int)Enums.DiceType.D6);
        zombieDiceTypes.Add((int)Enums.DiceType.D6);
    }
    
    [ClientCallback]
    public void SingleRoll()
    {
        bool lp = isLocalPlayer;
        Debug.Log("Single Roll Button pressed ;) value: " + singleDiceDropdown.value + " " + isLocalPlayer);
        CmdRollDice(Enums.RollType.SingleRoll, (Enums.DiceType)singleDiceDropdown.value);
    }

    [ClientCallback]
    public void VikingRoll()
    {
        Debug.Log("Viking Roll button pressed!");
        CmdRollDice(Enums.RollType.VikingRoll, Enums.DiceType.None);
    }

    [ClientCallback]
    public void ZombieRoll()
    {
        Debug.Log("Zombie Roll button pressed!");
        CmdRollDice(Enums.RollType.ZombieRoll, Enums.DiceType.None);
    }

    [ClientCallback]
    public void ChangeVikingDice()
    {
        bool lp = isLocalPlayer;
        bool ic = isClient;
        string toLog = "Changing dice type to vikings to the next values: ";
        for (int i = 0; i < vikingDiceTypes.Count; ++i)
        {
            vikingDiceTypes[i] = charDiceDropdowns[i].value;
            toLog += vikingDiceTypes[i].ToString() + ", ";
        }
        Debug.Log(toLog.Remove(toLog.Length - 2) + lp + " " + ic);
    }

    [ClientCallback]
    public void ChangeZombieDice()
    {
        string toLog = "Changing dice type to zombies to the next values: ";
        for (int i = 0; i < zombieDiceTypes.Count; ++i)
        {
            zombieDiceTypes[i] = charDiceDropdowns[i].value;
            toLog += zombieDiceTypes[i].ToString() + ", ";
        }
        Debug.Log(toLog.Remove(toLog.Length - 2));
    }

    [Server]
    int CalculateResult(Enums.DiceType dice)
    {
        switch (dice)
        {
            case Enums.DiceType.CoinFlip:
                return CalculateCoinFlip();
            case Enums.DiceType.D4:
                return CalculateD4();
            case Enums.DiceType.D4Plus1:
                return CalculateD4() + 1;
            case Enums.DiceType.D6:
                return CalculateD6();
            case Enums.DiceType.D6Plus2:
                return CalculateD6() + 2;
            case Enums.DiceType.D10Max8:
                int resultD10 = CalculateD10();
                if (resultD10 > 8) { return 8; }
                return resultD10;
            case Enums.DiceType.D12Min3:
                int resultD12 = CalculateD12();
                if (resultD12 < 3) { return 3; }
                return resultD12;
            case Enums.DiceType.D4X2:
                return CalculateD4() + CalculateD4();
            case Enums.DiceType.D3X3:
                return CalculateD3() + CalculateD3() + CalculateD3();
            case Enums.DiceType.D4Plus4:
                return CalculateD4() + 4;
            case Enums.DiceType.None: //Pass through
            default:
                return -1;
        }
    }

    [Server] int CalculateCoinFlip() { return UnityEngine.Random.Range(1, 3); }
    [Server] int CalculateD3() { return UnityEngine.Random.Range(1, 4); }
    [Server] int CalculateD4() { return UnityEngine.Random.Range(1, 5); }
    [Server] int CalculateD6() { return UnityEngine.Random.Range(1, 7); }
    [Server] int CalculateD8() { return UnityEngine.Random.Range(1, 9); }
    [Server] int CalculateD10() { return UnityEngine.Random.Range(1, 11); }
    [Server] int CalculateD12() { return UnityEngine.Random.Range(1, 13); }

    [Command]
    void CmdRollDice(Enums.RollType rollType, Enums.DiceType diceType)
    {
        Debug.Log("Rolling Dice in CmdRollDice");
        if (rollType == Enums.RollType.SingleRoll)
        {
            int rollResult = CalculateResult(diceType);
            if (DiceRollEvent != null)
            {
                DiceRollEvent("Dice Roll: " + rollResult + ", DiceType: " + diceType);
            }
            RpcRollSingleDice(rollResult, diceType); 
        }
        else
        {
            int[] rollResults = new int[4];
            string[] diceTypes = new string[4];

            if (rollType == Enums.RollType.VikingRoll)
            {
                for (int i = 0; i < vikingDiceTypes.Count; ++i)
                {
                    Enums.DiceType dt = (Enums.DiceType)vikingDiceTypes[i];
                    rollResults[i] = CalculateResult(dt);
                    diceTypes[i] = dt.ToString();
                }
                RpcRollMultipleDice(rollResults, diceTypes, vikingNamesFlight1, Enums.RollType.VikingRoll);
            }
            else //Roll type is zombies
            {
                for (int i = 0; i < zombieDiceTypes.Count; ++i)
                {
                    Enums.DiceType dt = (Enums.DiceType)zombieDiceTypes[i];
                    rollResults[i] = CalculateResult(dt);
                    diceTypes[i] = dt.ToString();
                }
                RpcRollMultipleDice(rollResults, diceTypes, zombieNamesFlight1, Enums.RollType.ZombieRoll);
            }
        }
    }

    [ClientRpc]
    void RpcRollSingleDice(int rollValue, Enums.DiceType diceType)
    {
        Debug.Log("RpcRollSingle entered ;)");
        DiceRollUI.Instance.RollSingleDice(rollValue, diceType.ToString());
    }

    [ClientRpc]
    void RpcRollMultipleDice(int[] rollValues, string[] diceTypes, string[] charNames, Enums.RollType rollType)
    {
        DiceRollUI.Instance.RollMultipleDice(rollValues, diceTypes, charNames, rollType);
    }
}
