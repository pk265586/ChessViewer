using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ChessViewer.UI.Models;
using ChessViewer.UI.Static;

namespace ChessViewer.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeViewModel(HomeFormMode.Load));
        }

        [HttpPost]
        public ActionResult Save()
        {
            //TBD
            return View("Index", new HomeViewModel());
        }

        public ActionResult Play(string rawMoves) 
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Play));
        }

        public ActionResult Edit()
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Edit));
        }

        public ActionResult Load()
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Load));
        }
    }
}