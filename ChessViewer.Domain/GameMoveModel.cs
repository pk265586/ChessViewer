using System;

namespace ChessViewer.Domain
{
    public class GameMoveModel
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long MoveNumber { get; set; }
        public string WhiteFrom { get; set; }
        public string WhiteTo { get; set; }
        public string BlackFrom { get; set; }
        public string BlackTo { get; set; }
    }
}
