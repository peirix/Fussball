using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;
using Fussball.ViewModels;

namespace Fussball.Controllers
{
    public class PlayerController : Controller
    {
        PlayerRepository playerRep = new PlayerRepository();
        GoalRepository goalRep = new GoalRepository();
        GameRepository gameRep = new GameRepository();
        //
        // GET: /Player/

        public ActionResult Index()
        {
            return View(playerRep.GetAllPlayers());
        }

        public ActionResult Details(int id)
        {
            var player = playerRep.GetPlayer(id);
            var goals = player.TotalGoals();
            var games = playerRep.GetPlayerGames(id);

            var avg = (double)goals / (double)games;

            avg = Math.Round(avg, 2);

            PlayerViewModel viewmodel = new PlayerViewModel
            {
                Player = player,
                Games = games,
                Goals = goals,
                AvgGoals = avg
            };
            
            return View(viewmodel);
        }

    }
}
