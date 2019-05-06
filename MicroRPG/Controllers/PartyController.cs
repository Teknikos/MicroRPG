using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRPG.Models;
using MicroRPG.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MicroRPG.Controllers
{
    public class PartyController : Controller
    {
        IMemoryCache cache;
        PartyService backService;

        public PartyController(IMemoryCache cache, PartyService backService)
        {
            this.cache = cache;
            this.backService = backService;
        }

        [Route("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult _CreateOrLoad()
        {
            return PartialView();
        }

        [Route("Load")]
        public IActionResult Load()
        {
            return View();
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Summary")]
        public IActionResult Summary()
        {
            return View();
        }

        [Route("Backstory")]
        public IActionResult Backstory()
        {
            string currentPlayerID = "";

            PartyBackstoryVM pb = backService.GetValidCase(currentPlayerID);
            

            return View(pb);
        }

        [Route("Spells")]
        public IActionResult Spells()
        {
            return View();
        }
    }
}