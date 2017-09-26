using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class PassageData
    {
        public List<Tuple<long, double, double>> LocationData { get; set; }
        public List<Tuple<long, double, double, double>> AccData { get; set; }
        public List<Tuple<long, double, double, double>> GyroData { get; set; }
        public List<Tuple<long, double>> LightData { get; set; }

        public PassageData()
        {
            LocationData = new List<Tuple<long, double, double>>();
            AccData = new List<Tuple<long, double, double, double>>();
            GyroData = new List<Tuple<long, double, double, double>>();
            LightData = new List<Tuple<long, double>>();
        }
    }
}