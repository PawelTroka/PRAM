using System;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAdditionDivideAndConquer
{
    internal class FastAdditionDivideAndConquerProcessor : Processor
    {
        private readonly int count;
        private readonly int processorCount;
        private readonly int span;
        private int algorithmPhase;
        private int idx;
        private int resultSum;
        private int sum;

        public FastAdditionDivideAndConquerProcessor(int number, int count)
        {
            Number = number;
            this.count = count;
            sum = 0;
            idx = 0;
            algorithmPhase = 1;
            resultSum = 0;
            processorCount = (int) Math.Ceiling(count/Math.Log(count, 2));
            span = (int) Math.Ceiling((double) count/processorCount);
            DataToRead = new MemoryAddress("data", Number*span);
        }

        public override dynamic Run(dynamic data)
        {
            if (algorithmPhase == 1)
            {
                if (Number*span + idx < count)
                {
                    sum += data;
                }
                idx++;
                //first loop of the algorithm
                if (idx < span)
                {
                    if (Number*span + idx < count)
                    {
                        DataToRead = new MemoryAddress("data", Number*span + idx);
                    }
                    else
                    {
                        DataToRead = new MemoryAddress();
                    }
                    DataToWrite = new MemoryAddress();
                    return null;
                }
                DataToWrite = new MemoryAddress("results", Number);
                if (Number*2 >= processorCount)
                {
                    Stop();
                }
                else
                {
                    DataToRead = new MemoryAddress("results", Number*2);
                }
                algorithmPhase = 2;
                return sum;
            }
            //second loop of the algorithm
            if (2*Number + (int) Math.Pow(2, TickCount - span - 1) < processorCount)
            {
                resultSum += data;
            }
            if ((int) Math.Pow(2, TickCount - span) < processorCount)
            {
                if (Number%Math.Pow(2, TickCount - span) == 0)
                {
                    if (2*Number + (int) Math.Pow(2, TickCount - span) < processorCount)
                    {
                        DataToRead = new MemoryAddress("results", 2*Number + (int) Math.Pow(2, TickCount - span));
                    }
                    else
                    {
                        DataToRead = new MemoryAddress();
                    }
                    DataToWrite = new MemoryAddress("results", 2*Number);
                    return resultSum;
                }
                Stop();
                return resultSum;
            }
            Stop();
            return resultSum;
        }
    }
}