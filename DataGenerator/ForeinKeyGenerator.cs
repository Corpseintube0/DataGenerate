using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace DataGenerator
{
    public class ForeignKeyGenerator : TestDataGenerator
    {
        /// <summary>
        /// Набор значений.
        /// </summary>
        private DataSet _ds;

        /// <summary>
        /// Инициализатор генератора случайных чисел.
        /// </summary>
        private int _seed;

        /// <summary>
        /// Обертка для класса Random.
        /// </summary>
        private Random _rnd = RandomProvider.GetThreadRandom();

        public ForeignKeyGenerator(DataSet arg)
        {
            _seed = DateTime.Now.Millisecond;
            _ds = arg;
        }
        public override string Next()
        {
            DataTable dt = _ds.Tables[0];
            int count = dt.Rows.Count;
            int index = _rnd.Next(0, count - 1);

            return dt.Rows[index].ItemArray[0].ToString();
        }

        public override string[] NextSet(int amt)
        {
            string[] ret;
            ret = new string[amt];
            //если уникальные и диапазон значений меньше требуемого для уникальных значений
            if (_ds.Tables[0].Rows.Count <= amt && UniqueValues)
            {
                amt = _ds.Tables[0].Rows.Count;
                ret = new string[amt];
                for (int i = 0; i < amt; ++i)
                    ret[i] = _ds.Tables[0].Rows[i].ItemArray[0].ToString();
            }
            else
            if (UniqueValues) //для уникальных значений
            {
                List<string> retList = new List<string>();
                for (int i = 0; i < amt; ++i)
                {
                    string temp = Next();
                    if (Array.IndexOf(retList.ToArray(), temp) > -1)
                    {
                        --i;
                        continue;
                    }
                    else
                        retList.Add(temp);
                }
                for (int i = 0; i < amt; ++i)
                    ret[i] = retList[i];
            }
            else //обычный случай
            {
                for (int i = 0; i < amt; ++i)
                    ret[i] = Next();
            }
            if (NullValues > 0) //обнуляем значения из набора на указанный процент
            {
                int thunc = amt / (100 / NullValues);
                for (int i = 0; i < thunc; ++i)
                    ret[i] = null;
            }
            return ret;
        }
    }
}
