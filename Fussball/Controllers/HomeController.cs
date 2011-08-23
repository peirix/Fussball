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
        public ActionResult Play(int blue1, int blue2, int red1, int red2, bool? isTest = false)
        {
            var blue1StartDefCount = GetDefCount(blue1);
            var blue2StartDefCount = GetDefCount(blue2);

            var red1StartDefCount = GetDefCount(red1);
            var red2StartDefCount = GetDefCount(red2);

            int blueOff, blueDef, redOff, redDef;

            if (blue1StartDefCount > blue2StartDefCount)
            {
                blueDef = blue2;
                blueOff = blue1;
            }
            else
            {
                blueDef = blue1;
                blueOff = blue2;
            }

            if (red1StartDefCount > red2StartDefCount)
            {
                redDef = red2;
                redOff = red1;
            }
            else
            {
                redDef = red1;
                redOff = red2;
            }

            var game = new Game()
            {
                Blue1 = blueDef,
                Blue2 = blueOff,
                Red1 = redDef,
                Red2 = redOff,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                IsTest = isTest.Value
            };

            gameRep.Add(game);
            gameRep.Save();

            return RedirectToAction("Play", "Home",  new { BlueDef = blueDef,
                                                           BlueOff = blueOff,
                                                           RedDef = redDef,
                                                           RedOff = redOff,
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


        //AJAX-method
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

        //AJAX-method
        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteGoal(int goalID)
        {
            var goal = goalRep.GetGoal(goalID);
            goalRep.Delete(goal);
            goalRep.Save();
        }

        //AJAX-method
        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteGame(int gameID)
        {
            var game = gameRep.GetGame(gameID);
            gameRep.Delete(game);
            gameRep.Save();
        }


        //Helpers
        private double GetDefCount(int playerId)
        {
            var games = gameRep.GetGamesForPlayer(playerId).Count();
            var defCount = gameRep.GetGamesForPlayer(playerId).Where(g => g.Blue1 == playerId || g.Red1 == playerId).Count();

            return games == 0 ? 0 : games / defCount;
        }
    }
}
