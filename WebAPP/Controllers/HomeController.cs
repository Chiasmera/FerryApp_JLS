using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            FerryBL ferryBL = new FerryBL();
            HashSet<Ferry> ferries = ferryBL.GetAll();
            if (ferries == null)
            {
                ferries = new HashSet<Ferry>();
            }
            return View(ferries);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}