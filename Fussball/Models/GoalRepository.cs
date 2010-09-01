using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fussball.Models
{
    class GoalRepository
    {
        FussballDataContext db = new FussballDataContext();

        //Queries
        public Goal GetScore(int ScoreID)
        {
            return db.Goals.Where(s => s.ID == ScoreID).SingleOrDefault();
        }

        public List<Goal> GetScoreByGame(int gameID)
        {
            return (from score in db.Goals
                    where score.GameID == gameID
                    select score).ToList();
        }

        //Insert/Delete/Update
        public void Add(Goal score)
        {
            db.Goals.InsertOnSubmit(score);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
