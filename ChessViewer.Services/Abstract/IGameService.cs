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
        GameModel GetGameByName(string gameName, bool loadMoves);
        List<GameModel> GetAllGames();
        void SaveGame(string gameName, string rawMoves);
    }
}
