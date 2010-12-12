using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fussball.Models
{
    public partial class Player
    {
        public string GetSquareImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Square))
                return this.Image_Square;
            else
                return "/Content/img/default_square.jpg";
        }

        public string GetSmallImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Small))
                return this.Image_Small;
            else
                return "/Content/img/default_small.jpg";
        }

        public string GetLargeImage()
        {
            if (!string.IsNullOrEmpty(this.Image_Large))
                return this.Image_Large;
            else
                return "/Content/img/default_small.jpg";
        }

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
            return this.Goals.Where(g => g.SelfGoal == 1).Count() + this.Goals1.Where(g => g.SelfGoal == 1).Count();
        }

        public double AverageGoals()
        {
            if (this.TotalGames() == 0) return 0;

            return Math.Round((double)this.TotalGoals() / (double)this.TotalGames(), 2);
        }

        public int LetInGoals()
        {
            return this.Goals1.Count();
        }
    }
}