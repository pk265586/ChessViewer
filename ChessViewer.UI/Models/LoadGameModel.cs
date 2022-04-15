using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessViewer.UI.Models
{
    public class LoadGameModel
    {
        public string SelectGameName { get; set; }
        public List<string> GameNames { get; set; } = new List<string>();
    }
}