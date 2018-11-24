using System;
namespace energyDataApp.Models
{
    public class EnergyData
    {
        public string NODE { get; set; }
        public string ISO { get; set; }
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
