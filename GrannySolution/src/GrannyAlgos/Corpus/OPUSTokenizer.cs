using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace GrannyAlgos.Corpus
{
    public class OPUSDocument
    {
        public string[] dialogues;
    }

    public class Sentence
    {
        public string[] words;
    }

    public class BoxedString
    {
        public string Line;
    }

    public class OPUSTokenizer: ITokenizer
    {
        public List<Sentence> sentences = new List<Sentence>();

        public OPUSTokenizer(OPUSDocument document, CultureInfo info)
        {
            Initialize(document, info, new HashSet<string>());
        }

        public OPUSTokenizer(OPUSDocument document, CultureInfo info, HashSet<string> excludeWords)
        {
            Initialize(document, info, excludeWords);
        }

        public void Initialize(OPUSDocument document, CultureInfo info, HashSet<string> excludeWords)
        {
            foreach (var dialogue in document.dialogues)
            {
                var dirtySentences = Normalizer.NGramClearUtils.SplitSentences(dialogue);
                foreach (var sentence in dirtySentences)
                {
                    var cleanSentence = Normalizer.NGramClearUtils.CleanForNgrams(sentence, info);
                    sentences
                        .Add(
                            new Sentence
                            {
                                words = cleanSentence.Split(' ')
                                .Where(x => !string.IsNullOrWhiteSpace(x) && !excludeWords.Contains(x))
                                .ToArray()
                            });
                }
            }
        }

        public IEnumerable<string> GetNGrams(int N)
        {
            foreach(var sentence in sentences.Where( x => x.words.Length > 0))
            {
                if (sentence.words.Length < N)
                    continue; // asked for 3 gram and 2 words? skip
                else
                {
                    for (int i = 0; i < sentence.words.Length - N; i++)
                    {
                        var ngrams = sentence.words.Skip(i).Take(N)  ;

                        if (N > 1 && ngrams.All(x => x == ngrams.First()))
                            continue; // No need to keep duplicate words for our use case

                        string ngram = string.Join(" ", ngrams);

                        //if ((ngram.StartsWith("estamos aqu") 
                        //            //&& ngram.Contains("pinturas")
                        //            )
                        //    || (ngram.Contains("do viejos t"))
                        //    )
                        //{
                        //    var lines = new string[] { ("found:" + ngram + "   for:" + debugText.Line + "\n")};
                        //    System.Console.WriteLine(".!.");
                        //    System.IO.File.AppendAllLines(@"C:\Users\Dario\Documents\GitHub\Grammar-Navy-Corpus\ES\ChatSubs\open_subtitles_es\findENCODINGERRORS.txt",

                            //        lines);
                            //}
                        yield return ngram;
                    }
                }
            }

            yield break;
        }
    }
}
