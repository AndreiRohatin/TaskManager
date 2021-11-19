using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskManager.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string uid)
        {
            if(string.IsNullOrWhiteSpace(uid)) return View();
            ViewBag.Leaves = ((Task)Program.MasterNode.Descendants().FirstOrDefault(o => o.UID == uid))?.Children;
            if (ViewBag.Leaves == null) ViewBag.Leaves = new List<Task>();
            return View();
        }

        public IActionResult Leaves(string uid)
        {
            return Content(
                !string.IsNullOrWhiteSpace(uid) ? JsonSerializer.Serialize(Program.MasterNode.Descendants().FirstOrDefault(o => o.UID == uid).Children) : JsonSerializer.Serialize(Program.Tasks), "application/json");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


    }
}