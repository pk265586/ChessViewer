using System;

using ChessViewer.UI.Static;

namespace ChessViewer.UI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel() 
        {
        }

        public HomeViewModel(HomeFormMode formMode)
        {
            FormMode = formMode;
        }

        public HomeFormMode FormMode { get; set; }

        public GameViewModel EditGame { get; set; } = new GameViewModel();

        public SelectGameModel SelectGame { get; set; } = new SelectGameModel();

        public PlayGameModel PlayGame { get; set; } = new PlayGameModel();
    }
}