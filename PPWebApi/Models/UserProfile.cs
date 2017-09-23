using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class UserProfile
    {
        public long id_uzytk { get; set; }
        public string nazwa_uzytk { get; set; }
        public string email { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string haslo { get; set; }
        public bool administrator { get; set; }
    }
}