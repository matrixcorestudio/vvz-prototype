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
    //public SyncListString CharacterNames = new SyncListString(); //size 8
    //public SyncListInt Stocks = new SyncListInt(); //size 8
    //public SyncListInt EnergyPower = new SyncListInt(); //size 8
    //public SyncListInt StFxs = new SyncListInt(); // size 16
    //public SyncListInt StFxsTurns = new SyncListInt(); //size 16
    private const int MAX = 10;
    private const int MIN = 0;

    private bool foundHUDMngr;
    private HUDManager hUDManager;

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
        CmdChangeCharacterName(index, newName);
    }

    [ClientCallback]
    public void AddStock(int index, int currentValue)
    {
        CmdAddStock(index, currentValue);
    }

    [ClientCallback]
    public void RemoveStock(int index, int currentValue)
    {
        CmdRemoveStock(index, currentValue);
    }

    [ClientCallback]
    public void AddEnergy(int index, int currentValue)
    {
        CmdAddEnergy(index, currentValue);
    }

    [ClientCallback]
    public void RemoveEnergy(int index, int currentValue)
    {
        CmdRemoveEnergy(index, currentValue);
    }

    [ClientCallback]
    public void ChangeStatusEffect(int index, int statusEffect)
    {
        CmdChangeStatusEffect(index, statusEffect);
    }

    [ClientCallback]
    public void AddTurnsToStatusEffect(int index, int currentValue)
    {
        CmdAddTurnsToStatusEffect(index, currentValue);
    }

    [ClientCallback]
    public void RemoveTurnsToStatusEffect(int index, int currentValue)
    {
        CmdRemoveTurnsToStatusEffect(index, currentValue);
    }

    [Command]
    private void CmdChangeCharacterName(int index, string newName)
    {
        RpcChangeCharacterName(index, newName);
    }

    [Command]
    private void CmdAddStock(int index, int currentValue)
    {
        if (currentValue == MAX)
        {
            return;
        }
        RpcAddStock(index, currentValue);
    }

    [Command]
    private void CmdRemoveStock(int index, int currentValue)
    {
        if (currentValue == MIN)
        {
            return;
        }
        RpcRemoveStock(index, currentValue);
    }

    [Command]
    private void CmdAddEnergy(int index, int currentValue)
    {
        RpcAddEnergy(index, currentValue);
    }

    [Command]
    private void CmdRemoveEnergy(int index, int currentValue)
    {
        if (currentValue == MIN)
        {
            return;
        }
        RpcRemoveEnergy(index, currentValue);
    }

    [Command]
    private void CmdChangeStatusEffect(int index, int newValue)
    {
        RpcChangeStatusEffect(index, newValue);
    }

    [Command]
    private void CmdAddTurnsToStatusEffect(int index, int currentValue)
    {
        if (currentValue == MAX)
        {
            return;
        }
        RpcAddTurnsToStatusEffect(index, currentValue);
    }

    [Command]
    private void CmdRemoveTurnsToStatusEffect(int index, int currentValue)
    {
        if (currentValue == MIN)
        {
            return;
        }
        RpcRemoveTurnsToStatusEffect(index, currentValue);
    }

    [ClientRpc]
    private void RpcChangeCharacterName(int index, string newName)
    {
        HUDUISingleton.Instance.UpdateNames(index, newName);
    }

    [ClientRpc]
    private void RpcAddStock(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateStocks(index, currentValue + 1);
    }

    [ClientRpc]
    private void RpcRemoveStock(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateStocks(index, currentValue - 1);
    }

    [ClientRpc]
    private void RpcAddEnergy(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateEnergy(index, currentValue + 1);
    }

    [ClientRpc]
    private void RpcRemoveEnergy(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateEnergy(index, currentValue - 1);
    }

    [ClientRpc]
    private void RpcChangeStatusEffect(int index, int newValue)
    {
        HUDUISingleton.Instance.UpdateStatusEffect(index, newValue);
    }

    [ClientRpc]
    private void RpcAddTurnsToStatusEffect(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateStatusEffectTurns(index, currentValue + 1);
    }

    [ClientRpc]
    private void RpcRemoveTurnsToStatusEffect(int index, int currentValue)
    {
        HUDUISingleton.Instance.UpdateStatusEffectTurns(index, currentValue - 1);
    }
}
