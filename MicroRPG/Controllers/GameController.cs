using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MicroRPG.Models.Constants;

namespace MicroRPG.Controllers
{
    public class GameController : Controller
    {
        

        [Route("Main")]
        public IActionResult Main()
        {
            return View ();
        }

        [Route("PlayerDetails")]
        public IActionResult _PlayerDetails()
        {
            return PartialView();
        }

        [Route("Creatures")]
        public IActionResult _Creatures()
        {
            string res = HttpContext.Session.GetString(SelectedEnvironment);
            return PartialView(nameof(_Creatures), res);
        }

        [Route("Creatures/{id}")]
        public IActionResult _CreatureDetails(string id)
        {
            return PartialView(nameof(_CreatureDetails), id);
        }

        [Route("Obstacles")]
        public IActionResult _Obstacles()
        {
            return PartialView();
        }

        [Route("Obstacles/{id}")]
        public IActionResult _ObstacleDetails(string id)
        {
            return PartialView(nameof(_ObstacleDetails), id);
        }

        [Route("Puzzles")]
        public IActionResult _Puzzles()
        {
            return PartialView();
        }

        [Route("Puzzles/{id}")]
        public IActionResult _PuzzleDetails(string id)
        {
            return PartialView(nameof(_PuzzleDetails), id);
        }
    }
}