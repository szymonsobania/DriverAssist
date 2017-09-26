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

            var table = PassageTable.GetPassageTable(Session["token"]?.ToString());

            return View(table);
        }

        public ActionResult Statistics(string passageId)
        {
            ViewBag.Message = "Dane przejazdu";

            return View(model.GetCoordinates(Session["token"]?.ToString(), passageId));
        }

        public class Zakres
        {
            public string start { set; get; }
            public string end { set; get; }
        }

        public ActionResult SelectedData(Zakres zakres)
        {
            var response = "Zwracam dane od " + zakres.start + " do " + zakres.end;

            //Zwroc dane tak aby w zakresie start end było 500 rekordow, a poza tym zakresem 100 (rowno rozmieszczone)
            return Json(response);
        }
    }
}