using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChessViewer.Domain;
using ChessViewer.Data;
using ChessViewer.Services.Abstract;
using ChessViewer.Services.Static;

namespace ChessViewer.Services.Implementation
{
    public class GameService : IGameService
    {
        private GameRepository GetGameRepository() =>
            new GameRepository(AppSettings.ConnectionString);

        private GameMovesRepository GetGameMovesRepository() =>
            new GameMovesRepository(AppSettings.ConnectionString);

        public void SaveGame(string gameName, string rawMoves) 
        {
            var moves = ConvertToMovesList(rawMoves);
            SaveGame(gameName, moves);
        }

        public void SaveGame(string gameName, List<GameMoveModel> moves) 
        {
            var repository = GetGameRepository();
            var movesRepository = GetGameMovesRepository();

            var game = repository.GetGameByName(gameName);
            if (game == null)
            {
                game = new GameModel { Name = gameName };
                repository.InsertGame(game);
            }
            else
                movesRepository.DeleteGameMoves(game.Id);

            movesRepository.SaveGameMoves(moves);
        }

        public List<GameMoveModel> ConvertToMovesList(string rawMoves) 
        {
            var result = new List<GameMoveModel>();
            var lines = Utils.SplitToLines(rawMoves);
            foreach (var line in lines) 
            {
                var move = ParseLine(line);
                if (move != null)
                    result.Add(move);
            }

            return result;
        }

        private GameMoveModel ParseLine(string line)
        {
            if (line.IsNullOrEmpty())
                return null;
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0 || parts.Length > 2)
                return null;

            var cellArray = parts.Select(p => p.Remove(0, 1).Split('-')).ToArray();
            var model = new GameMoveModel 
            {
                WhiteFrom = cellArray[0][0],
                WhiteTo = cellArray[0][1],
                BlackFrom = cellArray.LastOrDefault()?.FirstOrDefault(),
                BlackTo = cellArray.LastOrDefault()?.LastOrDefault(),
            };

            return model;
        }

        public GameModel GetGameByName(string gameName) 
        {
            return GetGameRepository().GetGameByName(gameName);
        }

        public List<GameModel> GetAllGames() 
        {
            return GetGameRepository().GetAllGames();
        }
    }
}
