using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessViewer.UI.Models
{
    public class GameMoveViewModel
    {
        public int MoveNumber { get; set; }
        public string WhiteMove { get; set; }
        public string BlackMove { get; set; }
    }
}