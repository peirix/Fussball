using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;
using Fussball.ViewModels;
using Newtonsoft.Json;

namespace Fussball.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private PlayerRepository playerRep = new PlayerRepository();
        private GameRepository gameRep = new GameRepository();
        private GoalRepository goalRep = new GoalRepository();

        public ActionResult Index()
        {
            var players = playerRep.GetAllPlayers();

            return View(players);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Play(int Blue1, int Blue2, int Red1, int Red2, bool? IsTest = false)
        {
            var blue1StartDefCount = gameRep.GetGamesForPlayer(Blue1).Where(g => g.Blue1 == Blue1 || g.Red1 == Blue1).Count();
            var blue2StartDefCount = gameRep.GetGamesForPlayer(Blue2).Where(g => g.Blue1 == Blue2 || g.Red1 == Blue2).Count();

            var red1StartDefCount = gameRep.GetGamesForPlayer(Red1).Where(g => g.Blue1 == Red1 || g.Red1 == Red1).Count();
            var red2StartDefCount = gameRep.GetGamesForPlayer(Red2).Where(g => g.Blue1 == Red2 || g.Red1 == Red2).Count();

            int BlueOff, BlueDef, RedOff, RedDef;

            if (blue1StartDefCount > blue2StartDefCount)
            {
                BlueDef = Blue2;
                BlueOff = Blue1;
            }
            else
            {
                BlueDef = Blue1;
                BlueOff = Blue2;
            }

            if (red1StartDefCount > red2StartDefCount)
            {
                RedDef = Red2;
                RedOff = Red1;
            }
            else
            {
                RedDef = Red1;
                RedOff = Red2;
            }

            var game = new Game()
            {
                Blue1 = BlueDef,
                Blue2 = BlueOff,
                Red1 = RedDef,
                Red2 = RedOff,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                IsTest = IsTest.Value
            };

            gameRep.Add(game);
            gameRep.Save();

            return RedirectToAction("Play", "Home",  new { BlueDef = BlueDef,
                                                           BlueOff = BlueOff,
                                                           RedDef = RedDef,
                                                           RedOff = RedOff,
                                                           GameID = game.ID });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Play(int BlueDef, int BlueOff, int RedDef, int RedOff, int GameID)
        {
            var viewmodel = new PlayGameViewModel
            {
                Game = gameRep.GetGame(GameID),
                Blue1 = playerRep.GetPlayer(BlueDef),
                Blue2 = playerRep.GetPlayer(BlueOff),
                Red1 = playerRep.GetPlayer(RedDef),
                Red2 = playerRep.GetPlayer(RedOff)
            };

            return View(viewmodel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GameOver(int gameID, int winningTeam)
        {
            var game = gameRep.GetGame(gameID);
            game.DateEnd = DateTime.Now;
            game.WinningTeam = winningTeam;
            gameRep.Save();

            return RedirectToAction("GameOver", "Home", new { id = gameID });
        }

        public ActionResult GameOver(int id)
        {
            var game = gameRep.GetGame(id);

            var viewmodel = new GameOverViewModel
            {
                BlueGoals = goalRep.GetGoalsByGameAndTeam(game.ID, 0).Count(),
                RedGoals = goalRep.GetGoalsByGameAndTeam(game.ID, 1).Count(),
                Blue1 = playerRep.GetPlayer(game.Blue1),
                Blue2 = playerRep.GetPlayer(game.Blue2),
                Red1 = playerRep.GetPlayer(game.Red1),
                Red2 = playerRep.GetPlayer(game.Red2),
                Game = game,
                GameScore = goalRep.GetGoalsByGame(game.ID).ToList()
            };

            if (game.IsTest)
            {
                gameRep.Delete(game);
                gameRep.Save();
            }

            return View(viewmodel);
        }


        //AJAX
        [AcceptVerbs(HttpVerbs.Post)]
        public string ScoreGoal(int scorer, int position, int team, int gameID, int oppDefID, int selfGoal)
        {
            var score = new Goal
            {
                PlayerID = scorer,
                Position = position,
                GoalDate = DateTime.Now,
                Team = team,
                GameID = gameID,
                OppDefenseID = oppDefID,
                SelfGoal = selfGoal
            };

            goalRep.Add(score);
            goalRep.Save();

            return string.Format("{0}", score.ID);
        }

        //AJAX
        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteGoal(int goalID)
        {
            var goal = goalRep.GetGoal(goalID);
            goalRep.Delete(goal);
            goalRep.Save();
        }

        //AJAX
        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteGame(int gameID)
        {
            var game = gameRep.GetGame(gameID);
            gameRep.Delete(game);
            gameRep.Save();
        }
    }
}
