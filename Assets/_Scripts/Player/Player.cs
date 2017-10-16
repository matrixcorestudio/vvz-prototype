using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using Prototype.Utilities;

namespace Prototype.Player
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    public class Player : NetworkBehaviour
    {
        private string[] vikingNamesFlight1 = { "[L] Storm Caller", "Dice Master", "Gambler", "Earl Stone" };
        private string[] zombieNamesFlight1 = { "[L] Puniszher", "Crawler", "Lizard Tongue", "Life-Taker" };

        private Color[] vikingColorsFlight1 = { Color.white, new Color(1f, 165f / 255f, 0), Color.red, Color.black};
        private Color[] zombieColorsFlight1 = { Color.magenta, new Color(165f / 255f, 42f / 255f, 42f / 255f), Color.green, Color.yellow};

        public List<Text> vikingNames;
        public List<Text> zombieNames;
        public GameObject vikingPlayer;
        public GameObject zombiePlayer;

        [SyncVar] public new string name;
        [SyncVar] public Color playerTextColor;

        private Enums.PlayerType playerType;

        [Header("Sprites")]
        [SerializeField] SpriteRenderer vikingSprite;
        [SerializeField] SpriteRenderer zombieSprite;

        private void Start()
        {
            if (playerTextColor == Color.blue)
            {
                playerType = Enums.PlayerType.Vikings;
                InitializeVikings();
            }
            else if(playerTextColor == Color.magenta)
            {
                playerType = Enums.PlayerType.Zombies;
                InitializeZombies();
            }
            else
            {
                playerType = Enums.PlayerType.Spectator;
                InitializeSpectator();
            }
        }

        private void InitializeVikings()
        {
            gameObject.GetComponent<NetworkTransformChild>().target = vikingPlayer.transform;
            zombiePlayer.GetComponent<ZombiePlayer>().DestroyZombies();
            for (int i = 0; i < vikingNamesFlight1.Length; ++i)
            {
                vikingNames[i].text = vikingNamesFlight1[i];
                vikingNames[i].color = vikingColorsFlight1[i];
            }

            vikingPlayer.GetComponent<VikingPlayer>().EnablePlayer();

            if (isLocalPlayer)
            {
                vikingSprite.sortingOrder = -1;
            }
        }

        private void InitializeZombies()
        {
            gameObject.GetComponent<NetworkTransformChild>().target = zombiePlayer.transform;
            vikingPlayer.GetComponent<VikingPlayer>().DestroyVikings();
            for (int i = 0; i < zombieNamesFlight1.Length; ++i)
            {
                zombieNames[i].text = zombieNamesFlight1[i];
                zombieNames[i].color = zombieColorsFlight1[i];
            }

            zombiePlayer.GetComponent<ZombiePlayer>().EnablePlayer();

            if (isLocalPlayer)
            {
                zombieSprite.sortingOrder = -1; 
            }
        }

        private void InitializeSpectator()
        {

        }
    } 
}
