using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace DataGenerator
{
    /// <summary>
    /// Класс для обеспечения функционирования генератора случайного текста.
    /// </summary>
    public class RandomTextVocabularyDB : SQLiteDB
    {
        private Random _rnd = RandomProvider.GetThreadRandom();

        public RandomTextVocabularyDB() : base("vocabulary.db")
        {

        }

        public DataSet GetRandomValues(int amt)
        {
            DataSet res = new DataSet();
            for (int i = 0; i < amt; ++i)
            {
                //TODO: написать составной SQL запрос, для выбора из базы сразу одного большого DataSet
                int table = _rnd.Next(1, 28);
                string sql = "SELECT Count(*) FROM Letter_" + table.ToString();
                DataSet dsCount = SelectRows(sql);
                int count = Int32.Parse(dsCount.Tables[0].Rows[0].ItemArray[0].ToString());
                int index = _rnd.Next(0, count - 1);
                using (_connection = new SQLiteConnection(_connectionString))
                {
                    _connection.Open();
                    sql = String.Format("SELECT value FROM Letter_{0} WHERE id={1}", table, index);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, _connection);
                    adapter.Fill(res);
                }
            }
            return res;
        }
    }
}
