using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class GetStatistic
    {
        public string token { get; set; }
        public string passage_guid { get; set; }
    }
}