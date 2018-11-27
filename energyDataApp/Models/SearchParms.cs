
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;

namespace energyDataApp.Models
{
    public class SearchParms
    {

        static HttpClient client = new HttpClient();

        public string FilterCol { get; set; }
        public string MinNum { get; set; }
        public string MaxNum { get; set; }
        public string SortCol { get; set; }
        public string SortMethod { get; set; }

        public List<EnergyData> AllEnergyData = new List<EnergyData>();

        public void FillEnergyData () {

            TextReader reader = new StreamReader("DataTable.csv");
            var csv = new CsvReader(reader);
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = csv.GetRecord<EnergyData>();
                this.AllEnergyData.Add(record);

            }

        }

        public async void FillEnergyDataAsync()
        {
            List<EnergyData> data = new List<EnergyData>();

            HttpResponseMessage response = await client.GetAsync("/EnergyData/GetAll");
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<List<EnergyData>>();
                //this.AllEnergyData.AddRange(data);
            }
        }

        public void FillParamaters (string FilterCol, string MaxNum, string MinNum, string SortCol, string SortMethod) {

            this.FilterCol = FilterCol;
            this.MaxNum = MaxNum;
            this.MinNum = MinNum;
            this.SortCol = SortCol;
            this.SortMethod = SortMethod;
        }

        //public List<object> FilterData () {

        //    // I need FilterCol
        //    // I need Min, Max or both
        //    // I will find the Min and Max, from the col If it is diclared
        //    // Then I use the min and max's indexes to find the rest of what needs to be displayed
        //    // By ether min - max, 0 - max, min - 1501

        //    string MaxIndex = "0"

        //    for (int i = 0; i < this.AllEnergyData.Count; i++) {

        //        var record = this.AllEnergyData[i];
        //        var valueFromCol = record.GetType().GetProperty(this.FilterCol).GetValue(record, null);

        //        //if(this.MaxNum < valueFromCol) {
        //        //    MaxNum = (i - 1) + "";
        //        //}
               
        //    }

        //    return this.AllEnergyData;
        //}

        public List<EnergyData> SortData()
        {

            return this.AllEnergyData;
        }

    }
}
