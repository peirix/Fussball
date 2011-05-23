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
    }
}