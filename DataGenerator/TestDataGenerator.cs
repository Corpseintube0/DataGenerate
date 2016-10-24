using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Sql;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Tools.DataGenerator;


namespace DataGeneration
{
    //[GeneratorStyles(DesignerStyles = GeneratorDesignerStyles.CanProduceUniqueValues)]
    //public class MyGenerator : Generator
    //{

    //}
    public abstract class TestDataGenerator
    {
        /// <summary>
        /// Значения не должны повторяться в наборе.
        /// </summary>
        public bool UniqueValues
        {
            get;
            set;
        }
        /// <summary>
        /// Зациклить данные в наборе.
        /// </summary>
        public bool CicleValues
        {
            get;
            set;
        }
        /// <summary>
        /// Процент NULL значений в наборе.
        /// </summary>
        public byte NullValues
        {
            get;
            set;
        }

        /// <summary>
        /// Процент пустых значений в наборе.
        /// </summary>
        public byte EmptyValues
        {
            get;
            set;
        }

        /// <summary>
        /// Возвращает сгенерированное значение, в соответствии с текущими настройками.
        /// </summary>
        public abstract string Next();

        /// <summary>
        /// Возвращает набор сгенерированных значений в виде массива, в соответствии с текущими настройками.
        /// </summary>
        /// <param name="count">Количество возвращаемых значений.</param>
        /// <returns></returns>
        public abstract string[] NextSet(int amt);
    }
}
