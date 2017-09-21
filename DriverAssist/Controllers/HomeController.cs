using DriverAssist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DriverAssist.Controllers
{
    public class HomeController : Controller
    {
        private Model model = new Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Administrator page";
            var table = UserTable.GetUserTable();

            return View(table);
        }

        public ActionResult UserProfile()
        {
            ViewBag.Message = "Ustawienia uzytkownika";

            return View();
        }

        public ActionResult Passages()
        {
            ViewBag.Message = "Lista przejazdów";

            var table = PassageTable.GetPassageTable();

            return View(table);
        }

        public ActionResult Statistics()
        {
            ViewBag.Message = "Dane przejazdu";

            return View(model.Coordinates);
        }

        public ActionResult LoginFailed()
        {
            return View("Error");
        }



    }
}