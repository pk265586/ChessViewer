using System;

namespace ChessViewer.Domain
{
    public class GameMoveModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public string WhiteFrom { get; set; }
        public string WhiteTo { get; set; }
        public string BlackFrom { get; set; }
        public string BlackTo { get; set; }
    }
}
