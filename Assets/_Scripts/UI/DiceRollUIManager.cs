using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;
using Prototype.Player;

public class DiceRollUIManager : MonoBehaviour
{
    public Dropdown singleDiceDropdown;
    public Dropdown[] charDiceDropdowns;

    public delegate void OnDiceRoll(string result);
    public event OnDiceRoll DiceRollEvent;

    PlayerDiceRoll playerDiceRoll;

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

    }

    public void Init(PlayerDiceRoll playerDiceRoll)
    {
        this.playerDiceRoll = playerDiceRoll;
    }

    public void SingleRoll()
    {
        Debug.Log("Single Roll Button pressed ;) value: " + singleDiceDropdown.value);
        playerDiceRoll.RollDice(Enums.RollType.SingleRoll, (Enums.DiceType)singleDiceDropdown.value);
    }

    public void VikingRoll()
    {
        Debug.Log("Viking Roll button pressed!");
        playerDiceRoll.RollDice(Enums.RollType.VikingRoll, Enums.DiceType.None);
    }

    public void ZombieRoll()
    {
        Debug.Log("Zombie Roll button pressed!");
        playerDiceRoll.RollDice(Enums.RollType.ZombieRoll, Enums.DiceType.None);
    }

    public void ChangeVikingDice()
    {
        int[] newValues = new int[4];
        for (int i = 0; i < charDiceDropdowns.Length; ++i)
        {
            newValues[i] = charDiceDropdowns[i].value;
        }
        playerDiceRoll.ChangeVikingDice(newValues);
    }

    public void ChangeZombieDice()
    {
        int[] newValues = new int[4];
        for (int i = 0; i < charDiceDropdowns.Length; ++i)
        {
            newValues[i] = charDiceDropdowns[i].value;
        }
        playerDiceRoll.ChangeZombieDice(newValues);
    }


}
