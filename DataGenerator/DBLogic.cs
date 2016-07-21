using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataGen
{
    /// <summary>
    /// Класс, отвечающий за представление БД.
    /// </summary>
    public class DBLogic
    {
        /// <summary>
        /// Подключение к БД.
        /// </summary>
        private SqlConnection _conn;

        /// <summary>
        /// Адаптер данных.
        /// </summary>
        private SqlDataAdapter _dataAdapter;

        /// <summary>
        /// Получает имя подключенной базы данных.
        /// </summary>
        public String DBName
        {
            get { return _conn.Database; }
        }

        public bool Connect(String connectionString)
        {
            _conn = new SqlConnection();
            _conn.ConnectionString = connectionString;
            _conn.Open();
            return true;
        }

        /// <summary>
        /// Закрывает подключение к БД.
        /// </summary>
        public void ConnectionClose()
        {
            _conn.Close();
        }

        /// <summary>
        /// Выбрать строки из БД.
        /// </summary>
        /// <param name="queryString">SQL-запрос SELECT.</param>
        /// <returns></returns>
        public DataSet SelectRows(string queryString = "SELECT * FROM [dbo].[Person]")
        {
            DataSet ret = new DataSet();
            using (_conn)
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, _conn);
                adapter.Fill(ret); //заполняем DataSet
                return ret;
            }
        }

        /// <summary>
        /// Получает список пользовательских таблиц, хранящихся в БД.
        /// </summary>
        public DataSet GetTables()
        {
            return SelectRows("SELECT * FROM INFORMATION_SCHEMA.TABLES");
        }

        /// <summary>
        /// Возвращает набор имён таблиц БД.
        /// </summary>
        public String[] GetTableNames()
        {
            var ret = new List<String>();

            var ds = SelectRows("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES");
            var workTable = ds.Tables[0];
            DataRow[] rows = workTable.Select();
            foreach (DataRow row in rows)
            {
                foreach (DataColumn column in workTable.Columns)
                    ret.Add (String.Format("{0}", row[column]));
            }

            return ret.ToArray();
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public String DataSetToString(DataSet arg)
        {
            var workTable = arg.Tables[0];
            DataRow[] currentRows = workTable.Select();
            if (currentRows.Length < 1)
                return "No Current Rows Found";
            else
            {
                string ret = "";
                foreach (DataColumn column in workTable.Columns)
                    ret += String.Format("\t{0}", column.ColumnName);

                //ret += "\tRowState";

                foreach (DataRow row in currentRows)
                {
                    foreach (DataColumn column in workTable.Columns)
                        ret += String.Format("\t{0}", row[column]);

                   // ret += String.Format("\t" + row.RowState);
                }
                return ret;
            }
        }

        /// <summary>
        /// Получает метаданные таблицы.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        public DataTable GetTableSchema(string tableName)
        {
            using (_conn)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM " + tableName + ";", _conn);

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();
                /*
                foreach (DataRow row in schemaTable.Rows)
                {
                    foreach (DataColumn column in schemaTable.Columns)
                    {
                        Console.WriteLine(String.Format("{0} = {1}", column.ColumnName, row[column]));
                    }
                }
                */           
                return schemaTable;
            }
        }
    }
}
