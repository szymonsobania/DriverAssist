using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AuthWebApi;
using DriverAssist.Helpers;
using Newtonsoft.Json;
using AuthWebApi.Models;
using WebGrease.Css.Ast.Selectors;

namespace DriverAssist.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Session["LoginError"] = null;
            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("Email", username);
            values.Add("Password", PasswordHelper.GetHash(password));
            var content = new FormUrlEncodedContent(values);
            var httpResponseMessage = httpClient.PostAsync(PPConfig.EndPointAdress + "auth", content).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var contents = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<LoginResponse>(contents);
                if (result.Result == "OK")
                {
                    Session["user"] = username;
                    Session["token"] = result.Token;
                    var values2 = new Dictionary<string, string>();
                    values2.Add("token", result.Token);
                    var content2 = new FormUrlEncodedContent(values2);
                    var httpResponseMessage2 = httpClient.PostAsync(PPConfig.EndPointAdress + "checkadmin", content2).Result;
                    if (httpResponseMessage2.StatusCode == HttpStatusCode.OK)
                    {
                        var contents2 = httpResponseMessage2.Content.ReadAsStringAsync().Result;
                        var result2 = JsonConvert.DeserializeObject<Response>(contents2);
                        if (result2.Result == "OK")
                            Session["admin"] = "Y";
                        else
                            Session["admin"] = "N";
                    }
                    return RedirectToAction("Index", "Home");
                }
                Session["LoginError"] = "Bad email or password.";
                return RedirectToAction("Index", "Login");
            }
            Session["LoginError"] = "Connection error";
            return RedirectToAction("Index", "Login");
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