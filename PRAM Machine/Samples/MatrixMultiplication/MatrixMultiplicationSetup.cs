using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixMultiplication {
    /// <summary>
    /// Class used to initialize sample of matrix multiplication implementation.
    /// </summary>
    public static class MatrixMultiplicationSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to multiply two random square matrixes.
        /// Matrixes are initialized with values ranging from 0 to limit. In memory matrixes
        /// are represented by vectors of length equal to size^2. Memory is initialized with 
        /// three matrixes 'MatrixA' and 'MatrixB' containing initial vaules and 'MatrixC' 
        /// in which the multiplication result will be stored. Apart from that memory has 
        /// size^2 vectors for storing temporary results.
        ///
        /// This implementation uses MatrixMultiplicationProcessor from this namespace.
        /// </summary>
        /// <param name="size">Size of the matrix</param>
        /// <param name="limit">Maximum value, defaults to 5</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int size = 2, int limit = 5) {
            List<Processor> processors = new List<Processor>();
            for (int row = 0; row < size; row++) {
                for (int column = 0; column < size; column++) {
                    for (int number = 0; number < size; number++) { 
                        processors.Add(new MatrixMultiplicationProcessor(number, row, column, size));
                    }
                }
            }
            PRAM<MemoryTypes.CREW> memory = new PRAM<MemoryTypes.CREW>();
            Random random = new Random();
            List<int> MatrixA = new List<int>();
            List<int> MatrixB = new List<int>();
            for (int i = 0; i < size * size; i++) {
                MatrixA.Add(random.Next(limit));
                MatrixB.Add(random.Next(limit));
            }
            memory.AddNamedMemory("MatrixA", MatrixA);
            memory.AddNamedMemory("MatrixB", MatrixB);
            memory.AddNamedMemory("MatrixC", size * size, 0);
            for (int i = 0; i < size * size; i++) {
                memory.AddNamedMemory("temp" + i.ToString(), size, 0);
            }
            PRAMMachine<MemoryTypes.CREW> machine = new PRAMMachine<MemoryTypes.CREW>(processors, memory);
            return machine;
        }
    }
}
