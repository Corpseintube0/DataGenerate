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
    /// Генератор случайного текста.
    /// </summary>
    public class RandomTextGenerator : TestDataGenerator
    {
        /// <summary>
        /// Инициализатор генератора случайных чисел.
        /// </summary>
        //private int _seed;

        private Random _rnd = RandomProvider.GetThreadRandom();

        private RandomTextVocabularyDB _vocabulary;

        public RandomTextGenerator()
        {
            _vocabulary = new RandomTextVocabularyDB();
            WordsAmount = 20;
        }

        public RandomTextGenerator(int wordsAmt)
        {
            _vocabulary = new RandomTextVocabularyDB();
            WordsAmount = wordsAmt;
        }

        /// <summary>
        /// Количество слов для генерации.
        /// </summary>
        public int WordsAmount { get; set; }

        public override string Next()
        {
            string result = "";
            var ds = _vocabulary.GetRandomValues(WordsAmount);
            for (int i = 0; i < WordsAmount; ++i)
                result += ds.Tables[0].Rows[i].ItemArray[0].ToString() + " ";
            return result;
        }

        public override string[] NextSet(int amt)
        {
            string[] ret;
            ret = new string[amt];
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
            
            return ret;
        }
    }
}
