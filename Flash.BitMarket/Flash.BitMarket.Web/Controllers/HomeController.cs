using Flash.BitMarket.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flash.BitMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPriceQuoteService priceQuoteService;

        public HomeController(IPriceQuoteService priceQuoteService)
        {
            this.priceQuoteService = priceQuoteService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}