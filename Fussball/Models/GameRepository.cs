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
        public Game GetGame(int GameID)
        {
            return db.Games.Where(g => g.ID == GameID).SingleOrDefault();
        }

        //Insert/Delete/Update
        public void Add(Game game)
        {
            db.Games.InsertOnSubmit(game);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}