namespace Prototype.Utilities
{

    public static class Enums
    {
        public enum DiceType
        {
            None,
            CoinFlip,
            D4,
            D4Plus1,
            D6,
            D6Plus2,
            D10Max8,
            D12Min3,
            D4X2,
            D3X3,
            D4Plus4
        }

        public enum PlayerType
        {
            Vikings,
            Zombies,
            Spectator
        }

        public enum RollType
        {
            SingleRoll,
            VikingRoll,
            ZombieRoll
        }
    }

}