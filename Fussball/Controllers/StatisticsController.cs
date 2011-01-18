﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.ViewModels;
using Fussball.Models;

namespace Fussball.Controllers
{
    public class StatisticsController : Controller
    {
        PlayerRepository playerRep = new PlayerRepository();
        GameRepository gameRep = new GameRepository();
        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            ViewData["TopScorer"] = playerRep.GetTopScorer();
            ViewData["WorstScorer"] = playerRep.GetWorstScorer();
            ViewData["MostSelfScore"] = playerRep.GetMostSelfScores();
            ViewData["LeastSelfScore"] = playerRep.GetLeastSelfScores();

            var allGames = gameRep.GetAllGames();

            ViewData["BlueWins"] = allGames.Where(g => g.WinningTeam == 0).Count();
            ViewData["RedWins"] = allGames.Where(g => g.WinningTeam == 1).Count();

            return View(playerRep.GetAllPlayers());
        }



    }
}
