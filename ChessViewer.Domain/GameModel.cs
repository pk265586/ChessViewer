using System;
using System.Collections.Generic;

namespace ChessViewer.Domain
{
    public class GameModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<GameMoveModel> Moves { get; set; }
    }
}
