using System;
using System.Collections.Generic;
using System.Linq;

namespace GrannyAlgos.Containers
{
    public class NGramList
    {
        private Dictionary<string, int> frequencies;

        /// <summary>
        /// new NGramList(File.ReadAllLines());
        /// </summary>
        /// <param name="lines"></param>
        public NGramList(string[] lines)
        {
            frequencies = new Dictionary<string, int>(lines.Length);

            foreach(var line in lines)
            {
                var csvLine = line.Split(';');

                if(!(frequencies).ContainsKey(csvLine[1]))
                {
                    frequencies[csvLine[1]] = int.Parse(csvLine[0]);
                }
            }
        }

        public HashSet<string> GetTopNGrams(int n)
        {
            var top = frequencies.AsEnumerable().OrderByDescending(x => x.Key).Select(x => x.Key);
            return new HashSet<string>(top.Take(n));
        }
    }
}
