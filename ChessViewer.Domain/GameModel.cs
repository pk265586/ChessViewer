using System;

namespace ChessViewer.Domain
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public bool IsNew => Id <= 0;
    }
}
