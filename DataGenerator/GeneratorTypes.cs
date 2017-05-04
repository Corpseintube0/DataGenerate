using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    /// <summary>
    /// Задает типы генераторов данных.
    /// </summary>
    public enum GeneratorTypes
    {
        GUID,
        Integer,
        Text,
        Date,
        Names,
        Lastnames,
        ForeignKey,
        Time,           //время
        DateTime,       //дата+время
        Department,     //отдел
        WorkingPosition,//должность
        Country,        //страна
        Emails,         //почтовые адреса
        Hash,           //хэш-функция
        Gender,         //пол
        PhoneNumber,    //номер телефона    
        Cities,         //Города
        PostalCode      //Почтовый индекс
    }
}
