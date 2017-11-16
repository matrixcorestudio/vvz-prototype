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


    public string[] vikingNamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone" };
    public string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };

    public SyncListString CharacterNames = new SyncListString();
    public SyncListInt Stocks = new SyncListInt();
    public SyncListInt EnergyPower = new SyncListInt();
    public SyncListInt StFxs = new SyncListInt(); // size 16
    public SyncListInt StFxsTurns = new SyncListInt(); //size 16

    private Player player;
    private bool foundHUDMngr;
    private bool foundPlayer = false;

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

        var hudMngr = FindObjectOfType<HUDManager>();
        if (hudMngr != null)
        {
            foundHUDMngr = true;
            hudMngr.Init(this);
        }


    }

    [Server]
    private void InitSyncLists()
    {
        for (int i = 0; i < 8; ++i)
        {
            CharacterNames.Add(i < 4 ? vikingNamesFlight1[i] : zombieNamesFlight1[i]);
            Stocks.Add(i < 4 ? (i == 1 ? 4 : 3) : (i == 4 ? 6 : 5));
            EnergyPower.Add(i < 4 ? 5 : 0);            
        }

        for (int i = 0; i < 16; ++i)
        {
            StFxs.Add(0);
            StFxsTurns.Add(0);
        }
    }
}
