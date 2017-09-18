using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class Reading
    {
        public string AuthToken { get; set; }
        public string PackageNumber { get; set; }
        public string Content { get; set; }
        public bool LastPackage { get; set; }
    }
}