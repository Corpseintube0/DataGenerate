using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration
{
    public class DateTimeGenerator : TestDataGenerator
    {
        private Random _rnd = RandomProvider.GetThreadRandom();

        public DateTime Min { get; set; }
        public DateTime Max { get; set; }

        public DateTimeGenerator(DateTime min, DateTime max)
        {
            Min = min;
            Max = max;
        }

        public DateTimeGenerator()
        {
            Min = DateTime.MinValue;
            Max = DateTime.Now;
        }

        //TODO: переделать
        public override string Next()
        {
            int year = _rnd.Next(Min.Year, Max.Year);
            int month = _rnd.Next(Min.Month, Max.Month);
            int day = _rnd.Next(Min.Day, Max.Day);

            var ret = new DateTime(year, month, day);
            return ret.Date.ToString();
        }
    }
}
