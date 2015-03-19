using System;
using System.Collections.Generic;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Find3OnesInMatrix
{
    /*
     * Zadanie 2.
Dana jest kwadratowa macierz zero-jedynkowa A o rozmiarze n, gdzie n
jest potęgą dwójki (n=2^k). Podaj możliwie najszybszy algorytm równole-
gły CRCW PRAM sprawdzający, czy w A znajduje się wiersz zawierający
przynajmniej trzy sąsiadujące jedynki. Podaj czas działania, liczbę użytych
procesorów i model rozwiązywania konfliktów zapisu w swoim algorytmie.
     */

    public static class Find3OnesInMatrixSetup
    {
        public static IPRAMMachine Setup(int size = 3, int limit = 2)
        {
            var processors = new List<Processor>();
            for (int row = 0; row < size; row++)
                for (int column = 0; column < size; column++)
                    processors.Add(new Find3OnesInMatrixProcessor(row, column, size));

            var memory = new PRAM<MemoryTypes.CRCW>();
            var random = new Random();

            var matrix = new Matrix<int>(size, size);

            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    matrix[i, j] = (random.Next(limit));

            memory.AddNamedMemory("Matrix", matrix);

            memory.AddNamedMemory("result", false);

            var machine = new PRAMMachine<MemoryTypes.CRCW>(processors, memory);
            return machine;
        }
    }
}