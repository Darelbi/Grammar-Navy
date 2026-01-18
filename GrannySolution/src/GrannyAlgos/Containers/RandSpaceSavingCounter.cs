using System;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using System.Linq;

namespace GrannyAlgos.Containers
{

    public class RandSpaceSavingCounter<TVal> : IRandSpaceSavingCounter<TVal>
    {
        public class Node<KVal> : StablePriorityQueueNode, IEquatable<RandSpaceSavingCounter<KVal>.Node<KVal>>
        {
            public Node(KVal val)
            {
                    Value = val;
            }

            public KVal Value {  get; private set;}

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }

            bool IEquatable<RandSpaceSavingCounter<KVal>.Node<KVal>>.Equals(RandSpaceSavingCounter<KVal>.Node<KVal> other)
            {
                return Value.Equals(other.Value);
            }
        }

        private Random rnd;
        private Dictionary<Node<TVal>, Node<TVal>> dic;
        private StablePriorityQueue<Node<TVal>> queue;
        private long seenElements; // how many things we examined => used for % probability of insertion
        private int maxElements; // fixed size
        private int minPriorityInQueue; // lowest priority found in the queue
        private int maxPriorityInQueue; // highest priority found in the queue
        private int nextPriorityCheckIn; // how many insertions before check priorities
        private const int PriorityCheckInterval = 10000;
        private long abosluteOffset;

        public int Count
        {
            get
            {
                return dic.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public RandSpaceSavingCounter(int capacity)
        {
            maxElements = capacity;
            Clear();
            rnd = new Random();
        }

        public void Add(TVal item)
        {
            seenElements++;
            var candidate = new Node<TVal>(item);

            if (dic.ContainsKey(candidate))
                AddExisting(candidate);
            else
            {
                if (Count < maxElements)
                    AddAnyway(candidate, priority:0);
                else
                    AddRandomized(candidate);
            }
        }

        private void AddExisting(Node<TVal> item)
        {
            var existing = dic[item];
            var priority = existing.Priority + 1;
            queue.UpdatePriority(existing, priority);
            PriorityCheck(priority);
        }

        private void PriorityCheck(int insertedPriority)
        {
            //we value the relative priority of elements, not the absolute value
            nextPriorityCheckIn--;
            if (nextPriorityCheckIn <= 0)
            {
                nextPriorityCheckIn = PriorityCheckInterval;
                int min, max;
                queue.AddAllPriorities(0, out min, out max);

                // center max and min priorities around 0 value
                // no guarantee the elmements will slowly grow a big difference anyway
                int midpoint = (max - min) / 2;
                abosluteOffset += midpoint; //used to keep track of absolute counter values
                queue.AddAllPriorities( -midpoint, out min, out max);
                maxPriorityInQueue = max;
                minPriorityInQueue = min;
            }
        }

        private void AddAnyway(Node<TVal> item, int priority)
        {
            queue.Enqueue(item, priority);
            dic.Add(item, item);
        }

        private void AddRandomized(Node<TVal> item)
        {
            long minValue = minPriorityInQueue + abosluteOffset + 1;

            if(rnd.NextDouble() <  ( 1.0/ minValue))
            {
                var victim = queue.Dequeue();
                dic.Remove(victim);
                AddAnyway(item, minPriorityInQueue + 1);
            }
        }

        public void Clear()
        {
            seenElements = 0;
            abosluteOffset = 0;
            minPriorityInQueue = 0;
            maxPriorityInQueue = 0;
            nextPriorityCheckIn = PriorityCheckInterval;
            dic = new Dictionary<Node<TVal>, Node<TVal>>(maxElements);
            queue = new StablePriorityQueue<Node<TVal>>(maxElements);
        }

        public bool Contains(TVal item)
        {
            return dic.ContainsKey(new Node<TVal>(item));
        }

        public void CopyTo(TVal[] array, int arrayIndex)
        {
            dic.Keys.Select(x => x.Value).ToArray().CopyTo(array, arrayIndex);
        }

        public bool Remove(TVal item)
        {
            var candidate = new Node<TVal>(item);
            if(Contains(item))
            {
                var key = dic[candidate];
                queue.Remove(key);
                dic.Remove(key);
                return true;
            }
            return false;
        }

        public IEnumerable<KeyValuePair<TVal, long>> GetOccurences()
        {
            return dic.Select(x => new KeyValuePair<TVal, long>(x.Key.Value, x.Key.Priority + abosluteOffset))
                .OrderByDescending(x => x.Value);
        }

        public IEnumerator<TVal> GetEnumerator()
        {
            return dic.Keys.Select( x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
