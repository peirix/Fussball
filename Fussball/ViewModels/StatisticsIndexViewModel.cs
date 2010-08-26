using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fussball.Models;

namespace Fussball.ViewModels
{
    public class StatisticsIndexViewModel
    {
        public Player MostScore { get; set; }
        public Player MostWins { get; set; }
        public Player WorstScore { get; set; }
        public Player MostLosses { get; set; }
    }
}