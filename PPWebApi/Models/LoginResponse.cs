using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class LoginResponse : Response
    {
        public string Token { get; set; }
    }
}