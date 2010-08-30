using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.ViewModels;
using Fussball.Models;

namespace Fussball.Controllers
{
    public class StatisticsController : Controller
    {
        PlayerRepository playerRep = new PlayerRepository();
        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            //Get the properties for the statistics view model

            var viewmodel = new Dictionary<string, Player>();
            viewmodel.Add("flest mål", playerRep.GetTopScorer());
            viewmodel.Add("færrest mål", playerRep.GetWorstScorer());
            viewmodel.Add("flest selvmål", playerRep.GetMostSelfScores());
            viewmodel.Add("færrest selvmål", playerRep.GetLeastSelfScores());

            return View(viewmodel);
        }



    }
}
