using System;
using System.Collections.Generic;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAdditionDivideAndConquer
{
    /// <summary>
    ///     Class used to initialize sample of fast addition, using divide and conquer, implementation.
    /// </summary>
    public static class FastAdditionDivideAndConquerSetup
    {
        /// <summary>
        ///     This method prepares PRAM machine ready to perform fast addition,
        ///     using divide and conquer, on a vector of integer values.
        ///     Memory is initialized with vector 'data' that has length count and contains
        ///     random integer values ranging from 0 to limit. Apart from that memory has
        ///     another vector named 'results' that is used to store temporary results.
        ///     Final value is stored in cell 0 of 'results' vector.
        ///     This implementation uses FastAdditionDivideAndConquerProcessor from this namespace.
        /// </summary>
        /// <param name="count">Length of the 'data' vector.</param>
        /// <param name="limit">Maximum value, defaults to 10.</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int count, int limit = 10)
        {
            var processors = new List<Processor>();
            for (int i = 0; i < (int) Math.Ceiling(count/Math.Log(count, 2)); i++)
            {
                processors.Add(new FastAdditionDivideAndConquerProcessor(i, count));
            }
            var memory = new PRAM<MemoryTypes.EREW>();
            var values = new List<int>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                values.Add(random.Next(limit));
            }
            memory.AddNamedMemory("data", values);
            memory.AddNamedMemory("results", ((int) Math.Ceiling(Math.Log(count, 2))), 0);
            IPRAMMachine machine = new PRAMMachine<MemoryTypes.EREW>(processors, memory);
            return machine;
        }
    }
}