using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fussball.Models;
using Fussball.ViewModels;

namespace Fussball.Controllers
{
    public class PlayerController : Controller
    {
        PlayerRepository playerRep = new PlayerRepository();
        GoalRepository goalRep = new GoalRepository();
        GameRepository gameRep = new GameRepository();
        //
        // GET: /Player/

        public ActionResult Index()
        {
            return View(playerRep.GetAllPlayers());
        }

        public ActionResult Details(int id)
        {
            var player = playerRep.GetPlayer(id);

            
            return View(player);
        }

        public ActionResult NewPlayer()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewPlayer(string name)
        {
            var player = new Player { Name = name };

            playerRep.Add(player);
            playerRep.Save();

            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }

    }
}
