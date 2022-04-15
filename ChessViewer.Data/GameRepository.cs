﻿using System;
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
        
        private SqlHelper sqlHelper => new SqlHelper(connectionString); 

        public GameRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string INSERT_TEXT = "Insert Into Games (Name) Values (@Name)";
        private const string SELECT_ALL_TEXT = "Select * From Games Order by Name";
        private const string SELECT_BY_NAME = "Select * From Games Where Name = @Name";

        public void InsertGame(GameModel model) 
        {
            string sqlText = INSERT_TEXT;
            int newId = sqlHelper.ExecInsert(sqlText, new[] { new SQLiteParameter("@Name", model.Name) });
            model.Id = newId;
        }

        public List<GameModel> GetAllGames() 
        { 
            return sqlHelper.GetEntityList(SELECT_ALL_TEXT, GetGameByReader);
        }

        public GameModel GetGameByName(string gameName) 
        {
            return sqlHelper.GetEntity(SELECT_BY_NAME, new SQLiteParameter("@Name", gameName), GetGameByReader);
        }

        private GameModel GetGameByReader(IDataReader reader)
        {
            return new GameModel 
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
            };
        }
    }
}
