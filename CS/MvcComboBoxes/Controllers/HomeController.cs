using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcComboBoxes.Models;

namespace MvcComboBoxes.Controllers {
    public class HomeController : Controller {
        private NWindEntities db = new NWindEntities();
        public ActionResult Index() {
            var countries = db.Orders.GroupBy(p => p.ShipCountry).Select(g => g.FirstOrDefault()).ToList();
            return View(countries);
        }

        public ActionResult CountryComboView() {
            return PartialView();
        }
        public ActionResult CityComboView(string countryName) {
            var cities = db.Orders.Where(c => c.ShipCountry == countryName).ToList();
            var citiesDistinct = cities.GroupBy(p => p.ShipCity).Select(g => g.First()).ToList();
            return PartialView(citiesDistinct);
        }
    }
}