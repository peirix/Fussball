using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fussball.Models;

namespace Fussball.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }
        public int Goals { get; set; }
        public int Games { get; set; }
        public double AvgGoals { get; set; }
    }
}
