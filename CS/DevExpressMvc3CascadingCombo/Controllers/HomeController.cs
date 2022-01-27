using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using CS.Models;
namespace CS.Controllers {
    public class HomeController : Controller {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index() {
            return View(new Customer { ID = 0, Country = 2, City = 2 });
        }
        public ActionResult CountryPartial() {
            return PartialView(new Customer());
        }
        public ActionResult CityPartial() {
            int country = (Request.Params["Country"] != null) ? int.Parse(Request.Params["Country"]) : -1;
            return PartialView(new Customer { Country = country });
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index([ModelBinder(typeof(DevExpressEditorsBinder))]Customer customer) {
            if(ModelState.IsValid) {
                //Post customer to database.
                return RedirectToAction("Success");
            }
            else
                return View(customer);
        }
        public ActionResult Success() {
            return View();
        }
    }
}