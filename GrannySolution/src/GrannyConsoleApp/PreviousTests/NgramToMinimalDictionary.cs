using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using GrannyAlgos.ValuableWords;

namespace GrannyConsoleApp.PreviousTests
{
    public class NgramToMinimalDictionary
    {
        public void NgramToMinimalDictionaryRun()
        {
            //string path = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\";
            //var words = File.ReadAllLines(path + "wordRank_LogSum.vdic.csv", test1);


            string read2Grams = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\2-Grams-100000.csv";
            string read3Grams = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\3-Grams-100000.csv";
            string read4Grams = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\4-Grams-100000.csv";
            var n2grams = File.ReadAllLines(read2Grams);
            var n3grams = File.ReadAllLines(read3Grams);
            var n4grams = File.ReadAllLines(read4Grams);
            var grams = n2grams.Concat(n3grams).Concat(n4grams).ToArray(); ;


            Console.WriteLine("---START---");

            var builder = new WordGraphBuilder();
            var graph = builder.GetGraph(grams);
            builder.PageWalk(graph, 0.85f, 0.00001f, 1000);

            var AllWordsSorted = graph.Values
                .SelectMany(x => x)
               // .Where(z=>z.Ghost == false)
               .GroupBy(k => k.Word)
               .Select(
                z => new { Word = z.Key, Score = z.Select(r => r.CurrentScore).Sum() }
               )
                .OrderByDescending(y => y.Score)
                .Select(x => x.Word).ToArray();

            int[] wordsToTake = new int[] { 400, 900, 1400, 1900, 2400, 2900, 3400, 3900, 4400, 4900 };
            var top100 = File.ReadAllLines(@"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\Words-Frequency.csv")
                    .Take(100);

            foreach (var n in wordsToTake)
            {
                string path = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\";
                File.WriteAllLines(path + "wordRank_LogSum_" + (100 + n) + ".vdic.csv", top100.Concat(AllWordsSorted.Take(n)));
            }

            Console.WriteLine("---FINISH---");
            Console.ReadKey();

            //string path = @"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\";
            //var findWords = new ValuableWords(new string[] { word });
            //findWords.AddCsvLines(n2grams);
            //findWords.AddCsvLines(n3grams);
            //findWords.AddCsvLines(n4grams);
            //var result =
            //findWords.BuildValuableDictionary(35000, new int[] { 4 });
            //File.WriteAllLines(path+ word + ".vdic.csv", result);
        }
    }
}
