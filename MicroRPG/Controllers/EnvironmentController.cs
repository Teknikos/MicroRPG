using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MicroRPG.Models.Constants;

namespace MicroRPG.Controllers
{
    public class EnvironmentController : Controller
    {
        [Route("environment")]
        public IActionResult Choose()
        {
            return View();
        }
        [Route("environment/Zoomed")]
        public IActionResult Zoomed()
        {
            return PartialView();
        }

        [Route("environment/{id}")]
        public IActionResult SetEnvironment(string id)
        {
            HttpContext.Session.SetString(SelectedEnvironment, id);
            return Ok();
        }
    }
}