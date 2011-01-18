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
        public Goal GetGoal(int goalID)
        {
            return db.Goals.Where(s => s.ID == goalID).SingleOrDefault();
        }

        public List<Goal> GetGoalsByGame(int gameID)
        {
            return (from goal in db.Goals
                    where goal.GameID == gameID
                    select goal).ToList();
        }

        public List<Goal> GetGoalsByGameAndTeam(int gameID, int team)
        {
            return (from goal in db.Goals
                    where goal.GameID == gameID
                    where goal.Team == team
                    select goal).ToList();
        }

        public List<Goal> GetGoalsByPlayer(int playerID)
        {
            return (from goal in db.Goals
                    where goal.PlayerID == playerID
                    where goal.SelfGoal == 0
                    select goal).ToList();
        }

        //Insert/Delete/Update
        public void Add(Goal goal)
        {
            db.Goals.InsertOnSubmit(goal);
        }

        public void Delete(Goal goal)
        {
            db.Goals.DeleteOnSubmit(goal);
        }

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
