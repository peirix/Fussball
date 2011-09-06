using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;
using Fussball.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using LinqToTwitter;

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

            int blueOff = blue1,
                blueDef = blue2,
                redOff = red1,
                redDef = red2;

            if (blue1StartDefCount < blue2StartDefCount)
            {
                blueDef = blue2;
                blueOff = blue1;
            }

            if (red1StartDefCount < red2StartDefCount)
            {
                redDef = red2;
                redOff = red1;
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

            var status = string.Format(GetStartTweet(),
                                       playerRep.GetPlayer(blue1),
                                       playerRep.GetPlayer(blue2),
                                       playerRep.GetPlayer(red1),
                                       playerRep.GetPlayer(red2));
            
            Tweet(status);

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

            var blue = playerRep.GetPlayer(game.Blue1) + " og " + playerRep.GetPlayer(game.Blue2);
            var red = playerRep.GetPlayer(game.Red1) + " og " + playerRep.GetPlayer(game.Red2);
            
            var winners = winningTeam == 0 ? blue : red;
            var losers = winningTeam == 0 ? red : blue;

            var status = string.Format(GetEndTweet(), winners, losers);
            
            Tweet(status);

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
        private decimal GetDefCount(int playerId)
        {
            var games = gameRep.GetGamesForPlayer(playerId).Count();
            if (games == 0)
                return 0;

            var defCount = gameRep.GetGamesForPlayer(playerId).Where(g => g.Blue1 == playerId || g.Red1 == playerId).Count();

            return Math.Round((decimal)defCount/games, 10);
        }

        private void Tweet(string status)
        {
            var auth = new SingleUserAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"],
                    OAuthToken = ConfigurationManager.AppSettings["twitterOAuthToken"],
                    AccessToken = ConfigurationManager.AppSettings["twitterAccessToken"]
                }
            };

            using (var twitterCtx = new TwitterContext(auth))
            {
                twitterCtx.UpdateStatus(status);
            }
        }

        private string GetStartTweet()
        {
            var tweets = new List<string>();
            tweets.Add("{0} og {1} tenkte de skulle lære {2} og {3} ei lita lekse");
            tweets.Add("{0} og {1} er i ferd med å yppe på seg bråk av {2} og {3}");
            tweets.Add("{0} og {1} tar på seg oppgaven og slå {2} og {3}");
            tweets.Add("{0} og {1} skal make things straight med {2} og {3}");
            tweets.Add("{0} og {1} skal kose seg med en lett match mot {2} og {3}");
            tweets.Add("{0} og {1} skal rundspille {2} og {3}");

            var rnd = new Random();
            return tweets.ElementAt(rnd.Next(tweets.Count));
        }

        private string GetEndTweet()
        {
            var tweets = new List<string>();
            tweets.Add("{1} fikk akkurat grisejuling av {0}");
            tweets.Add("{1} ble plukket i småbiter av {0}");
            tweets.Add("{1} ble rundspilt av {0}");
            tweets.Add("{1} ble slått ned i støvlene av {0}");
            tweets.Add("{1} ble sprunget over i gjørma av {0}");
            tweets.Add("{0} har akkurat knust {1}");
            tweets.Add("{0} plukker opp småbiter av {1}");
            tweets.Add("{0} skraper sammen restene av {1}");
            tweets.Add("{0} hevdet nok sin rett som seiersherre over {1}");

            var rnd = new Random();
            return tweets.ElementAt(rnd.Next(tweets.Count));
        }
    }
}
