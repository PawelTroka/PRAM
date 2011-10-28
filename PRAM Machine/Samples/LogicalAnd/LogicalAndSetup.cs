using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.LogicalAnd {
    /// <summary>
    /// Class used to initialize sample of logical and implementation.
    /// </summary>
    public static class LogicalAndSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to perform quick logical and 
        /// operation on vector of boolean values.
        /// Memory is initialized with vector 'data' that has length count and contains
        /// random boolean values. Apart from that memory contains one cell vector named
        /// 'result' initialized with true.
        ///
        /// This implementation uses LogicalAndProcessor from this namespace.
        /// </summary>
        /// <param name="conut">Size of the data.</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count) {
            Random random = new Random();
            List<int> values = new List<int>();
            for (int i = 0; i < count; i++) {
                values.Add(random.Next(2));
            }
            List<Processor> processors = new List<Processor>();
            // we add ranking processors
            for (int i = 0; i < count; i++) {
                processors.Add(new LogicalAndProcessor(i));
            }
            PRAM<MemoryTypes.ERCW> memory = new PRAM<MemoryTypes.ERCW>();
            memory.AddNamedMemory("data", values);
            memory.AddNamedMemory("result", 1, 1);
            PRAMMachine<MemoryTypes.ERCW> machine = new PRAMMachine<MemoryTypes.ERCW>(processors, memory);
            return machine;
        }
    }
}
