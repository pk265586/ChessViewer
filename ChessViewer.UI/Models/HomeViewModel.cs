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

        public EditGameModel EditModel { get; set; } = new EditGameModel();

        public LoadGameModel LoadModel { get; set; } = new LoadGameModel();
    }
}