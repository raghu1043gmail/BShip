using BShip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BShip.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View(new GameBoard());
        }
        public ActionResult LoadNewGame()
        {
            return Json(new GameBoard());
        }
    }
}