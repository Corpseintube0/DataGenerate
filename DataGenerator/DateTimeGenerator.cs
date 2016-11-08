using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class DateTimeGenerator : TestDataGenerator
    {
        private Random _rnd = RandomProvider.GetThreadRandom();

        public DateTime Min { get; set; }
        public DateTime Max { get; set; }

        public DateTimeGenerator(DateTime min, DateTime max)
        {
            Min = min;
            Max = max;
        }

        public DateTimeGenerator()
        {
            Min = DateTime.MinValue;
            Max = DateTime.Now;
        }

        private DateTime NextDate()
        {
            if (Min >= Max)
                throw new Exception("Параметр min должен быть меньше параметра max");
            int daysDiff = (Max - Min).Days + 1;
            return Min.AddDays(_rnd.Next(daysDiff));
        }

        public override string Next()
        {
            var ret = NextDate();
            return ret.Date.ToString();
        }

        public override string[] NextSet(int amt)
        {
            string[] ret;
            ret = new string[amt];
            if (UniqueValues) //для уникальных значений
            {
                List<DateTime> retDate = new List<DateTime>();
                for (int i = 0; i < amt; ++i)
                {
                    DateTime temp = NextDate();
                    if (Array.IndexOf(retDate.ToArray(), temp) > -1)
                    {
                        --i;
                        continue;
                    }
                    else
                        retDate.Add(temp);
                }
                for (int i = 0; i < amt; ++i)
                    ret[i] = retDate[i].ToString();
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
                    ret[i] = DateTime.MinValue.ToString();
            }

            if (EmptyValues > 0) //добавляем пустые значения
            {
                int thunc = amt / (100 / EmptyValues);
                for (int i = 0; i < thunc; ++i)
                    ret[i] = "";
            }
            return ret;
        }
    }
}
