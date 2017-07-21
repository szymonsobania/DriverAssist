using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriverAssist.Models
{
    public class PassageModel
    {
        public string UserID { get; set; }
        public int Passage_ID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Length { get; set; }
        public string Car { get; set; }
    }


    public class PassageTable
    {
        public static IList<PassageModel> GetPassageTable()
        {
            List<PassageModel> table = new List<PassageModel>();

            table.Add(new PassageModel()
            {
                Passage_ID = 1,
                Date = new DateTime(2017, 03, 29),
                Time = new TimeSpan(0, 2, 34, 21),
                Length = 28,
                Car = "Toyota Yaris"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 2,
                Date = new DateTime(2017, 03, 17),
                Time = new TimeSpan(0, 1, 57, 43),
                Length = 31,
                Car = "Peugeot 207"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 3,
                Date = new DateTime(2017, 03, 29),
                Time = new TimeSpan(0, 2, 34, 21),
                Length = 13,
                Car = "Renault Clio"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 4,
                Date = new DateTime(2017, 04, 01),
                Time = new TimeSpan(0, 5, 04, 41),
                Length = 5,
                Car = "Mazda 3"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 5,
                Date = new DateTime(2017, 03, 29),
                Time = new TimeSpan(0, 2, 34, 21),
                Length = 258,
                Car = "Toyota Yaris"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 6,
                Date = new DateTime(2017, 03, 17),
                Time = new TimeSpan(0, 1, 57, 43),
                Length = 78,
                Car = "Peugeot 207"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 7,
                Date = new DateTime(2017, 03, 29),
                Time = new TimeSpan(0, 2, 34, 21),
                Length = 41,
                Car = "Renault Clio"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 8,
                Date = new DateTime(2017, 04, 01),
                Time = new TimeSpan(0, 5, 04, 41),
                Length = 12,
                Car = "Mazda 3"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 9,
                Date = new DateTime(2017, 03, 29),
                Time = new TimeSpan(0, 2, 34, 21),
                Length = 41,
                Car = "Renault Clio"

            });

            table.Add(new PassageModel()
            {
                Passage_ID = 10,
                Date = new DateTime(2017, 04, 01),
                Time = new TimeSpan(0, 5, 04, 41),
                Length = 12,
                Car = "Mazda 3"

            });


            return table;
        }
    }
}