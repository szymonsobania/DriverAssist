using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AuthWebApi;
using AuthWebApi.Models;
using DriverAssist.Helpers;
using DriverAssist.Models;
using Newtonsoft.Json;

namespace DriverAssist.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("token", Session["token"].ToString());
            var content = new FormUrlEncodedContent(values);
            var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "user", content).Result;
            UserProfile model = null;
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<UserProfile>(contents);
                if (result != null)
                {
                    model = result;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(UserProfile model)
        {
            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("token", Session["token"].ToString());
            var content = new FormUrlEncodedContent(values);
            var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "user", content).Result;
            UserProfile userProfile = null;
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<UserProfile>(contents);
                if (result != null)
                {
                    userProfile = result;
                }
            }

            userProfile.imie = model.imie;
            userProfile.nazwisko = model.nazwisko;
            userProfile.nazwa_uzytk = model.nazwa_uzytk;

            var json = JsonConvert.SerializeObject(userProfile);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage2 = httpClient.PutAsync(PPConfig.EndPointAdress + "user", httpContent);
            while (!httpResponseMessage2.IsCompleted) ;
            return RedirectToAction("Index", "Home");
        }
    }
}