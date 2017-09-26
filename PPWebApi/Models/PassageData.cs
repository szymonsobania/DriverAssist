using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class PassageData
    {
        public List<long> LocationTimestamp { get; set; }
        public List<double> LocationLat { get; set; }
        public List<double> LocationLng { get; set; }
        public List<long> AccTimestamp { get; set; }
        public List<double> AccX { get; set; }
        public List<double> AccY { get; set; }
        public List<double> AccZ { get; set; }
        public List<long> GyroTimestamp { get; set; }
        public List<double> GyroX { get; set; }
        public List<double> GyroY { get; set; }
        public List<double> GyroZ { get; set; }
        public List<long> LightTimestamp { get; set; }
        public List<double> LightIntensity { get; set; }

        public PassageData()
        {
            LocationTimestamp = new List<long>();
            LocationLat = new List<double>();
            LocationLng = new List<double>();
            AccTimestamp = new List<long>();
            AccX = new List<double>();
            AccY = new List<double>();
            AccZ = new List<double>();
            GyroTimestamp = new List<long>();
            GyroX = new List<double>();
            GyroY = new List<double>();
            GyroZ = new List<double>();
            LightTimestamp = new List<long>();
            LightIntensity = new List<double>();
        }
    }
}