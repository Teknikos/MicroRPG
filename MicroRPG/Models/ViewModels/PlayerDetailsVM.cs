using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models.ViewModels
{
    public class PlayerDetailsVM
    {
        public string Name { get; set; }

        public int Attack { get; set; }
        public int DamageReduction { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Speed { get; set; }
        public int Wisdom { get; set; }
        
    }
}
