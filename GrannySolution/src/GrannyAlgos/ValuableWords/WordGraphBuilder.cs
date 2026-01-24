using System;
using System.Collections.Generic;
using System.Linq;

namespace GrannyAlgos.ValuableWords
{
    public class WordGraphBuilder
    {
        public void PageWalk(Dictionary<string, List<WordGraphNode>> graph, float damping = 0.85f, float delta = 0.0001f, int maxIter = 10000)
        {
            int totalNodes = 0;
            foreach (var item in graph.Values)
            {
                foreach (var node in item)
                {
                    totalNodes++;
                }
            }

            // initialize scores
            double scoreEach = 1.0 / totalNodes;
            foreach (var item in graph.Values)
            {
                foreach (var node in item)
                {
                    node.TempScore = 0;
                    node.CurrentScore = scoreEach;
                }
            }

            // page rank algorithm with slight chnanges.
            for (int i=0; i<maxIter; i++)
            {
                int successCount = 0;

                // init oldScore
                foreach (var item in graph.Values)
                    foreach (var node in item)
                        node.TempScore = 0;

                // propagate values depending on links and weights
                foreach (var item in graph.Values)
                    foreach(var node in item)
                        NodeWalk(node);

                // concretize the page rank for 1 iteration
                foreach (var item in graph.Values)
                    foreach (var node in item)
                        NodeRun(node, scoreEach, damping, delta, ref successCount);

                var limitNodes = (1.0 - delta) * totalNodes;

                Console.WriteLine("Iteration " + i + " stablenodes: " + successCount + " / " + totalNodes);
                if (successCount >= limitNodes)
                    break;
            }
        }

        private void NodeRun(WordGraphNode node, double scoreEach, float damping, float delta, ref int successCount)
        {
            var oldScore = node.CurrentScore;
            node.CurrentScore = scoreEach*(1.0 - damping) + damping * node.TempScore;
            var newScore = node.CurrentScore;

            if ((newScore - oldScore) < delta)
                successCount++;
        }

        /// <summary>
        /// Read from CurrentScore and store in OldScore
        /// </summary>
        /// <param name="node"></param>
        /// <param name="successCount"></param>
        private void NodeWalk(WordGraphNode node)
        {
            double ammass = 0;
            foreach (var neigh in node.OutWeights.Values)
                ammass += neigh.Insertions> 5 ? neigh.Count : 0;

            if (ammass < 10)
                return;

            foreach (var keyvalue in node.OutWeights)
            {
                var weights = keyvalue.Value;
                var altName = keyvalue.Key;
                var weight = weights.Insertions > 5 ? weights.Count : 0;

                var neight = node.Outgoings.Where(x => x.AltName == keyvalue.Key).First();
                neight.TempScore += node.CurrentScore * (weight / ammass);
            }

        }

        private void UpsertNodeInlist(Dictionary<string, List<WordGraphNode>> graph, string word, string altname, WordGraphNode node)
        {
            if(!graph.ContainsKey(word))
                graph[word] = new List<WordGraphNode>();

            if (!graph[word].Contains(node))
                graph[word].Add(node);
        }

        public Dictionary<string,List<WordGraphNode>> GetGraph(string[] csvLines)
        {
            Dictionary<string, List<WordGraphNode>> graph = new Dictionary<string, List<WordGraphNode>>(csvLines.Length);

            // Add single words
            foreach (var line in csvLines)
            {
                var cols = line.Split(';');
                var count = long.Parse(cols[0]);
                var insertions = long.Parse(cols[1]);
                var words = cols[2].Split(' ');

                if (words.Length > 1 && words.All(x => x == words.First()))
                    continue;

                foreach (var word in words)
                {
                    if (word == words.First() || word == words.Last())
                        UpsertNodeInlist(graph, word, word, new WordGraphNode(word, word, false));
                }
            }

            // Add any ngram (2,3,4 words)
            foreach (var line in csvLines)
            {
                var cols = line.Split(';');
                var count = long.Parse(cols[0]);
                var insertions = long.Parse(cols[1]);
                var ngram = cols[2];
                var words = ngram.Split(' ');

                if (words.Length > 1 && words.All(x => x == words.First()))
                    continue;

                var alternativePath = words.Select(
                    (x, i) =>
                    {
                        if (x == words.First() || x == words.Last()) // first and last node are in the graph
                        {
                            // use regular nodes
                            return graph[x].Where(y => y.Ghost == false).First();
                        }
                        else // paths of Ngrams (middle nodes) are ghosts in the etherworld.
                        {
                            // create ghost nodes
                            var ghost = new WordGraphNode(x, ngram, true);
                            //UpsertNodeInlist(graph, x, ngram, ghost);                            
                            return ghost;
                        }
                    }
                    ).ToArray();

                for (int i = 0; i < alternativePath.Length - 1; i++)
                {
                    var wordA = alternativePath[i];
                    var wordB = alternativePath[i + 1];

                    //here lies the real graph, the others are just references.
                    wordA.LinkToNextWord(wordB, count, insertions);
                }
            }

            return graph;
        }
    }
}
