using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DriverAssist
{
    public class PPConfig
    {
        private static string configFilePath = "C:\\PP\\PPConfig";
        private static string endPointAdress;
        public static string EndPointAdress
        {
            get
            {
                if (string.IsNullOrEmpty(endPointAdress))
                {
                    var lines = File.ReadAllLines(configFilePath);
                    endPointAdress = lines[0];
                }
                return endPointAdress;
            }
        }
    }
}