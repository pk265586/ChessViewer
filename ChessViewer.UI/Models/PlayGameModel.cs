using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ChessViewer.Domain;

namespace ChessViewer.UI.Models
{
    public class PlayGameModel
    {
        public string GameName { get; set; }
        public GameMoveModel[] Moves { get; set; }
    }
}