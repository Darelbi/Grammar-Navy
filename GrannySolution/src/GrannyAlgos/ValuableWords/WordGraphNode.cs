using System;
using System.Collections.Generic;
using System.Linq;

namespace GrannyAlgos.ValuableWords
{
    public class Weights
    {
        public long Count { get; set; }
        public long Insertions { get; set; }
    }

    public class WordGraphNode : IEquatable<WordGraphNode>
    {
        public string Word { get; private set; }

        public string AltName { get; private set; }

        public bool Ghost { get; private set; }

        public int StaticHashCode { get; private set; }

        public WordGraphNode(string word, string altname, bool ghost)
        {
            StaticHashCode = (word + altname).GetHashCode();
            Word = word;
            AltName = altname;
            Outgoings = new List<WordGraphNode>();
            Ingoings = new List<WordGraphNode>();
            OutWeights = new Dictionary<string, Weights>();
            Ghost = ghost;
        }

        public double CurrentScore { get; set; }

        public double TempScore { get; set; }

        public List<WordGraphNode> Outgoings { get; set; }

        public Dictionary<string, Weights> OutWeights {get; set; }

        public List<WordGraphNode> Ingoings { get; set; }

        /// <summary>
        /// Note since ghost nodes creates duplicate words those are referenced by ngram
        /// </summary>
        /// <param name="ngram"></param>
        /// <param name="node"></param>
        /// <param name="Count"></param>
        /// <param name="Insertions"></param>
        public void LinkToNextWord(WordGraphNode node, long Count, long Insertions)
        {
            var ngram = node.AltName;
            OutWeights[node.AltName] = new Weights() { Count = Count, Insertions = Insertions };
            //AltName = ngram; // how do we find owner of the weight? with this.

            if (!node.Ingoings.Contains(this))
                node.Ingoings.Add(this);

            if (!Outgoings.Contains(node))
                Outgoings.Add(node);
        }

        public override int GetHashCode()
        {
            return StaticHashCode;
        }

        public bool Equals(WordGraphNode other)
        {
            return Word.Equals(other.Word) && AltName.Equals(other.AltName);
        }
    }
}
