using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ChessViewer.UI.Models;
using ChessViewer.UI.Static;
using ChessViewer.Services.Abstract;

namespace ChessViewer.UI.Controllers
{
    public class HomeController : Controller
    {
        IGameService gameService;

        public HomeController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel(HomeFormMode.Load);
            model.LoadModel.GameNames = gameService.GetAllGames().Select(g => g.Name).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(GameViewModel saveModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new HomeViewModel(HomeFormMode.Edit) { CurrentGame = saveModel });
            }

            gameService.SaveGame(saveModel.GameName, saveModel.RawMoves);
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