using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessViewer.UI.Models
{
    public class GameViewModel
    {
        public string RawMoves { get; set; }
        public string GameName { get; set; }

        public List<GameMoveViewModel> Moves { get; set; }
    }
}