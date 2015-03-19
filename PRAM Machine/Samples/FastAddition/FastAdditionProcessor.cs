using System;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAddition
{
    internal class FastAdditionProcessor : Processor
    {
        private readonly int count;
        private int sum;

        public FastAdditionProcessor(int number, int count)
        {
            Number = number;
            this.count = count;
            sum = 0;
            DataToRead = new MemoryAddress("data", Number*2);
        }

        public override dynamic Run(dynamic data)
        {
            if (2*Number + (int) Math.Pow(2, TickCount - 1) < 2*count)
            {
                sum += data;
            }
            // If first processor should work
            if ((int) Math.Pow(2, TickCount) < 2*count)
            {
                // If any other processor should work
                if (Number%Math.Pow(2, TickCount) == 0)
                {
                    if (2*Number + (int) Math.Pow(2, TickCount) < 2*count)
                    {
                        DataToRead = new MemoryAddress("data", 2*Number + (int) Math.Pow(2, TickCount));
                    }
                    else
                    {
                        DataToRead = new MemoryAddress();
                    }
                    DataToWrite = new MemoryAddress("data", 2*Number);
                    return sum;
                }
                Stop();
                return sum;
            }
            Stop();
            return sum;
        }
    }
}