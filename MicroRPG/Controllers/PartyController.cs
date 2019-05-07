using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRPG.Models.Backstory;
using MicroRPG.Models;
using MicroRPG.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(PartyCreateVM playerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(playerVM);
            }

            backService.AddPlayer(playerVM);

            return RedirectToAction(nameof(Summary));
        }

        [Route("Summary")]
        public IActionResult Summary()
        {
            PartySummaryVM partySummary = backService.GetPartySummary();

            return View(nameof(Summary), partySummary);
        }

        [Route("Backstory")]
        public IActionResult Backstory()
        {

            return View(backService.GetPartyIDs());
        }

        [Route("Backstory/{playerID}")]
        public IActionResult Backstory(int playerID)
        {
            PartyBackstoryVM pb = backService.GetValidCase(playerID);

            return Ok(pb);
        }

        [Route("Spells")]
        public IActionResult Spells()
        {
            return View();
        }
    }
}