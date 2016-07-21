using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGen
{
    /// <summary>
    /// Обертка словаря для текстовых генераторов данных.
    /// </summary>
    class Thesaurus
    {
        /// <summary>
        /// Файл, с которым сопоставлен словарь.
        /// </summary>
        private FileInfo fi;

        /// <summary>
        /// Количество значений в словаре.
        /// </summary>
        private int _size;

        /// <summary>
        /// Индексы начала слов в файле.
        /// </summary>
        private int[] _indicies;

        /// <summary>
        /// Получает размер словаря.
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Получает лексему из файла словаря с заданным индексом.
        /// </summary>
        public String this[int index]
        {
            get
            {
                if (index >= _size)
                    throw new IndexOutOfRangeException(String.Format("Индекс [{0}], размер словаря: {1}", index, _size));

                int beginRead = _indicies[index];
                int count = _indicies[index + 1] - _indicies[index] - 2;
                char[] buf = new char[count];

                var sr = fi.OpenText();
                sr.ReadBlock(new char[beginRead], 0, beginRead); //пропустили нужное число символов
                sr.Read(buf, 0, count);
                sr.Close();

                return new string(buf);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Thesaurus, сопоставляя его с текстовым файлом на диске.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public Thesaurus(string fileName)
        {
            List<int> indicies = new List<int>();
            indicies.Add(0); //первое слово начинается с нулевого символа

            fi = new FileInfo(fileName);
            var sr = fi.OpenText();
            _size = 0;
            int i = 0;
            while (/*!sr.EndOfStream*/true)
            {
                ++i;
                if (sr.Read() == '\n')
                {
                    indicies.Add(i); //запомнили новый индекс
                    ++_size;  //увеличили размер словаря
                }
                if (sr.EndOfStream)
                {
                    indicies.Add(i+2);
                    break;
                }
            }
            sr.Close();
            _indicies = indicies.ToArray();
        }
    }
}
