using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;
using Fussball.ViewModels;

namespace Fussball.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private FussballDataContext db = new FussballDataContext();
        private GameRepository gameRep = new GameRepository();
        private ScoreRepository scoreRep = new ScoreRepository();

        public ActionResult Index()
        {
            var players = db.Players.ToList();

            return View(players);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Play(int BlueDef, int BlueOff, int RedDef, int RedOff)
        {
            var game = new Game()
            {
                Blue1 = BlueDef,
                Blue2 = BlueOff,
                Red1 = RedDef,
                Red2 = RedOff,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
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
                Blue1 = db.Players.Where(p => p.ID == BlueDef).Single(),
                Blue2 = db.Players.Where(p => p.ID == BlueOff).Single(),
                Red1 = db.Players.Where(p => p.ID == RedDef).Single(),
                Red2 = db.Players.Where(p => p.ID == RedOff).Single()
            };

            return View(viewmodel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GameOver(int gameID, int winningTeam)
        {
            var game = gameRep.GetGame(gameID);
            game.WinningTeam = winningTeam;

            return RedirectToAction("GameOver", "Home", new { gameID = gameID });
        }

        public ActionResult GameOver(int gameID)
        {
            var game = gameRep.GetGame(gameID);

            var viewmodel = new GameOverViewModel
            {
                Blue1 = db.Players.Where(p => p.ID == game.Blue1).Single(),
                Blue2 = db.Players.Where(p => p.ID == game.Blue2).Single(),
                Red1 = db.Players.Where(p => p.ID == game.Red1).Single(),
                Red2 = db.Players.Where(p => p.ID == game.Red2).Single(),
                Game = game,
                GameScore = scoreRep.GetScoreByGame(game.ID)
            };

            return View(viewmodel);
        }


        //AJAX
        [AcceptVerbs(HttpVerbs.Post)]
        public void ScoreGoal(int scorer, int position, int team, int gameID, int oppDefID, int selfGoal)
        {
            var score = new Score
            {
                PlayerID = scorer,
                Position = position,
                ScoreDate = DateTime.Now,
                Team = team,
                GameID = gameID,
                OppDefenseID = oppDefID,
                SelfGoal = selfGoal
            };

            scoreRep.Add(score);
            scoreRep.Save();
        }
    }
}
