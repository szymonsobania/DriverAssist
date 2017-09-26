﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using AuthWebApi.Models;
using Newtonsoft.Json;

namespace DriverAssist.Models
{
    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Acc { get; set; }
        public double Magn { get; set; }
    }

    public class StringObj
    {
        public string Str { get; set; }
    }

    public class Model
    {
        //public List<Coordinate> Coordinates { get; set; }

        public PassageData GetCoordinates(string authToken, string passageGuid)
        {
            if (authToken != null)
            {
                var pkg = new GetStatistic {token = authToken, passage_guid = passageGuid};
                var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, 10, 0) };
                var json = JsonConvert.SerializeObject(pkg);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "statistics", httpContent).Result;
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<PassageData>(contents);
                    int step = result.LocationTimestamp.Count / 500;
                    var res = new PassageData();
                    for (int i = 0; i < result.LocationTimestamp.Count; i += step)
                    {
                        res.LocationTimestamp.Add(result.LocationTimestamp[i]);
                        res.LocationLat.Add(result.LocationLat[i]);
                        res.LocationLng.Add(result.LocationLng[i]);
                    }
                    return res;
                }
            }

            return new PassageData();
        }

        public Model()
        {
            //Coordinates = new List<Coordinate>();

            ////DEMO tylko do testow
            //String path = HttpContext.Current.Server.MapPath("~/img/gliwice_kato.txt");
            //String line;
            //try
            //{   // Open the text file using a stream reader.
            //    using (StreamReader sr = new StreamReader(path))
            //    {
            //        while ((line = sr.ReadLine()) != null)
            //        {
            //            var coords = line.Split(';');
            //            Coordinates.Add(new Coordinate { Lat = Double.Parse(coords[0], CultureInfo.InvariantCulture), Lng = Double.Parse(coords[1], CultureInfo.InvariantCulture) });
            //            for (int i = 0; i < 1; i++)  // testy wydajnosciowe :p
            //            {
            //                Coordinates.Add(new Coordinate { Lat = Double.Parse(coords[0], CultureInfo.InvariantCulture), Lng = Double.Parse(coords[1], CultureInfo.InvariantCulture) + 0.00001 * i });
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //}
        }

    }
}