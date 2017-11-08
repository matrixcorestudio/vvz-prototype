using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;

public class DiceRollUIManager : NetworkBehaviour
{
    public Dropdown singleDiceDropdown;
    public Dropdown[] charDiceDropdowns;

    public delegate void OnDiceRoll(string result);
    public event OnDiceRoll DiceRollEvent;

    private void Awake()
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
    }
    
    public void SingleRoll()
    {
        Debug.Log("Single Roll Button pressed ;) value: " + singleDiceDropdown.value);
        CmdRollDice((Enums.DiceType)singleDiceDropdown.value);
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
    void CmdRollDice(Enums.DiceType diceType)
    {
        int rollResult = CalculateResult(diceType);
        if (DiceRollEvent != null)
        {
            DiceRollEvent("Dice Roll: " + rollResult + ", DiceType: " + diceType);
        }
        RpcRollDice(rollResult, diceType, "LOLZ");
    }

    [ClientRpc]
    void RpcRollDice(int rollValue, Enums.DiceType diceType, string playerName)
    {
        DiceRollUI.Instance.RollDice(rollValue, diceType.ToString(), playerName);
    }
}
