using System;
using System.Collections.Generic;

namespace ChessViewer.UI.Models
{
    public class SelectGameModel
    {
        public string SelectGameName { get; set; }
        public List<string> GameNames { get; set; } = new List<string>();
    }
}