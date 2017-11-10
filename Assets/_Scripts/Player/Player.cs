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
        [SerializeField] ToggleEvent onToggleSpectator;

        [SerializeField] List<Text> characterNames;
        [SerializeField] List<SpriteRenderer> renderers;
        [SerializeField] Sprite vikingSprite;
        [SerializeField] Sprite zombieSprite;

        
        private string[] vikingNamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone" };
        private string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };
        private Color[] vikingColorsFlight1 = { Color.white, new Color(1f, 165f / 255f, 0), Color.red, Color.black};
        private Color[] zombieColorsFlight1 = { Color.magenta, new Color(165f / 255f, 42f / 255f, 42f / 255f), Color.green, Color.yellow};

        private Enums.PlayerType m_playerType;
        public Enums.PlayerType PlayerType { get { return m_playerType; } }

        private Enums.DiceType[] vikingDiceTypes;
        public Enums.DiceType[] VikingDiceTypes
        {
            get { return vikingDiceTypes; }
            set { vikingDiceTypes = value; }
        }

        private Enums.DiceType[] zombieDiceTypes;
        public Enums.DiceType[] ZombieDiceTypes
        {
            get { return zombieDiceTypes; }
            set { zombieDiceTypes = value; }
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            gameObject.tag = "LocalPlayer";
        }

        private void Awake()
        {
            vikingDiceTypes = new Enums.DiceType[4];
            zombieDiceTypes = new Enums.DiceType[4];

            //Hardcoded for flight 1
            vikingDiceTypes[0] = Enums.DiceType.D6Plus2;
            vikingDiceTypes[1] = Enums.DiceType.D6Plus2;
            vikingDiceTypes[2] = Enums.DiceType.D12Min3;
            vikingDiceTypes[3] = Enums.DiceType.D10Max8;

            //Hardcoded for flight 1
            zombieDiceTypes[0] = Enums.DiceType.D6;
            zombieDiceTypes[1] = Enums.DiceType.D4X2;
            zombieDiceTypes[2] = Enums.DiceType.D6;
            zombieDiceTypes[3] = Enums.DiceType.D6;
        }

        private void Start()
        {
            if (playerTextColor == Color.blue)
            {
                m_playerType = Enums.PlayerType.Vikings;
                InitializeVikings();
            }
            else if(playerTextColor == Color.magenta)
            {
                m_playerType = Enums.PlayerType.Zombies;
                InitializeZombies();
            }
            else
            {
                m_playerType = Enums.PlayerType.Spectator;
                InitializeSpectator();
            }            
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

            EnablePlayer();
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

            EnablePlayer();
        }

        private void InitializeSpectator()
        {
            onToggleSpectator.Invoke(false);
        }
    } 
}
