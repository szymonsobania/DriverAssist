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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UserProfile()
        {
            ViewBag.Message = "Ustawienia uzytkownika";

            return View();
        }

        public ActionResult Maps()
        {
            ViewBag.Message = "Dane przejazdu";

            return View(model.Coordinates);
        }

        public ActionResult Passages()
        {
            ViewBag.Message = "Lista przejazdów";

            return View(model.Coordinates);
        }

    }
}