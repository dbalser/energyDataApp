using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using energyDataApp.Models;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace energyDataApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
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
