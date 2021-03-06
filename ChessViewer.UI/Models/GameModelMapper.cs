using System;
using System.Collections.Generic;
using System.Text;

using ChessViewer.Domain;

namespace ChessViewer.UI.Models
{
    public class GameModelMapper
    {
        public GameViewModel ToViewModel(GameModel domainModel) 
        {
            var viewModel = new GameViewModel 
            {
                GameName = domainModel.Name,
                RawMoves = GetRawMoves(domainModel.Moves)
            };

            return viewModel;
        }

        private string GetRawMoves(IEnumerable<GameMoveModel> moves)
        {
            var builder = new StringBuilder();
            foreach (var move in moves) 
            {
                builder.AppendLine(move.ToString());
            }
            return builder.ToString();
        }
    }
}