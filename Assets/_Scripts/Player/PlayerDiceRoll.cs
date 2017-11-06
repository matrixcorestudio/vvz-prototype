using UnityEngine;
using UnityEngine.Networking;
using Prototype.Player;

[RequireComponent(typeof(Player))]
public class PlayerDiceRoll : NetworkBehaviour 
{
	public enum DiceType
	{
		None, CoinFlip, D4, D4Plus1, D6, D6Plus2, D10Max8, D12Min3, D4X2, D3X3, D4Plus4
	}

	public int offsetX = 0;
	public int offsetY = 0;
	public int buttonWidht = 70;
	public int buttonHeight = 20;

	Player m_player;

	public delegate void OnDiceRoll(Player player, string result);
	public event OnDiceRoll diceRollEvent;

	void Start()
	{
		m_player = GetComponent<Player>();
	}

	[ClientCallback]
	void OnGUI()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		int xpos = offsetX + 10;
		int ypos = offsetY + 10;        
		int vSpacing = 30;

        ypos += vSpacing;
        if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4.ToString()))
        {
            CmdRollDice(DiceType.D4);
        }

        ypos += vSpacing;
        if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4Plus1.ToString()))
        {
            CmdRollDice(DiceType.D4Plus1);
        }

        ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D6.ToString()))
		{
			CmdRollDice(DiceType.D6);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D6Plus2.ToString()))
		{
			CmdRollDice(DiceType.D6Plus2);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D10Max8.ToString()))
		{
			CmdRollDice(DiceType.D10Max8);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D12Min3.ToString()))
		{
			CmdRollDice(DiceType.D12Min3);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4X2.ToString()))
		{
			CmdRollDice(DiceType.D4X2);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D3X3.ToString()))
		{
			CmdRollDice(DiceType.D3X3);
		}

		ypos += vSpacing;
		if (GUI.Button(new Rect(xpos, ypos, buttonWidht, buttonHeight), DiceType.D4Plus4.ToString()))
		{
			CmdRollDice(DiceType.D4Plus4);
		}
	}

	[Server]
	int CalculateResult(DiceType dice)
	{
		switch (dice)
		{
            case DiceType.D4:
                return CalculateD4();
            case DiceType.D4Plus1:
                return CalculateD4() + 1;
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

	[Server] int CalculateD3()  { return Random.Range(1, 4); }
	[Server] int CalculateD4()  { return Random.Range(1, 5); }
	[Server] int CalculateD6()  { return Random.Range(1, 7); }
	[Server] int CalculateD8()  { return Random.Range(1, 9); }
	[Server] int CalculateD10() { return Random.Range(1, 11); }
	[Server] int CalculateD12() { return Random.Range(1, 13); }


	[Command]
	void CmdRollDice (DiceType diceType)
	{
		int rollResult = CalculateResult(diceType);
		if(diceRollEvent != null)
		{
			diceRollEvent(m_player, "Dice Roll: "+rollResult+", DiceType: "+diceType);
		}
		RpcRollDice(rollResult, diceType, m_player.name);
	}

	[ClientRpc]
	void RpcRollDice (int rollValue, DiceType diceType, string playerName)
	{
		DiceRollUI.Instance.RollDice(rollValue, diceType.ToString(), playerName);
	}
}
