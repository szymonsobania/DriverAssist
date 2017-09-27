using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Antlr.Runtime.Tree;
using AuthWebApi.Models;
using DriverAssist.Controllers;
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

        private PassageData _sensors = new PassageData();

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
                    _sensors = result;
                    //var res = new PassageData();
                    //double step = result.LocationTimestamp.Count / 500.0;
                    //if (step <= 1)
                    //{
                    //    res.LocationTimestamp = result.LocationTimestamp;
                    //    res.LocationLat = result.LocationLat;
                    //    res.LocationLng = result.LocationLng;
                    //}
                    //else
                    //{
                    //    for (double i = 0; i < result.LocationTimestamp.Count; i += step)
                    //    {
                    //        res.LocationTimestamp.Add(result.LocationTimestamp[(int)i]);
                    //        res.LocationLat.Add(result.LocationLat[(int)i]);
                    //        res.LocationLng.Add(result.LocationLng[(int)i]);
                    //    }
                    //}
                    
                    //step = result.AccTimestamp.Count / 500.0;
                    //if (step <= 1)
                    //{
                    //    res.AccTimestamp = result.AccTimestamp;
                    //    res.AccX = result.AccX;
                    //    res.AccY = result.AccY;
                    //    res.AccZ = result.AccZ;
                    //}
                    //else
                    //{
                    //    for (double i = 0; i < result.AccTimestamp.Count; i += step)
                    //    {
                    //        res.AccTimestamp.Add(result.AccTimestamp[(int)i]);
                    //        res.AccX.Add(result.AccX[(int)i]);
                    //        res.AccY.Add(result.AccY[(int)i]);
                    //        res.AccZ.Add(result.AccZ[(int)i]);
                    //    }
                    //}

                    //step = result.GyroTimestamp.Count / 500.0;
                    //if (step <= 1)
                    //{
                    //    res.GyroTimestamp = result.GyroTimestamp;
                    //    res.GyroX = result.GyroX;
                    //    res.GyroY = result.GyroY;
                    //    res.GyroZ = result.GyroZ;
                    //}
                    //else
                    //{
                    //    for (double i = 0; i < result.GyroTimestamp.Count; i += step)
                    //    {
                    //        res.GyroTimestamp.Add(result.GyroTimestamp[(int)i]);
                    //        res.GyroX.Add(result.GyroX[(int)i]);
                    //        res.GyroY.Add(result.GyroY[(int)i]);
                    //        res.GyroZ.Add(result.GyroZ[(int)i]);
                    //    }
                    //}

                    //step = result.LightTimestamp.Count / 500.0;
                    //if (step <= 1)
                    //{
                    //    res.LightTimestamp = result.LightTimestamp;
                    //    res.LightIntensity = result.LightIntensity;
                    //}
                    //else
                    //{
                    //    for (double i = 0; i < result.LightTimestamp.Count; i += step)
                    //    {
                    //        res.LightTimestamp.Add(result.LightTimestamp[(int)i]);
                    //        res.LightIntensity.Add(result.LightIntensity[(int)i]);
                    //    }
                    //}
                    return result;
                }
            }

            return new PassageData();
        }

        public void UpdateSensorData(string authToken, string passageGuid, long start, long end, bool delete)
        {
            if (authToken != null)
            {
                var pkg = new UpdateStatistic
                {
                    Token = authToken,
                    PassageGuid = passageGuid,
                    Delete = delete,
                    StartTimestamp = start,
                    EndTimestamp = end
                };
                var httpClient = new HttpClient {Timeout = new TimeSpan(0, 0, 10, 0)};
                var json = JsonConvert.SerializeObject(pkg);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "updatestat", httpContent).Result;
                //if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                //{
                //    string contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //    var result = JsonConvert.DeserializeObject<PassageData>(contents);
                //    return result;
                //}
            }
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