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

namespace energyDataApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TextReader reader = new StreamReader("DataTable.csv");
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<EnergyData>();

            return View();
        }

        public IActionResult Create(SearchParms NewParms)
        {
            return View("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
