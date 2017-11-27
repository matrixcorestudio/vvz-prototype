using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;

public class HUDUISingleton : Singleton<HUDUISingleton>
{
    public string[] NamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone"
                               ,"[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };


    [SerializeField] InputField[] CharacterNameFields; //size 8
    [SerializeField] InputField[] MoveInputFields; //size 8
    [SerializeField] Text[] StocksTexts; //size 8
    [SerializeField] Text[] EnergyTexts; //size 8
    [SerializeField] Dropdown[] StFxDropdowns; //size 16
    [SerializeField] InputField[] StFxValueInputFields; // size 16
    [SerializeField] Text[] StFxTurnsTexts; //size 16

    private void Start()
    {
        for (int i = 0; i < StocksTexts.Length; ++i)
        {
            CharacterNameFields[i].text = NamesFlight1[i];
            MoveInputFields[i].text = string.Empty;
            StocksTexts[i].text = (i < 4 ? (i == 0 ? 4 : 3) : (i == 4 ? 6 : 5)).ToString();
            EnergyTexts[i].text = (i < 4 ? 5 : 0).ToString();
        }

        var options = new List<string>(Enum.GetNames(typeof(Enums.StatusEffect)));

        for (int i = 0; i < StFxDropdowns.Length; ++i)
        {
            StFxDropdowns[i].ClearOptions();
            StFxDropdowns[i].AddOptions(options);
            StFxDropdowns[i].value = 0;
            StFxValueInputFields[i].text = string.Empty;
            StFxTurnsTexts[i].text = "0";
        }
    }

    public void UpdateNames(int index, string newName)
    {
        CharacterNameFields[index].text = newName;
        NamesFlight1[index] = newName;
    }

    public void UpdateMovementValue(int index, string movement)
    {
        MoveInputFields[index].text = movement;
    }

    public void UpdateStocks(int index, int newValue)
    {
        StocksTexts[index].text = newValue.ToString();
    }

    public void UpdateEnergy(int index, int newValue)
    {
        EnergyTexts[index].text = newValue.ToString();
    }

    public void UpdateStatusEffect(int index, int newValue)
    {
        StFxDropdowns[index].value = newValue;
    }

    public void UpdateStatusEffectValue(int index, string newValue)
    {
        StFxValueInputFields[index].text = newValue;
    }

    public void UpdateStatusEffectTurns(int index, int newValue)
    {
        StFxTurnsTexts[index].text = newValue.ToString();
    }

}
