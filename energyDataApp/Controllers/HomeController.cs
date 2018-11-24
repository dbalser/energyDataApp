using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using energyDataApp.Models;
using System.Diagnostics.Contracts;
using System.IO;
using CsvHelper;
using System.Collections;

namespace energyDataApp.Controllers
{
    public class HomeController : Controller
    {

        public SearchParms CurrentSearchParms = new SearchParms();

        public IActionResult Index()
        {
            TextReader reader = new StreamReader("DataTable.csv");
            var csv = new CsvReader(reader);
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = csv.GetRecord<EnergyData>();
                CurrentSearchParms.AllEnergyData.Add(record);

            }

            return View(CurrentSearchParms);
        }

        public IActionResult Create(string FilterCol, string MaxNum, string MinNum, string SortCol, string SortMethod)
        {
            return View("Index", CurrentSearchParms);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
