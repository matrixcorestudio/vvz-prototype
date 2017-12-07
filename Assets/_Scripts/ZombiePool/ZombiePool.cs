using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ZombiePool : NetworkBehaviour
{
    private ZombiePoolManager zombiePoolMngr;
    private bool foundZombiePoolMngr = false;

    private void Update()
    {
        if (!isLocalPlayer || foundZombiePoolMngr)
        {
            return;
        }

        zombiePoolMngr = FindObjectOfType<ZombiePoolManager>();
        if (zombiePoolMngr != null)
        {
            Debug.Log("Found Zombie Pool Manager");
            foundZombiePoolMngr = true;
            zombiePoolMngr.Init(this);
        }
    }

    #region ClientCallbacks
    [ClientCallback]
    public void AddRound()
    {
        CmdAddRound();
    }

    [ClientCallback]
    public void Next4Cards()
    {
        CmdNext4Cards();
    }

    [ClientCallback]
    public void UseCard(int index)
    {
        CmdUseCard(index);
    }

    #endregion

    #region Commands
    [Command]
    private void CmdAddRound()
    {
        RpcAddRound();
    }

    [Command]
    private void CmdNext4Cards()
    {
        RpcNext4Cards();
    }

    [Command]
    private void CmdUseCard(int index)
    {
        RpcUseCard(index);
    }
    #endregion

    #region ClientRPCs
    [ClientRpc]
    public void RpcAddRound()
    {
        ZombiePoolUISingleton.Instance.AddRound();
    }

    [ClientRpc]
    public void RpcNext4Cards()
    {
        ZombiePoolUISingleton.Instance.Next4Cards();
    }

    [ClientRpc]
    public void RpcUseCard(int index)
    {
        ZombiePoolUISingleton.Instance.UseCard(index);
    }
    #endregion
}
