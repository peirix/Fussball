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

        public List<Player> GetAllPlayers()
        {
            return db.Players.OrderBy(p => p.Name).ToList();
        }

        public int GetPlayerGames(int playerID)
        {
            var games = db.Games.ToList();
            return GetPlayerGames(games, playerID);
        }

        public int GetPlayerGames(List<Game> games, int playerID)
        {
            return (from game in games
                    where game.Blue1 == playerID
                    || game.Blue2 == playerID
                    || game.Red1 == playerID
                    || game.Red2 == playerID
                    select game).Count();
        }

        public int GetPlayerGoals(Player player)
        {
            return player.Goals.Where(s => s.SelfGoal == 0).Count();
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
                    where GetPlayerGames(player.ID) > 0
                    orderby player.Goals.Where(s => s.SelfGoal == 0).Count()
                    select player).First();
        }

        public Player GetMostSelfScores()
        {
            return (from player in db.Players
                    orderby player.Goals.Where(s => s.SelfGoal == 1).Count() descending
                    select player).First();
        }

        public Player GetLeastSelfScores()
        {
            return (from player in db.Players.ToList()
                    where GetPlayerGames(player.ID) > 0
                    orderby player.Goals.Where(s => s.SelfGoal == 1).Count()
                    select player).First();
        }

        public Player GetTopWinner()
        {
            //Get all winning teams

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