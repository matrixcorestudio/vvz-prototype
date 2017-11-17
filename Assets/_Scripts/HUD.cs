using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;
using Prototype.Player;

public class HUD : NetworkBehaviour
{
    public delegate void OnNameChange(int index);
    public event OnNameChange NameChangedEvent;
    public delegate void OnStockChange(int index);
    public event OnStockChange StockChangedEvent;
    public delegate void OnEnergyChange(int index);
    public event OnEnergyChange EnergyChangedEvent;
    public delegate void OnStFxChange(int index);
    public event OnStFxChange StFxChangedEvent;
    public delegate void OnStFxTurnsChange(int index);
    public event OnStFxTurnsChange StFxTurnsChangedEvent;


    public string[] NamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone"
                               ,"[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };
    //public string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };

    public SyncListString CharacterNames = new SyncListString(); //size 8
    public SyncListInt Stocks = new SyncListInt(); //size 8
    public SyncListInt EnergyPower = new SyncListInt(); //size 8
    public SyncListInt StFxs = new SyncListInt(); // size 16
    public SyncListInt StFxsTurns = new SyncListInt(); //size 16

    private Player player;
    private bool foundPlayer = false;
    private bool foundHUDMngr;
    private HUDManager hUDManager;

    private void Start()
    {
        if (isServer)
        {
            InitSyncLists();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer || foundHUDMngr)
        {
            return;
        }

        hUDManager = FindObjectOfType<HUDManager>();
        if (hUDManager != null)
        {
            Debug.Log("Found HUD Manager");
            foundHUDMngr = true;
            hUDManager.Init(this);
        }
    }

    [ClientCallback]
    public void ChangeCharacterName(int index, string newName)
    {
        
    }

    [ClientCallback]
    public void AddStock(int index)
    {
        Debug.Log("Adding 1 stock to viking leader");
        CmdAddStock(index);
    }

    [ClientCallback]
    public void RemoveStock(int index)
    {

    }

    [ClientCallback]
    public void AddEnergy(int index)
    {

    }

    [ClientCallback]
    public void RemoveEnergy(int index)
    {

    }

    [ClientCallback]
    public void ChangeStatusEffect(int index, Enums.StatusEffect statusEffect)
    {

    }

    [ClientCallback]
    public void AddTurnsToStatusEffect(int index)
    {

    }

    [ClientCallback]
    public void RemoveTurnsToStatusEffect(int index)
    {

    }

    [Command]
    private void CmdChangeCharacterName(int index, string newName)
    {
        CharacterNames[index] = newName;
        if (NameChangedEvent != null)
        {
            NameChangedEvent(index);
        }
    }

    [Command]
    private void CmdAddStock(int index)
    {
        ++Stocks[index];
        RpcAddStock(index);
    }

    [ClientRpc]
    private void RpcAddStock(int index)
    {
        if (StockChangedEvent != null)
        {
            StockChangedEvent(index);
        }
        //if (hUDManager == null)
        //{
        //    hUDManager = FindObjectOfType<HUDManager>();
        //}
        //hUDManager.UpdateStocks(index);
        //HUDManager.Instance.UpdateStocks(index);
    }

    [Server]
    private void InitSyncLists()
    {
        for (int i = 0; i < 8; ++i)
        {
            CharacterNames.Add(i < 4 ? NamesFlight1[i] : NamesFlight1[i]);
            Stocks.Add(i < 4 ? (i == 0 ? 4 : 3) : (i == 4 ? 6 : 5));
            EnergyPower.Add(i < 4 ? 5 : 0);            
        }

        for (int i = 0; i < 16; ++i)
        {
            StFxs.Add(0);
            StFxsTurns.Add(0);
        }
    }
}
