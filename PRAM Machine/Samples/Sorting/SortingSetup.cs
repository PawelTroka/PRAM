using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Memory;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Samples.Sorting {
    /// <summary>
    /// Class used to initialize sample of constant time sorting implementation.
    /// </summary>
    static class SortingSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to sort vector of integers which is
        /// initialized with random numbers, ranging from 0 to limit. Prepares memory
        /// with vectors named 'values' containing numbers to be sorted, 'ranks' which is
        /// used to store rank of every sorted value and 'result' to which final sorted 
        /// sequence will be stored.
        /// 
        /// This implementation uses SortingProcessor from this namespace.
        /// </summary>
        /// <param name="count">How many values are to be sorted.</param>
        /// <param name="limit">Maximum value, defaults to 30</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count, int limit = 30) {
            Random random = new Random();
            List<int> values = new List<int>();
            for (int i = 0; i < count; i++) {
                values.Add(random.Next(30));
            }
            List<Processor> processors = new List<Processor>();
            // we add ranking processors
            for (int i = 0; i < count; i++) { 
                processors.Add(new SortingProcessor(count));
            }
            // we add comparing processors for each unique pair of values
            for (int i = 0; i < count; i++) {
                for (int j = i + 1; j < count; j++) {
                    SortingProcessor processor = new SortingProcessor(count, i, j);
                    processors.Add(processor);
                }
            }
            PRAM<newCRCW> memory = new PRAM<newCRCW>();
            memory.AddNamedMemory("values", values);
            memory.AddNamedMemory("ranks", count, 0);
            memory.AddNamedMemory("result", count, 0);
            PRAMMachine<newCRCW> machine = new PRAMMachine<newCRCW>(processors, memory);
            return machine;
        }
    }
}
