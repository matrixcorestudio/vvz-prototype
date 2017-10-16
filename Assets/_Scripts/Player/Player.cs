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
        [SyncVar] public new string name;
        [SyncVar] public Color playerTextColor;

        [SerializeField] ToggleEvent onToggleShared;
        [SerializeField] ToggleEvent onToggleLocal;
        [SerializeField] ToggleEvent onToggleRemote;

        [SerializeField] List<Text> characterNames;
        [SerializeField] List<SpriteRenderer> renderers;
        [SerializeField] Sprite vikingSprite;
        [SerializeField] Sprite zombieSprite;

        private Enums.PlayerType playerType;
        private string[] vikingNamesFlight1 = { "[L] Storm Caller", "Dice Master", "Gambler", "Earl Stone" };
        private string[] zombieNamesFlight1 = { "[L] Puniszher", "Crawler", "Lizard Tongue", "Life-Taker" };
        private Color[] vikingColorsFlight1 = { Color.white, new Color(1f, 165f / 255f, 0), Color.red, Color.black};
        private Color[] zombieColorsFlight1 = { Color.magenta, new Color(165f / 255f, 42f / 255f, 42f / 255f), Color.green, Color.yellow};

        

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
            EnablePlayer();
        }

        public void EnablePlayer()
        {
            onToggleShared.Invoke(true);
            if (isLocalPlayer)
            {
                onToggleLocal.Invoke(true);
            }
            else
            {
                onToggleRemote.Invoke(true);
            }
        }

        public void DisablePlayer()
        {
            onToggleShared.Invoke(false);
            if (isLocalPlayer)
            {
                onToggleLocal.Invoke(false);
            }
            else
            {
                onToggleRemote.Invoke(false);
            }
        }

        private void InitializeVikings()
        {
            for (int i = 0; i < vikingNamesFlight1.Length; ++i)
            {
                characterNames[i].text = vikingNamesFlight1[i];
                characterNames[i].color = vikingColorsFlight1[i];
                renderers[i].sprite = vikingSprite;
                if (isLocalPlayer)
                {
                    renderers[i].sortingOrder = -1;
                }
            }
        }

        private void InitializeZombies()
        {
            for (int i = 0; i < zombieNamesFlight1.Length; ++i)
            {
                characterNames[i].text = zombieNamesFlight1[i];
                characterNames[i].color = zombieColorsFlight1[i];
                renderers[i].sprite = zombieSprite;
                if (isLocalPlayer)
                {
                    renderers[1].sortingOrder = -1;
                }
            }
        }

        private void InitializeSpectator()
        {
            //if(isLocalPlayer)
            //{
            //    foreach (GameObject character in transform)
            //    {
            //        Network.Destroy(character);
            //    }
            //}
        }
    } 
}
