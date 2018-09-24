using System;

namespace VV_ProjetoLobosOvelhas
{
    internal class Randomizador
    {
        private static readonly int SEED = 1111;
        private static readonly Random rand = new Random(SEED);
        private static readonly bool useShared = true;

    public static Random getRandom()
        {
            if (useShared)
            {
                return rand;
            }
            else
            {
                return new Random();
            }
        }

        public static void reset()
        {
            if (useShared)
            {
                rand.Next(SEED);
            }
        }
    }
}