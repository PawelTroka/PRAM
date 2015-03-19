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

    internal class Find3OnesInMatrixProcessor : Processor
    {
        private readonly int _column;
        private readonly int _row;
        private readonly int _size;

        public Find3OnesInMatrixProcessor(int row, int column, int size)
        {
            _row = row;
            _column = column;
            _size = size;

            DataToRead = new MemoryAddress("Matrix", row,column);

            if (_column >= _size - 2)
                Stop();
        }

        public override dynamic Run(dynamic data)
        {
            DataToRead = new MemoryAddress("Matrix", _row, _column + TickCount+1);

            if (data != 1)
                Stop();
            else if (TickCount == 2)
            {
                Stop();
                DataToWrite = new MemoryAddress("result", 0);
                return true;
            }
            return null;
        }
    }
}