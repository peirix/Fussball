﻿using System;
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

            int goals, letIn, selfGoals, gamesWon;
            goals = letIn = selfGoals = gamesWon = 0;

            foreach (var game in lastGames)
            {
                var gameGoals = goalRep.GetGoalsByGame(game.ID);
                goals += gameGoals.Where(g => g.PlayerID == this.ID && g.SelfGoal == 0).Count();
                letIn += gameGoals.Where(g => g.OppDefenseID == this.ID && g.SelfGoal == 0).Count();
                selfGoals += gameGoals.Where(g => g.PlayerID == this.ID && g.SelfGoal == 1).Count();
                if (game.WinningTeam == 0 && (game.Blue1 == this.ID || game.Blue2 == this.ID))
                    gamesWon++;
                else if (game.WinningTeam == 1 && (game.Red1 == this.ID || game.Red2 == this.ID))
                    gamesWon++;

                Debug.WriteLine("foreach() " + sw.ElapsedMilliseconds);
            }
            

            this.Stats = new RankStats(lastGames.Count(), gamesWon, goals, selfGoals, letIn);
            
            sw.Stop();
            Debug.WriteLine("SetRanking(" + this.Name + ") " + sw.Elapsed.Milliseconds);
        }

        public string BestColor()
        {
            var games = this.GetAllGames();
            var redWins = games.Where(g => g.WinningTeam == 1 && (g.Red1 == this.ID || g.Red2 == this.ID)).Count();
            var blueWins = games.Where(g => g.WinningTeam == 0 && (g.Blue1 == this.ID || g.Blue2 == this.ID)).Count();

            if (redWins > blueWins)
                return "Rød (" + redWins + " seiere)";
            else if (redWins < blueWins)
                return "Blå (" + blueWins + " seiere)";
            else
                return "Begge";
        }

        public string BestPosition()
        {
            var goals = goalRep.GetGoalsByPlayer(this.ID);

            var defGoals = goals.Where(g => g.Position == 0).Count();
            var offGoals = goals.Where(g => g.Position == 1).Count();

            if (defGoals > offGoals)
                return "Forsvar (" + defGoals + " mål)";
            else if (defGoals < offGoals)
                return "Angrep (" + offGoals + " mål)";
            else
                return "Begge";
        }

        public string PlayedMostWith()
        {
            var games = this.GetAllGames();

            var red1Games = games.Where(g => g.Red1 == this.ID);
            var mostRed1Games = from r in red1Games
                                group r by r.Red2 into g
                                select new { ID = g.Key, Count = g.Count() };

            var red2Games = games.Where(g => g.Red2 == this.ID);
            var mostRed2Games = from r in red2Games
                                group r by r.Red1 into g
                                select new { ID = g.Key, Count = g.Count() };

            var blue1Games = games.Where(g => g.Blue1 == this.ID);
            var mostBlue1Games = from r in blue1Games
                                group r by r.Blue2 into g
                                select new { ID = g.Key, Count = g.Count() };

            var blue2Games = games.Where(g => g.Blue2 == this.ID);
            var mostBlue2Games = from r in blue2Games
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

        public string BestTeamPlayer()
        {
            return "Ikke implementert";
        }

        private List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();

            games.AddRange(this.Games);
            games.AddRange(this.Games1);
            games.AddRange(this.Games2);
            games.AddRange(this.Games3);
            return games;
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

        public RankStats(int games, int gamesWon, int goals, int selfGoals, int letInGoals)
        {
            this.Games = games;
            this.GamesWon = gamesWon;
            this.Goals = goals;
            this.SelfGoals = selfGoals;
            this.LetInGoals = letInGoals;
            this.Points = goals + gamesWon - letInGoals - ((selfGoals * 1.5) / games);
        }
    }
}