using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fussball.Models
{
    public partial class Game
    {
        GoalRepository goalRep = new GoalRepository();
        PlayerRepository playerRep = new PlayerRepository();

        public int RedGoals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Red1 || g.PlayerID == this.Red2).Count();
        }

        public int BlueGoals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Blue1 || g.PlayerID == this.Blue2).Count();
        }

        public string Blue1Name()
        {
            return playerRep.GetPlayer(Blue1).Name;
        }

        public int Blue1Goals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Blue1).Count();
        }

        public string Blue2Name()
        {
            return playerRep.GetPlayer(Blue2).Name;
        }

        public int Blue2Goals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Blue2).Count();
        }

        public string Red1Name()
        {
            return playerRep.GetPlayer(Red1).Name;
        }

        public int Red1Goals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Red1).Count();
        }

        public string Red2Name()
        {
            return playerRep.GetPlayer(Red2).Name;
        }

        public int Red2Goals()
        {
            return goalRep.GetGoalsByGame(this.ID).Where(g => g.PlayerID == this.Red2).Count();
        }
    }
}