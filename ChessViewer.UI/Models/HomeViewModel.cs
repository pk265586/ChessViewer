using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}