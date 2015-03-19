using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.ListRanking {
    /// <summary>
    /// Class used to initialize sample of list ranking implementation.
    /// </summary>
    public static class ListRankingSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to calculate ranks of list nodes.
        /// Memory contains random list with two additional vectors used by the algorithm.
        /// 
        /// This implementation uses ListRankingProcessor from this namespace
        /// </summary>
        /// <param name="count">List length</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count) {
            List<Processor> processors = new List<Processor>();
            for (int i = 0; i < count; i++) {
                processors.Add(new ListRankingProcessor(i));
            }
            List<int> next = new List<int>(new int[count]);
            List<int> takenSpaces = new List<int>();
            Random random = new Random();
            int prev = random.Next(count);
            next[prev] = -1;
            takenSpaces.Add(prev);
            for (int i = count - 1; i > 0; i--) {
                int idx = random.Next(count);
                while (takenSpaces.Contains(idx)) {
                    idx = random.Next(count);
                }
                takenSpaces.Add(idx);
                next[idx] = prev;
                prev = idx;
            }
            PRAM<MemoryTypes.EREW> memory = new PRAM<MemoryTypes.EREW>();
            memory.AddNamedMemory("next", next);
            memory.AddNamedMemory("ranks", count, 0);
            memory.AddNamedMemory("pointers", count, 0);
            return new PRAMMachine<MemoryTypes.EREW>(processors, memory);
        }
    }
}
