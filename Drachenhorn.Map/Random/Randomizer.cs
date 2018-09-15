using System;
using System.Collections.Generic;
using System.Text;

namespace Drachenhorn.Map.Random
{
    public static class Randomizer
    {
        private static System.Random _random = new System.Random();

        private static int _seed;
        public static int Seed
        {
            get
            {
                if (_seed == 0)
                    Reset();
                return _seed;
            }
            private set
            {
                if (_seed == value)
                    return;
                _seed = value;
                _random = new System.Random(_seed);
            }
        }

        public static void Reset(int seed = 0)
        {
            if (seed == 0)
                seed = new System.Random().Next(Int32.MinValue, Int32.MaxValue);

            Seed = seed;
        }


        public static double Get()
        {
            return _random.NextDouble();
        }

        public static int Get(ushort max)
        {
            return _random.Next(0, max);
        }

        public static int Get(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
