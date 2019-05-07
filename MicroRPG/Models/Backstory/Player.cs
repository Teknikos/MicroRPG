using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRPG.Models.Backstory
{
    public class Player
    {
        public string Name { get; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public int StatGainCount { get; set; }  

        public List<Tag> Tags { get; set; }

        private static int idCount = 0;

        public int ID { get; }

        public Stats Stats { get; set; }

        public Player(string name, int age)
        {
            Name = name;
            Age = age;
            Gender = string.Empty;
            StatGainCount = 0;
            ID = idCount++;

            Tags = new List<Tag>();

            Stats = new Stats
            {
                Attack = 0,
                Speed = 0,
                Wisdom = 0,
                HP = 0,
                MP = 0,
                DamageReduction = 0,
            };

        }

    }
}
