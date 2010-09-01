using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fussball.Models;

namespace Fussball.ViewModels
{
    public class GameOverViewModel
    {
        public List<Goal> GameScore { get; set; }
        public Player Blue1 { get; set; }
        public Player Blue2 { get; set; }
        public Player Red1 { get; set; }
        public Player Red2 { get; set; }
        public Game Game { get; set; }
    }
}