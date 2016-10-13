﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DataGen
{
    /// <summary>
    /// Класс, отвечающий за представление БД.
    /// </summary>
    public class DBLogic
    {
        //Строка подключения к БД.
        public string ConnectionString;

        /// <summary>
        /// Подключение к БД.
        /// </summary>
        private SqlConnection _conn;

        /// <summary>
        /// Получает имя подключенной базы данных.
        /// </summary>
        public String DBName
        {
            get { return _conn.Database; }
        }

        /// <summary>
        /// Производит попытку подключения к БД.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        /// <returns>True если подключение успешно, иначе False.</returns>
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
            if (_conn != null)
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
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(queryString, _conn);
            adapter.Fill(ret); //заполняем DataSet
            return ret;
        }

        ///// <summary>
        ///// Выполняет вставку строки в таблицу.
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="values"></param>
        ///// <returns>Адаптер с командой на вставку строки.</returns>
        //public SqlDataAdapter InsertInto(string tableName, string[] values)
        //{
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    var schema = GetTableSchema(tableName);
        //    List<string> columnNames = new List<string>();
        //    foreach (DataColumn col in schema.Columns)
        //        columnNames.Add(col.ColumnName); //получили имена столбцов
        //    string command = "INSERT INTO " + tableName + " (";
        //    for (int i = 0; i < columnNames.Count; ++i)
        //        command += columnNames[i] + ", ";
        //    command.Trim(',');
        //    command += ") VALUES (";
        //    for (int i = 0; i < values.Length; ++i)
        //        command += values[i] + ", ";
        //    command.Trim(',');
        //    command += ")";

        //    adapter.InsertCommand = new SqlCommand(command, _conn);

        //    return adapter;
        //}

        /// <summary>
        /// Вставка строки в таблицу.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="values"></param>
        /// <returns>True если вставка выполнена успешно, иначе False.</returns>
        public bool InsertIntoTable(string table, string[] values)
        {
            //Общий вид запроса
            string sql = string.Format("Insert Into {0} (", table); // Оператор SQL
            var schema = GetTableSchema(table); //получили метаданные таблицы
            //DisplayData(schema); //тест
            foreach (DataRow row in schema.Rows)
            {
                sql += string.Format(" {0}, ", row["ColumnName"]);
            }
            sql = sql.Remove(sql.Length - 2, 2); //удалили ', '
            sql += " ) Values( ";
            for (int i = 0; i < values.Length; ++i)
            {
                sql += string.Format("'{0}',", values[i]); //////
            }
            sql = sql.Remove(sql.Length - 1); //удалили ','
            sql += ")";

            //Параметризованная команда
            using (SqlCommand cmd = new SqlCommand(sql, _conn = new SqlConnection(ConnectionString)))
            {
                _conn.Open();
                int i = 0;
                foreach (DataRow row in schema.Rows)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = string.Format("@{0}", row["ColumnName"]);
                    param.SqlDbType = GetSqlTypeFromString(string.Format("{0}", row["DataTypeName"])); //(SqlDbType)row["DataTypeName"]; //
                    switch (param.SqlDbType)
                    {
                        case SqlDbType.UniqueIdentifier:
                            param.Value = Guid.Parse(values[i]);
                            break;
                        case SqlDbType.Date:
                            param.Value = DateTime.Parse(values[i]);
                            break;
                        case SqlDbType.NChar:
                            param.Value = values[i];
                            break;
                        default:
                            throw new Exception("Неверный тип данных!");
                    }
                    ++i;
                    //тестировать
                    param.Size = (int)row["ColumnSize"];
                    cmd.Parameters.Add(param);
                }
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        
        //преобразование строки в тип данных
        private SqlDbType GetSqlTypeFromString(string arg)
        {
            switch (arg)
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "binary":
                    return SqlDbType.Binary;
                case "bit":
                    return SqlDbType.Bit;
                case "char":
                    return SqlDbType.Char;
                case "datetime":
                    return SqlDbType.DateTime;
                case "decimal":
                    return SqlDbType.Decimal;
                case "float":
                    return SqlDbType.Float;
                case "image":
                    return SqlDbType.Image;
                case "int":
                    return SqlDbType.Int;
                case "money":
                    return SqlDbType.Money;
                case "nchar":
                    return SqlDbType.NChar;
                case "ntext":
                    return SqlDbType.NText;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "real":
                    return SqlDbType.Real;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "text":
                    return SqlDbType.Text;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.VarChar;
                case "variant":
                    return SqlDbType.Variant;
                case "xml":
                    return SqlDbType.Xml;
                case "udt":
                    return SqlDbType.Udt;
                case "structured":
                    return SqlDbType.Structured;
                case "date":
                    return SqlDbType.Date;
                case "time":
                    return SqlDbType.Time;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                default:
                    throw new Exception(arg + ": Неизвестный тип данных!");
            }
        }

        /// <summary>
        /// lifehack TODO: delete
        /// </summary>
        public void CreateEnumList()
        {
            var fi = new FileInfo("enum.txt");
            var file = fi.OpenWrite();
            var tw = new StreamWriter(file);
            for (int i=0; i<35; ++i)
            {
                SqlDbType type = (SqlDbType)i;
                tw.WriteLine("case \"{0}\":", type.ToString().ToLower());
                tw.WriteLine("\t return SqlDbType." + type.ToString() + ";");
            }
            tw.Close();
            file.Close();
        }

        /// <summary>
        /// Вывод данных в файл (тест)
        /// </summary>
        private void DisplayData(DataTable table)
        {
            var fi = new FileInfo("output.txt");
            var file = fi.OpenWrite();
            var tw = new StreamWriter(file);
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    tw.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                tw.WriteLine("============================");
            }
            tw.Close();
            file.Close();
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
        /// Получает список с именами столбцов. 
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        public String[] GetColumnNames(string tableName)
        {
            //var schema = GetTableSchema(tableName);
            //List<string> columnNames = new List<string>();
            //foreach (DataColumn col in schema.Columns)
            //    columnNames.Add(col.ColumnName); //получили имена столбцов
            //return columnNames.ToArray();
            List<string> columnNames = new List<string>();
            var ds = SelectRows("SELECT * FROM " + tableName);
            var workTable = ds.Tables[0];
            foreach (DataColumn column in workTable.Columns)
                columnNames.Add(column.ColumnName);
            return columnNames.ToArray();
        }

        /// <summary>
        /// Получает список с типами столбцов. 
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        public String[] GetColumnTypes(string tableName)
        {
            //var schema = GetTableSchema(tableName);
            //List<string> columnTypes = new List<string>();
            //foreach (DataColumn col in schema.Columns)
            //    columnTypes.Add(col.DataType.ToString()); //получили типы столбцов
            //return columnTypes.ToArray();
            List<string> columnTypes = new List<string>();
            var ds = SelectRows("SELECT * FROM " + tableName);
            var workTable = ds.Tables[0];
            foreach (DataColumn column in workTable.Columns)
                columnTypes.Add(column.DataType.ToString());
            return columnTypes.ToArray();

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
            using (_conn = new SqlConnection(ConnectionString))
            {
                _conn.Open();
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
