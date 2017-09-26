using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using AuthWebApi;
using AuthWebApi.Controllers;
using AuthWebApi.Models;
using Newtonsoft.Json;

namespace DriverAssist.Models
{
    public class PassageModel
    {
        public long UserID { get; set; }
        public long Passage_ID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Length { get; set; }
        public string Car { get; set; }
    }


    public class PassageTable
    {
        public static IList<Passage> GetPassageTable(string authToken)
        {
            if (authToken != null)
            {
                GetUserToken token = new GetUserToken {token = authToken};
                var httpClient = new HttpClient {Timeout = new TimeSpan(0, 0, 10, 0)};
                var json = JsonConvert.SerializeObject(token);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponseMessage = httpClient.PostAsync(@"http://localhost:50236/passages", httpContent).Result;
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<Passage>>(contents);
                    return result;
                }
            }

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 1,
            //    Date = new DateTime(2017, 03, 29),
            //    Time = new TimeSpan(0, 2, 34, 21),
            //    Length = 28,
            //    Car = "Toyota Yaris"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 2,
            //    Date = new DateTime(2017, 03, 17),
            //    Time = new TimeSpan(0, 1, 57, 43),
            //    Length = 31,
            //    Car = "Peugeot 207"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 3,
            //    Date = new DateTime(2017, 03, 29),
            //    Time = new TimeSpan(0, 2, 34, 21),
            //    Length = 13,
            //    Car = "Renault Clio"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 4,
            //    Date = new DateTime(2017, 04, 01),
            //    Time = new TimeSpan(0, 5, 04, 41),
            //    Length = 5,
            //    Car = "Mazda 3"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 5,
            //    Date = new DateTime(2017, 03, 29),
            //    Time = new TimeSpan(0, 2, 34, 21),
            //    Length = 258,
            //    Car = "Toyota Yaris"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 6,
            //    Date = new DateTime(2017, 03, 17),
            //    Time = new TimeSpan(0, 1, 57, 43),
            //    Length = 78,
            //    Car = "Peugeot 207"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 7,
            //    Date = new DateTime(2017, 03, 29),
            //    Time = new TimeSpan(0, 2, 34, 21),
            //    Length = 41,
            //    Car = "Renault Clio"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 8,
            //    Date = new DateTime(2017, 04, 01),
            //    Time = new TimeSpan(0, 5, 04, 41),
            //    Length = 12,
            //    Car = "Mazda 3"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 9,
            //    Date = new DateTime(2017, 03, 29),
            //    Time = new TimeSpan(0, 2, 34, 21),
            //    Length = 41,
            //    Car = "Renault Clio"

            //});

            //table.Add(new PassageModel()
            //{
            //    Passage_ID = 10,
            //    Date = new DateTime(2017, 04, 01),
            //    Time = new TimeSpan(0, 5, 04, 41),
            //    Length = 12,
            //    Car = "Mazda 3"

            //});


            return new List<Passage>();
        }
    }
}