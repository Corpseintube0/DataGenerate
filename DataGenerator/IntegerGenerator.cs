using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class IntegerGenerator : TestDataGenerator
    {
        private int _seed;
        private int _lower, _upper;

        private Random _rnd = RandomProvider.GetThreadRandom();

        public IntegerGenerator(int seed)
        {
            _seed = seed;
        }

        public IntegerGenerator()
        {
            _seed = Environment.TickCount;
        }

        public IntegerGenerator(int seed, int from, int to)
        {
            _seed = seed;
            _lower = from;
            _upper = to;
        }

        public IntegerGenerator(int min, int max)
        {
            _seed = DateTime.Now.Millisecond;
            _lower = min;
            _upper = max;
        }

        public override string Next()
        {
            return _rnd.Next(_lower, _upper).ToString();
        }

        //TODO: test
        private int[] NextUniqueSet(int amt)
        {
            List<int> sort = new List<int>(_upper - _lower + 1);
            for (int i = _lower; i <= _upper; ++i)
                sort[i - _lower] = i;   //упорядоченный массив значений на всем диапазоне
            List<int> nonSort = new List<int>(amt);
            for (int i = 0; i < amt; ++i)
            {
                int index = _rnd.Next(0, sort.Count);
                nonSort.Add(sort[index]); //случайное число из упорядоченного массива
                sort.RemoveAt(index); //удалили вхождение случайного числа
            }
            return nonSort.ToArray();
        }

        public override string[] NextSet(int amt)
        {
            string[] ret;
            ret = new string[amt];
            //если уникальные и диапазон значений меньше требуемого для уникальных значений
            if (_upper - _lower <= amt && UniqueValues)
            {
                amt = _upper - _lower;
                ret = new string[amt];
                for (int i = 0; i < amt;  ++i)
                    ret[i] = (_lower + i).ToString();
            }
            else
            if (UniqueValues) //код для уникальных значений
            {
                List<int> retint = new List<int>();
                for (int i = 0; i < amt; ++i)
                {
                    int temp = _rnd.Next(_lower, _upper);
                    if (Array.IndexOf(retint.ToArray(), temp) > -1)
                    {
                        --i;
                        continue;
                    }
                    else
                        retint.Add(temp);
                }
                for (int i = 0; i < amt; ++i)
                    ret[i] = retint[i].ToString();
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
                    ret[i] = "0";
            }
            return ret;
        }
    }
}
