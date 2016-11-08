using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class TextGenerator : TestDataGenerator
    {
        /// <summary>
        /// Инициализатор генератора случайных чисел.
        /// </summary>
        private int _seed;

        /// <summary>
        /// Представление для текстового файла словаря.
        /// </summary>
        private Thesaurus _lexicon;

        /// <summary>
        /// Обертка для класса Random.
        /// </summary>
        private Random _rnd = RandomProvider.GetThreadRandom();

        public TextGenerator(String fileName)
        {
            _seed = DateTime.Now.Millisecond;
            _lexicon = new Thesaurus(fileName);
        }

        public TextGenerator(int seed, String fileName)
        {
            _seed = seed;
            _lexicon = new Thesaurus(fileName);
        }

        public override string Next()
        {
            int index = _rnd.Next(0, _lexicon.Count);
            return _lexicon[index];
        }

        /// <summary>
        /// Генерирует набор случайных значений в соответствии с заданными параметрами.
        /// </summary>
        /// <param name="amt">Требуемое количество.</param>
        /// <returns>Набор сгенерированных значений в виде массива типа string.</returns>
        public override string[] NextSet(int amt)
        {
            string[] ret;
            ret = new string[amt];
            //если уникальные и диапазон значений меньше требуемого для уникальных значений
            if ( _lexicon.Count <= amt && UniqueValues)
            {
                amt = _lexicon.Count;
                ret = new string[amt];
                for (int i = 0; i < amt; ++i)
                    ret[i] = _lexicon[i];
            }
            else
            if (UniqueValues) //для уникальных значений
            {
                List<string> retString = new List<string>();
                for (int i = 0; i < amt; ++i)
                {
                    string temp = Next();
                    if (Array.IndexOf(retString.ToArray(), temp) > -1)
                    {
                        --i;
                        continue;
                    }
                    else
                        retString.Add(temp);
                }
                for (int i = 0; i < amt; ++i)
                    ret[i] = retString[i];
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