using System.Collections.Generic;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Sorting
{
    internal class SortingProcessor : Processor
    {
        private readonly int Count;
        private readonly Stack<int> DataAddresses;
        private readonly int IndexA;
        private readonly int IndexB;
        private readonly Stack<dynamic> Values;
        private int Value;

        public SortingProcessor(int count, int idxA = 0, int idxB = 0)
        {
            IndexA = idxA;
            IndexB = idxB;
            DataAddresses = new Stack<int>();
            DataAddresses.Push(IndexB);
            DataAddresses.Push(IndexA);
            Values = new Stack<dynamic>();
            Count = count;
        }

        public override dynamic Run(dynamic data)
        {
            // Ranking Processor
            if (Number < Count)
            {
                if (TickCount == 0)
                {
                    DataToRead = new MemoryAddress("values", Number);
                    return null;
                }
                if (TickCount == 1)
                {
                    Value = data;
                    DataToRead = new MemoryAddress("ranks", Number);
                    return null;
                }
                if (TickCount == 3)
                {
                    DataToWrite = new MemoryAddress("result", data);
                    Stop();
                    return Value;
                }
                return null;
            }
            // Comparing processor
            Values.Push(data);
            // we still have data to read
            if (DataAddresses.Count != 0)
            {
                DataToRead = new MemoryAddress("values", DataAddresses.Pop());
                return null;
                // we have read all the necessary data
            }
            DataToRead = new MemoryAddress();
            int a = Values.Pop();
            int b = Values.Pop();
            if (a > b)
            {
                DataToWrite = new MemoryAddress("ranks", IndexA);
            }
            else
            {
                DataToWrite = new MemoryAddress("ranks", IndexB);
            }
            Stop();
            return 1;
        }
    }
}