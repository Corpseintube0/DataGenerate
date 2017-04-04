using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.IO;
using System.Data;

namespace DataGenerator
{
    /// <summary>
    /// Класс для хранения структуры данных целевой БД и обеспечения функционала ETL модели.
    /// </summary>
    public class StagedDB
    {
        private SQLiteConnection _conn;
        private string _connectionString;
        
        public StagedDB()
        {
            string databaseName = @"StagedDB.db";
            _connectionString = @"Data Source=StagedDB.db;";
            if (!File.Exists(databaseName))
                SQLiteConnection.CreateFile(databaseName);
            if (!File.Exists(databaseName))
                throw new Exception();
            var names = GetTableList();
            foreach (string tableName in names)
                DropTable(tableName);
            using (_conn = new SQLiteConnection(_connectionString))
            {
                _conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand("CREATE TABLE FKList (FKName TEXT PRIMARY KEY, TableName TEXT, ColumnName TEXT, RefTableName TEXT, RefColumnName TEXT);", _conn);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Получает имена таблиц, хранящихся в базе. 
        /// </summary>
        public string[] GetTableList()
        {
            using (_conn = new SQLiteConnection(_connectionString))
            {
                _conn.Open();
                List<string> res = new List<string>();
                SQLiteCommand command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", _conn);
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                    res.Add(record["name"].ToString());
                return res.ToArray();
            }
        }

        /// <summary>
        /// Удаляет таблицу и её содержимое из БД.
        /// </summary>
        public void DropTable(string tableName)
        {
            using (_conn = new SQLiteConnection(_connectionString))
            {
                _conn.Open();
                var query = string.Format("DROP TABLE {0};", tableName);
                SQLiteCommand command = new SQLiteCommand(query, _conn);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Создает таблицу для хранения внешних ключей.
        /// </summary>
        public void CreateFKTable(string tableName, string valueType)
        {
            using (_conn = new SQLiteConnection(_connectionString))
            {
                _conn.Open();
                var query = string.Format("CREATE TABLE {0} (FK_value {1} PRIMARY KEY);", tableName, valueType);
                //var query = string.Format("CREATE TABLE {0} (FK_value {1});", tableName, valueType);
                SQLiteCommand command = new SQLiteCommand(query, _conn);
                command.ExecuteNonQuery();
            }
        }

        ///// <summary>
        ///// Заполняет таблицу внешнего ключа пулом значений.
        ///// </summary>
        //public void FillFKTable(string tableName, string[] values)
        //{
        //    using (_conn = new SQLiteConnection(_connectionString))
        //    {
        //        _conn.Open();
        //        SQLiteCommand command;
        //        foreach (string value in values)
        //        {
        //            command = new SQLiteCommand(String.Format("INSERT INTO '{0}' ('FK_value') VALUES ('{1}');", tableName, value), _conn);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void InsertDataset(string tableName, DataSet newDataSet)
        {
            using (_conn = new SQLiteConnection(_connectionString))
            {
                string sql = string.Format("SELECT * FROM {0};", tableName);
                _conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, _conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow row in newDataSet.Tables[0].Rows)
                {
                    DataRow newRow = ds.Tables[0].NewRow();
                    foreach (DataColumn col in newDataSet.Tables[0].Columns)
                        newRow[col.ColumnName] = row[col.ColumnName];
                    ds.Tables[0].Rows.Add(newRow);
                }

                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
                //PrintDataSet(ds);
                adapter.Update(ds);
                //PrintDataSet(ds);
                // перезагружаем данные
                //adapter.Fill(ds);
            }
        }

        //отладочный метод TODO: delete
        private void PrintTable(DataTable dt)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferWidth = 1000;

            foreach (System.Collections.DictionaryEntry de in dt.ExtendedProperties)
                Console.WriteLine("Ключ = {0}, Значение = {1}", de.Key, de.Value);
            Console.WriteLine();

            // Создание объекта DataTableReader
            DataTableReader dtReader = dt.CreateDataReader();
            while (dtReader.Read())
            {
                for (int i = 0; i < dtReader.FieldCount; i++)
                    Console.Write("{0,-20}", dtReader.GetValue(i).ToString().Trim());
                Console.WriteLine();
            }
            dtReader.Close();
        }

        //отладочный метод TODO: delete
        private void PrintDataSet(DataSet ds)
        {
            Console.BufferWidth = 1000;
            // Вывод имени и расширенных свойств
            Console.WriteLine("*** Объекты DataSet ***\n");
            Console.WriteLine("Имя DataSet: {0}", ds.DataSetName);
            foreach (System.Collections.DictionaryEntry de in ds.ExtendedProperties)
                Console.WriteLine("Ключ = {0}, Значение = {1}", de.Key, de.Value);
            Console.WriteLine();

            // Вывод каждой таблицы
            foreach (DataTable dt in ds.Tables)
            {
                //Console.WriteLine("=> Таблица: {0}", dt.TableName);

                // Вывод имени столбцов
                for (int curCol = 0; curCol < dt.Columns.Count; ++curCol)
                    Console.Write("{0,-20}", dt.Columns[curCol].ColumnName);
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------");

                // Выводим содержимое таблицы
                PrintTable(dt);
            }
        }

        public DataSet SelectRows(string queryString)
        {
            using (_conn = new SQLiteConnection(_connectionString))
            {
                _conn.Open();
                DataSet ret = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                adapter.SelectCommand = new SQLiteCommand(queryString, _conn);
                adapter.Fill(ret); //заполняем DataSet
                return ret;
            }
        }
    }
}
