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
            if (format == "asc")
            {
                return _context.EnergyRecord.OrderBy((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();
            }
            else {
                return _context.EnergyRecord.OrderByDescending((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();
            }
        }

        [HttpGet("{col}/{max}/{min}")]
        public ActionResult<List<EnergyRecord>> Filter(string col, string max, string min)
        {
            // I want to find the index of the min and max
            // then I want to find all the numbers inbetween the and make a list

            List<EnergyRecord> data = _context.EnergyRecord.ToList();
            int MaxIndex = new int();
            int MinIndex = new int();
            decimal minInt = Int32.Parse(min);
            decimal maxInt = Int32.Parse(max);

            for (var i = 0; i < data.Count; i++) {

                object recordValue = data[i].GetType().GetProperty(col).GetValue(data[i], null);
                decimal recordInt = Convert.ToInt32(recordValue);


                if (min != "0") {

                    decimal difference = Math.Abs(minInt - recordInt);

                    if (difference < 1) {
                        MinIndex = i;
                    }
                }

                if (max != "0")
                {

                    decimal difference = Math.Abs(maxInt - recordInt);

                    if (difference < 1)
                    {
                        MaxIndex = i;
                    }
                }
            }

            return data;
        }
    }
}
