using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AuthWebApi;
using AuthWebApi.Models;
using DriverAssist.Models;
using Newtonsoft.Json;

namespace DriverAssist.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var httpClient = new HttpClient();
            var httpResponseMessage = httpClient.GetAsync(PPConfig.EndPointAdress + "intervals?token=" + Session["token"]).Result;
            Interwalies model = null;
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Interwaly>>(contents);
                if (result != null)
                {
                    model = new Interwalies()
                    {
                        AccelerometerIntervalLength = (from r in result
                            where r.nazwa == "AccelerometerIntervalLength"
                            select r.wartosc).FirstOrDefault(),
                        GpsIntervalLength = (from r in result where r.nazwa == "GpsIntervalLength" select r.wartosc)
                            .FirstOrDefault(),
                        GyroscopeIntervalLength = (from r in result
                            where r.nazwa == "GyroscopeIntervalLength"
                            select r.wartosc).FirstOrDefault(),
                        MagneticIntervalLength = (from r in result
                            where r.nazwa == "MagneticIntervalLength"
                            select r.wartosc).FirstOrDefault()
                    };
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Interwalies interwalies)
        {
            List<Interwaly> list = new List<Interwaly>();
            list.Add(new Interwaly(){nazwa= "AccelerometerIntervalLength",wartosc = interwalies.AccelerometerIntervalLength});
            list.Add(new Interwaly() { nazwa = "GpsIntervalLength", wartosc = interwalies.GpsIntervalLength });
            list.Add(new Interwaly() { nazwa = "GyroscopeIntervalLength", wartosc = interwalies.GyroscopeIntervalLength });
            list.Add(new Interwaly() { nazwa = "MagneticIntervalLength", wartosc = interwalies.MagneticIntervalLength });
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(list);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "intervals", httpContent);
            while (!httpResponseMessage.IsCompleted) ;
            Session["saved"] = "Saved!";
            return RedirectToAction("Index", "Settings");
        }
    }
}