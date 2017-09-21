using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DriverAssist.Helpers;
using Newtonsoft.Json;
using AuthWebApi.Models;
using WebGrease.Css.Ast.Selectors;

namespace DriverAssist.Controllers
{
    public class LoginController : Controller
    {
        private static string endPoint = "http://localhost:50236/auth";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("Email", username);
            values.Add("Password", PasswordHelper.GetHash(password));
            var content = new FormUrlEncodedContent(values);
            var httpResponseMessage = httpClient.PostAsync(endPoint, content).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<LoginResponse>(contents);
                if (result.Result == "OK")
                {
                    Session["user"] = username;
                    Session["token"] = result.Token;
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("LoginFailed", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public bool IsLogged()
        {
            return Session["user"] != null;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}