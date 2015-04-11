using System;
using System.Collections.Generic;
using System.Windows.Media;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixMultiplicationUsingMatrixDataType {
    /// <summary>
    /// Class used to initialize sample of matrix multiplication implementation.
    /// </summary>
    public static class MatrixMultiplicationUsingMatrixDataTypeSetup {
        /// <summary>
        /// This method prepares PRAM machine ready to multiply two random square matrixes.
        /// Matrixes are initialized with values ranging from 0 to limit. In memory matrixes
        /// are represented by vectors of length equal to size^2. Memory is initialized with 
        /// three matrixes 'MatrixA' and 'MatrixB' containing initial vaules and 'MatrixC' 
        /// in which the multiplication result will be stored. Apart from that memory has 
        /// size^2 vectors for storing temporary results.
        ///
        /// This implementation uses MatrixMultiplicationUsingMatrixDataTypeProcessor from this namespace.
        /// </summary>
        /// <param name="size">Size of the matrix</param>
        /// <param name="limit">Maximum value, defaults to 5</param>
        /// <returns>Ready PRAM machine.</returns>
        public static IPRAMMachine Setup(int size = 2, int limit = 5) {
            List<Processor> processors = new List<Processor>();
            for (int row = 0; row < size; row++) {
                for (int column = 0; column < size; column++) {
                    for (int number = 0; number < size; number++) {
                        processors.Add(new MatrixMultiplicationUsingMatrixDataTypeProcessor(number, row, column, size));
                    }
                }
            }
            var memory = new PRAM<MemoryTypes.CREW>();
            var random = new Random();

            var MatrixA = new Matrix<int>(size, size);
            var MatrixB = new Matrix<int>(size, size);
            var MatrixC = new Matrix<int>(size, size);

            for (int i = 0; i < size ; i++) 
                for (int j = 0; j < size; j++)
                {
                    MatrixA[i, j]=(random.Next(limit));
                    MatrixB[i, j]=(random.Next(limit));
                    MatrixC[i, j] = 0;
                }
            
            memory.AddNamedMemory("MatrixA", MatrixA);
            memory.AddNamedMemory("MatrixB", MatrixB);
            memory.AddNamedMemory("MatrixC", MatrixC);

            for (int i = 0; i < size * size; i++) {
                memory.AddNamedMemory("temp" + i.ToString(), size, 0);
            }
            //for (int number = 0; number < size; number++)
            //{
            //    memory.AddNamedMemory("temp" + number.ToString(), new Matrix<int>(size,size));
            //}
            PRAMMachine<MemoryTypes.CREW> machine = new PRAMMachine<MemoryTypes.CREW>(processors, memory);
            return machine;
        }
    }
}
