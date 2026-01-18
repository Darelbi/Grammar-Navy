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

    public class OPUSTokenizer: ITokenizer
    {
        public List<Sentence> sentences = new List<Sentence>();

        public OPUSTokenizer(OPUSDocument document, CultureInfo info)
        {
            foreach(var dialogue in document.dialogues)
            {
                var dirtySentences = Normalizer.NGramClearUtils.SplitSentences(dialogue);
                foreach (var sentence in dirtySentences)
                {
                    var cleanSentence = Normalizer.NGramClearUtils.CleanForNgrams(sentence, info);
                    sentences.Add(new Sentence { words = cleanSentence.Split(' ') });
                }
            }
        }

        public IEnumerable<string> GetNGrams(int N)
        {
            foreach(var sentence in sentences)
            {
                if (sentence.words.Length < N)
                    continue; // asked for 3 gram and 2 words? skip
                else
                {
                    for(int i=0; i< sentence.words.Length-N;i++)
                    {
                        yield return string.Join(" ", sentence.words.Skip(i).Take(N));
                    }
                }
            }

            yield break;
        }
    }
}
