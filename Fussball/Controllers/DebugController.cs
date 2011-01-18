using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;

namespace Fussball.Controllers
{
    public class DebugController : Controller
    {
        GameRepository gameRep = new GameRepository();
        GoalRepository goalRep = new GoalRepository();

        //
        // GET: /Debug/

        public ActionResult Index()
        {
            List<Game> redWins = new List<Game>();
            List<Game> blueWins = new List<Game>();
            List<Game> testGame = new List<Game>();

            foreach (var game in gameRep.GetAllGames())
            {
                var goals = goalRep.GetGoalsByGame(game.ID);
                if (game.IsTest)
                {
                    gameRep.Delete(game);
                    gameRep.Save();
                }
                else if (goals.Where(g => g.Team == 0).Count() == 10)
                {
                    blueWins.Add(game);
                    game.WinningTeam = 0;
                    gameRep.Save();
                }
                else if (goals.Where(g => g.Team == 1).Count() == 10)
                {
                    redWins.Add(game);
                    game.WinningTeam = 1;
                    gameRep.Save();
                }
                else
                {
                    //if the goal count doesn't match 10 on any side it's a test game
                    gameRep.Delete(game);
                    gameRep.Save();
                }
            }

            ViewData["BlueWins"] = blueWins;

            return View(redWins);
        }

    }
}
