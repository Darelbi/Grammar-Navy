using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.Containers
{
    public interface IRandSpaceSavingCounter<TVal>: ICollection<TVal>
    {
        IEnumerable<KeyValuePair<TVal, long>> GetOccurences();
    }
}
