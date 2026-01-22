using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrannyAlgos.ValuableWords
{
    public class ValuedWord
    {
        public ValuedWord(bool isWord, string word, int clusters, string[] ngram)
        {
            IsWordVar = isWord;
            TouchedBy = new int[clusters];

            if(!isWord)
            {
                Word = ngram[0];
                Edges = ngram.Skip(1).ToArray();
            }
            else
            {
                Word = word;
            }
        }

        public bool IsWordVar { get; private set; }

        /// <summary>
        /// Is Word
        /// </summary>
        /// <returns></returns>
        public bool IsWord() { return IsWordVar; }

        /// <summary>
        /// Is N gram
        /// </summary>
        /// <returns></returns>
        public bool IsNGram() { return !IsWordVar; }

        /// <summary>
        /// Word for dictionary?
        /// </summary>
        public string Word { get; private set; }

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
