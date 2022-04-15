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

        private SqlHelper sqlHelper => new SqlHelper(connectionString);

        private static string INSERT_TEXT = "Insert Into GameMoves (GameId, MoveNumber, WhiteMove, BlackMove) Values (@GameId, @MoveNumber, @WhiteMove, @BlackMove)";

        public GameMovesRepository(string connectionString)
        {
            this.connectionString = connectionString;
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
                        new SQLiteParameter("@WhiteMove", move.WhiteMove),
                        new SQLiteParameter("@BlackMove", move.BlackMove)
                    });
            }
        }
    }
}
