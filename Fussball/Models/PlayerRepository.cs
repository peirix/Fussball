﻿using System;
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

        public Player GetTopScorer()
        {
            return (from player in db.Players
                    orderby player.Scores.Where(s => s.SelfGoal == 0).Count() descending
                    select player).First();
        }

        public Player GetWorstScorer()
        {
            return (from player in db.Players
                    orderby player.Scores.Where(s => s.SelfGoal == 0).Count()
                    select player).First();
        }

        public Player GetMostSelfScores()
        {
            return (from player in db.Players
                    orderby player.Scores.Where(s => s.SelfGoal == 1).Count() descending
                    select player).First();
        }

        public Player GetLeastSelfScores()
        {
            return (from player in db.Players
                    orderby player.Scores.Where(s => s.SelfGoal == 1).Count()
                    select player).First();
        }

        public Player GetTopWinner()
        {
            //Get all winning teams

            throw new NotImplementedException();
        }

        //Insert/Update

        //Persist
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}