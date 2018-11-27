using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using energyDataApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace energyDataApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyDataController : ControllerBase
    {
        private readonly EnergyDataContext _context;

        public EnergyDataController(EnergyDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<EnergyRecord>> GetAll()
        {
            return _context.EnergyRecord.ToList();
        }


        [HttpGet("{col}/{format}")]
        public ActionResult<List<EnergyRecord>> Sort(string col, string format)
        {
            List<EnergyRecord> list = _context.EnergyRecord.ToList();
            List<EnergyRecord> SortedList = (System.Collections.Generic.List<energyDataApp.Models.EnergyRecord>)(from data in list
                                                                                                                 orderby data.AvgPrice
                                                                                                                 select data);


            return SortedList;
        }
    }
}
