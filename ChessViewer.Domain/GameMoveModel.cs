using System;

namespace ChessViewer.Domain
{
    public class GameMoveModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public string WhiteMove { get; set; }
        public string BlackMove { get; set; }
    }
}
