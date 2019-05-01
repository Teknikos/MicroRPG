using MicroRPG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models
{
    public class PlayersService
    {
        public PlayerVM GetPlayer()
        {
            PlayerVM player = new PlayerVM { Name = "Freds", Health = 1 };
            return player;
        }
    }
}
