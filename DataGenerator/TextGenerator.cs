using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen
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
        /// 
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
    }
}
