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
        private readonly EnergyDataDBContext _context;

        public EnergyDataController(EnergyDataDBContext context)
        {
            _context = context;
        }

        public List<Energyrecords> Filter(List<Energyrecords> data, string col, string max, string min)
        {
            List<Energyrecords> FilteredRecords = new List<Energyrecords>();
            double minInt = Int32.Parse(min);
            double maxInt = Int32.Parse(max);

            for (var i = 0; i < data.Count; i++)
            {

                object recordValue = data[i].GetType().GetProperty(col).GetValue(data[i], null);
                double recordInt = Convert.ToDouble(recordValue);

                if (maxInt == 0)
                {

                    if (minInt <= recordInt)
                    {
                        FilteredRecords.Add(data[i]);
                    }
                }
                else if (minInt < recordInt)
                {

                    if (maxInt >= recordInt)
                    {
                        FilteredRecords.Add(data[i]);
                    }
                }
            }

            return FilteredRecords;
        }

        public List<Energyrecords> Sort(string col, string format)
        {

            // This handles the numeric string cols 
            if (col == "Avgprice" || col == "Maxprice" || col == "Minprice" || col == "Avgcongestion" || col == "Maxcongestion" || col == "Mincongestion" ) {

                if (format == "asc")
                {
                    return _context.Energyrecords.OrderBy((x) => Convert.ToDouble(x.GetType().GetProperty(col).GetValue(x, null))).ToList();
                }

                return _context.Energyrecords.OrderByDescending((x) => Convert.ToDouble(x.GetType().GetProperty(col).GetValue(x, null))).ToList();

            }

            // This handles string cols
            if (format == "asc")
            {
                return _context.Energyrecords.OrderBy((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();
            }

            return _context.Energyrecords.OrderByDescending((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();

        }

        [HttpGet]
        public ActionResult<List<Energyrecords>> GetAll()
        {
            return _context.Energyrecords.ToList();
        }


        [HttpGet("{col}/{format}")]
        public ActionResult<List<Energyrecords>> SortAction(string col, string format)
        {
            return Sort(col, format);
        }

        [HttpGet("{col}/{max}/{min}")]
        public ActionResult<List<Energyrecords>> FilterAction(string col, string max, string min)
        {
            return Filter(_context.Energyrecords.ToList(), col, max, min);
        }

        [HttpGet("{filcol}/{max}/{min}/{sortcol}/{format}")]
        public ActionResult<List<Energyrecords>> SortAndFilterAction(string filcol, string max, string min, string sortcol, string format)
        {
            List<Energyrecords> data = Sort(sortcol, format);
            return Filter(data, filcol, max, min);
        }

    }
}
