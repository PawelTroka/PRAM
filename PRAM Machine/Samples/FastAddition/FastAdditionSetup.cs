using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAddition {
    /// <summary>
    /// Class used to initialize sample of fast addition implementation.
    /// </summary>
    public static class FastAdditionSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to perform fast addition on a vector 
        /// of integer values.
        /// Memory is initialized with vector 'data' that has length count and contains
        /// random integer values ranging from 0 to limit. Final value is stored in 
        /// cell 0 of 'data' vector.
        ///
        /// This implementation uses FastAdditionProcessor from this namespace.
        /// </summary>
        /// <param name="count">Length of the 'data' vector.</param>
        /// <param name="limit">Maximum value, defaults to 10.</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count, int limit = 10) {
            List<Processor> processors = new List<Processor>();
            for (int i = 0; i < count; i++) {
                processors.Add(new FastAdditionProcessor(i, count));
            }
            PRAM<MemoryTypes.EREW> memory = new PRAM<MemoryTypes.EREW>();
            List<int> values = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 2 * count; i++) {
                values.Add(random.Next(limit));
            }
            memory.AddNamedMemory("data", values);
            IPRAMMachine machine = new PRAMMachine<MemoryTypes.EREW>(processors, memory);
            return machine;
        }
    }
}
