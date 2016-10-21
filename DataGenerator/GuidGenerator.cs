using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration
{
    public class GuidGenerator : TestDataGenerator
    {
        public override string Next()
        {
            return Guid.NewGuid().ToString();
        }

        public GuidGenerator()
        {
            
        }
    }
}
