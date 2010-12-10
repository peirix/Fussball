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
            //TODO: WTF!?

            //Get the properties for the statistics view model
            List<StatisticsIndexViewModel> viewmodel = new List<StatisticsIndexViewModel>();
            viewmodel.Add(new StatisticsIndexViewModel
            {
                Desc = "Flest mål",
                Player = playerRep.GetTopScorer(),
                Num = playerRep.GetTopScorer().Goals.Where(g => g.SelfGoal == 0).Count()
            });
            viewmodel.Add(new StatisticsIndexViewModel
            {
                Desc = "Færrest mål",
                Player = playerRep.GetWorstScorer(),
                Num = playerRep.GetWorstScorer().Goals.Where(g => g.SelfGoal == 0).Count()
            });
            viewmodel.Add(new StatisticsIndexViewModel
            {
                Desc = "Flest selvmål",
                Player = playerRep.GetMostSelfScores(),
                Num = playerRep.GetMostSelfScores().Goals.Where(g => g.SelfGoal == 1).Count()
            });
            viewmodel.Add(new StatisticsIndexViewModel
            {
                Desc = "Færrest selvmål",
                Player = playerRep.GetLeastSelfScores(),
                Num = playerRep.GetLeastSelfScores().Goals.Where(g => g.SelfGoal == 1).Count()
            });

            var playerGames = new Dictionary<Player, int>();
            foreach (var player in playerRep.GetAllPlayers())
            {
                playerGames.Add(player, playerRep.GetPlayerGames(player.ID));
            }


            ViewData["AllPlayers"] = playerGames;

            return View(viewmodel);
        }



    }
}
