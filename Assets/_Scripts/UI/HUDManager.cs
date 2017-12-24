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
    public GameObject CharactersInfo;
    public GameObject StatusEffectsInfo;
    public InputField[] CharacterNameFields; //size 8
    public InputField[] MovementInputFields; //size 8
    public Text[] StocksTexts; //size 8
    public Text[] EnergyTexts; //size 8
    public Dropdown[] StFxDropdowns; //size 16
    public InputField[] StFxValueInputFields; //size 16
    public Text[] StFxTurnsTexts; //size 16

    private HUD hud;

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
        else if (Input.GetButtonDown("Characters Info"))
        {
            CharactersInfo.SetActive(!CharactersInfo.activeSelf);
        }
        else if (Input.GetButtonDown("Status Effects Info"))
        {
            StatusEffectsInfo.SetActive(!StatusEffectsInfo.activeSelf);
        }

    }

    public void Init(HUD hud)
    {
        this.hud = hud;
    }

#region UI calls
    public void ChangeCharacterName(int index)
    {
        hud.ChangeCharacterName(index, CharacterNameFields[index].text);
    }

    public void ChangeMovementValue(int index)
    {
        hud.ChangeMovementValue(index, MovementInputFields[index].text);
    }

    public void AddStock(int index)
    {
        hud.AddStock(index, int.Parse(StocksTexts[index].text));
    }

    public void RemoveStock(int index)
    {
        hud.RemoveStock(index, int.Parse(StocksTexts[index].text));
    }

    public void AddEnergy(int index)
    {
        hud.AddEnergy(index, int.Parse(EnergyTexts[index].text));
    }

    public void RemoveEnergy(int index)
    {
        hud.RemoveEnergy(index, int.Parse(EnergyTexts[index].text));
    }

    public void ChangeStatusEffect(int index)
    {
        hud.ChangeStatusEffect(index, StFxDropdowns[index].value);
    }

    public void ChangeStatusEffectValue(int index)
    {
        hud.ChangeStatusEffectValue(index, StFxValueInputFields[index].text);
    }

    public void AddTurnsToStatusEffect(int index)
    {
        hud.AddTurnsToStatusEffect(index, int.Parse(StFxTurnsTexts[index].text));
    }

    public void RemoveTurnsToStatusEffect(int index)
    {
        hud.RemoveTurnsToStatusEffect(index, int.Parse(StFxTurnsTexts[index].text));
    }
#endregion

}
