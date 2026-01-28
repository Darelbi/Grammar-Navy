using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.Containers
{
    public static class StringHash
    {
        public static ulong ULong(string text)
        {
            const ulong offsetBasis = 14695981039346656037UL;
            const ulong prime = 1099511628211UL;

            ulong hash = offsetBasis;

            unchecked
            {
                for (int i = 0; i < text.Length; i++)
                {
                    // char in .NET è UTF-16: due byte
                    hash ^= text[i];
                    hash *= prime;
                }
            }

            return hash;
        }

    }
}
