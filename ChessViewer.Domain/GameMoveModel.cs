using System;

namespace ChessViewer.Domain
{
    public class GameMoveModel
    {
        /*public long Id { get; set; }
        public long GameId { get; set; }*/
        public long MoveNumber { get; set; }
        public string WhiteFrom { get; set; }
        public string WhiteTo { get; set; }
        public string BlackFrom { get; set; }
        public string BlackTo { get; set; }

        public override string ToString()
        {
            return $"{MoveNumber}. {GetMoveNotation(WhiteFrom, WhiteTo)} {GetMoveNotation(BlackFrom, BlackTo)}";
        }

        public string GetMoveNotation(string from, string to) 
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return "";
            return $"{from}-{to}";
        }
    }
}
