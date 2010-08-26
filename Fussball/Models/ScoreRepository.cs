using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fussball.Models
{
    class ScoreRepository
    {
        FussballDataContext db = new FussballDataContext();

        //Queries
        public Score GetScore(int ScoreID)
        {
            return db.Scores.Where(s => s.ID == ScoreID).SingleOrDefault();
        }

        public List<Score> GetScoreByGame(int gameID)
        {
            return (from score in db.Scores
                    where score.GameID == gameID
                    select score).ToList();
        }

        //Insert/Delete/Update
        public void Add(Score score)
        {
            db.Scores.InsertOnSubmit(score);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
