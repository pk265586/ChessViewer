using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChessViewer.Domain;

namespace ChessViewer.Services.Abstract
{
    public interface IGameService
    {
        List<GameModel> GetAllGames();
        void SaveGame(string gameName, string rawMoves);
    }
}
