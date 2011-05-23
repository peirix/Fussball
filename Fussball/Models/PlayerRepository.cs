using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fussball.Models
{
    public class PlayerRepository
    {
        FussballDataContext db = new FussballDataContext();

        //Queries
        public Player GetPlayer(int id)
        {
            return db.Players.Where(p => p.ID == id).Single();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return db.Players.OrderBy(p => p.Name).ToList();
        }

        public int GetPlayerGamesCount(int playerID)
        {
            return GetPlayerGames(playerID).Count();
        }

        public IEnumerable<Game> GetPlayerGames(int playerID)
        {
            return from game in db.Games
                   where game.Blue1 == playerID
                   || game.Blue2 == playerID
                   || game.Red1 == playerID
                   || game.Red2 == playerID
                   && !game.IsTest
                   select game;
        }

        public Player GetTopScorer()
        {
            return (from player in db.Players
                    orderby player.Goals.Where(s => s.SelfGoal == 0).Count() descending
                    select player).First();
        }

        public Player GetWorstScorer()
        {
            return (from player in db.Players.ToList()
                    where GetPlayerGamesCount(player.ID) > 0
                    orderby player.Goals.Where(s => s.SelfGoal == 0).Count()
                    select player).FirstOrDefault();
        }

        public Player GetMostSelfScores()
        {
            return (from player in db.Players
                    orderby player.Goals.Where(s => s.SelfGoal == 1).Count() descending
                    select player).FirstOrDefault();
        }

        public Player GetLeastSelfScores()
        {
            return (from player in db.Players.ToList()
                    where GetPlayerGamesCount(player.ID) > 0
                    orderby player.Goals.Where(s => s.SelfGoal == 1).Count()
                    select player).FirstOrDefault();
        }

        public Player GetBestRanked()
        {
            return (from player in db.Players
                    orderby (player.TotalGoals() - player.LetInGoals() - (player.TotalSelfGoals() * 2)) / player.TotalGames() descending
                    select player).FirstOrDefault();
        }

        public Player GetTopWinner()
        {
            //Get all winning teams
            var red1Wins = db.Games.Where(g => g.WinningTeam == 0).Select(g => g.Red1);
            var red2Wins = db.Games.Where(g => g.WinningTeam == 0).Select(g => g.Red2);
            var blue1Wins = db.Games.Where(g => g.WinningTeam == 1).Select(g => g.Blue1);
            var blue2Wins = db.Games.Where(g => g.WinningTeam == 1).Select(g => g.Blue2);

            //Count which player occures the most

            throw new NotImplementedException();
        }

        //Insert/Update
        public void Add(Player player)
        {
            db.Players.InsertOnSubmit(player);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}