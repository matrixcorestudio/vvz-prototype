using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Utilities;
using Prototype.Player;

public class HUDManager : NetworkBehaviour
{
    public string[] vikingNamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone" };
    public string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };

    private SyncListInt vikingStocks = new SyncListInt();
    private SyncListInt vikingPP = new SyncListInt();
    private SyncListInt vikingStatus1 = new SyncListInt();
    private SyncListInt vikingStatusTurns1 = new SyncListInt();
    private SyncListInt vikingStatus2 = new SyncListInt();
    private SyncListInt vikingStatusTurns2 = new SyncListInt();

    private SyncListInt zombieStocks = new SyncListInt();
    private SyncListInt zombieRP = new SyncListInt();
    private SyncListInt zombieStatus1 = new SyncListInt();
    private SyncListInt zombieStatusTurns1 = new SyncListInt();
    private SyncListInt zombieStatus2 = new SyncListInt();
    private SyncListInt zombieStatusTurns2 = new SyncListInt();


}
