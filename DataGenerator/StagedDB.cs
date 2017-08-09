using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataGenerator
{
    /// <summary>
    /// Класс для обеспечения доступа к храненимым данным промежуточной БД и обеспечения функционала ETL модели.
    /// </summary>
    public class StagedDB : SQLiteDB
    {
        private string[] _tables =  new string[] {"FKList", "AutoTypeAssignRules", "TableColumnAttributes" };

        private void CreateOrClearTables()
        {
            var tables = GetTableList();
            if (!tables.Contains("FKList"))
            {
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    //создание таблицы с данными о внешних ключах
                    SQLiteCommand command =
                        new SQLiteCommand("CREATE TABLE FKList (FKName TEXT PRIMARY KEY, TableName TEXT, ColumnName TEXT, RefTableName TEXT, RefColumnName TEXT);", _connection);
                    command.ExecuteNonQuery();
                }
            }
            else
                DeleteValues("FKList");
            if (!tables.Contains("AutoTypeAssignRules"))
            {
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    //создание таблицы с правилами авто-назначения генераторов
                    SQLiteCommand command = new SQLiteCommand("CREATE TABLE AutoTypeAssignRules (Priority INTEGER PRIMARY KEY, GenType TEXT, Rule TEXT);", _connection);
                    command.ExecuteNonQuery();
                }
            }
            //else
            //    DeleteValues("AutoTypeAssignRules");
            if (!tables.Contains("TableColumnAttributes"))
            {
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    //создание таблицы с атрибутами столбцов таблиц БД
                    string sql = "CREATE TABLE TableColumnAttributes " +
                                 "(ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                 "TABLE_NAME TEXT, " +
                                 "COLUMN_NAME TEXT, " +
                                 "DATA_TYPE TEXT, " +
                                 "IS_NULLABLE TEXT, " +
                                 "CHARACTER_MAXIMUM_LENGTH INTEGER);";
                    SQLiteCommand command = new SQLiteCommand(sql, _connection);
                    command.ExecuteNonQuery();
                }
            }
            else
                DeleteValues("TableColumnAttributes");
            if (!tables.Contains("CustomGeneratorsList"))
            {
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    //создание таблицы с данными о внешних ключах
                    SQLiteCommand command =
                        new SQLiteCommand("CREATE TABLE CustomGeneratorsList (CGName TEXT PRIMARY KEY, Path TEXT);", _connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteValues(string tablename)
        {
            using (_connection = new SQLiteConnection(_connectionString))
            {
                string sql = String.Format("DELETE FROM {0};", tablename);
                _connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, _connection);
                command.ExecuteNonQuery();
            }
        }
        public StagedDB(string dbName) : base(dbName)
        {
            CreateOrClearTables();
        }
    }
}
