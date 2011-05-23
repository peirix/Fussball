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
        GameRepository gameRep = new GameRepository();
        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LastTen()
        {
            var players = playerRep.GetAllPlayers();
            foreach (var player in players)
                player.SetRanking();

            return View(players);
        }

        public ActionResult AllGames()
        {
            var players = playerRep.GetAllPlayers();
            
            return View(players);
        }

        public ActionResult General()
        {
            var allGames = gameRep.GetAllGames();

            ViewData["BlueWins"] = allGames.Where(g => g.WinningTeam == 0).Count();
            ViewData["RedWins"] = allGames.Where(g => g.WinningTeam == 1).Count();

            return View();
        }

    }
}
