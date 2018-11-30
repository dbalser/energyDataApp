using System;
using System.Collections.Generic;

namespace energyDataApp
{
    public partial class Energyrecords
    {
        public int Id { get; set; }
        public string Node { get; set; }
        public string Iso { get; set; }
        public string Nodetype { get; set; }
        public string Pricingtype { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Avgprice { get; set; }
        public string Maxprice { get; set; }
        public string Minprice { get; set; }
        public string Avgcongestion { get; set; }
        public string Maxcongestion { get; set; }
        public string Mincongestion { get; set; }
    }
}
