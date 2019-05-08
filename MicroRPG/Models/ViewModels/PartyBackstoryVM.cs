using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models.ViewModels
{
    public class PartyBackstoryVM
    {
        public PartyBackstoryVM()
        {
            Description = string.Empty;
            Outcomes = new string[0];
        }

        public string CurrentPlayerName { get; set; }

        public string Description { get; set; }

        public string[] Outcomes { get; set; }

        public int[] PlayerIDs { get; set; }
    }
}
