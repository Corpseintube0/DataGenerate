using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration
{
    /// <summary>
    /// Задает типы генераторов данных.
    /// </summary>
    public enum GeneratorTypes
    {
        GUID,
        Int,
        Text,
        Date,
        Names,
        Surnames,
    }
}
