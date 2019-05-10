using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRPG.Models.Backstory
{
    public static class BSConstants
    {
        public const int MininumPlayers = 2;
        public const int MaximumPlayers = 6;
        public const string ThisPlayer = "_Player";
        public const string RelatedPlayer = "_Related";
        public const string OtherPlayer = "_OtherPlayer#";

        public enum Class
        {
            Cleric, Mage, Rogue, Warrior 
        };

        public static readonly Dictionary<Class, Stats> StatsForClass = new Dictionary<Class, Stats>
        {
            { Class.Cleric, new Stats { Attack = 10, Speed = 8, Wisdom = 7, HP = 14, MP = 9, DamageReduction = 3 } },
            { Class.Mage, new Stats{ Attack = 3, Speed = 6, Wisdom = 18, HP = 12, MP = 15, DamageReduction = 0 } },
            { Class.Rogue, new Stats{ Attack = 11, Speed = 18, Wisdom = 4, HP = 16, MP = 6, DamageReduction = 2 } },
            { Class.Warrior, new Stats{ Attack = 18, Speed = 11, Wisdom = 3, HP = 20, MP = 2, DamageReduction = 2 } },
        };

        public static readonly Tag LawfulTag = new Tag("Lawful");
        public static readonly Tag UnlawfulTag = new Tag("Unlawful");
        public static readonly Tag OutcastTag = new Tag("Outcast");
        public static readonly Tag GreedyTag = new Tag("Greedy");
        public static readonly Tag HonorableTag = new Tag("Honorable");
        public static readonly Tag ReligiousTag = new Tag("Religious");
        public static readonly Tag NoFamilyTag = new Tag("NoFamily");
        
    }
}
