﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Get_5_Day_Forecast.Model
{
    public class AvgDayForecast
    {
        [Key]
        public Guid ForecastId { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public decimal AvgMaxTemp { get; set; }
        public decimal AvgMinTemp { get; set; }
    }
}
