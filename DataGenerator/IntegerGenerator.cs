using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration
{
    public class IntegerGenerator : TestDataGenerator
    {
        private int _seed;
        private int _lower, _upper;

        private Random _rnd = RandomProvider.GetThreadRandom();

        public IntegerGenerator(int seed)
        {
            _seed = seed;
        }

        public IntegerGenerator()
        {
            _seed = Environment.TickCount;
        }

        public IntegerGenerator(int seed, int from, int to)
        {
            _seed = seed;
            _lower = from;
            _upper = to;
        }

        //public IntegerGenerator(int seed, long from, long to)
        //{
        //    _seed = seed;
        //    _lower = from;
        //    _upper = to;
        //}

        public IntegerGenerator(int from, int to)
        {
            _seed = DateTime.Now.Millisecond;
            _lower = from;
            _upper = to;
        }

        //public IntegerGenerator(long from, long to)
        //{
        //    _seed = DateTime.Now.Millisecond;
        //    _lower = from;
        //    _upper = to;
        //}

        public override string Next()
        {
            return _rnd.Next(_lower, _upper).ToString();
        }
    }
}
