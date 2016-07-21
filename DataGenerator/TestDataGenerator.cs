using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Sql;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Tools.DataGenerator;


namespace DataGen
{
    //[GeneratorStyles(DesignerStyles = GeneratorDesignerStyles.CanProduceUniqueValues)]
    //public class MyGenerator : Generator
    //{

    //}
    public abstract class TestDataGenerator
    {
        /// <summary>
        /// Значения уникальны.
        /// </summary>
        public bool UniqueValues
        {
            get;
            set;
        }
        /// <summary>
        /// Зациклить данные.
        /// </summary>
        public bool CicleValues
        {
            get;
            set;
        }
        /// <summary>
        /// Процент NULL значений.
        /// </summary>
        public byte NullValues
        {
            get;
            set;
        }

        /// <summary>
        /// Процент пустых значений.
        /// </summary>
        public byte EmptyValues
        {
            get;
            set;
        }

        public abstract string Next();
    }
}
