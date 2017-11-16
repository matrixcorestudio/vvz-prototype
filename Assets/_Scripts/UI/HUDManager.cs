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
            StFxDropdowns[i].AddOptions(stFxs);
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
    }

    public void UpdateNames(int index)
    {

    }

    public void UpdateStocks(int index)
    {
       
    }

    public void UpdateEnergy(int index)
    {

    }

    public void UpdateStFx(int index)
    {

    }

    public void UpdateStFxTurns(int index)
    {

    }
}
