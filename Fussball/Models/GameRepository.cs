using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fussball.Models
{
    public class GameRepository
    {
        FussballDataContext db = new FussballDataContext();

        //Queries
        public IEnumerable<Game> GetAllGames()
        {
            return db.Games;
        }

        public IEnumerable<Game> GetGamesForPlayer(int playerId)
        {
            return db.Games.Where(
                    g => g.Blue1 == playerId || g.Blue2 == playerId || g.Red1 == playerId || g.Red2 == playerId);
        }

        public Game GetGame(int GameID)
        {
            return db.Games.Where(g => g.ID == GameID).SingleOrDefault();
        }

        //Insert/Delete/Update
        public void Add(Game game)
        {
            db.Games.InsertOnSubmit(game);
        }

        public void Delete(Game game)
        {
            db.Games.DeleteOnSubmit(game);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}