using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAdditionDivideAndConquer {
    class FastAdditionDivideAndConquerProcessor : Processor {
        private int count;
        private int sum;
        private int idx;
        private int span;
        private int processorCount;
        private int algorithmPhase;
        private int resultSum;

        public FastAdditionDivideAndConquerProcessor(int number, int count) : base() {
            this.Number = number;
            this.count = count;
            this.sum = 0;
            idx = 0;
            algorithmPhase = 1;
            resultSum = 0;
            processorCount = (int)Math.Ceiling((double)count / Math.Log(count, 2));
            span = (int)Math.Ceiling((double)count / processorCount);
            this.DataToRead = new MemoryAddress("data", this.Number * span);
        }

        public override dynamic Run(dynamic data) {
            if (algorithmPhase == 1) {
                if (this.Number * span + idx < count) {
                    sum += data;
                }
                idx++;
                //first loop of the algorithm
                if (idx < span) {
                    if (this.Number * span + idx < count) {
                        this.DataToRead = new MemoryAddress("data", this.Number * span + idx);
                    } else {
                        this.DataToRead = new MemoryAddress();
                    }
                    this.DataToWrite = new MemoryAddress();
                    return null;
                } else {
                    this.DataToWrite = new MemoryAddress("results", this.Number);
                    if (this.Number * 2 >= processorCount) {
                        this.Stop();
                    } else {
                        this.DataToRead = new MemoryAddress("results", this.Number * 2);
                    }
                    algorithmPhase = 2;
                    return sum;
                }
            } else {
                //second loop of the algorithm
                if (2 * Number + (int)Math.Pow(2, TickCount - span - 1) < processorCount) {
                    resultSum += data;
                }
                if ((int)Math.Pow(2, TickCount - span) < processorCount) {
                    if (Number % Math.Pow(2, TickCount - span) == 0) {
                        if (2 * Number + (int)Math.Pow(2, TickCount - span) < processorCount) {
                            this.DataToRead = new MemoryAddress("results", 2 * Number + (int)Math.Pow(2, TickCount - span));
                        } else {
                            this.DataToRead = new MemoryAddress();
                        }
                        this.DataToWrite = new MemoryAddress("results", 2 * Number);
                        return resultSum;
                    } else {
                        Stop();
                        return resultSum;
                    }
                } else {
                    Stop();
                    return resultSum;
                }
            }
        }
    }
}
