using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePoolManager : MonoBehaviour
{
    public GameObject ZombiePoolPanel;

    private ZombiePool zombiePool;

    private void Update()
    {
        if (Input.GetButtonDown("Zombie Pool"))
        {
            ZombiePoolPanel.SetActive(!ZombiePoolPanel.activeSelf);
        }
    }

    public void Init(ZombiePool zombiePool)
    {
        this.zombiePool = zombiePool;
    }

#region UI Calls
    public void Next4Cards()
    {
        if (this.zombiePool == null)
        {
            return;
        }

        zombiePool.Next4Cards();
    }
    
    public void AddRound()
    {
        if (this.zombiePool == null)
        {
            return;
        }

        zombiePool.AddRound();
    }

    public void UseCard(int index)
    {
        if (this.zombiePool == null)
        {
            return;
        }

        zombiePool.UseCard(index);
    }

#endregion
}
