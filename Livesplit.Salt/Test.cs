using System;

namespace LiveSplit.Salt
{
    public static class Test
    {
        public static void Main()
        {
            using SaltMemory mem = new SaltMemory();

            while (!mem.IsHooked)
            {
                mem.Hook();
            }

            mem.IsGameEnding();
            Console.ReadLine();
        }
    }
}
