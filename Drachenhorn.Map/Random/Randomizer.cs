namespace Drachenhorn.Map.Random
{
    public static class Randomizer
    {
        private static System.Random _random = new System.Random();

        private static int _seed;

        #region Properties

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

        #endregion

        public static void Reset(int seed = 0)
        {
            if (seed == 0)
                seed = new System.Random().Next(int.MinValue, int.MaxValue);

            Seed = seed;
        }


        public static double Get()
        {
            return _random.NextDouble();
        }

        public static int Get(ushort max)
        {
            return _random.Next(0, max + 1);
        }

        public static int Get(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}