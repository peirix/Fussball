using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.ViewModels;

namespace Fussball.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            //Get the properties for the statistics view model

            var viewmodel = new StatisticsIndexViewModel();
            return View();
        }

    }
}
