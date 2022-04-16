using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChessViewer.Domain;

namespace ChessViewer.Data
{
    public class GameMovesRepository
    {
        private readonly string connectionString;

        private SQLiteHelper sqlHelper => new SQLiteHelper(connectionString);

        private static string INSERT_TEXT =
            "Insert Into GameMoves (GameId, MoveNumber, WhiteFrom, WhiteTo, BlackFrom, BlackTo) " +
            "Values (@GameId, @MoveNumber, @WhiteFrom, @WhiteTo, @BlackFrom, @BlackTo)";

        private static string DELETE_BY_GAME_TEXT = "Delete GameMoves Where GameId = @GameId";

        public GameMovesRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DeleteGameMoves(long gameId) 
        {
            sqlHelper.ExecSql(DELETE_BY_GAME_TEXT, new SQLiteParameter("@GameId", gameId));
        }

        public void SaveGameMoves(List<GameMoveModel> moves) 
        {
            foreach (var move in moves) 
            {
                sqlHelper.ExecSql(INSERT_TEXT, 
                    new[] 
                    { 
                        new SQLiteParameter("@GameId", move.GameId),
                        new SQLiteParameter("@MoveNumber", move.MoveNumber),
                        new SQLiteParameter("@WhiteFrom", move.WhiteFrom),
                        new SQLiteParameter("@WhiteTo", move.WhiteTo),
                        new SQLiteParameter("@BlackFrom", move.BlackFrom),
                        new SQLiteParameter("@BlackTo", move.BlackTo)
                    });
            }
        }
    }
}
