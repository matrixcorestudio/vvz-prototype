using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public enum CardType
    {
        None, Blessing, Curse
    }

    private int blessingLimit = 57;
    private int cursesLimit = 1054;

    private int xpos = Screen.width - 100;
    private int ypos = 10;
    private int buttonWidth = 80;
    private int buttonHeight = 20;

    private List<int> blessings;
    private List<int> curses;

    private int lastCardIndex = -1;
    private CardType lastCardType = CardType.None;
    private bool removeLastCard = false;
    private bool returnCurse = false;

    private CardDictionaries cards;

    // Use this for initialization
    void Start()
    {
        cards = new CardDictionaries();
        InitBlessings();
        InitCurses();
        //Debug.Log(blessings);
    }

    void OnGUI()
    {

        int _ypos = ypos;
        int _xpos = xpos;

        if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Blessing"))
        {
            lastCardType = CardType.Blessing;
            lastCardIndex = blessings[0];
            removeLastCard = true;
            returnCurse = false;
        }
        _ypos += buttonHeight + 10;
        if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Curse"))
        {
            lastCardType = CardType.Curse;
            lastCardIndex = curses[0];
            removeLastCard = true;
            returnCurse = true;
        }
        _ypos += buttonHeight + 10;
        if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth, buttonHeight), "Random"))
        {
            lastCardType = (Random.Range(0, 100) % 2) == 0 ? CardType.Blessing : CardType.Curse;
            lastCardIndex = (lastCardType == CardType.Blessing ? blessings[0] : curses[0]);
            removeLastCard = true;
        }

        if (removeLastCard)
        {
            _ypos += buttonHeight + 10;
            if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth + 20, buttonHeight), "Remove Last"))
            {
                RemoveCard(lastCardType == CardType.Blessing ? blessings : curses, lastCardType);
                removeLastCard = false;
                returnCurse = false;
            }
        }

        if (returnCurse)
        {
            _ypos += buttonHeight + 10;
            if (GUI.Button(new Rect(_xpos, _ypos, buttonWidth + 20, buttonHeight), "Return Curse"))
            {
                ShuffleDeck(curses, 3);
                returnCurse = false;
                removeLastCard = false;
            }
        }

        if (lastCardIndex >= 0)
        {
            _ypos += buttonHeight + 10;
            GUI.Label(new Rect(10, _ypos, Screen.width, buttonHeight),
                string.Format("Last card drawed: {0}  {1}",
                lastCardIndex,
                lastCardType == CardType.Blessing ? cards.blessingDictionary[lastCardIndex] : cards.curseDictionary[lastCardIndex]));
            _ypos += buttonHeight + 30;
            GUI.Label(new Rect(_xpos - 240, _ypos, 300, buttonHeight),
                string.Format("Remaining cards: {0} in {1} : {2} in {3}",
                blessings.Count, CardType.Blessing, curses.Count, CardType.Curse)); 
        }


    }

    private void ShuffleDeck(List<int> deck, int iterations)
    {
        if (iterations == 0)
        {
            PrintDeckToConsolini(deck);
            return;
        }

        List<int> half1 = new List<int>();
        List<int> half2 = new List<int>();

        for (int i = 0; i < deck.Count; ++i)
        {
            if (i < deck.Count / 2)
            {
                half1.Add(deck[i]);
            }
            else
            {
                half2.Add(deck[i]);
            }
        }

        for (int i = 0; i < deck.Count; ++i)
        {
            int rand = (Random.Range(0, 100) % 2);
            int next = -1;
            if (rand != 0 && half1.Count > 0)
            {
                next = half1[0];
                half1.RemoveAt(0);
            }
            else if (rand == 0 && half2.Count > 0)
            {
                next = half2[0];
                half2.RemoveAt(0);
            }
            else
            {
                if (half1.Count > 0)
                {
                    next = half1[0];
                    half1.RemoveAt(0);
                }
                else
                {
                    next = half2[0];
                    half2.RemoveAt(0);
                }
            }

            deck[i] = next;
        }
        ShuffleDeck(deck, iterations - 1);
    }
    private void RemoveCard(List<int> deck, CardType deckType)
    {
        deck.RemoveAt(0);
        if (deck.Count == 0)
        {
            if (deckType == CardType.Blessing)
            {
                InitBlessings();
                ShuffleDeck(blessings, 3);
            }
            else
            {
                InitCurses();
                ShuffleDeck(curses, 3);
            }
        }
    }

    private void InitBlessings()
    {
        blessings = new List<int>();
        //40 5 times, 41 3 times and 42 2 times
        for (int b = 0; b < blessingLimit; ++b)
        {
            if (b == 40)
            {
                for (int i = 0; i < 5; ++i)
                {
                    blessings.Add(b);
                }
            }
            else if (b == 41)
            {
                for (int i = 0; i < 3; ++i)
                {
                    blessings.Add(b);
                }
            }
            else if (b == 42)
            {
                for (int i = 0; i < 2; ++i)
                {
                    blessings.Add(b);
                }
            }
            else
            {
                blessings.Add(b);
            }
        }

        ShuffleDeck(blessings, Random.Range(4, 7));
    }

    private void InitCurses()
    {
        curses = new List<int>();
        for (int c = 1000; c < cursesLimit; ++c)
        {
            curses.Add(c);
        }

        ShuffleDeck(curses, Random.Range(4, 7));
    }

    private void PrintDeckToConsolini(List<int> deck)
    {
        CardType deckType = (deck[0] < 1000 ? CardType.Blessing : CardType.Curse);
        for (int i = 0; i < deck.Count; ++i)
        {
            Debug.LogFormat("{0} Card #{1}: {2}", deckType, i, deck[i]);
        }
    }


    #region Card Dictionaries
    class CardDictionaries
    {
        public readonly Dictionary<int, string> blessingDictionary;
        public readonly Dictionary<int, string> curseDictionary;

        public CardDictionaries()
        {
            blessingDictionary = new Dictionary<int, string>();
            curseDictionary = new Dictionary<int, string>();

            blessingDictionary.Add(0, "Lightboots - is Quick? No - Viking	Quickness(5): Add +5 to your total movement on your next movement phase.");
            blessingDictionary.Add(1, "Boots of Haste - is Quick? No - Viking	Quickness(TM*2): Double your total movement on your next movement phase.");
            blessingDictionary.Add(2, "Invisibility Cloack - is Quick? No - Viking	Invisible(): Cannot be attacked for 3 turns.");
            blessingDictionary.Add(3, "Tomahawk - is Quick? No - Zombie	Paralyzes any zombie (including yours) in range(7) for 2 turns.");
            blessingDictionary.Add(4, "Backpack - is Quick? No - Viking	Hiker(1): Increase your inventory size by 1 during this stock. When dying, your inventory size returns to normal. 1 use.");
            blessingDictionary.Add(5, "Thief Hood - is Quick? No - Viking	Steal a card to target viking in range(10). 1 use.");
            blessingDictionary.Add(6, "Zombie Taming Rod - is Quick? No - Zombie	Take possesion of target opponent's zombie for 1 turn. Cannot use your own zombie during this effect.");
            blessingDictionary.Add(7, "Golden Dice - is Quick? No - Both	Can re-roll a dice throw for your viking or for your zombie (can only choose one). 3 uses.");
            blessingDictionary.Add(8, "Map of Retreat - is Quick? No - Viking	Connoisseur: This viking can change its direction to any other direction available. 3 uses");
            blessingDictionary.Add(9, "Heavy Armor - is Quick? No - Viking	Buffed: Other vikings cannot pass through this viking.");
            blessingDictionary.Add(10, "Amulet of Loss - is Quick? No - Viking	Card Keeper: Your cards will not be removed when killed. 10 turns duration");
            blessingDictionary.Add(11, "Item Swap Rune - is Quick? No - Viking	Swap any card with target opponent.");
            blessingDictionary.Add(12, "Payback - is Quick? No - Viking	Card Keeper: Your cards will not be removed when killed. 5 turns duration. Killer dies.");
            blessingDictionary.Add(13, "Clairvoyance - is Quick? Yes - Viking	Flip face up the next 3 cards of each deck.");
            blessingDictionary.Add(14, "Hidden Arsenal - is Quick? Yes - Viking	All players with less than 3 cards, draw powerups until they have 3 cards in total.");
            blessingDictionary.Add(15, "Song of Hunting - is Quick? No - Viking	Your next zombie kill will grant you Quickness(4). If you kill your zombie, receive Quickness(8).");
            blessingDictionary.Add(16, "Breadcrumbs - is Quick? No - Viking	Activate for the first time and place a Breadcrumb in current square. Any further activation of this card will warp your viking to this square.");
            blessingDictionary.Add(17, "Magic Breadcrumbs - is Quick? No - Viking	Activate for the first time on a regular tile and place a Magic Breadcrumb. Activate the second time on another regular tile and both tiles with magic breadcrumbs become optional warps for everyone.");
            blessingDictionary.Add(18, "Geiger Counter - is Quick? No - Viking	Sneaky: Your Viking is not killed when Zombies land next to it, only when they land on the same square. 4 turns.");
            blessingDictionary.Add(19, "Made In Heaven - is Quick? Yes - Both	Everyone receives Quickness(TM * 2) for their next 3 movement phases.");
            blessingDictionary.Add(20, "Tankard of Greed - is Quick? Yes - Viking	Every Viking receives 2 powerups.");
            blessingDictionary.Add(21, "Toxic Waste - is Quick? No - Zombie	Your Zombie doesn't reduce its movement count when stepping in radioactive zones for the whole movement phase. 1 use.");
            blessingDictionary.Add(22, "Bite Jump - is Quick? Yes - Zombie	If a Viking is directly in front of your zombie, move it instantly to that viking's position and attack it..");
            blessingDictionary.Add(23, "Hunting Games - is Quick? Yes - Both	For the next 15 rounds, every opposing viking that kills your zombie gains 3 powerups + 5PP, any viking you kill with your zombie grants you with 4 powerups + 7 PP + Petrification");
            blessingDictionary.Add(24, "Vitamin Z - is Quick? Yes - Zombie	Agression(1): Your Zombie's range increases by 1 for 3 turns.");
            blessingDictionary.Add(25, "Brain Bug - is Quick? No - Zombie	Flexible: Your zombie can pass through other zombies for 5 turns.");
            blessingDictionary.Add(26, "Forceful Entry - is Quick? No - Viking	Select one card that is not quick from opossing player and turn it quick. 1 use.");
            blessingDictionary.Add(27, "Infuse - is Quick? No - Both	Swap the movement values between your Viking and your Zombie. 1 use.");
            blessingDictionary.Add(28, "Horse Rider - is Quick? No - Viking	Swift Strike: When equipped with a melee weapon, your viking can attack a zombie by stepping on it. 3 turns.");
            blessingDictionary.Add(29, "Disarm - is Quick? No - Viking	The nearest opossing Viking from your zombie receives Butterfingers(All) for 3 turns.");
            blessingDictionary.Add(30, "Restless Screeching - is Quick? No - Viking	Vikings in range(7) from your zombie receive Slowness(1) for their next 3 movement phases.");
            blessingDictionary.Add(31, "Risky Maneuver - is Quick? No - Viking	Move your viking to target zombie's position in range(10) and point to the same direction.");
            blessingDictionary.Add(32, "Dream Team Games - is Quick? No - Viking	For  the next 10 rounds, the nearest opossing Viking becomes your ally. Whenever you step into each other when moving, both gain +1 PP, if either land on their ally, both gain +2 PP and remove each other negative status effect. When either player draws from a deck, the other player draws too from the same deck. When either player’s Viking or Zombie respawns or is killed, the other player’s respawns/dies too. Both players win the game if either Viking escapes. Both players lose if either Viking loses all stocks.");
            blessingDictionary.Add(33, "Unstoppable Cavalry - is Quick? Yes - Viking	Keep advancing in the direction you are facing until you can no longer advance. Any character you land or step into, gets Buried for their next turn.");
            blessingDictionary.Add(34, "Shock Elixir - is Quick? No - Both	Removes Paralysis from any of your Characters.");
            blessingDictionary.Add(35, "Heaven's Gathering - is Quick? No - Viking	Pay 7 PP to activate this card. When activating, all vikings are moved to your viking's position and face to the same direction as yours.");
            blessingDictionary.Add(36, "Red Bool - is Quick? No - Viking	Flying: For 2 turns, your Viking can't be attacked by normal attacks and its movement does not count as Stepping.");
            blessingDictionary.Add(37, "Red Mystic Emblem - is Quick? No - Viking	Blessed: For 3 turns, your viking can't take trap cards. If you activate 2 Mystic Emblem cards, you additionally receive Heaven Protection for 10 turns. If you activate 3 Mystic Emblem cards, you additionally receive Heaven Benediction(2) for 2 turns.");
            blessingDictionary.Add(38, "Green Mystic Emblem - is Quick? No - Viking	Good Luck: For 3 turns, any card square where your viking lands is resolved as a Powerup square. If you activate 2 Mystic Emblem cards, you additionally receive Heaven Protection for 10 turns. If you activate 3 Mystic Emblem cards, you additionally receive Heaven Benediction(2) for 2 turns.");
            blessingDictionary.Add(39, "Blue Mystic Emblem - is Quick? No - Viking	Cured: For 3 turns, your Viking can't receive any negative status effect and also loses the ones it have. If you activate 2 Mystic Emblem cards, you additionally receive Heaven Protection for 10 turns. If you activate 3 Mystic Emblem cards, you additionally receive Heaven Benediction(2) for 2 turns.");
            blessingDictionary.Add(40, "Helping Hands 1 - is Quick? Yes - Viking	Your Viking gains +1 PP.");
            blessingDictionary.Add(41, "Helping Hands 2 - is Quick? Yes - Viking	Your Viking gains +3 PP.");
            blessingDictionary.Add(42, "Helping Hands 3 - is Quick? Yes - Viking	Your Viking gains +5 PP.");
            blessingDictionary.Add(43, "Helping Friends 1 - is Quick? Yes - Viking	Your Viking gains +1 stock.");
            blessingDictionary.Add(44, "Helping Friends 2 - is Quick? Yes - Viking	Your Viking gains +2 stocks.");
            blessingDictionary.Add(45, "Helping Angels 1 - is Quick? Yes - Viking	Strength(1): For its next 3 turns, your Viking gains +1 PP.");
            blessingDictionary.Add(46, "Helping Angels 2 - is Quick? Yes - Viking	Strength(2): For its next 2 turns, your Viking gains +2 PP. Heaven Protection: For 2 turns, your Viking cannot die.");
            blessingDictionary.Add(47, "Helping Angels 3 - is Quick? Yes - Viking	Strength(2): For its next 3 turns, your Viking gains +3 PP. Heaven Protection: For 3 turns, your Viking cannot die.");
            blessingDictionary.Add(48, "Helping God 1 - is Quick? Yes - Viking	Heaven Benediction(1): For 3 turns, your Viking gains +1 stock.");
            blessingDictionary.Add(49, "Helping God 2 - is Quick? Yes - Viking	Heaven Benediction(2): For 2 turns, your Viking gains +2 stocks. Cured: For 2 turns, your Viking cannot have negative status effects and loses the ones it have.");
            blessingDictionary.Add(50, "Basic Sword - is Quick? No - Viking	(Melee / TD: 5 / #U: 1) Instinct(0).");
            blessingDictionary.Add(51, "Steel Lance - is Quick? No - Viking	(Melee / TD: 8 / #U: 2) Slowness(1), Instinct(1).");
            blessingDictionary.Add(52, "Zombie Attacking Axe - is Quick? No - Viking	(Melee / TD: 11 / #U: 1) Slowness(1), Instinct(1), ");
            blessingDictionary.Add(53, "Wood Shield - is Quick? No - Viking	(Shield / TD: 5 / #U: 1) Blocker: Zombies can't pass through your Viking and can't attack you.");
            blessingDictionary.Add(54, "Silver Shield - is Quick? No - Viking	(Shield / TD: 10 / #U: 2) Weakness(1), Blocker: Zombies can't pass through your Viking and can't attack you.");
            blessingDictionary.Add(55, "Simple Bow - is Quick? No - Viking	(Bow / TD: 6 / #U: 2) Instinct Range(3, 0, Front): Infringes Arrow in the Knee to target zombie in range.");
            blessingDictionary.Add(56, "Majestic Bow - is Quick? No - Viking	(Bow / TD: 9 / #U: 4) Instinct Range(4, 1, Front): Infringes Arrow In The Knee to target zombie in range.");
            blessingDictionary.Add(57, "Sniper Bow - is Quick? No - Viking	(Bow / TD: 10 / #U: 3) Instinct Range(7, 3, Front): Infringes Arrow In The Knee to target zombie in range.");

            curseDictionary.Add(1000, "404: Not Found - is Quick? Yes - Viking	Error");
            curseDictionary.Add(1001, "Power Outage - is Quick? No - Zombie	Block the radioactive zones for 2 turns. Any character inside those zones when this card is activaded, dies.");
            curseDictionary.Add(1002, "Zombie Ambush - is Quick? Yes - Viking	You die by the attack of multiple zombies. Pay 7 PP to avoid.");
            curseDictionary.Add(1003, "Waste Pool - is Quick? Yes - Viking	Slowness(TM / 2): Divide by 2 your total movement during your next 2 movement phases.");
            curseDictionary.Add(1004, "Concussion - is Quick? Yes - Viking	Paralysis: This viking cannot move during its next movement phase. ");
            curseDictionary.Add(1005, "Nuke - is Quick? Yes - Zombie	Every zombie dies and gain paralysis for their next movement phase.");
            curseDictionary.Add(1006, "Nuclear Shockwave - is Quick? Yes - Zombie	Every zombie gains Quickness(3) for their next movement phase.");
            curseDictionary.Add(1007, "Poison Gas - is Quick? Yes - Viking	Death Counter(3, Highground): Viking dies unless it touches a highground during its next 3 turns.");
            curseDictionary.Add(1008, "Turbine Wind - is Quick? Yes - Viking	Lose all your cards in your inventory.");
            curseDictionary.Add(1009, "Walk of the Damned - is Quick? Yes - Viking	Paralysis: This viking cannot move. The turn duration will be determined by a random value between 1 and 3.");
            curseDictionary.Add(1010, "Rebelion - is Quick? Yes - Zombie	Paralysis: Your zombie cannot move for the next 2 movement phases.");
            curseDictionary.Add(1011, "Fatigue 1 - is Quick? Yes - Viking	Threatened(2): You cannot roll dice for this viking, instead, your total movement is 2 during your next 3 movement phases.");
            curseDictionary.Add(1012, "Amulet of Doom - is Quick? No - Viking	When you lose this card card from your inventory, you die.");
            curseDictionary.Add(1013, "Disease 3 - is Quick? Yes - Viking	Sickness: This viking cannot draw cards for 3 turns.");
            curseDictionary.Add(1014, "Broken Leg - is Quick? Yes - Viking	Attention Seeker(2): The area in which this viking can be killed by a zombie extends to range(2).");
            curseDictionary.Add(1015, "Disease 1 - is Quick? Yes - Viking	Sickness: This viking cannot draw cards for 1 turns.");
            curseDictionary.Add(1016, "Pandemic - is Quick? Yes - Viking	All vikings, except yours, receive Sickness for their next turn.");
            curseDictionary.Add(1017, "Fatigue 2 - is Quick? Yes - Viking	Threatened(4): You cannot roll dice for this viking, instead, your total movement is 4 during your next 3 movement phases.");
            curseDictionary.Add(1018, "Spooky Reactor - is Quick? Yes - Viking	Receive Petrification for your next turn unless you had it before. Shuffle this card back into the deck. This card is destroyed when every viking has received Petrification at least once.");
            curseDictionary.Add(1019, "Fairgrounds - is Quick? Yes - Viking	Every player discards randomly until everyone has the same amount of cards.");
            curseDictionary.Add(1020, "Sudden Escape - is Quick? Yes - Viking	Receive Threatened(4) and Connoisseur during your next movement phase, then receive Petrification.");
            curseDictionary.Add(1021, "Crawler Ambush - is Quick? Yes - Viking	You will die by the attack of multiple zombies unless you discard any 2 cards or pay 4 PP.");
            curseDictionary.Add(1022, "Thin Ice - is Quick? Yes - Viking	Sum 12 or more during your next 3 dice roll phases or else your viking warps right back to the spawn. (No kill)");
            curseDictionary.Add(1023, "Aftershock - is Quick? Yes - Both	Return any character in any event square to spawn (no kill),");
            curseDictionary.Add(1024, "Megathrust - is Quick? Yes - Viking	If this is the 3th time this card is drawed, everyone dies and this card is destroyed. If not, shuffle back to deck.");
            curseDictionary.Add(1025, "Heavy Blizzard - is Quick? Yes - Both	Everyone receives Slowness(TM / 2) for their next movement phase.");
            curseDictionary.Add(1026, "Gibs and Guts - is Quick? Yes - Zombie	Petrification: Your Zombie can't do anything for its next turn.");
            curseDictionary.Add(1027, "Painful Sacrifice - is Quick? Yes - Both	Discard all cards in your inventory or your zombie dies.");
            curseDictionary.Add(1028, "Viking Revenge - is Quick? No - Viking	Your Viking is Buried for its next 3 turns. At your 4th turn, receive Climber(3).");
            curseDictionary.Add(1029, "Collapsing Gorund - is Quick? Yes - Viking	Return your zombie back to where it was in your previous Begin Turn phase.");
            curseDictionary.Add(1030, "Peace Message - is Quick? No - Both	For 2 turns, everyone receive Fear.");
            curseDictionary.Add(1031, "Control C - is Quick? No - Viking	Copy Paster(1): whenever an opossing player draws a card, you receive one from the same deck. 1 turn.");
            curseDictionary.Add(1032, "Reality Changer - is Quick? Yes - Viking	All players' characters changes positions with the player that is next to him/her in the round's turn order.");
            curseDictionary.Add(1033, "Doom Tales - is Quick? Yes - Viking	Card Doomed(Powerup, 3): If you draw 3 powerups in the next 5 turns, your viking dies.");
            curseDictionary.Add(1034, "Fragile pillar - is Quick? No - Both	Create a wreckage tile one space behind you.");
            curseDictionary.Add(1035, "Infected Spore - is Quick? Yes - Zombie	Change your zombie type to any of your choosing till your third end phase.");
            curseDictionary.Add(1036, "Harming Fists 1 - is Quick? Yes - Viking	Your Viking loses 1 PP.");
            curseDictionary.Add(1037, "Harming Fists 2 - is Quick? Yes - Viking	Your Viking loses 2 PP.");
            curseDictionary.Add(1038, "Harming Fists 3 - is Quick? Yes - Viking	Your Viking loses 4 PP.");
            curseDictionary.Add(1039, "Harming Enemies 1 - is Quick? Yes - Viking	Your Viking loses 1 stock.");
            curseDictionary.Add(1040, "Harming Demons 1 - is Quick? Yes - Viking	Weakness(1): For 3 turns, your Viking loses 1 PP.");
            curseDictionary.Add(1041, "Harming Demons 2 - is Quick? Yes - Viking	Weakness(2): For 2 turns, your Viking loses 2 PP.");
            curseDictionary.Add(1042, "Harming Demons 3 - is Quick? Yes - Viking	Weakness(1): For 5 turns, your Viking loses 1 PP. Bad Luck: If landing on a card tile, you always draw a trap.");
            curseDictionary.Add(1043, "Harming Devil 1 - is Quick? Yes - Viking	If your Viking has more than 2 stocks, your Viking receives Hell Damnation(1) for 2 turns.");
            curseDictionary.Add(1044, "Harming Devil 2 - is Quick? Yes - Viking	Hell Damnation(1): For its next 2 turns, your Viking loses 1 stock.");
            curseDictionary.Add(1045, "Open Circuit 1 - is Quick? Yes - Viking	Disabled(Passive): Your Viking cannot use any passive ability for 3 turns.");
            curseDictionary.Add(1046, "Open Circuit 2 - is Quick? Yes - Viking	Disabled(Active): Your Viking cannot use any active ability for 3 turns.");
            curseDictionary.Add(1047, "Open Circuit 3 - is Quick? Yes - Viking	Disruption: Your Viking cannot use any ability at all for 3 turns.");
            curseDictionary.Add(1048, "Fool's Retreat - is Quick? No - Viking	Respawn your Viking.");
            curseDictionary.Add(1049, "Fool's Walk - is Quick? No - Viking	Reduce the movement of your Viking by 2 for this turn.");
            curseDictionary.Add(1050, "Fool's Invitation - is Quick? No - Viking	Move an opponent's Zombie to your Viking's position, facing the same way.");
            curseDictionary.Add(1051, "Fool's Nothingness - is Quick? No - Viking	Discard all your inventory. Discard triggers are not activated.");
            curseDictionary.Add(1052, "Fool's Greed - is Quick? No - Viking	Draw 2 Traps");
            curseDictionary.Add(1053, "Fool's Dichotomy - is Quick? No - Viking	Your Viking can be killed by Vikings as it was a Zombie");
            curseDictionary.Add(1054, "Fool's Duality - is Quick? No - Viking	Your Vikings gains any negative status effect until end of turn.");
        }
    }            
    #endregion

}                                  
                                   
                                   