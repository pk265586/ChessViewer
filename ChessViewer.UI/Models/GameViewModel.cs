using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessViewer.UI.Models
{
    public class GameViewModel
    {
        [Required]
        public string RawMoves { get; set; }

        [Required]
        public string GameName { get; set; }

        public List<GameMoveViewModel> Moves { get; set; }
    }
}