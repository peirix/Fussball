using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Policy;
using System.Diagnostics;

namespace Fussball.Models
{
    public partial class Player
    {
        public RankStats Stats { get; set; }

        public string GetSquareImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Square))
                return this.Image_Square;
            else
                return HttpContext.Current.Request.ApplicationPath + "/Content/img/default_square.jpg";
        }

        public string GetSmallImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Small))
                return this.Image_Small;
            else
                return HttpContext.Current.Request.ApplicationPath + "/Content/img/default_small.jpg";
        }

        public string GetLargeImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Large))
                return this.Image_Large;
            else
                return HttpContext.Current.Request.ApplicationPath + "/Content/img/default_large.jpg";
        }

        // goals  = mål
        // goals1 = sluppet inn mål


        public int TotalGames()
        {
            return this.Games.Count() + this.Games1.Count() + this.Games2.Count() + this.Games3.Count();
        }

        public int TotalWins()
        {
            return WinsInGameList(this.Games) + WinsInGameList(this.Games1) + WinsInGameList(this.Games2) + WinsInGameList(this.Games3);
        }

        private int WinsInGameList(IEnumerable<Game> games)
        {
            return games.Where(g => (g.WinningTeam == 0 && (g.Blue1 == this.ID || g.Blue2 == this.ID)) ||
                                     g.WinningTeam == 1 && (g.Red1 == this.ID || g.Red2 == this.ID)).Count();
        }

        public int TotalGoals()
        {
            return this.Goals.Where(g => g.SelfGoal == 0).Count();
        }

        public int TotalSelfGoals()
        {
            return this.Goals.Where(g => g.SelfGoal == 1).Count();
        }

        public double AverageGoals()
        {
            if (this.TotalGames() == 0) return 0;

            return (double)this.TotalGoals() / (double)this.TotalGames();
        }

        public int LetInGoals()
        {
            return this.Goals1.Count();
        }

        GoalRepository goalRep = new GoalRepository();
        PlayerRepository playerRep = new PlayerRepository();

        public void SetRanking()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var lastGames = playerRep.GetPlayerGames(this.ID).OrderByDescending(g => g.DateStart).Take(10);

            int goalsFromDef, goalsFromOff, letIn, selfGoals, gamesWon;
            goalsFromDef = goalsFromOff = letIn = selfGoals = gamesWon = 0;

            foreach (var game in lastGames)
            {
                Debug.WriteLine("foreach() start " + sw.ElapsedMilliseconds);
                var gameGoals = goalRep.GetGoalsByGame(game.ID).ToList();
                Debug.WriteLine("foreach() 1 " + sw.ElapsedMilliseconds);
                goalsFromDef += gameGoals.Where(g => g.PlayerID == this.ID && g.Position == 0 && g.SelfGoal == 0).Count();
                Debug.WriteLine("foreach() 2 " + sw.ElapsedMilliseconds);
                goalsFromOff += gameGoals.Where(g => g.PlayerID == this.ID && g.Position == 1 && g.SelfGoal == 0).Count();
                Debug.WriteLine("foreach() 3 " + sw.ElapsedMilliseconds);
                letIn += gameGoals.Where(g => g.OppDefenseID == this.ID && g.SelfGoal == 0).Count();
                Debug.WriteLine("foreach() 4 " + sw.ElapsedMilliseconds);
                selfGoals += gameGoals.Where(g => g.PlayerID == this.ID && g.SelfGoal == 1).Count();
                Debug.WriteLine("foreach() 5 " + sw.ElapsedMilliseconds);
                if (game.WinningTeam == 0 && (game.Blue1 == this.ID || game.Blue2 == this.ID))
                    gamesWon++;
                else if (game.WinningTeam == 1 && (game.Red1 == this.ID || game.Red2 == this.ID))
                    gamesWon++;

                Debug.WriteLine("foreach() stop " + sw.ElapsedMilliseconds);
            }


            this.Stats = new RankStats(lastGames.Count(), gamesWon, goalsFromDef, goalsFromOff, selfGoals, letIn);
            
            Debug.WriteLine("SetRanking(" + this.Name + ") " + sw.Elapsed.Milliseconds);
            sw.Stop();
        }

        public string BestColor()
        {
            var games = GetAllGames();
            var redWins = games.Where(g => g.WinningTeam == 1 && (g.Red1 == this.ID || g.Red2 == this.ID)).Count();
            var blueWins = games.Where(g => g.WinningTeam == 0 && (g.Blue1 == this.ID || g.Blue2 == this.ID)).Count();

            if (redWins > blueWins)
                return "Rød (" + redWins + " seiere)";
            if (redWins < blueWins)
                return "Blå (" + blueWins + " seiere)";
            
            return "Begge";
        }

        public string BestPosition()
        {
            var goals = goalRep.GetGoalsByPlayer(this.ID);

            var defGoals = goals.Where(g => g.Position == 0).Count();
            var offGoals = goals.Where(g => g.Position == 1).Count();

            if (defGoals > offGoals)
                return "Forsvar (" + defGoals + " mål)";
            if (defGoals < offGoals)
                return "Angrep (" + offGoals + " mål)";
            
            return "Begge";
        }

        private List<IEnumerable<Game>> GetGamesList()
        {
            var gameList = new List<IEnumerable<Game>>();
            var games = this.GetAllGames();
            gameList.Add(games.Where(g => g.Red1 == this.ID));
            gameList.Add(games.Where(g => g.Red2 == this.ID));
            gameList.Add(games.Where(g => g.Blue1 == this.ID));
            gameList.Add(games.Where(g => g.Blue2 == this.ID));

            return gameList;
        }

        private string MostOccuredPlayerInGameList(List<IEnumerable<Game>> games)
        {
            var mostRed1Games = from r in games.ElementAt(0)
                                group r by r.Red2 into g
                                select new { ID = g.Key, Count = g.Count() };

            var mostRed2Games = from r in games.ElementAt(1)
                                group r by r.Red1 into g
                                select new { ID = g.Key, Count = g.Count() };

            var mostBlue1Games = from r in games.ElementAt(2)
                                 group r by r.Blue2 into g
                                 select new { ID = g.Key, Count = g.Count() };

            var mostBlue2Games = from r in games.ElementAt(3)
                                 group r by r.Blue1 into g
                                 select new { ID = g.Key, Count = g.Count() };

            var totalList = mostRed1Games.ToList();
            totalList.AddRange(mostRed2Games.ToList());
            totalList.AddRange(mostBlue1Games.ToList());
            totalList.AddRange(mostBlue2Games.ToList());

            var tempAgg = from t in totalList
                          group t by t.ID into g
                          select new { ID = g.Key, Count = g.Sum(p => p.Count) };


            var temp = tempAgg.OrderByDescending(g => g.Count).First();
            var player = playerRep.GetPlayer(temp.ID);
            return player.Name + " (" + temp.Count + ")";
        }

        public string PlayedMostWith()
        {
            var games = GetGamesList();

            return MostOccuredPlayerInGameList(games);

        }

        public string BestTeamPlayer()
        {
            var gamesList = GetGamesList();
            var winningGames = new List<IEnumerable<Game>>();
            foreach (var games in gamesList)
            {
                winningGames.Add(games.Where(g => (g.WinningTeam == 0 && (g.Blue1 == this.ID || g.Blue2 == this.ID)) ||
                                                   g.WinningTeam == 1 && (g.Red1 == this.ID || g.Red2 == this.ID)));
            }

            return MostOccuredPlayerInGameList(winningGames);
        }

        public IEnumerable<Game> GetLast10Games()
        {
            var games = GetAllGames().OrderByDescending(g => g.DateStart);

            return games.Take(10);
        }

        IEnumerable<Game> GetAllGames()
        {
            IEnumerable<Game> AllGames = Games;
            AllGames = AllGames.Union(Games1)
                               .Union(Games2)
                               .Union(Games3);
            return AllGames;
        }
    }

    public class RankStats
    {
        public int Games { get; set; }
        public int GamesWon { get; set; }
        public int Goals { get; set; }
        public int SelfGoals { get; set; }
        public int LetInGoals { get; set; }
        public double Points { get; set; }

        public RankStats(int games, int gamesWon, int goalsFromDef, int goalsFromOff, int selfGoals, int letInGoals)
        {
            this.Games = games;
            this.GamesWon = gamesWon;
            this.Goals = goalsFromDef + goalsFromOff;
            this.SelfGoals = selfGoals;
            this.LetInGoals = letInGoals;
            this.Points = (goalsFromDef * 1.2) + goalsFromOff + gamesWon - letInGoals - (selfGoals * 1.5);
        }
    }
}