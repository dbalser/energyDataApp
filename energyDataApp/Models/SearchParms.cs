﻿using System;
namespace energyDataApp.Models
{
    public class SearchParms
    {
        public string FilterCol { get; set; }
        public int MinNum { get; set; }
        public int MaxNum { get; set; }
        public string SortCol { get; set; }
        public string SortMethod { get; set; }
    }
}