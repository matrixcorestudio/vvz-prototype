using Prototype.Player;
using Prototype.Utilities;
using UnityEngine;
using UnityEngine.Networking;

public class WeaklingZombieSpawner : NetworkBehaviour
{
    public GameObject zombiePrefab;
    Player m_player;

    private void Start()
    {
        m_player = GetComponent<Player>();
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButtonDown("Spawn Weakling") && m_player.PlayerType == Enums.PlayerType.Zombies)
        {
            CmdSpawnZombie();
        }
    }

    [Command]
    void CmdSpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab, transform.position + Vector3.left, transform.rotation, transform);
        NetworkServer.SpawnWithClientAuthority(zombie, connectionToClient);
    }
}
