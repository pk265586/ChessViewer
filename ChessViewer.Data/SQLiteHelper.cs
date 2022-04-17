using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;

namespace ChessViewer.Data
{
    public class SQLiteHelper
    {
        private readonly string connectionString;
        
        public SQLiteHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T GetEntity<T>(string sqlText, Func<IDataReader, T> mapper)
        {
            return GetEntity(sqlText, parameters: null, mapper: mapper);
        }

        public  T GetEntity<T>(string sqlText, SQLiteParameter parameter, Func<IDataReader, T> mapper)
        {
            return GetEntity(sqlText, new[] { parameter }, mapper);
        }

        public  T GetEntity<T>(string sqlText, SQLiteParameter[] parameters, Func<IDataReader, T> mapper)
        {
            var list = GetEntityList(sqlText, parameters, mapper);
            return list.FirstOrDefault();
        }

        public List<T> GetEntityList<T>(string sqlText, Func<IDataReader, T> mapper)
        {
            return GetEntityList(sqlText, parameters: null, mapper: mapper);
        }

        public List<T> GetEntityList<T>(string sqlText, SQLiteParameter parameter, Func<IDataReader, T> mapper)
        {
            return GetEntityList(sqlText, new[] { parameter }, mapper);
        }

        public List<T> GetEntityList<T>(string sqlText, SQLiteParameter[] parameters, Func<IDataReader, T> mapper)
        {
            using (var connection = new SQLiteConnection(connectionString))
            using (var cmd = new SQLiteCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(cmd, parameters);
                var reader = cmd.ExecuteReader();
                var result = new List<T>();
                while (reader.Read())
                {
                    result.Add(mapper(reader));
                }
                return result;
            }
        }

        public bool RowExists(string sqlText, SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(connectionString))
            using (var statement = new SQLiteCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(statement, parameters);
                var reader = statement.ExecuteReader();
                return reader.Read();
            } 
        }

        public void ExecSql(string sqlText)
        {
            ExecSql(sqlText, parameters: null);
        }

        public void ExecSql(string sqlText, SQLiteParameter parameter) 
        {
            ExecSql(sqlText, new[] { parameter });
        }

        public void ExecSql(string sqlText, SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(connectionString))
            using (var cmd = new SQLiteCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(cmd, parameters);
                cmd.ExecuteNonQuery();
            }
        }

        public long ExecInsert(string sqlText, SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var cmd = new SQLiteCommand(sqlText, connection))
                {
                    try
                    {
                        InitCommand(cmd, parameters);
                        cmd.ExecuteNonQuery();
                        long newId = connection.LastInsertRowId;
                        transaction.Commit();
                        return newId;
                    }
                    catch (SQLiteException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void InitCommand(SQLiteCommand cmd, SQLiteParameter[] parameters)
        {
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
        }
    }
}
