using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ChessViewer.Models;
using ChessViewer.Static;

namespace ChessViewer.Controllers
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

        public ActionResult Edit(string rawMoves)
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Edit));
        }

        public ActionResult Load(string rawMoves)
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Load));
        }
    }
}