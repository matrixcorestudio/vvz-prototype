namespace Prototype.Utilities
{
    public static class Enums
    {
        public enum TileType
        {
            Empty = 1,
            Regular = 2,
            Radioactive = 4,
            ZombieSpawn = 8,
            VikingSpawn = 16,
            VikingGoal = 32,
            Curse = 64,
            Blessing = 128,
            RandomCard = 256,
            Highground = 512,
            WarpForced = 1024,
            WarpOptional = 2048,
            GreenHillZone = 4096,
            Switch = 8192,
            PPSpace = 16384,
            ConditionalGate = 32768
        }

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

        public enum StatusEffect
        {
            None,
            Paralysis,
            Threatened,
            Slowness,
            Quickness,
            Disruption,
            Fear,
            Connoisseur,
            Cured,
            Flying,
            DivineProtection,
            Invisible,
            Buried,
            Nonexistent,
            Petrification,
            Wounded,
            Disabled,
            Sickness,
            Blessed,
            Blocker,
            GoodLuck,
            Hiker,
            Instinct,
            Sneaky,
            BadLuck,
            Butterfingers,
            Cursed,
            Exposed,
            GamblerLuck,
            Nervousness,
            MoreFear,
            Aggression,
            Flexible,
            Unstoppable,
            Intimidating,
            Radical,
            Allergic
        }
    }

}