using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration
{
    public class RandomTextGenerator : TestDataGenerator
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
        /// 
        /// </summary>
        private Random _rnd = RandomProvider.GetThreadRandom();

        public RandomTextGenerator()
        {
            _seed = DateTime.Now.Millisecond;
            _lexicon = new Thesaurus("pldf-win.txt");
        }

        public RandomTextGenerator(int wordsAmt)
        {
            _seed = DateTime.Now.Millisecond;
            WordsAmount = wordsAmt;
            _lexicon = new Thesaurus("pldf-win.txt");
        }

        public int WordsAmount { get; set; }

        public override string Next()
        {
            string ret = "";
            for (int i = 0; i < WordsAmount; ++i)
            {
                int index = _rnd.Next(0, _lexicon.Count);
                ret += _lexicon[index] + " ";
            }
            return ret;
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
