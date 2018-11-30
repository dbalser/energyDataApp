using System;
using System.Collections.Generic;

namespace energyDataApp.Models
{
    public partial class EnergyRecord
    {
        //public int Id { get; set; }
        public string Node { get; set; }
        public string Iso { get; set; }
        public string NodeType { get; set; }
        public string PricingType { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string AvgPrice { get; set; }
        public string MaxPrice { get; set; }
        public string MinPrice { get; set; }
        public string AvgCongestion { get; set; }
        public string MaxCongestion { get; set; }
        public string MinCongestion { get; set; }
    }
}
