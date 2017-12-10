using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePoolManager : MonoBehaviour
{
    public GameObject ZombiePoolPanel;

    private ZombiePool zombiePool;
    private Button[] buttons;
    private bool activateButtons = false;

    private void Start()
    {
        buttons = ZombiePoolPanel.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Zombie Pool"))
        {
            ZombiePoolPanel.SetActive(!ZombiePoolPanel.activeSelf);
        }

        if (activateButtons)
        {
            activateButtons = false;
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }

    }

    public void Init(ZombiePool zombiePool, bool isLocalPlayer = false)
    {
        this.zombiePool = zombiePool;
        activateButtons = isLocalPlayer;
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
