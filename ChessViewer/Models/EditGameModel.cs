using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessViewer.Models
{
    public class EditGameModel
    {
        public string RawMoves { get; set; }
        public string NewGameName { get; set; }
        //public int OverwriteGameId { get; set; }
    }
}