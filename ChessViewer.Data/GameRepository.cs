using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using ChessViewer.Domain;
using System.Data;

namespace ChessViewer.Data
{
    public class GameRepository
    {
        private readonly string connectionString;
        
        private SQLiteHelper sqlHelper => new SQLiteHelper(connectionString); 

        public GameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string INSERT_TEXT = "Insert Into Games (Name) Values (@Name)";
        private const string SELECT_ALL_TEXT = "Select * From Games Order by Name";
        private const string SELECT_BY_NAME = "Select * From Games Where Name = @Name";

        public void SaveGame(GameModel model) 
        {
            var existingGame = GetGameByName(model.Name, loadMoves: false);
            var gameMovesRepository = new GameMovesRepository(connectionString);

            if (existingGame == null)
            {
                InsertGame(model);
            }
            else
            {
                model.Id = existingGame.Id;
                gameMovesRepository.DeleteGameMoves(existingGame.Id);
            }

            gameMovesRepository.SaveGameMoves(model.Id, model.Moves);
        }

        private void InsertGame(GameModel model) 
        {
            string sqlText = INSERT_TEXT;
            long newId = sqlHelper.ExecInsert(sqlText, new[] { new SQLiteParameter("@Name", model.Name) });
            model.Id = newId;
        }

        public List<GameModel> GetAllGames() 
        { 
            return sqlHelper.GetEntityList(SELECT_ALL_TEXT, GetGameByReader);
        }

        public GameModel GetGameByName(string gameName, bool loadMoves) 
        {
            var game = sqlHelper.GetEntity(SELECT_BY_NAME, new SQLiteParameter("@Name", gameName), GetGameByReader);
            if (game != null && loadMoves)
            {
                game.Moves = new GameMovesRepository(connectionString).GetGameMoves(game.Id);
            }

            return game;
        }

        private GameModel GetGameByReader(IDataReader reader)
        {
            return new GameModel 
            {
                Id = (long)reader["Id"],
                Name = (string)reader["Name"],
            };
        }
    }
}
