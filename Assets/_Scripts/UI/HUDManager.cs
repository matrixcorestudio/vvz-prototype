using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;
using Prototype.Player;

public class HUDManager : MonoBehaviour
{
    public GameObject VikingInfo;
    public GameObject ZombieInfo;
    public GameObject KeysInfo;
    public InputField[] CharacterNameFields; //size 8
    public Text[] StocksTexts; //size 8
    public Text[] EnergyTexts; //size 8
    public Dropdown[] StFxDropdowns; //size 16
    public Text[] StFxTurnsTexts; //size 16

    private HUD hud;
    
    private void Start()
    {
        var stFxs = new List<string>(Enum.GetNames(typeof(Enums.StatusEffect)));
        for (int i = 0; i < StFxDropdowns.Length; ++i)
        {
            StFxDropdowns[i].ClearOptions();
            StFxDropdowns[i].AddOptions(stFxs);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Viking Info"))
        {
            VikingInfo.SetActive(!VikingInfo.activeSelf);
        }
        else if (Input.GetButtonDown("Zombie Info"))
        {
            ZombieInfo.SetActive(!ZombieInfo.activeSelf);
        }
        else if (Input.GetButtonDown("Keys Info"))
        {
            KeysInfo.SetActive(!KeysInfo.activeSelf);
        }
    }

    public void Init(HUD hud)
    {
        this.hud = hud;
        this.hud.NameChangedEvent += UpdateNames;
        this.hud.StockChangedEvent += UpdateStocks;
        this.hud.EnergyChangedEvent += UpdateEnergy;
        this.hud.StFxChangedEvent += UpdateStFx;
        this.hud.StFxTurnsChangedEvent += UpdateStFxTurns;

        for (int i = 0; i < 8; ++i)
        {
            CharacterNameFields[i].text = this.hud.CharacterNames[i];
            StocksTexts[i].text = this.hud.Stocks[i].ToString();
            EnergyTexts[i].text = this.hud.EnergyPower[i].ToString();
        }

        for (int i = 0; i < 16; ++i)
        {
            StFxDropdowns[i].value = this.hud.StFxs[i];
            StFxTurnsTexts[i].text = this.hud.StFxsTurns[i].ToString();
        }
    }

#region UI calls
    public void ChangeCharacterName(int index)
    {
        hud.ChangeCharacterName(index, CharacterNameFields[index].text);
    }

    public void AddStock(int index)
    {
        hud.AddStock(index);
    }

    public void RemoveStock(int index)
    {
        hud.RemoveStock(index);
    }

    public void AddEnergy(int index)
    {
        hud.AddEnergy(index);
    }

    public void RemoveEnergy(int index)
    {
        hud.RemoveEnergy(index);
    }

    public void ChangeStatusEffect(int index)
    {
        hud.ChangeStatusEffect(index, (Enums.StatusEffect)StFxDropdowns[index].value);
    }

    public void AddTurnsToStatusEffect(int index)
    {
        hud.AddTurnsToStatusEffect(index);
    }

    public void RemoveTurnsToStatusEffect(int index)
    {
        hud.RemoveTurnsToStatusEffect(index);
    }
#endregion

#region events
    public void UpdateNames(int index)
    {
        CharacterNameFields[index].text = hud.CharacterNames[index];
    }

    public void UpdateStocks(int index)
    {
        StocksTexts[index].text = hud.Stocks[index].ToString();
        //StocksTexts[index].text = (int.Parse(StocksTexts[index].text) + 1).ToString();
    }

    public void UpdateEnergy(int index)
    {
        EnergyTexts[index].text = hud.EnergyPower[index].ToString();
    }

    public void UpdateStFx(int index)
    {
        StFxDropdowns[index].value = hud.StFxs[index];
    }

    public void UpdateStFxTurns(int index)
    {
        StFxTurnsTexts[index].text = hud.StFxsTurns[index].ToString();
    }

#endregion
}
