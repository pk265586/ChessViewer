using System;
using System.ComponentModel.DataAnnotations;

namespace ChessViewer.UI.Models
{
    public class GameViewModel
    {
        [Required]
        public string RawMoves { get; set; }

        [Required]
        public string GameName { get; set; }
    }
}