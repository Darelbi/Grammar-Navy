using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.ValuableWords
{
    public class ValuableWords
    {
        public ValuableWords(string[] seeds)
        {
            Seeds = seeds;
            ngrams = new Dictionary<string, ValuedNGram>(10000);
        }

        public string[] Seeds { get; private set; }

        private Dictionary<string, ValuedNGram> ngrams;

        public void AddCsvLines(string[] csvLines)
        {
            foreach(var line in csvLines)
            {
                var cols = line.Split(';');
                var count = long.Parse(cols[0]);
                var insertions = long.Parse(cols[1]);
                var words = cols[2].Split(' ');

                ngrams.Add(cols[2],
                new ValuedNGram(cols[2], Seeds.Length, words, count, insertions));
            }
        }

        public string[] NoonWalk(Dictionary<string, ValuedNGram> wordsDic, int outIteration, int[] innerIterations)
        {
            Random rnd = new Random();
            for (int i = 0; i < outIteration; i++)
                for (int s = 0; s < Seeds.Length; s++)
                {
                    if (i % 300 == 0)
                        System.Console.WriteLine("at: " + i);

                    ValuedNGram word = wordsDic[Seeds[s]];

                    for (int j = 0; j < innerIterations[s]; j++)
                    {
                        word = word.Touch(s, rnd.NextDouble());
                        if (word == null)
                            break;
                    }
                }

            return wordsDic.Values.OrderByDescending(x => x.TouchedBy.Sum()).Select(x => x.Word).ToArray();
            //return null;
        }

        public string[] BuildValuableDictionary(int outIteration, int[] innerIterations)
        {
            var sorted = ngrams.Values.OrderByDescending(x => (x.Count * (x.Insertions > 2 ? 10 : 1))).ToArray();

            Dictionary<string, ValuedNGram> wordsDic = new Dictionary<string, ValuedNGram>(sorted.Length);

            // Prepare nodes
            foreach(var sor in sorted)
            {
                foreach(var edge in sor.Edges)
                {
                    if (!wordsDic.ContainsKey(edge))
                    {
                        wordsDic.Add(edge, new ValuedNGram(edge, Seeds.Length, null, sor.Count, sor.Insertions));
                    }
                }
            }

            System.Console.WriteLine("Prepared nodes");

            // Link
            foreach (var sor in sorted)
            {
                foreach (var edge in sor.Edges)
                {
                    // each word get best score for the best ngram it has it.
                    wordsDic[edge].TryToIncreaseScore(sor.Count, sor.Insertions);

                    // reachable edges
                    var outgoings = sor.Edges.SkipWhile(x => x != edge).Skip(1).Select( x => wordsDic[x]).ToArray();
                      
                    // only set as outoing the next word in the ngram
                    if(outgoings.Length > 0)
                            wordsDic[edge].AddOutgoings(outgoings);
                }
            }

            System.Console.WriteLine("Added edges");

            return NoonWalk(wordsDic, outIteration, innerIterations);
        }
    }
}
