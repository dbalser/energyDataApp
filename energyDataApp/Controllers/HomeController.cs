using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using energyDataApp.Models;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace energyDataApp.Controllers
{
    public class HomeController : Controller
    {

        public SearchParms CurrentSearchParms = new SearchParms();

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create(string FilterCol, string MaxNum, string MinNum, string SortCol, string SortMethod)
        {
            //CurrentSearchParms.FillParamaters(FilterCol, MaxNum, MinNum, SortCol, SortMethod);
            //CurrentSearchParms.FillEnergyData();

            //CurrentSearchParms.FilterData();
            //CurrentSearchParms.SortData();

            return Redirect("/Home/Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
