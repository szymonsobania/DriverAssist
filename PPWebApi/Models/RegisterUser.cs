﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class RegisterUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}