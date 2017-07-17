using UnityEngine;
using UnityEngine.Networking;

public class PlayerDiceRoll : NetworkBehaviour 
{
	public enum DiceType
	{
		None, D6, D6Plus2, D10Max8, D12Min3, D4X2, D3X3, D4Plus4  
	}

	public int offsetX = 0;
	public int offsetY = 0;
	public int buttonWidht = 70;
	public int buttonHeight = 20;

	int lastVikingRollValue = 0;
	int lastZombieRollValue = 0;

	DiceType lastDiceType = DiceType.None;

	void OnGUI()
	{
		int xpos = offsetX + 10;
		int ypos = offsetY + 10;        
		int vSpacing = 30;

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D6.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D6);
			lastDiceType = DiceType.D6;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D6Plus2.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D6Plus2);
			lastDiceType = DiceType.D6Plus2;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D10Max8.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D10Max8);
			lastDiceType = DiceType.D10Max8;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D12Min3.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D12Min3);
			lastDiceType = DiceType.D12Min3;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4X2.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D4X2);
			lastDiceType = DiceType.D4X2;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D3X3.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D3X3);
			lastDiceType = DiceType.D3X3;
			CmdRollDice();
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4Plus4.ToString()))
		{
			//lastVikingRollValue = CalculateResult(lastDiceType = DiceType.D4Plus4);
			lastDiceType = DiceType.D4Plus4;
			CmdRollDice();
		}
	}

	[Server]
	int CalculateResult(DiceType dice)
	{
		switch (dice)
		{
		case DiceType.D6:
			return CalculateD6();
		case DiceType.D6Plus2:
			return CalculateD6() + 2;
		case DiceType.D10Max8:
			int resultD10 = CalculateD10();
			if (resultD10 > 8) { return 8; }
			return resultD10;
		case DiceType.D12Min3:
			int resultD12 = CalculateD12();
			if (resultD12 < 3) { return 3; }
			return resultD12;
		case DiceType.D4X2:
			return CalculateD4() + CalculateD4();
		case DiceType.D3X3:
			return CalculateD3() + CalculateD3() + CalculateD3();
		case DiceType.D4Plus4:
			return CalculateD4() + 4;
		case DiceType.None: //Pass through
		default:
			return -1;
		}
	}

	int CalculateD3() { return Random.Range(1, 3); }
	int CalculateD4() { return Random.Range(1, 4); }
	int CalculateD6() { return Random.Range(1, 6); }
	int CalculateD8() { return Random.Range(1, 8); }
	int CalculateD10() { return Random.Range(1, 10); }
	int CalculateD12() { return Random.Range(1, 12); }

	void Update ()
	{
		if(!isLocalPlayer)
			return;
	}

	[Command]
	void CmdRollDice ()
	{
		lastVikingRollValue = CalculateResult(lastDiceType);
		lastZombieRollValue = CalculateResult(lastDiceType);
		RpcRollDice(lastVikingRollValue, lastZombieRollValue);
	}

	[ClientRpc]
	void RpcRollDice (int vRoll, int zRoll)
	{
		DiceRollUI.Instance.RollDice(vRoll, zRoll);
		DiceRollUI.Instance.lastVikingRollValue = vRoll;
		DiceRollUI.Instance.lastZombieRollValue = zRoll;
		DiceRollUI.Instance.lastDiceType = lastDiceType.ToString();
		DiceRollUI.Instance.UpdateInfo();
	}
}
