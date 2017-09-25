using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
    public class Passage
    {
        public long UserID { get; set; }
        public long Passage_ID { get; set; }
        public Guid PassageGuid { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Length { get; set; }
        public string Car { get; set; }
    }
}