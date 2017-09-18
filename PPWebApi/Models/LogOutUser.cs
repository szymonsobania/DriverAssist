using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class LogOutUser
    {
        public string AuthToken { get; set; }
        public string UserAgent { get; set; }
    }
}