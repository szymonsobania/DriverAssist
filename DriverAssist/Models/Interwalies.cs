using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriverAssist.Models
{
    public class Interwalies
    {
        public int AccelerometerIntervalLength { get; set; }
        public int GpsIntervalLength { get; set; }
        public int GyroscopeIntervalLength { get; set; }
        public int MagneticIntervalLength { get; set; }
    }
}