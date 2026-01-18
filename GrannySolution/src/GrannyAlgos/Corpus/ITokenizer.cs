using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.Corpus
{
    public interface ITokenizer
    {
        IEnumerable<string> GetNGrams(int N);
    }
}
