using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.ValuableWords
{
    public class ValuedNGram
    {
        public ValuedNGram(string word, int clusters, string[] ngram, long count, long insertions)
        {
            TouchedBy = new int[clusters];
            Count = count;
            Insertions = insertions;
            if(ngram != null && ngram.Length > 1)
                Edges = ngram.Skip(1).ToArray();
            Word = word;
            Outgoing = new List<ValuedNGram>();
        }

        public void TryToIncreaseScore(long count, long insertions)
        {
            if (count > this.Count)
                this.Count = count;

            if (insertions > this.Insertions)
                this.Insertions = insertions;
        }

        public List<ValuedNGram> Outgoing { get; private set; }

        public void AddOutgoings(ValuedNGram[] outgoing)
        {
            foreach (var outg in outgoing) // add only new words
                if (!Outgoing.Any( x => x.Word == outg.Word))
                    Outgoing.Add(outg);
        }

        /// <summary>
        /// Word for dictionary?
        /// </summary>
        public string Word { get; private set; }

        internal ValuedNGram Touch(int s, double random)
        {
            TouchedBy[s]++;
            
            // I know is slow this way. Fast prototyping sorry.
            var sorted = Outgoing.OrderByDescending(x => (x.Count * (x.Insertions > 2 ? 10 : 1))).ToArray();

            if (!sorted.Any())
                return null;

            var sum = sorted.Select(x => (x.Count * (x.Insertions > 2 ? 10 : 1))).Sum();

            var upTo = (sum-1) * random;

            return sorted.SkipWhile(x => 
            {
                var amount = (x.Count * (x.Insertions > 2 ? 10 : 1));
                upTo -= amount;
                return amount < upTo;
            })
            .First();
        }

        /// <summary>
        /// In case of backwalk, ignore the touch.
        /// </summary>
        public int CurrentTouch { get; private set; }

        /// <summary>
        /// Walked from a cluster
        /// </summary>
        public int[] TouchedBy { get; private set; }

        /// <summary>
        /// Estimated count of Ngrams in the table
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// How many times the Ngram was inserted in the CSV for real
        /// </summary>
        public long Insertions { get; private set; }

        /// <summary>
        /// Words from Ngrams
        /// </summary>
        public string[] Edges { get; private set; }
    }
}
