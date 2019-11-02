using System;
using System.Diagnostics;
using System.Text;

namespace LiveSplit.Salt
{
    public static class ProcessExtensions
    {
        public static string GetString(this Process self, IntPtr address)
        {
            if (address == IntPtr.Zero)
            {
                return null;
            }

            StringBuilder str = new StringBuilder();

            int len = self.Read<int>(address, 0x4);

            // Probably safe to assume something has gone wrong in this case
            if (len > 1024)
            {
                return null;
            }

            for (int i = 0; i < sizeof(char) * len; i += sizeof(char))
            {
                str.Append(self.Read<char>(address, 0x8 + i));
            }

            return str.ToString();
        }
    }
}
