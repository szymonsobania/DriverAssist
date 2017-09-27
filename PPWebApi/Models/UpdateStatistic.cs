using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class UpdateStatistic
    {
        public string Token { get; set; }
        public string PassageGuid { get; set; }
        public long StartTimestamp { get; set; }
        public long EndTimestamp { get; set; }
        public bool Delete { get; set; }
    }
}