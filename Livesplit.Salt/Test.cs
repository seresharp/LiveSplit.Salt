using System;
using System.Threading;

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

            string anim = mem.GetPlayerAnim(0);

            while (true)
            {
                Thread.Sleep(10);
                string newAnim = mem.GetPlayerAnim(0);

                if (anim != newAnim)
                {
                    anim = newAnim;
                    Console.WriteLine(anim);
                }
            }
        }
    }
}
