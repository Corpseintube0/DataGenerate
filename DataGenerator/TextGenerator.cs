using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen
{
    class TextGenerator : TestDataGenerator
    {
        /// <summary>
        /// Инициализатор генератора случайных чисел.
        /// </summary>
        private int _seed;

        /// <summary>
        /// Количество значений в словаре.
        /// </summary>
        private int _dictionarySize;

        /// <summary>
        /// 
        /// </summary>
        private Random _rnd = RandomProvider.GetThreadRandom();

        public TextGenerator(String fileName)
        {
            _seed = DateTime.Now.Millisecond;
            
            var fi = new FileInfo(fileName);
            var sr = fi.OpenText();
            _dictionarySize = 0;
            while (!sr.EndOfStream)
            {
                if (sr.Read() == '\n')
                    ++_dictionarySize;  //определяем размер словаря
            }
            sr.Close();
        }

        public TextGenerator(int seed, String fileName)
        {
            _seed = seed;

            var fi = new FileInfo(fileName);
            var sr = fi.OpenText();
            _dictionarySize = 0;
            while (!sr.EndOfStream)
            {
                if (sr.Read() == '\n')
                    ++_dictionarySize;  //определяем размер словаря
            }
            sr.Close();
        }

        public override string Next()
        {
            _rnd.Next(0, _dictionarySize);
            //TODO: Сделать класс Dictionary (обертка словаря)
        }
    }
}
