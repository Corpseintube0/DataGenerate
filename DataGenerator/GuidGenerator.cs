using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class GuidGenerator : TestDataGenerator
    {

        public override string Next()
        {
            return Guid.NewGuid().ToString();
        }

        public override string[] NextSet(int amt)
        {
            string[] ret = new string[amt];
            for (int i = 0; i < amt; ++i)
                ret[i] = Next();
            return ret;
        }

        public GuidGenerator()
        {
            
        }
    }
}
