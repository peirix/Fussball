using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fussball.Models;

namespace Fussball.ViewModels
{
    public class StatisticsIndexViewModel
    {
        public string Desc { get; set; }
        public Player Player { get; set; }
        public int Num { get; set; }
    }
}