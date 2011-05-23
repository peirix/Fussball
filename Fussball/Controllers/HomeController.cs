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
        public ActionResult Play(int BlueDef, int BlueOff, int RedDef, int RedOff, bool IsTest)
        {
            var game = new Game()
            {
                Blue1 = BlueDef,
                Blue2 = BlueOff,
                Red1 = RedDef,
                Red2 = RedOff,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                IsTest = IsTest
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

            return RedirectToAction("GameOver", "Home", new { gameID = gameID });
        }

        public ActionResult GameOver(int gameID)
        {
            var game = gameRep.GetGame(gameID);

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
    }
}
