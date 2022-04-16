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
            return Select();
        }

        public ActionResult PlaySelected(string gameName)
        {
            var game = gameService.GetGameByName(gameName, loadMoves: true);
            if (game == null)
                return Select();

            //TDB gameService.
            return PlayRaw(game.Name);
        }

        public ActionResult PlayRaw(string rawMoves)
        {
            //TBD
            return View("Index", new HomeViewModel(HomeFormMode.Play));
        }

        public ActionResult Edit(string gameName = null)
        {
            var model = new HomeViewModel(HomeFormMode.Edit);
            if (gameName != null) 
            {
                var game = gameService.GetGameByName(gameName, loadMoves: true);
                if (game != null)
                {
                    model.EditGame = new GameModelMapper().ToViewModel(game);
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Save(GameViewModel saveModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new HomeViewModel(HomeFormMode.Edit) { EditGame = saveModel });
            }

            gameService.SaveGame(saveModel.GameName, saveModel.RawMoves);
            return Select();
        }

        public ActionResult Select(string selectedName = null)
        {
            var model = new HomeViewModel(HomeFormMode.Select);
            model.SelectGame.SelectGameName = selectedName;
            model.SelectGame.GameNames = gameService.GetAllGames().Select(g => g.Name).ToList();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Select(SelectGameModel game)
        {
            bool isPlay = Request.Form["Play"] != null;
            return isPlay ? PlaySelected(game.SelectGameName) : Edit(game.SelectGameName);
        }
    }
}