using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class PlayerManager : NetworkBehaviour 
{
    public enum PlayerType { Viking, Zombie, Spectator}

	[SerializeField] ToggleEvent onToggleShared;
	[SerializeField] ToggleEvent onToggleLocal;
	[SerializeField] ToggleEvent onToggleRemote;

	[SyncVar] public string playerName;
	[SyncVar] public Color playerTextColor;

	public Text vikingName;
	public Text zombieName;

    private PlayerType playerType;

	[Header("Sprites")]
	[SerializeField] SpriteRenderer vikingSprite;
	[SerializeField] SpriteRenderer zombieSprite;

	void Start () 
	{
		vikingName.text = zombieName.text = playerName;
		vikingName.color = zombieName.color = playerTextColor;
		EnablePlayer();
	}

	void EnablePlayer ()
	{
		onToggleShared.Invoke (true);
		if(isLocalPlayer)
		{
			onToggleLocal.Invoke(true);
			vikingSprite.sortingOrder = zombieSprite.sortingOrder = -1;
		}
		else
		{
			onToggleRemote.Invoke(true);
		}
	}

	void DisablePlayer ()
	{
		onToggleShared.Invoke (false);
		if(isLocalPlayer)
		{
			onToggleLocal.Invoke(false);
		}
		else
		{
			onToggleRemote.Invoke(false);
		}
	}
}
