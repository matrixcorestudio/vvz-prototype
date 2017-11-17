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
        [SyncVar] public Color playerColor;

        public Enums.PlayerType playerType = Enums.PlayerType.Vikings;

        [SerializeField] ToggleEvent onToggleShared;
        [SerializeField] ToggleEvent onToggleLocal;
        [SerializeField] ToggleEvent onToggleRemote;

        [SerializeField] List<Text> characterNames;
        [SerializeField] List<SpriteRenderer> renderers;

        private string[] vikingNamesFlight1 = { "[1] Storm Caller", "[2] Dice Master", "[3] Gambler", "[4] Earl Stone" };
        private string[] zombieNamesFlight1 = { "[1] Puniszher", "[2] Crawler", "[3] Lizard Tongue", "[4] Life-Taker" };
        private Color[] vikingColorsFlight1 = { Color.white, new Color(1f, 165f / 255f, 0), Color.red, Color.black};
        private Color[] zombieColorsFlight1 = { Color.magenta, new Color(165f / 255f, 42f / 255f, 42f / 255f), Color.green, Color.yellow};

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
            if (playerType == Enums.PlayerType.Vikings)
            {
                InitializeCharacters(vikingNamesFlight1, vikingColorsFlight1);
            }
            else if(playerType == Enums.PlayerType.Zombies)
            {
                InitializeCharacters(zombieNamesFlight1, zombieColorsFlight1);
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

        void InitializeCharacters(string[] names, Color[] colors)
        {
            for (int i = 0; i < names.Length; ++i)
            {
                characterNames[i].text = names[i];
                characterNames[i].color = colors[i];
                if (isLocalPlayer)
                {
                    renderers[i].sortingOrder = 1;
                }
            }
        }
    } 
}
