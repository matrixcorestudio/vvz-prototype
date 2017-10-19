using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class MiniZombie : NetworkBehaviour {
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    [SerializeField] ToggleEvent onToggleLocal;
	public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            EnableMiniZombie();
        }
	}

    void EnableMiniZombie()
    {
        onToggleLocal.Invoke(true);
    }

    [Command]
    public void CmdRemoveZombie()
    {
        NetworkServer.Destroy(gameObject);
    }
}
