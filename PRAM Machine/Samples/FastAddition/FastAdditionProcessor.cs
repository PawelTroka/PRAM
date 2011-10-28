using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.FastAddition {
    class FastAdditionProcessor : Processor {
        private int count;
        private int sum;

        public FastAdditionProcessor(int number, int count) : base() {
            this.Number = number;
            this.count = count;
            this.sum = 0;
            this.DataToRead = new MemoryAddress("data", this.Number * 2);
        }

        public override dynamic Run(dynamic data) {
            if (2 * Number + (int)Math.Pow(2, TickCount - 1) < 2 * count) {
                sum += data;
            }
            // If first processor should work
            if ((int)Math.Pow(2, TickCount) < 2 * count) {
                // If any other processor should work
                if (Number % Math.Pow(2, TickCount) == 0) {
                    if (2 * Number + (int)Math.Pow(2, TickCount) < 2 * count) {
                        this.DataToRead = new MemoryAddress("data", 2 * Number + (int)Math.Pow(2, TickCount));
                    } else {
                        this.DataToRead = new MemoryAddress();
                    }
                    this.DataToWrite = new MemoryAddress("data", 2 * Number);
                    return sum;
                } else {
                    Stop();
                    return sum;
                }
            } else {
                Stop();
                return sum;
            }
        }
    }
}
