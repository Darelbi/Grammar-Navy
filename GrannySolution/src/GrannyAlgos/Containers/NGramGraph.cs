using System;
using System.Collections.Generic;
using System.Linq;

namespace GrannyAlgos.Containers
{
    public class NGramKnot : IEquatable<NGramKnot>
    {
        public NGramKnot(string ngram, long count, long insertions )
        {
            NGram = ngram;
            Edges = ngram.Split(' ');
            Count = count;
            Insertions = insertions;
        }

        public long Count { get; private set; }

        public long Insertions { get; private set; }

        public string NGram { get; private set; }

        public string[] Edges { get; private set; }


        public override int GetHashCode()
        {
            return NGram.GetHashCode();
        }

        bool IEquatable<NGramKnot>.Equals(NGramKnot other)
        {
            return NGram.Equals(other.NGram);
        }
    }

    public class NGramGraph
    {
        private Dictionary<string, NGramKnot> frequencies;

        /// <summary>
        /// new NGramList(File.ReadAllLines());
        /// </summary>
        /// <param name="lines"></param>
        public NGramGraph(string[] lines)
        {
            frequencies = new Dictionary<string, NGramKnot>(lines.Length);

            foreach(var line in lines)
            {
                var csvLine = line.Split(';');

                if(!(frequencies).ContainsKey(csvLine[1]))
                {
                    frequencies[csvLine[2]] = new NGramKnot(line, 
                        long.Parse(csvLine[0]),
                        long.Parse(csvLine[1]));
                }
            }
        }

        public HashSet<string> GetTopNGrams(int n)
        {
            var top = frequencies.AsEnumerable().OrderByDescending(x => x.Value).Select(x => x.Key);
            return new HashSet<string>(top.Take(n));
        }
    }
}
