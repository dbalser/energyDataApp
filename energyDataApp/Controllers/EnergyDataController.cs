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

        public List<EnergyRecord> Filter(List<EnergyRecord> data, string col, string max, string min)
        {
            List<EnergyRecord> FilteredRecords = new List<EnergyRecord>();
            double minInt = Int32.Parse(min);
            double maxInt = Int32.Parse(max);

            for (var i = 0; i < data.Count; i++)
            {

                object recordValue = data[i].GetType().GetProperty(col).GetValue(data[i], null);
                double recordInt = Convert.ToDouble(recordValue);

                if (maxInt == 0)
                {

                    if (minInt < recordInt)
                    {
                        FilteredRecords.Add(data[i]);
                    }
                }
                else if (minInt < recordInt)
                {

                    if (maxInt > recordInt)
                    {
                        FilteredRecords.Add(data[i]);
                    }
                }
            }

            return FilteredRecords;
        }

        public List<EnergyRecord> Sort(string col, string format)
        {

            if (format == "asc")
            {
                return _context.EnergyRecord.OrderBy((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();
            }
            else
            {
                return _context.EnergyRecord.OrderByDescending((x) => x.GetType().GetProperty(col).GetValue(x, null)).ToList();
            }
        }

        [HttpGet]
        public ActionResult<List<EnergyRecord>> GetAll()
        {
            return _context.EnergyRecord.ToList();
        }


        [HttpGet("{col}/{format}")]
        public ActionResult<List<EnergyRecord>> SortAction(string col, string format)
        {
            return Sort(col, format);
        }

        [HttpGet("{col}/{max}/{min}")]
        public ActionResult<List<EnergyRecord>> FilterAction(string col, string max, string min)
        {

            return Filter(_context.EnergyRecord.ToList(), col, max, min);
        }

        [HttpGet("{filcol}/{max}/{min}/{sortcol}/{format}")]
        public ActionResult<List<EnergyRecord>> SortAndFilterAction(string filcol, string max, string min, string sortcol, string format)
        {
            List<EnergyRecord> data = Sort(sortcol, format);
            return Filter(data, filcol, max, min);
        }

    }
}
